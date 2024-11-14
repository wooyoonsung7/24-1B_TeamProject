using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class InsideInventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    //인벤토리의 슬롯들에게 명령을 내리기위한 변수
    [SerializeField]
    private GameObject go_InsideInventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;

    //인멘토리를 열었을 때, 플레이어조작을 멈추기 위한 변수
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private HandController handController;
    [SerializeField]
    private GameObject outsideInventory;
    [SerializeField]
    private GameObject staminaSlider;

    public bool isFull = false;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetColor(0);
        }
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                InventoryAcited();
            }
            else
            {
                InventoryUnactivated();
            }
        }
    }

    private void InventoryAcited()
    {
        go_InsideInventoryBase.SetActive(true);
        Cursor.lockState = CursorLockMode.None;              //마우스조작 활성화
        Cursor.visible = true;
        playerController.enabled = false;                    //캐릭터조작 비활성화
        handController.enabled = false;
        outsideInventory.SetActive(false);
        staminaSlider.SetActive(false);
    }

    private void InventoryUnactivated()
    {
        go_InsideInventoryBase.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;           //마우스조작 비활성화
        Cursor.visible = false;
        playerController.enabled = true;                    //캐릭터조작 재활성화
        handController.enabled = true;
        outsideInventory.SetActive(true);
        staminaSlider.SetActive(true);
    }

    public void AcuquireItem(IItem _item)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item);
                return;
            }
        }
    }

    public void UsedItem(IItem _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].ClearSlot();
                    return;
                }
            }
        }
    }

    public void CheckSlotFull()
    {
        int count = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                count++;
            }
        }
        if (count >= 9)
        {
            isFull = true;
            Debug.Log("인벤토리가 가득 찼읍니다");
        }
    }
}
