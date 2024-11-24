using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public GameObject player;
    public float maxdistance = 3;
    public float mindistance = 3;
 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Tests();
    }

    private void Tests()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.y += maxdistance;
        if (transform.position.y < targetPos.y)
        {
            //사운드종료
        }
        targetPos.y -= mindistance;
        if (transform.position.y > targetPos.y)
        {
            //사운드 종료
        }
    }
}
