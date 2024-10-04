using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
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
    [SerializeField] LayerMask ObstacleMask;
    Vector3 myPos;
    Vector3 lookDir;

    List<Collider> hitTargetList = new List<Collider>();

    float move;
    float rotate;
    Rigidbody rb;
    private NavMeshAgent navMeshAgent; // 경로 계산 AI 에이전트
    bool isFindPlayer = true;

    float floorHigh = 3f;
    float bottomHigh = 0.5f;
    float findTime = 3f;               //플레이어가 숨었을 경우, 어그로가 풀릴 때까지의 시간
    float timer = 0f;
    float seizeRadius = 0.5f;
    bool isChacking = false;

    Sequence sequence;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        myPos = transform.position + Vector3.up * 0.5f;
        rb = GetComponent<Rigidbody>();

        sequence = DOTween.Sequence();
    }

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void FixedUpdate()
    {
        CheckObject();
        ChasePlayer();
    }

    void Update()
    {
        CheckDeath();
        //EnemyMove();
    }

    private void CheckObject()
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
            if (targetAngle <= ViewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
            {
                Vector3 maxHigh = transform.position * floorHigh;
                Vector3 minHigh = transform.position * bottomHigh;
                if (maxHigh.y > targetPos.y && minHigh.y < targetPos.y)
                {
                    if (isFindPlayer)
                    {
                        hitTargetList.Add(PlayerColli);
                        isFindPlayer = false;
                    }
                    Debug.DrawLine(myPos, targetPos, Color.red);
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

    private void ChasePlayer()
    {
        if (!isFindPlayer)                                                   //플레이어를 쫓아간다.
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(hitTargetList[0].transform.position);
        }
    }

    private void CheckDeath()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, seizeRadius, TargetMask);
        if (playerController.isHide)                                         //플레이어가 숨기성공했을 때
        {
            timer += Time.deltaTime;
            if (findTime <= timer)
            {
                navMeshAgent.isStopped = true;
                hitTargetList.Clear();
                isFindPlayer = true;
                timer = 0f;              //타이머초기화
                seizeRadius = 0.5f;      //일시적으로 범위를 늘린다.
                isChacking = true;       //해당 동작을 한번만 실행하기위한 불값
                if (isChacking)
                {
                    Vector3 act_1 = transform.rotation.eulerAngles;
                    act_1.y += 90f;
                    Vector3 act_2 = transform.rotation.eulerAngles;
                    act_2.y -= 90f;


                    sequence.Append(transform.DORotate(act_2, 2f))
                            .Append(transform.DORotate(act_2, 2f))
                            .Prepend(transform.DORotate(act_1, 2f));
                    isChacking = false;

                    //보수해야됨 //몬스터 행동전환 별도로 판단하는 시스템 필요(AI)
                }
            }
            else
            {
                seizeRadius = 1f;
                if (target.Length >= 1)
                {
                    //playerController.transform.DOLocalMove(transform.position + Vector3. * 1.5f, 1f);
                    playerController.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (target.Length >= 1)
            {
                //playerController.transform.position = transform.position + Vector3.forward * 1.5f;
                playerController.gameObject.SetActive(false);
            }
        }
    }

    private void Research()
    {

    }
}
