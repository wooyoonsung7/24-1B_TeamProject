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

    private bool EndEvent = false;

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
            GameManager.Instance.gotoHouse = true;
            GameManager.currentMap = 2;   //거리로 이동
            SceneManager.LoadScene("Street_1");
        }
    }

    public void GotoTargetHouse(int day)
    {
        if (targetHousedoor != null)
        {
            if (targetHousedoor.isOpen)
            {
                SceneManager.LoadScene("GameScene_" + day);
            }
        }
    }

    public void GoToStreet()
    {
        if (homeDoor != null)
        {
            if (homeDoor.isOpen)
            {
                SceneManager.LoadScene("Street_2");
            }
        }
    }

    private void DayOneEvent()
    {

    }

    private void DayTwoEnvent()
    {

    }

    private void AtStreetEnvent_1()
    {

    }

    private void AtStreetEnvent_2()
    {

    }

    private void AtStreetEnvent_3()
    {

    }
}
