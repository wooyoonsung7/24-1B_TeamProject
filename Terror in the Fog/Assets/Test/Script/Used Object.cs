using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedObject : MonoBehaviour, IItem
{
    public void Use(GameObject target)
    {
        Debug.Log("아이템 사용");
    }
}
