using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;



[System.Serializable]
public class FloorDate
{
    public GameObject[] columns;
}

public class ResearchManager : MonoBehaviour
{
    public static ResearchManager instance;

    public FloorDate[] floorIndex;
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

    public int stepNumber = 0;

    bool isOneTime = false;

    Vector3 dir;
    bool isheight = true;

    //잠수타서 사용할 변수
    Vector3 hidePos;
    Vector3 lookPos;
    float lookTime = 10f;
    float lookTimer = 0f;
    
    //문 Control용
    public bool isLookBack = false;
    public bool isdoorLocked = false;

    //잠긴문 못들어가는용
    public float timer = 0f;
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
    }
    void Start()
    {
        changeTime = Time.time;
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

    public void ChangeEnemyState(ENEMYSTATE newState)
    {
        enemystate = newState;
    }

    private void ChangeRoom()
    {
        Debug.Log("방 교체");
        stepNumber = 0;
        //Debug.Log("설정값이 " + currentroomNumber);
        isDoneIdex.Remove(moveIndex);
        --currentroomNumber;
        int randomNumber = Random.Range(0, currentroomNumber);

        if (currentroomNumber > 0)
        {
            moveIndex = isDoneIdex[randomNumber];
            ChangeEnemyState(ENEMYSTATE.OPENDOOR);
            changeTime = Time.time;
        }
        else
        {
            int randomNumber2 = Random.Range(0, 3);
            if (randomNumber2 == 0)
            {
                ChangeEnemyState(ENEMYSTATE.STOPACTION);
                changeTime = Time.time;
                stepNumber = 3;
            }
            else
            {
                ChangeFloor(false);
            }
        }
    }

    private void OpenDoor()
    {
        //Debug.Log(1);
        //Debug.Log("진짜 층은 " + floorNumber);
        //Debug.Log("바뀐 방의 개수는" + roomNumber);

        moveToPos = floorIndex[floorNumber].columns[moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && Time.time >= changeTime + waitTime)
        {
            changeTime = Time.time;
            ChangeEnemyState(ENEMYSTATE.ENTERROOM);
            isOneTime = true;
            timer = 0f; //타이머초기화
        }
    }
    private void EnterRoom()
    {
        timer += Time.deltaTime; //잠긴문확인용             --------나중에 잠긴문인 인덱스만 그렇게 변경-------------
        if (timer < 0.6f) return;

        //Debug.Log(2);
        moveToPos = floorIndex[floorNumber].columns[moveIndex + roomNumber].transform.position;
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
        //Debug.Log(3);
        if (isOneTime)
        {
            if (isheight)
            {
                EnemyAnimation.instance.sequence.Restart();
            }
            else
            {
                EnemyAnimation.instance.sequence2.Restart();
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
        //Debug.Log(4);
        if (isOneTime)
        {
            if (isheight)
            {
                EnemyAnimation.instance.sequence3.Restart();
                //stepNumber = 2;
            }
            else
            {
                EnemyAnimation.instance.sequence4.Restart();
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
        //Debug.Log(5);
        if (stepNumber == 3)
        {
            int randomNum = Random.Range(0, roomNumber - 1);
            hidePos = floorIndex[floorNumber + 2].columns[randomNum].transform.position;
            lookPos = floorIndex[floorNumber].columns[randomNum].transform.position;
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
            roomNumber = floorIndex[floorNumber].columns.Length / 2;
            r_IsEnd = false;
            ResetIndex();
        }
        else
        {
            if (floorNumber == 0 && stepNumber != 7)
            {
                stepNumber = 7;

                floorNumber = 1;
                roomNumber = floorIndex[floorNumber].columns.Length / 2;
                //Debug.Log("방의 개수는" + roomNumber);
                //Debug.Log("층은 " + floorNumber);
                r_IsEnd = false;
                ResetIndex();
            }
            else if (floorNumber == 1 && stepNumber != 7)
            {
                stepNumber = 7;
                floorNumber = 0;
                roomNumber = floorIndex[floorNumber].columns.Length / 2;

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
}
