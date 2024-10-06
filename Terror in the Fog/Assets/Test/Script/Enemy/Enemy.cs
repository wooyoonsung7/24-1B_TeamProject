using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

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

    Sequence sequence;
    void Start()
    {
        _stateMachine = new StateMachine(this, Research.GetInstance());
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        sequence = DOTween.Sequence();
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

                        Debug.DrawLine(myPos, targetPos, Color.red);
                    }
                }
            }
        }
        Debug.Log(hitTargetList);
    }

    public void OnDrawGizmos()
    {
        myPos = gameObject.transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, ViewRadius);
        Gizmos.DrawWireSphere(transform.position, seizeRadius);
    }

    public void ChasePlayer()
    {
        navMeshAgent.isStopped = false;
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
        navMeshAgent.isStopped = true;
        hitTargetList.Clear();

        Vector3 act_1 = transform.rotation.eulerAngles;
        act_1.y += 90f;
        Vector3 act_2 = transform.rotation.eulerAngles;
        act_2.y -= 90f;

        sequence.Append(transform.DOLocalRotate(act_2, 2f))
                .Append(transform.DOLocalRotate(act_2, 2f))
                .Prepend(transform.DOLocalRotate(act_1, 2f));
    }

    public void ResearchArea()
    {
        //Debug.Log("탐색재시작");

    }

}
