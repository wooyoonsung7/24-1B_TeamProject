using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    private bool isChange = true;
    [SerializeField] private int pointIndex; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("된다....");
            if (ResearchManager_Simple.instance != null)
            {
                Debug.Log("된다....");
                if (ResearchManager_Simple.instance.moveIndex == pointIndex)
                {
                    Debug.Log("된다");
                    ResearchManager_Simple.instance.isEnd = true;
                }
            }
        }
    }
}
