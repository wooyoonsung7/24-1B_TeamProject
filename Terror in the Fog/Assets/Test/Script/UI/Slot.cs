using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image itemImage;
    public IItem item;
    public string slotID;

    //아이템정보를 OutsideInventory스크립트로 가져가기 위한 변수
    public bool isItemExist = false;

    public bool isCanUse = false;

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

    //마우스 드래그가 시작 됐을 때 발생하는 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("된다0");
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;

            Debug.Log("된다1");        
        }
    }

    // 마우스 드래그 중일 때 계속 발생하는 이벤트
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;

            Debug.Log("된다2");
        }
    }

    //마우스 드래그가 끝났을 때 발생하는 이벤트
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        //SetColor(1);
    }

    //해당 슬롯에 무언가가 마우스 드롭 됐을 때 발생하는 이벤트
    //드래그 대상이 아니느 드롭 위치 대상이 된 슬롯에서 호출된다.
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot() // 예: 바뀔 아이템 = A, 바꿀 아이템 = B
    {
        IItem _item = item; //A를 잠시 저장

        AddItem(DragSlot.instance.dragSlot.item); //A대신에 B를 넣는다.

        if (_item != null)
        {
            DragSlot.instance.dragSlot.AddItem(_item); //저장했던 A를 B 대신에 넣는다.
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot(); //A가 없을 경우, 비운다.
            DragSlot.instance.dragSlot = null;
        }
    }
}
