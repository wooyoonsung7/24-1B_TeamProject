using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Rendering;
using UnityEngine;
using static IItem;

public class Book : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private int bookIndex;
    [SerializeField]
    private string bookName;

    private void Start()
    {
        type = ItemType.Used;
        itemName = "å";
        itemImage = _itemImage;
        itemName = bookName;
        isCanUse = false;
        index = bookIndex;
    }
    public void Use(GameObject target)
    {
        HandController handController = target.GetComponentInChildren<HandController>();
        if (handController != null)
        {
            if (handController.item != null)
            {
                if (handController.item.index == index)
                {
                    //handController.item.getIndex = index;
                    handController.item.isCanUse = true;
                    isCanUse = true;
                    handController.item.Use(target);
                }

            }
        }

    }
}
