using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static ResearchManager;

public class ResearchManager_Simple : MonoBehaviour
{
    public static ResearchManager_Simple instance;

    public GameObject[] columns;
    Vector3 moveToPos;
    [SerializeField] private Enemy enemy;

    public int moveIndex = 0;             //방번호를 대신하는 인덱스(배열의 순서)
                 
    
    private int roomNumber = 4;        //방의 총수

    //문앞으로 이동에서 문열기의 이동을 위한 변수
    public float changeTime = 0f;
    private float waitTime = 2f;
    private bool setTime = false;

    public bool isstepEnd = false;
    private bool isOneTime = false;

    private Vector3 dir;
    private bool isheight = true;

    //잠수타서 사용할 변수
    private Vector3 hidePos;
    private Vector3 lookPos;
    private float lookTime = 10f;
    private float lookTimer = 0f;
    private bool setTime_2 = false;

    //어떤 용으로 사용할지를 선택
    public enum EnemyMode
    {
        Tutorial,
        DayOne,
        DayTwo,
        AtStreet_1,
        AtStreet_2,
        AtStreet_3
    }

    public EnemyMode enemyMode;

    //문Control용
    public bool isLookBack = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //싱글톤 패턴사용
        }
    }
    private void Start()
    {
        SetMode();
    }
    public void SetMode()
    {
        switch (enemyMode)
        {
            case EnemyMode.Tutorial:
                StartCoroutine("Tuto");
                break;
            case EnemyMode.DayOne:
                DayOneEvent();
                break;
            case EnemyMode.DayTwo:
                DayTwoEnvent();
                break;
            case EnemyMode.AtStreet_1:
                AtStreetEnvent_1();
                break;
            case EnemyMode.AtStreet_2:
                AtStreetEnvent_2();
                break;
            case EnemyMode.AtStreet_3:
                AtStreetEnvent_3();
                break;
        }
    }

    private void TutorialEvent()  //튜토리얼용
    {
        //OpenDoor();
        roomNumber = 2;
    }
    private IEnumerator Tuto()
    {
        moveToPos = columns[++moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        yield return new WaitForSeconds(CheckTime(moveToPos));

        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {

            moveToPos = columns[++moveIndex].transform.position;
            enemy.navMeshAgent.SetDestination(moveToPos);
        }

        yield return new WaitForSeconds(CheckTime(moveToPos) + 6f * Time.deltaTime);

        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {

            EnemyAnimation.instance.sequence.Restart();
        }
        yield return new WaitUntil(() => isstepEnd);

        yield return StartCoroutine("GoBack");
    }

    public IEnumerator GoBack()
    {
        isOneTime = true; //반전활성화-------------------------------------다시 돌아가는 코드
        moveIndex = 2;
        moveToPos = columns[--moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        yield return new WaitForSeconds(CheckTime(moveToPos) + 6f * Time.deltaTime);

        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {

            moveToPos = columns[--moveIndex].transform.position;
            enemy.navMeshAgent.SetDestination(moveToPos);
            OFFState();
        }

        yield return new WaitForSeconds(CheckTime(moveToPos));

        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {
            enemy.gameObject.SetActive(false);
        }
    }
    private void OFFState()
    {
        enemy.enabled = false;
        enemy.GetComponent<SoundDetector>().isDetectOFF = true;
    }

    private float CheckTime(Vector3 moveToPos)
    {
        Vector3 currentPos;
        if (isOneTime) currentPos = columns[moveIndex + 1].transform.position;
        else currentPos = columns[moveIndex - 1].transform.position;

        float dis = Vector3.Distance(currentPos, moveToPos);                    //집주인과 이동할 위치사이의 거리
        float time = (int)dis / enemy.navMeshAgent.speed + 14 * Time.deltaTime;

        return time;
    }


    private void DayOneEvent()     //첫날의 행동패턴
    {
        AtStudy();
    }
    private void DayTwoEnvent()    //둘쨋날의 행동패턴
    {
        //OpenDoor();
        roomNumber = 3;
    }

    private void AtStreetEnvent_1() //3회차의 거리에서의 행동패턴
    {

    }

    private void AtStreetEnvent_2() //4회차의 거리에서의 행동패턴
    {

    }

    private void AtStreetEnvent_3() //5회차의 거리에서의 행동패턴
    {

    }

    public void ResetMenu() //다른 상태에서 돌아왔을 때용
    {
        if (EnemyMode.Tutorial == enemyMode)
        {
            //OpenDoor(); //그냥 나감
        }
        else if (EnemyMode.DayOne == enemyMode)
        {
            AtStudy();
        }
        else if (EnemyMode.DayTwo == enemyMode)
        {
            //다시 3개의 방을 돌아다님
        }
    }

    private void AtStudy() //1일차 서재용
    {
        moveToPos = columns[0].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);
        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                EnemyAnimation.instance.atStudySeq.Restart();
                Debug.Log("애니메이션 시작");
            }
        }
    }

    private void ChangeRoom() //튜토와 2일차용
    {
        if (moveIndex < (roomNumber - 1) * 2)
        {
            moveIndex += 2;
            //OpenDoor();
            changeTime = Time.time;
        }
        else
        {
            int randomNumber2 = Random.Range(0, 3);
            if (randomNumber2 == 0)
            {
                StopAction();
                changeTime = Time.time;
                //stepNumber = 3;
            }
            else
            {
                moveIndex = 0;
                //OpenDoor();
                changeTime = Time.time;
            }
        }
    }

    private void TutoSetting()
    {
        moveToPos = columns[moveIndex + 2].transform.position;
        lookPos = columns[moveIndex].transform.position;

        enemy.navMeshAgent.SetDestination(moveToPos);

        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                enemy.gameObject.transform.DOLookAt(lookPos, 1f);
                setTime = true;
            }
        }

        if (lookTime <= lookTimer)
        {
            moveToPos = columns[moveIndex + 3].transform.position;
            enemy.navMeshAgent.SetDestination(moveToPos);
        }
    }
    /*
    public void OpenDoor()
    {
        Debug.Log(1);

        moveToPos = columns[moveIndex].transform.position;

        enemy.navMeshAgent.SetDestination(moveToPos);
        StartCoroutine(Timer());
        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance )
        {
            Debug.Log("11111");
            changeTime = Time.time;
            EnterRoom();
            isOneTime = true;
        }
    }*/
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
        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                if (randomNumber == 0)
                {
                    LookBack();
                    isOneTime = true;
                }
                else
                {
                    LookAround();
                    isOneTime = true;
                }
                enemy.navMeshAgent.updateRotation = false;
            }
        }
    }
    private void LookAround()
    {
        Debug.Log(3);
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
        /*if (stepNumber == 1)
        {
            enemy.navMeshAgent.updateRotation = true;

            if (EnemyMode.Tutorial == enemyMode)
            {
                TutoSetting();
            }
            if (EnemyMode.DayTwo == enemyMode)
            {
                ChangeRoom();
            }
        }*/
    }
    private void LookBack()
    {
        Debug.Log(4);
        if (isOneTime)
        {
            if (isheight)
            {
                EnemyAnimation.instance.sequence3.Restart();
            }
            else
            {
                EnemyAnimation.instance.sequence4.Restart();
            }
            isOneTime = false;
        }
        /*
        if (stepNumber == 2)
        {
            LookAround();
            isOneTime = true;
            isLookBack = false;
        }*/
    }

    private void StopAction()
    {
        Debug.Log(5);
        /*
        int randomNumber = Random.Range(1, 3);  //2일차용 패턴
        if (stepNumber == 3)
        {
            hidePos = columns[moveIndex + randomNumber].transform.position;
            lookPos = columns[moveIndex + 4].transform.position;
            stepNumber = 4;
        }

        if (stepNumber == 4)
        {
            enemy.navMeshAgent.SetDestination(hidePos);
        }
        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                enemy.gameObject.transform.DOLookAt(lookPos, 1f);
                setTime_2 = true;

                if (lookTime <= lookTimer)
                {
                    setTime_2 = false;
                    lookTimer = 0f;
                    ChangeRoom();
                }
            }
        }*/
    }
    public void CheckTimer()
    {
        if (setTime_2)
        {
            lookTimer += Time.deltaTime;
        }

        if (setTime)
        {
            changeTime += Time.deltaTime;
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(waitTime);
        setTime = true;
    }

}
