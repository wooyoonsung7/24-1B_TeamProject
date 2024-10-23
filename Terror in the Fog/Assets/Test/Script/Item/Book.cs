using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static IItem;

public class Book : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private GameObject _itemPrefab;

    private void Start()
    {
        type = ItemType.Used;
        itemName = "책";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
        isCanUse = false;
    }
    public void Use(GameObject target)
    {
        Debug.Log("책 사용");
    }
}
