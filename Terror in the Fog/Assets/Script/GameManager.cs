using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour  //게임 전체적으로 <퀘스트, 각 회차의 맵>
{
    public static GameManager Instance;
    private static int Days = 0;

    private EVENTTYPE eventType;
    private enum EVENTTYPE
    {
        Tutorial,
        DayOne,
        DayTwo,
        DayThree,
        DayFour,
        DayFive
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SETDAY()
    {
        switch (eventType)
        {
            case EVENTTYPE.Tutorial:
                Tuto();
                break;
            case EVENTTYPE.DayOne:
                DayOne();
                break;
            case EVENTTYPE.DayTwo:
                DayTwo();
                break;
            case EVENTTYPE.DayThree:
                DayThree();
                break;
            case EVENTTYPE.DayFour:
                DayFour();
                break;
            case EVENTTYPE.DayFive:
                DayFive();
                break;
        }
    }

    private void Update()
    {
        CheckDays();
    }
    private void ChangeEvent(EVENTTYPE newType)
    {
        eventType = newType;
    }

    private void CheckDays()
    {
        if (Days == 0) ChangeEvent(EVENTTYPE.Tutorial);
        if (Days == 1) ChangeEvent(EVENTTYPE.DayOne);
        if (Days == 2) ChangeEvent(EVENTTYPE.DayTwo);
        if (Days == 3) ChangeEvent(EVENTTYPE.DayThree);
        if (Days == 4) ChangeEvent(EVENTTYPE.DayFour);
        if (Days == 5) ChangeEvent(EVENTTYPE.DayFive);
    }

    public void PassDay()
    {
        Days++;
    }

    private void Tuto()
    {

    }

    private void DayOne()
    {

    }
    private void DayTwo()
    {

    }
    private void DayThree()
    {

    }
    private void DayFour()
    {

    }
    private void DayFive()
    {

    }
}

