using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static IItem;

public class Key : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private GameObject _itemPrefab;

    public int keyIndex = 0;
    private void Start()
    {
        type = ItemType.Used;
        itemName = "열쇠";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
        isCanUse = false;          //여기에서 isCanUse는 의미 :  아이템이 사용(=없어짐)될 수 있는가?
    }
    public void Use(GameObject target)
    {
        Debug.Log("열쇠 사용");
        HandController handController = target.GetComponentInChildren<HandController>();
        if (handController != null)
        {
            if (handController.item != null)
            {
                if (handController.item.index == keyIndex)
                {
                    handController.item.isCanUse = true;
                    isCanUse = true;
                    Debug.Log("열린다");
                }
                else
                {
                    Debug.Log("열쇠가 유효하지 않습니다");
                }
            }
        }
    }
}
