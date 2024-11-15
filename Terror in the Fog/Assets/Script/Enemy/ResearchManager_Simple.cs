using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager_Simple : MonoBehaviour
{
    public static ResearchManager_Simple instance;

    public GameObject[] columns;
    Vector3 moveToPos;
    [SerializeField] private Enemy enemy;

    int moveIndex = 0;         //방번호를 대신하는 인덱스(배열의 순서)
    int roomNumber = 4;        //방의 총수
    int floorNumber = 0;
    int currentroomNumber = 4; //최근 방의 개수(변수)
    [SerializeField] private List<int> isDoneIdex = new List<int>();  //랜덤하게 숫자를 넣어주기 위한 변수;

    //ResetIndex()를 위한 전역불값
    bool r_IsEnd = false;
    //문앞으로 이동에서 문열기의 이동을 위한 변수
    public float changeTime = 0f;
    float waitTime = 2f;

    int stepNumber = 0;

    Sequence sequence;  //시작시, 바라보는 방향
    Sequence sequence2; //그 반대방향
    Sequence sequence3; //시작시, 바라보는 방향
    Sequence sequence4; //그 반대방향
    bool isOneTime = false;

    Vector3 dir;
    bool isheight = true;

    //잠수타서 사용할 변수
    Vector3 hidePos;
    Vector3 lookPos;
    float lookTime = 10f;
    float lookTimer = 0f;

    public bool isLookBack = false;
    public enum ENEMYSTATE
    {
        OPENDOOR,
        ENTERROOM,
        LOOKAROUND,
        LOOKBACK,
        STOPACTION,
        CHANGEROOM
    }

    public ENEMYSTATE enemystate;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //싱글톤 패턴사용
        }

        sequence = DOTween.Sequence();
        sequence2 = DOTween.Sequence();
        sequence3 = DOTween.Sequence();
        sequence4 = DOTween.Sequence();
    }
    void Start()
    {
        changeTime = Time.time;

        DOMotion();
    }

    public void RESEARCH()
    {
        switch (enemystate)
        {

            case ENEMYSTATE.OPENDOOR:
                OpenDoor();
                break;

            case ENEMYSTATE.ENTERROOM:
                EnterRoom();
                break;

            case ENEMYSTATE.LOOKAROUND:
                LookAround();
                break;

            case ENEMYSTATE.LOOKBACK:
                LookBack();
                break;

            case ENEMYSTATE.STOPACTION:
                StopAction();
                break;

            case ENEMYSTATE.CHANGEROOM:
                ChangeRoom();
                break;
        }
    }

    private void DOMotion()
    {
        sequence.Prepend(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 1.5f))
       .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 1.5f))
       .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), 1.5f))
       .SetAutoKill(false)
       .Pause()
       .OnComplete(() => stepNumber = 1);

        sequence2.Prepend(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), 1.5f))
        .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.5f))
        .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 1.5f))
        .SetAutoKill(false)
        .Pause()
        .OnComplete(() => stepNumber = 1);


        sequence3.Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -180, 0), 1.5f))
         .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.5f))
         .SetAutoKill(false)
         .Pause()
         .OnComplete(() => stepNumber = 2);

        sequence4.Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.5f))
         .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 1.5f))
         .SetAutoKill(false)
         .Pause()
         .OnComplete(() => stepNumber = 2);
    }

    public void ChangeEnemyState(ENEMYSTATE newState)
    {
        enemystate = newState;
    }

    public void TutorialEvent()
    {
        ChangeEnemyState(ENEMYSTATE.OPENDOOR);
    }


    public void ChangeRoom()
    {

    }
    public void OpenDoor()
    {
        Debug.Log(1);
        //Debug.Log("진짜 층은 " + floorNumber);
        //Debug.Log("바뀐 방의 개수는" + roomNumber);

        moveToPos = columns[moveIndex].transform.position;

        enemy.navMeshAgent.SetDestination(moveToPos);

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && Time.time >= changeTime + waitTime)
        {

            changeTime = Time.time;
            ChangeEnemyState(ENEMYSTATE.ENTERROOM);
            isOneTime = true;
        }
    }
    private void EnterRoom()
    {
        Debug.Log(2);
        moveToPos = columns[moveIndex + 1].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        dir = (moveToPos - enemy.transform.position).normalized;
        if (isOneTime)
        {
            if (dir.z > 0)
            {
                isheight = true;
            }
            else
            {
                isheight = false;
            }
            isOneTime = false;
        }

        int randomNumber = Random.Range(0, 3);

        if (randomNumber == 0)
        {
            isLookBack = true;
        }
        else
        {
            isLookBack = false;
        }
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && Time.time >= waitTime + changeTime)
        {
            if (randomNumber == 0)
            {
                ChangeEnemyState(ENEMYSTATE.LOOKBACK);
                isOneTime = true;
            }
            else
            {
                ChangeEnemyState(ENEMYSTATE.LOOKAROUND);
                isOneTime = true;
            }
            enemy.navMeshAgent.updateRotation = false;
        }
    }
    private void LookAround()
    {
        Debug.Log(3);
        if (isOneTime)
        {
            if (isheight)
            {
                sequence.Restart();
                //stepNumber = 1;
            }
            else
            {
                sequence2.Restart();
                //stepNumber = 1;
            }
            isOneTime = false;
        }
        if (stepNumber == 1)
        {
            enemy.navMeshAgent.updateRotation = true;
            ChangeEnemyState(ENEMYSTATE.CHANGEROOM);
        }
    }
    private void LookBack()
    {
        Debug.Log(4);
        if (isOneTime)
        {
            if (isheight)
            {
                sequence3.Restart();
                //stepNumber = 2;
            }
            else
            {
                sequence4.Restart();
                //stepNumber = 2;
            }
            isOneTime = false;
        }

        if (stepNumber == 2)
        {
            ChangeEnemyState(ENEMYSTATE.LOOKAROUND);
            isOneTime = true;
            isLookBack = false;
        }
    }

    private void StopAction()
    {
        Debug.Log(5);
        if (stepNumber == 3)
        {
            int randomNum = Random.Range(0, roomNumber - 1);
            hidePos = columns[randomNum].transform.position;
            lookPos = columns[randomNum].transform.position;
            stepNumber = 4;
        }

        if (stepNumber == 4)
        {
            enemy.navMeshAgent.SetDestination(hidePos);
        }
        if (enemy.navMeshAgent.remainingDistance <= 0.1f && Time.time >= waitTime + changeTime)
        {
            enemy.gameObject.transform.DOLookAt(lookPos, 1f);

            lookTimer += Time.deltaTime;
            if (lookTime <= lookTimer)
            {
                ChangeFloor(false);
                lookTimer = 0f;
            }
        }
    }

    public void ChangeFloor(bool isFisrt)
    {
        if (isFisrt)
        {
            stepNumber = 7;
            roomNumber = columns.Length / 2;
            r_IsEnd = false;
            ResetIndex();
        }
        else
        {
            if (floorNumber == 0 && stepNumber != 7)
            {
                stepNumber = 7;

                floorNumber = 1;
                roomNumber = columns.Length / 2;
                //Debug.Log("방의 개수는" + roomNumber);
                //Debug.Log("층은 " + floorNumber);
                r_IsEnd = false;
                ResetIndex();
            }
            else if (floorNumber == 1 && stepNumber != 7)
            {
                stepNumber = 7;
                floorNumber = 0;
                roomNumber = columns.Length / 2;

                //Debug.Log("방의 개수는" + roomNumber);
                //Debug.Log("층은 " + floorNumber);
                r_IsEnd = false;
                ResetIndex();
            }
        }
    }

    private void ResetIndex()
    {
        Debug.Log("초기화");
        isDoneIdex.Clear();
        if (!r_IsEnd)
        {
            currentroomNumber = roomNumber;
            for (int i = 0; i < roomNumber; i++)
            {
                isDoneIdex.Add(i);  //0~3까지만 관리하는 의도
            }
            r_IsEnd = true;
            Debug.Log(isDoneIdex.Count);
        }
        if (isDoneIdex.Count == roomNumber)
        {
            ChangeEnemyState(ENEMYSTATE.OPENDOOR);
            changeTime = Time.time;
        }
    }

    public void StopSquance()
    {
        sequence.Pause();
        sequence2.Pause();
        sequence3.Pause();
        sequence4.Pause();
    }
}
