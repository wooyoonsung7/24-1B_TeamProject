using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class SafeButton : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    public bool selected = false;
    public bool isCollect = false;
    Vector3 currentPos;
    private void Start()
    {
        currentPos = transform.position;
        type = ItemType.interacted;
        itemName = "금고버튼";
        isCanUse = true;
    }
    public void Use(GameObject target)
    {
        if (!selected)
        {
            //버튼 눌리고 돌아오는 애니메이션
            transform.position -= transform.right * 0.06f;
            selected = true;
        }
    }
    public void ResetValue()
    {
        transform.position = currentPos;
    }
}