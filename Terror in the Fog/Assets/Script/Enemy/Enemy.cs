using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public StateMachine stateMachine { get { return _stateMachine; } }
    public Animator animator { get { return _animator; } }
    public PlayerController playerController;

    private StateMachine _stateMachine;
    private Animator _animator;


    [Range(0f, 360f)][SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    [SerializeField] LayerMask TargetMask;
    int ObstacleMask = (1 << 6) | (1 << 7);
    Vector3 myPos;
    Vector3 lookDir;

    public List<Collider> hitTargetList = new List<Collider>();

    public NavMeshAgent navMeshAgent; // 경로 계산 AI 에이전트

    float floorHigh = 3f;
    float bottomHigh = 0.5f;
    public float seizeRadius = 0.5f;
    Collider[] target = new Collider[0];
    private bool isFind = false; //타이머기능용
    public bool isFindPlayer = false;  //chase상테에서만 적용하기위한 불값

    public float timer = 0f; //플레이 놓침타이머
    public bool isCheckAround = false;

    public bool pauseResearch = false;

    private float Timer = 0f;

    void Start()
    {
        _stateMachine = new StateMachine(this, Research.GetInstance());
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void FixedUpdate()
    {
        if (_stateMachine != null)
        {
            _stateMachine.FixedUpdateState(this);
        }
    }

    void Update()
    {
        if (_stateMachine != null)
        {
            _stateMachine.UpdateState(this);
        }

    }

    public void CheckObject()
    {
        myPos = transform.position + Vector3.up * 1.4f;

        float lookingAngle = transform.eulerAngles.y;
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + ViewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - ViewAngle * 0.5f);
        lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);

        Collider[] Targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        isFind = false; //플레이어감지 불값 끄기

        if (Targets.Length == 0) return;
        foreach (Collider PlayerColli in Targets)
        {
            Vector3 targetPos = PlayerColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            float distance = Vector3.Distance(targetPos, myPos);

            Debug.DrawRay(myPos, targetDir * distance, Color.red);
            if (targetAngle <= ViewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, distance, ObstacleMask))
            {
                Vector3 maxHigh = transform.position * floorHigh;
                Vector3 minHigh = transform.position * bottomHigh;
                if (maxHigh.y > targetPos.y && minHigh.y < targetPos.y)
                {
                    if (hitTargetList.Count < 1)
                    {
                        hitTargetList.Add(PlayerColli);
                    }

                    isFind = true; //플레이어감지 불값 켜기
                    Debug.Log("보인다");
                    Debug.DrawLine(myPos, targetPos, Color.red);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        myPos = transform.position + Vector3.up * 1.4f;
        Gizmos.DrawWireSphere(myPos, ViewRadius);
        Gizmos.DrawWireSphere(transform.position, seizeRadius);
    }

    public void StopFinding()
    {
        Debug.Log("확인되는기?" + isFind);
        if (isFindPlayer)
        {
            if (isFind)
            {
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    public void ChasePlayer()
    {
        Debug.Log("쫓는다");
        Debug.Log("쫓을 사람 : " + hitTargetList);

        Timer += Time.deltaTime;
        if (Timer > 5f* Time.deltaTime)
        {
            navMeshAgent.SetDestination(hitTargetList[0].transform.position);
            Timer = 0f;
        }

        target = Physics.OverlapSphere(transform.position, seizeRadius, TargetMask);
    }

    public void CheckDeath()
    {
        if (target.Length >= 1)
        {
            playerController.Death();
        }
    }

    public void CheckAround()  //초기화
    {
        isCheckAround = true;
        Debug.Log("상태전환확인");
        hitTargetList.Clear();

        //주변확인 애니메이션 및 회전
        EnemyAnimation.instance.checkAroundSeq.Restart();

    }

    public void ResearchArea()
    {
        if (ResearchManager.instance != null) ResearchManager.instance.RESEARCH();
        //ResearchManager_Simple도 추가해야함
    }

    public void RestartSearch()
    {
        if (ResearchManager.instance != null)
        {
            if (ResearchManager.instance.enemystate == ResearchManager.ENEMYSTATE.OPENDOOR || ResearchManager.instance.enemystate == ResearchManager.ENEMYSTATE.ENTERROOM)
            {
                ResearchManager.instance.changeTime = Time.time;
                ResearchManager.instance.ChangeEnemyState(ResearchManager.instance.enemystate);
                Debug.Log("돌아간다");
            }
            else
            {
                ResearchManager.instance.ChangeEnemyState(ResearchManager.ENEMYSTATE.CHANGEROOM);
            }
        }
    }

    public void DetectToSound()
    {
        SoundDetector.instance.CheckLevel();
        SoundDetector.instance.OnDetect();
    }

    public void ResetResearch()
    {
        if (ResearchManager.instance != null) ResearchManager.instance.ChangeFloor(true);
    }

    public void ResetSound()
    {
        SoundDetector.instance.ResetPos();
    }

    public void StopTween()
    {
        EnemyAnimation.instance.StopSquance();
    }
    public void StopMove()  //ResearchManager_Simple용 코드
    {
        if (ResearchManager_Simple.instance != null)
        {
            if (GameManager.Days == 0) ResearchManager_Simple.instance.StopCoroutine("Tuto");
            if (GameManager.Days == 1) ResearchManager_Simple.instance.StopCoroutine("DayOne");
        }
    }
    public void StartMove()
    {
        if (ResearchManager_Simple.instance != null)
        {
            if(GameManager.Days == 0) ResearchManager_Simple.instance.StartCoroutine("GoBack");
            if (GameManager.Days == 1) ResearchManager_Simple.instance.StartCoroutine("DayOne");
        }
    }
}
