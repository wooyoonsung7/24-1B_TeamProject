using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class HintPaper : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    private void Start()
    {
        type = ItemType.Used;
        itemName = "힌트종이";
    }
    public void Use(GameObject target)
    {
        Debug.Log("힌트종이 사용");
    }
}
