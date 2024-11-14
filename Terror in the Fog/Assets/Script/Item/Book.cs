using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static IItem;

public class Book : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private GameObject _itemPrefab;
    [SerializeField]
    private int bookIndex;

    private void Start()
    {
        type = ItemType.Used;
        itemName = "책";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
        isCanUse = false;
        index = bookIndex;
    }
    public void Use(GameObject target)
    {
        Debug.Log("책 사용");
        HandController handController = target.GetComponentInChildren<HandController>();
        if (handController != null)
        {
            if (handController.item != null)
            {
                if (handController.item.index == index)
                {
                    handController.item.isCanUse = true;
                    isCanUse = true;
                    handController.item.Use(target);
                    Debug.Log("맞춰진다");
                }
                else
                {
                    Debug.Log("아이템이 유효하지 않습니다");
                }
            }
        }

    }
}
