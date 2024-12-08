using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
    public float currentTime = 0f;

    private Vector3 currentPos;
    private bool isOneTime = true;
    private bool isOneTime2 = true;
    private bool isOneTime3 = true;
    public bool isOneTime4 = true;
    private bool isOneTime5 = true;
    private float timer2 = 0f;

    public bool stopResearch = false; //5일차 이벤트용
    void Start()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = new StateMachine(this, Research.GetInstance());
        Research.GetInstance().isOneTimeInGame = true;
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

        CheckMove();  //사운드 및 애니메이션용
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

        //Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        //Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        //Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);

        Collider[] Targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        isFind = false; //플레이어감지 불값 끄기

        if (Targets.Length == 0) return;
        foreach (Collider PlayerColli in Targets)
        {
            Vector3 targetPos = PlayerColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            float distance = Vector3.Distance(targetPos, myPos);

            //Debug.DrawRay(myPos, targetDir * distance, Color.red);
            if (!Physics.Raycast(myPos, targetDir, distance, ObstacleMask))
            {
                if (targetAngle <= ViewAngle * 0.5f)
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
        //Debug.Log("확인되는기?" + isFind);
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
        //Debug.Log("쫓는다");
        //Debug.Log("쫓을 사람 : " + hitTargetList);

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
        if (target.Length >= 1 && isOneTime4)
        {
            isOneTime4 = false;

            if (playerController.isHide)
            {

                StartCoroutine(CheckDead());
            }
            else
            {
                SoundManager.instance.PauseAllSound();
                _animator.SetTrigger("IsKill");
                navMeshAgent.isStopped = true;
                FindObjectOfType<KillAnimation>().StartCoroutine("Animation");
            }

        }
    }

    private IEnumerator CheckDead()
    {
        SoundManager.instance.PauseAllSound();
        _animator.SetTrigger("IsKill");
        SoundManager.instance.PlaySound("PlayerDead");
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        EventManager.instance.PlayerDead();
    }

    public void CheckAround()  //초기화  같은 층에서만 사운드재생
    {
        isCheckAround = true;
        //Debug.Log("상태전환확인");

        currentTime += Time.deltaTime;
        if (currentTime <= 0.2f)
        {
            hitTargetList.Clear();
        }
        if (currentTime <= 1.3)
        {
            transform.Rotate(0, 1, 0);
        }
        else if (currentTime <= 3.4)
        {
            transform.Rotate(0, -1, 0);
        }
        else
        {
            isCheckAround = false;
        }
    }

    public void CheckMove()
    {
        if (isOneTime3)
        {
            currentPos = gameObject.transform.position;
            isOneTime3 = false;
        }
        if (!isOneTime3)
        {
            timer2 += Time.deltaTime;
            if (timer2 > 0.1f)
            {
                if (currentPos == transform.position && isOneTime2)
                {
                    //Debug.Log("안들린다");
                    SoundManager.instance.PauseSound("EnemyMove");
                    _animator.SetBool("IsMove", false);
                    isOneTime = true;
                    isOneTime2 = false;
                }
                else if (currentPos != transform.position && isOneTime)
                {
                    if (CheckFloor()) SoundManager.instance.PlaySound("EnemyMove");
                    else SoundManager.instance.PauseSound("EnemyMove");
                    _animator.SetBool("IsMove", true);
                    isOneTime2 = true;
                    isOneTime = false;
                }
                isOneTime3 = true;
                timer2 = 0f;
            }
        }
        float distance = Vector3.Distance(playerController.transform.position, transform.position);
        if (distance <= SoundDetector.instance.r_DetectRadius && isOneTime5)
        {
            SoundManager.instance.PlaySound("HeartBit");
            isOneTime5 = false;
        }
        else if(distance > 10.5f && !isOneTime5)
        {
            SoundManager.instance.PauseSound("HeartBit");
            isOneTime5 = true;
        }

    }
    private bool CheckFloor()
    {
        float value = 5f;
        if (playerController.transform.position.y >= transform.position.y + value || playerController.transform.position.y <= transform.position.y - value)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ResearchArea()
    {
        //Debug.Log("탐색된다");
        if (ResearchManager.instance != null && !stopResearch) ResearchManager.instance.RESEARCH();
        //ResearchManager_Simple도 추가해야함
    }

    public void RestartSearch()
    {
        //Debug.Log("탐색초기화된다");
        if (ResearchManager.instance != null)
        {
            if (ResearchManager.instance.enemystate == ResearchManager.ENEMYSTATE.OPENDOOR || ResearchManager.instance.enemystate == ResearchManager.ENEMYSTATE.ENTERROOM)
            {
                ResearchManager.instance.changeTime = Time.time;
                ResearchManager.instance.ChangeEnemyState(ResearchManager.instance.enemystate);
                //Debug.Log("돌아간다");
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
            if (GameManager.Days == 1) ResearchManager_Simple.instance.StopSafeCoroutine();
            if (GameManager.Days == 2) ResearchManager_Simple.instance.StopSafeCoroutine();
        }
    }
    public void StartMove()
    {
        if (ResearchManager_Simple.instance != null)
        {
            if(GameManager.Days == 0) ResearchManager_Simple.instance.StartCoroutine("GoBack");
            if (GameManager.Days == 1) ResearchManager_Simple.instance.StartSafeCoroutine();
            if (GameManager.Days == 2) ResearchManager_Simple.instance.StartSafeCoroutine();
        }
    }
}
