using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    [SerializeField] private int pointIndex; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (ResearchManager_Simple.instance != null)
            {
                if (ResearchManager_Simple.instance.moveIndex == pointIndex)
                {
                    ResearchManager_Simple.instance.isEnd = true;
                }
            }
        }
    }
}
