using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static IItem;

public class ToolKit : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public void Use(GameObject target)
    {
        Debug.Log("도구함 사용");
    }
}
