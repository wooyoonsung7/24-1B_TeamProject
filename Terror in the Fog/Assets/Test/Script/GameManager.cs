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

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public FloorDate[] floorIndex;
    Vector3 moveToPos;
    [SerializeField] private Enemy enemy;

    int moveIndex = 0;         //방번호를 대신하는 인덱스(배열의 순서)
    int roomNumber = 4;        //방의 총수
    int currentroomNumber = 3; //최근 방의 개수(변수)
    [SerializeField] private List<int> isDoneIdex = new List<int>();  //랜덤하게 숫자를 넣어주기 위한 변수;
    
    //ResetIndex()를 위한 전역불값
    bool r_IsEnd = false;
    //문앞으로 이동에서 문열기의 이동을 위한 변수
    float changeTime = 0f;
    float waitTime = 2f;

    int stepNumber = 0;

    Sequence sequence;  //시작시, 바라보는 방향
    Sequence sequence2; //그 반대방향
    Sequence sequence3; //시작시, 바라보는 방향
    Sequence sequence4; //그 반대방향
    bool isOneTime = false;

    Vector3 dir;
    bool isheight = true;

    public enum ENEMYSTATE
    {
        CHANGEROOM,
        OPENDOOR,
        ENTERROOM,
        LOOKAROUND,
        LOOKBACK,
        STOPACTION
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
        ResetIndex();
        ChangeEnemyState(ENEMYSTATE.OPENDOOR);
        changeTime = Time.time;

        sequence.Prepend(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 1.5f))
        .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 1.5f))
        .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), 1.5f))
        .SetAutoKill(false)
        .Pause()
        .OnComplete(()=> stepNumber = 1);

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

    public void RESEARCH()
    {
        switch (enemystate)
        {
            case ENEMYSTATE.CHANGEROOM:
                ChangeRoom();
                break;

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
        }
    }

    public void ChangeEnemyState(ENEMYSTATE newState)
    {
        enemystate = newState;
    }

    public void ChangeRoom()
    {
        Debug.Log("방 교체");
        stepNumber = 0;

        isDoneIdex.Remove(moveIndex);
        --currentroomNumber;
        int randomNumber = Random.Range(0, currentroomNumber);
        moveIndex = isDoneIdex[randomNumber];

        if (isDoneIdex.Count == currentroomNumber)
        {
            if (currentroomNumber > 0)
            {
                ChangeEnemyState(ENEMYSTATE.OPENDOOR);
                changeTime = Time.time;
            }
            else
            {
                ResetIndex();
                r_IsEnd = false;
            }
        }
    }

    public void OpenDoor()
    {
        Debug.Log(1);

        moveToPos = floorIndex[0].columns[moveIndex].transform.position;
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
        Debug.Log(isheight);
        moveToPos = floorIndex[0].columns[moveIndex + 4].transform.position;
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

        int randomNumber = Random.Range(0, 4);
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
            }
            else
            {
                Debug.Log("된다");
                sequence2.Restart();
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
            }
            else
            {
                sequence4.Restart();
            }
            isOneTime = false;
        }

        if (stepNumber == 2)
        {
            ChangeEnemyState(ENEMYSTATE.LOOKAROUND);
            isOneTime = true;
        }
    }

    private void StopAction()
    {

    }

    private void ResetIndex()
    {
        if (!r_IsEnd)
        {
            currentroomNumber = roomNumber;
            for (int i = 0; i < roomNumber; i++)
            {
                isDoneIdex.Add(i);  //0~3까지만 관리하는 의도
            }
            r_IsEnd = true;
        }
        if (isDoneIdex.Count == roomNumber)
        {
            ChangeEnemyState(ENEMYSTATE.ENTERROOM);
        }
    }
}
