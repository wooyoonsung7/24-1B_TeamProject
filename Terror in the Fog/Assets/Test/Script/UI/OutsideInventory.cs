using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject i_InventoryParent;
    [SerializeField]
    private GameObject o_InventoryParent;
    [SerializeField]
    private int o_InventoryCount = 5;

    private Slot[] i_slots;

    private Transform[] o_slots;

    private Image[] itemImages;

    private GameObject[] checkImages;

    [SerializeField]
    private GameObject player;

    public bool isCanUse = false;
    IItem item;
    private void Start()
    {
        //배열크기 할당(필수)
        o_slots = new Transform[o_InventoryCount];
        itemImages = new Image[o_InventoryCount];
        checkImages = new GameObject[o_InventoryCount];

        //안의 인벤토리 컴포넌트 가져오기
        i_slots = i_InventoryParent.GetComponentsInChildren<Slot>();

        //밖의 인벤토리 Transform,컴포넌트 가져오기
        for (int i = 0; i < o_InventoryCount; i++)
        {
            o_slots[i] = o_InventoryParent.transform.GetChild(i).transform;
            itemImages[i] = o_slots[i].GetChild(1).GetComponent<Image>();
            checkImages[i] = o_slots[i].GetChild(0).gameObject;
            checkImages[i].SetActive(false);
        }
    }
    private void Update()
    {
        CopySlots();
    }

    //아이템 이미지의 투명도 조절
    private void SetColor(float _alpha, int i)
    {
        Color color = itemImages[i].color;
        color.a = _alpha;
        itemImages[i].color = color;
    }

    //안의 인벤토리를 밖의 인벤토리로 복사
    public void CopySlots()
    {
        for (int i = 0; i < o_InventoryCount; i++)
        {
            if (i_slots[i] == null || itemImages == null)
                return;

            if (i_slots[i].isItemExist)
            {
                item = i_slots[i].item;
                itemImages[i].sprite = i_slots[i].itemImage.sprite;
                SetColor(1,i);

            }
            else
            {
                item = null;
                itemImages[i].sprite = null;
                SetColor(0,i);
            }
        }
    }
    //마우스스크롤로 사용할 이미지 표시
    public void CheckCanUse(int idex)
    {
        if (!checkImages[idex].activeSelf)                //해당 슬롯의 사용가능 표시안된 한번 실행
        {
            for (int i = 0; i < o_InventoryCount; i++)
            {
                if (i != idex)                            //인벤토리의 사용가능표시, 아이템사용가능 초기화
                {
                    checkImages[i].SetActive(false);

                    if (i_slots[i].item != null)
                    {
                        i_slots[i].item.isCanUse = false;
                    }
                }
            }

            checkImages[idex].SetActive(true);            //인벤토리의 사용가능표시, 아이템사용가능 표시

            if (i_slots[idex].item != null)
            {
                item = i_slots[idex].item;
                item.isCanUse = true;
            }

        }
        if (i_slots[idex].item != null && checkImages[idex].activeSelf)
        {
            Debug.Log("이거2");
            if (Input.GetButtonDown("Use Item"))
            {
                Debug.Log("이거3");
                UsingItem(player, idex);
            }

        }
    }

    public void UsingItem(GameObject player, int idex)
    {
        if (item.isCanUse)
        {
            Debug.Log("된다");
            item.Use(player);

            if (item.type == IItem.ItemType.Consumed)
            {
                i_slots[idex].ClearSlot();

            }
        }
    }

}
