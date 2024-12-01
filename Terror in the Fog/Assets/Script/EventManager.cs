using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventManager : MonoBehaviour //이벤트관리
{
    public static EventManager instance;

    [Header("Tutorial Event")]
    [SerializeField] private Door roomdoor;
    [SerializeField] private Door housedoor;
    [SerializeField] private Enemy enemy;

    [Header("AtStreet")]
    [SerializeField] private Door targetHousedoor;

    [Header("AtHome")]
    [SerializeField] private Bed bed;
    [SerializeField] private Door homeDoor;

    [Header("DayOne Event")]
    [SerializeField] private Safe safe;
    [SerializeField] private Door day1Door;

    [Header("DayTwo Event")]
    [SerializeField] private Door day2Door;

    [Header("DayThree Event")]
    [SerializeField] private Door day3Door;

    [Header("DayFour Event")]
    [SerializeField] private Door day4Door;

    [Header("DayFive Event")]
    [SerializeField] private Door day5Door;

    private bool EndEvent = false;
    private bool EndEvent_2 = false;
    public static bool playerdead = false;
    public bool isGameOver = false;

    private int count = 0;
    private bool isOneTime = true;
    private void Awake()
    {
        instance = this;
    }

    public void TutoEvent()
    {
        if (roomdoor != null && !EndEvent)
        {
            if (roomdoor.isCanUse)
            {
                enemy.gameObject.SetActive(true);
                ResearchManager_Simple.instance.StartCoroutine("Tuto");
                EndEvent = true;
            }
        }
        if (ResearchManager_Simple.instance != null)
        {
            if (ResearchManager_Simple.instance.EventEnd)
            {
                ExitTargetHouse();
            }
        }
    }

    private void ExitTargetHouse() //타겟의 집에서 나가기
    {
        housedoor.isCanUse = true;
        if (housedoor.isOpen)
        {
            GameManager.currentMap = 4;   //거리로 이동
            SceneManager.LoadScene("Street_1");
            //튜토리얼 종료
        }
    }

    public void GotoTargetHouse(int day)
    {
        if (targetHousedoor != null)
        {
            if (targetHousedoor.isOpen)
            {
                GameManager.currentMap = 3;
                SceneManager.LoadScene("GameScene_" + day);
            }

            if (isOneTime)
            {
                isOneTime = false;
                SoundManager.instance.PlaySound("Knell");
                Debug.Log("들린다");
            }
        }
    }

    public void GoToStreet()
    {
        if (homeDoor != null)
        {
            if (homeDoor.isOpen)
            {
                GameManager.currentMap = 2;
                SceneManager.LoadScene("Street_2");
                isOneTime = true;
            }

            if (isOneTime)
            {
                isOneTime = false;
                SoundManager.instance.PlaySound("Fireplace");
                Debug.Log("들린다");
            }
        }
    }

    public void PlayerDead()
    {
        GameManager.Instance.GameOverCanvas.SetActive(true);
        isGameOver = true;
    }
    public IEnumerator AfterPlayerDead()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        playerdead = false;
        yield return new WaitForSeconds(0.1f);
        bed.Use(player);
        Debug.Log("침대사용완");
        //자고 일어나는 애니메이션만 제작할 것
        //애니메이션작동시 자동위치이동!!
    }
    public void CheckIventoryItem(string name)
    {
        Debug.Log("된다2");
        if (GameManager.Days == 1)
        {
            if (name == "루비목걸이") { day1Door.isCanUse = true; Debug.Log("된다"); }
        }

        if (GameManager.Days == 2)
        {
            if (name == "숲토큰") { count++; if (count == 2) { day2Door.isCanUse = true; } }
            if (name == "꽃토큰") { count++; if (count == 2) { day2Door.isCanUse = true; } }
        }

        if (GameManager.Days == 3)
        {
            if (name == "다이아반지") { day3Door.isCanUse = true; }
        }

        if (GameManager.Days == 4)
        {
            if (name == "신전토큰") { count++; if (count == 2) { day4Door.isCanUse = true; } }
            if (name == "근위대토큰") { count++; if (count == 2) { day4Door.isCanUse = true; } }

        }

        if (GameManager.Days == 5)
        {
            if (name == "사파이어귀걸이") { day5Door.isCanUse = true; }
        }
    }

    public void DayOneEvent()
    {
        SetSound();

        if (ResearchManager_Simple.instance != null)
        {
            if (!EndEvent)
            {
                ResearchManager_Simple.instance.StartSafeCoroutine();
                EndEvent = true;
            }

            if (safe == null || day1Door == null) return;

            if (safe.isUnLocked && !EndEvent_2)
            {
                ResearchManager_Simple.instance.isEnd_2 = true;
                EndEvent_2 = true;
            }
            if (day1Door.isOpen)
            {
                GameManager.currentMap = 4;   //거리로 이동
                SceneManager.LoadScene("Street_1");
            }
        }
    }

    public void DayTwoEvent()
    {
        SetSound();

        if (ResearchManager_Simple.instance != null)
        {
            if (!EndEvent)
            {
                ResearchManager_Simple.instance.StartSafeCoroutine();
                EndEvent = true;
            }

            if (day2Door == null) return;
            if (day2Door.isOpen)
            {
                GameManager.currentMap = 4;         //거리로 이동
                SceneManager.LoadScene("Street_1");
            }
        }
    }

    public void DayThreeEvent()
    {
        SetSound();

        if (ResearchManager.instance != null)
        {
            if (day3Door.isOpen)
            {
                GameManager.currentMap = 4;          //거리로 이동
                SceneManager.LoadScene("Street_1");
            }
        }
        if (ResearchManager_Simple.instance != null)  //Day3일 때, Street_1에만 해당 스크립트 넣기
        {

        }
    }

    public void DayFourEvent()
    {
        SetSound();

        if (ResearchManager.instance != null)
        {
            if (day4Door.isOpen)
            {
                GameManager.currentMap = 4;          //거리로 이동
                SceneManager.LoadScene("Street_1");
            }
        }
        if (ResearchManager_Simple.instance != null)  //Day4일 때, Street_1에만 해당 스크립트 넣기
        {

        }
    }

    public void DayFiveEvent()
    {
        SetSound();

        if (ResearchManager.instance != null)
        {
            if (day5Door.isOpen)
            {
                GameManager.currentMap = 4;          //거리로 이동
                SceneManager.LoadScene("Street_1");
            }
        }
        if (ResearchManager_Simple.instance != null)  //Day5일 때, Street_1에만 해당 스크립트 넣기
        {

        }
    }

    IEnumerator Day5Event()
    {
        while (true)
        {
            Debug.Log("이벤트진행중");
            Enemy enemy = FindObjectOfType<Enemy>();
            enemy.stopResearch = true;
            enemy.navMeshAgent.speed *= 1.5f;
            enemy.gameObject.GetComponent<SoundDetector>().isDetectOFF = true;
            enemy.navMeshAgent.SetDestination(new Vector3(11.15f, 5.57f, -41.88f));
            enemy.transform.rotation = Quaternion.Euler(0f, 90, 0);
            yield return new WaitForSeconds(1f);
            if (enemy.transform.position == new Vector3(11.15f, 5.57f, -41.88f)) break;

            yield return null;
        }
    }

    private void SetSound()
    {
        if (isOneTime)
        {
            isOneTime = false;
            SoundManager.instance.PlaySound("Fireplace");
            Debug.Log("들린다");
        }
    }
}
