using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static IItem;

public class SafeButton : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    public bool selected = false;
    public bool isCollect = false;
    public int ListIndex = 5;
    Vector3 currentPos;

    private Vector3 originalPosition;
    private Vector3 pressedPosition;

    private void Start()
    {
        currentPos = transform.position;
        type = ItemType.interacted;
        itemName = "금고버튼";
        isCanUse = true;

        originalPosition = transform.localPosition;
        pressedPosition = originalPosition + new Vector3(-0.001f, 0, 0); // 눌린 위치 계산
    }
    public void Use(GameObject target)
    {
        if (!selected)
        {
            //버튼 눌리고 돌아오는 애니메이션
            transform.DOLocalMove(pressedPosition, 0.2f);
            selected = true;
        }
    }
    public void ResetValue()
    {
        transform.DOLocalMove(originalPosition, 0.2f); // 원래 위치로 복귀
        //transform.position = currentPos;
    }
}