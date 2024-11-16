using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResearchManager_Simple;

public class EventManager : MonoBehaviour //이벤트관리
{
    [Header("Tutorial Event")]
    [SerializeField] private Door door;
    [SerializeField] private Enemy enemy;

    private EVENTTYPE eventType;
    public enum EVENTTYPE
    {
        Tutorial,
        DayOne,
        DayTwo,
        DayThree,
        AtStreet_1,
        AtStreet_2,
        AtStreet_3
    }
    void Update()
    {
        SETEVENT();
    }

    public void SETEVENT()
    {
        switch (eventType)
        {
            case EVENTTYPE.Tutorial:
                TutoEvent();
                break;
            case EVENTTYPE.DayOne:
                DayOneEvent();
                break;
            case EVENTTYPE.DayTwo:
                DayTwoEnvent();
                break;
            case EVENTTYPE.AtStreet_1:
                AtStreetEnvent_1();
                break;
            case EVENTTYPE.AtStreet_2:
                AtStreetEnvent_2();
                break;
            case EVENTTYPE.AtStreet_3:
                AtStreetEnvent_3();
                break;
        }
    }

    private void TutoEvent()
    {
        if (door != null)
        {
            if (door.isOpened)
            {
                enemy.gameObject.SetActive(true);
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
