using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



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

    private float timer = 0f;
    [SerializeField] private float pauseTime = 3f;

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

    void Start()
    {
        instance = this; //싱글톤 패턴사용
        ResetIdex();
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
        Debug.Log("찾은 것 취소");

        isDoneIdex.Remove(moveIndex);
        --currentroomNumber;
        int randomNumber = Random.Range(0, currentroomNumber);
        moveIndex = isDoneIdex[randomNumber];

        if (isDoneIdex.Count == currentroomNumber)
        {
            if (currentroomNumber > 0)
            {
                ChangeEnemyState(ENEMYSTATE.ENTERROOM);
            }
            else
            {
                ResetIdex();
            }
        }
    }

    public void OpenDoor()
    {
        moveToPos = floorIndex[0].columns[moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);
        ChangeEnemyState(ENEMYSTATE.ENTERROOM);
    }

    private void EnterRoom()
    {   
        moveToPos = floorIndex[0].columns[moveIndex + 4].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        int randomNumber = Random.Range(0, 4);
        if (randomNumber == 0)
        {
            ChangeEnemyState(ENEMYSTATE.LOOKBACK);
        }
        else
        {
            ChangeEnemyState(ENEMYSTATE.LOOKAROUND);
        }
    }

    private void LookAround()
    {
        bool isEnd = false;
        Vector3 act_1 = transform.rotation.eulerAngles;
        act_1.y += 90f;
        Vector3 act_2 = transform.rotation.eulerAngles;
        act_1.y += 180f;
        Vector3 act_3 = transform.rotation.eulerAngles;
        act_1.y -= 90f;
        transform.DOLocalRotate(act_2, 2f).OnComplete(() => transform.DOLocalRotate(act_1, 2f).OnComplete(() => transform.DOLocalRotate(act_3, 2f).OnComplete(() => isEnd = true)));
        if (isEnd)
        {
            ChangeEnemyState(ENEMYSTATE.CHANGEROOM);
        }
    }
    private void LookBack()
    {
        bool isEnd = false;
        bool startTimer = false;
        bool isOneTime = true;
        Vector3 act_1 = transform.rotation.eulerAngles;
        act_1.y += 180f;
        Vector3 act_2 = transform.rotation.eulerAngles;
        act_1.y -= 180f;
        if (isOneTime)
        {
            transform.DOLocalRotate(act_1, 2f).OnComplete(() => startTimer = true);
            isOneTime = false;
        }
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= pauseTime)
            {
                transform.DOLocalRotate(act_2, 2f).OnComplete(() => isEnd = true);
                timer = 0f;
                startTimer = false;
            }
        }
        if (isEnd)
        {
            ChangeEnemyState(ENEMYSTATE.LOOKAROUND);
        }
    }

    private void StopAction()
    {

    }

    private void ResetIndex()
    {
        bool isEnd = false;
        if (!isEnd)
        {
            currentroomNumber = roomNumber;
            for (int i = 0; i < roomNumber; i++)
            {
                isDoneIdex.Add(i);  //0~3까지만 관리하는 의도
            }
            isEnd = true;
        }
        if (isDoneIdex.Count == roomNumber)
        {
            ChangeEnemyState(ENEMYSTATE.ENTERROOM);
        }
    }
}
