using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.Threading.Tasks.Sources;
using static GameManager;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;
    public StateMachine stateMachine { get { return _stateMachine; } }
    public Animator animator { get { return _animator; } }
    public PlayerController playerController;
    public bool IsCanChase = false;
    //배경음악, 사운드 스크립티 선언

    private StateMachine _stateMachine;
    private int moveDirection; //이동방향
    private Rigidbody _rigidBody;
    private Animator _animator;

    //[SerializeField] bool DebugMode = false;
    [Range(0f, 360f)][SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    [SerializeField] LayerMask TargetMask;
    Vector3 myPos;
    Vector3 lookDir;

    public List<Collider> hitTargetList = new List<Collider>();
    RaycastHit hit;

    float move;
    float rotate;
    Rigidbody rb;
    public NavMeshAgent navMeshAgent; // 경로 계산 AI 에이전트

    float floorHigh = 3f;
    float bottomHigh = 0.5f;
    public float seizeRadius = 0.5f;
    Collider[] target = new Collider[0];
    private bool isFind = false; //타이머기능용
    public bool isFindPlayer = false;  //chase상테에서만 적용하기위한 불값

    public float timer = 0f; //플레이 놓침타이머
    public bool isCheckAround = false;
    Sequence sequence;
    void Start()
    {
        _stateMachine = new StateMachine(this, Research.GetInstance());
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        sequence = DOTween.Sequence().SetAutoKill(false);
        myPos = transform.position + Vector3.up * 0.5f;
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
            if (targetAngle <= ViewAngle * 0.5f)
            {
                Physics.Raycast(myPos, targetDir, out hit, ViewRadius);
                if (!hit.collider.gameObject.CompareTag("Object"))
                {
                    Vector3 maxHigh = transform.position * floorHigh;
                    Vector3 minHigh = transform.position * bottomHigh;
                    if (maxHigh.y > targetPos.y && minHigh.y < targetPos.y)
                    {
                        hitTargetList.Add(hit.collider);

                        isFind = true; //플레이어감지 불값 켜기
                        Debug.Log("보인다");
                        Debug.DrawLine(myPos, targetPos, Color.red);
                    }
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        myPos = gameObject.transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, ViewRadius);
        Gizmos.DrawWireSphere(transform.position, seizeRadius);
    }

    public void StopFinding()
    {
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
        navMeshAgent.SetDestination(hitTargetList[0].transform.position);

        target = Physics.OverlapSphere(transform.position, seizeRadius, TargetMask);
    }

    public void CheckDeath()
    {
        if (target.Length >= 1)
        {
            //playerController.transform.DOLocalMove(transform.position + Vector3. * 1.5f, 1f);
            playerController.gameObject.SetActive(false);
        }
    }

    public void CheckAround()
    {
        isCheckAround = true;
        Debug.Log("상태전환확인");
        navMeshAgent.isStopped = true;
        hitTargetList.Clear();

        Vector3 act_1 = transform.rotation.eulerAngles;
        act_1.y += 90f;
        Vector3 act_2 = transform.rotation.eulerAngles;
        Vector3 act_3 = transform.rotation.eulerAngles;
        act_2.y -= 90f;

        sequence.Append(transform.DOLocalRotate(act_2, 3f))
                .Append(transform.DOLocalRotate(act_3, 3f))
                .Prepend(transform.DOLocalRotate(act_1, 3f)).OnComplete(()=> isCheckAround = false);
    }

    public void ResearchArea()
    {
        GameManager.instance.RESEARCH();
    }

    public void StartState()
    {
        GameManager.instance.ChangeEnemyState(ENEMYSTATE.ENTERROOM);
    }

}
