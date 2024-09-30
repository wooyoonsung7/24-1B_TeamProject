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

    [SerializeField]
    private Sprite itemImage_2;
    [SerializeField]
    private GameObject itemPrefab_2;

    private void Start()
    {
        type = ItemType.Used;
        itemName = "도구함";
        itemImage = itemImage_2;
        itemPrefab = itemPrefab_2;
    }
    public void Use(GameObject target)
    {
        Debug.Log("도구함 사용");
    }
}
