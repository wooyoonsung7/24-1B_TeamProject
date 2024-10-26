using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class Token : MonoBehaviour, IItem
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

    public int tokenIndex;

    private void Start()
    {
        type = ItemType.Used;
        itemName = "토큰";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
        isCanUse = false;
        index = tokenIndex;

        this.gameObject.layer = 6;
    }
    public void Use(GameObject target)
    {
        Debug.Log("토큰 사용");
        HandController handController = target.GetComponentInChildren<HandController>();
        if (handController.item != null)
        {
            if (handController.item.index >= 10 && handController.item.index <= 14)
            {
                handController.item.isCanUse = true;
                isCanUse = true;
                handController.item.getIndex = index;
                handController.item.Use(target);

            }
            else
            {
                Debug.Log("아이템이 유효하지 않습니다");
            }

        }

    }
}
