using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static IItem;

public class Drawer : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField] private int drawerIndex = 0;
    [SerializeField] private bool isUnLocked = true;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject item2;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "서랍";
        isCanUse = isUnLocked;
        index = drawerIndex;
    }
    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            Vector3 moveToPos = transform.position + transform.forward * 0.5f;
            moveToPos.y = transform.position.y;
            transform.DOMove(moveToPos, 3f);

            if (item != null) item.gameObject.layer = 6;
            if (item2 != null) item2.gameObject.layer = 6;

            isCanUse = false;
        }
        else
        {
            Debug.Log("열쇠가 없음");
        }
    }
}
