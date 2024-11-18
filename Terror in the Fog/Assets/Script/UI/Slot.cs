using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Image itemImage;
    public IItem item;

    //아이템정보를 OutsideInventory스크립트로 가져가기 위한 변수
    public bool isItemExist = false;

    public bool isCanUse = false;


    public void Update() //계속되는 체킹
    {
        if (item != null)
        {
            isItemExist = true;
        }
        else
        {
            isItemExist = false;
        }
    }
    //아이템 이미지의 투명도 조절
    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(IItem _item, bool _isItemExist = true)
    {
        //바깥인벤토리에 아이템 이미지 활성화, 복사
        isItemExist = _isItemExist;

        //슬롯 아이템 활성화
        item = _item;
        itemImage.sprite = item.itemImage;
        SetColor(1);
    }

    // 해당 슬롯 하나 삭제
    public void ClearSlot(bool _isItemExist = false)
    {
        //바깥인벤토리에 아이템 이미지 비활성화, 복사끄기
        isItemExist = _isItemExist;

        //슬롯 아이템 비활성화
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }
}
