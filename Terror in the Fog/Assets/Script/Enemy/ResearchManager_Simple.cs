using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

    //문앞으로 이동에서 문열기의 이동을 위한 변수
    public float changeTime = 0f;

    public bool isstepEnd = false;
    private bool isOneTime = false;

    public bool isEnd = false; //1일차용

    //어떤 용으로 사용할지를 선택


    //문Control용
    public bool isLookBack = false;

    public bool EventEnd = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //싱글톤 패턴사용
        }
    }

    private IEnumerator Tuto()
    {
        MoveToPos();
        yield return new WaitForSeconds(CheckTime(moveToPos));
        MoveToPos_2();

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
            EventEnd = true;
        }
    }
    private void OFFState()
    {
        //enemy.enabled = false;
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

    private void MoveToPos()
    {
        moveToPos = columns[++moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);
    }
    private void MoveToPos_2()
    {
        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f)
        {
            MoveToPos();
        }
    }
    public IEnumerator DayOne()
    {
        while (true)
        {
            moveIndex = 0;
            Debug.Log("111");
            MoveToPos(); if (isEnd) yield return StartCoroutine(DayOneEvent());
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);  //애니메이션시간 5초(임시) + (애니메이션 실행)
                MoveToPos_2(); if (isEnd) yield return StartCoroutine(DayOneEvent());
            }
            yield return null;
            moveIndex = 0;
            yield return null;
        }
    }

    public IEnumerator DayOneEvent()
    {
        int temp = 7;
        moveIndex = 5;

        MoveToPos();
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(CheckTime(moveToPos));
            MoveToPos_2();
        }
        yield return new WaitForSeconds(CheckTime(moveToPos));
        if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && enemy.navMeshAgent.remainingDistance <= 0.1f) moveIndex++;
        if(moveIndex >= 9) EnemyAnimation.instance.sequence.Restart();
        yield return new WaitUntil(() => isstepEnd);
        
        moveIndex = temp;
        MoveToPos();
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(CheckTime(moveToPos));
            --temp; moveIndex = temp;
            MoveToPos_2();
        }
        yield return null;
        if (moveIndex <=6) isEnd = false; yield return StartCoroutine(DayOne());
    }

    public IEnumerator DayTwo()
    {
        moveIndex = 0;
        MoveToPos(); Debug.Log("1번째");
        yield return new WaitForSeconds(CheckTime(moveToPos));
        MoveToPos_2(); Debug.Log("2번째");
        yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
        MoveToPos_2(); Debug.Log("3번째");
        yield return new WaitForSeconds(CheckTime(moveToPos));
        MoveToPos_2(); Debug.Log("4번째");
        yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
        MoveToPos_2(); Debug.Log("5번째");
        yield return new WaitForSeconds(CheckTime(moveToPos) + 25f * Time.deltaTime);
        MoveToPos_2(); Debug.Log("6번째");
        yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);

        while (true)
        {
            moveIndex = 0;
            MoveToPos_2(); Debug.Log("1번째");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 80f * Time.deltaTime);
            MoveToPos_2(); Debug.Log("2번째");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
            MoveToPos_2(); Debug.Log("3번째");
            yield return new WaitForSeconds(CheckTime(moveToPos));
            MoveToPos_2(); Debug.Log("4번째");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
            MoveToPos_2(); Debug.Log("5번째");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 25f * Time.deltaTime);
            MoveToPos_2(); Debug.Log("6번째");
            yield return new WaitForSeconds(CheckTime(moveToPos) + 5f);
        }
    }
}
