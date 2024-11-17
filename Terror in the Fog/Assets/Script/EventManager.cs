using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour //이벤트관리
{
    public static EventManager instance;

    [Header("Tutorial Event")]
    [SerializeField] private Door door;
    [SerializeField] private Enemy enemy;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {

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
