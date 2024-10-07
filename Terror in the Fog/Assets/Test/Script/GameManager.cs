using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


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

    int moveIndex = 0;
    int roomNumber = 4;
    int currentroomNumber = 3;
    bool isSearchroom = false;
    public List<int> isDoneIdex = new List<int>();
    void Start()
    {
        instance = this; //싱글톤 패턴사용
        StartCoroutine(ResetIdex());
        ResearchRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResearchArea()
    {
        if (!isSearchroom)
        {
            Debug.Log("찾은 것 취소");

            isDoneIdex.Remove(moveIndex);
            --currentroomNumber;
            int randomNumber = Random.Range(0, currentroomNumber);
            moveIndex = isDoneIdex[randomNumber];

            if (currentroomNumber <= 0)
            {
                StartCoroutine(ResetIdex());
            }
        }
    }

    public void ResearchRoom()
    {
        isSearchroom = true;

        moveToPos = floorIndex[0].columns[moveIndex].transform.position;
        enemy.navMeshAgent.SetDestination(moveToPos);

        if (enemy.transform.position == moveToPos)
        {
            Debug.Log("방문 앞도착");
            moveToPos = floorIndex[0].columns[moveIndex + roomNumber].transform.position;
            enemy.navMeshAgent.SetDestination(moveToPos);
        }
        if (enemy.transform.position == moveToPos)
        {
            Debug.Log("방문 안으로 입장");
            isSearchroom = false;
        }
    }

    IEnumerator ResetIdex()
    {
        currentroomNumber = roomNumber - 1;
        for (int i = 1; i <= roomNumber; i++)
        {
            isDoneIdex.Add(i);  //0~3까지만 관리하는 의도
        }
        yield return null;
    }
}
