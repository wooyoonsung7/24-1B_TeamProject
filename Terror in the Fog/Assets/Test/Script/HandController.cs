using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    private Camera playerCam;

    [SerializeField]
    private float distance = 1f;

    private bool pickupActivated = false;

    public LayerMask whatIsTarget;

    private RaycastHit hitInfo;

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private InsideInventory theInventory;
    private OutsideInventory theInventory_2;

    [SerializeField]
    private float scrollSpeed = 2f;
    float scrollPoint = 0f;
    int i = 0;

    IItem item;

    private void Awake()
    {

    }
    void Start()
    {
        playerCam = Camera.main;
        playerCam = GetComponentInChildren<Camera>();
        theInventory_2 = FindObjectOfType<OutsideInventory>();
    }

    void Update()
    {
        CheckItem();
        TryPickUp();
        SellectItem();
        UseItem();
    }
        
    private void TryPickUp() //아이템 줍기
    {
        if (Input.GetButtonDown("Interaction"))
        {
            CheckItem();
            CanPickUp();
            Interaction();
        }
    }
    private void CheckItem() //아이템유무 확인
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDir = transform.forward;

        Debug.DrawRay(rayOrigin, rayDir * distance, Color.red);

        if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, distance, whatIsTarget)) //해당 아이템이 아이템임을 인식
        {

            item = hitInfo.collider.GetComponent<IItem>();

            ItemInfoAppear();
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear() //아이템임을 표시 하기
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = item.itemName+ "상화작용" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear() //아이템임을 표시 끄기
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void CanPickUp() //아이템 획득
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                if (item.type == IItem.ItemType.Used || item.type == IItem.ItemType.Consumed)
                {
                    Debug.Log(item.itemName + " 획득 했습니다.");
                    theInventory.AcuquireItem(item);
                    Destroy(hitInfo.transform.gameObject);
                    ItemInfoDisappear();

                }
            }
        }
    }
    public void SellectItem()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        if (scroll != 0)
        {
            scrollPoint += scroll;

            if (0f >= scrollPoint && scrollPoint > -1f)
            {
                i = 0;
            }
            else if (-1f >= scrollPoint && scrollPoint > -2f)
            {
                i = 1;
            }
            else if (-2f >= scrollPoint && scrollPoint > -3f)
            {
                i = 2;
            }
            else if (-3f >= scrollPoint && scrollPoint > -4f)
            {
                i = 3;
            }
            else if (-4f >= scrollPoint && scrollPoint >= -5f)
            {
                i = 4;
            }
            else if (scrollPoint < -5f)
            {
                scrollPoint = 0f;
            }
            else
            {
                scrollPoint = -5f;
            }
        }
        theInventory_2.CheckCanUse(i);
    }

    private void UseItem() //인벤안의 아이템 사용
    {
        if (Input.GetButtonDown("Use Item"))
        {
            theInventory_2.UsingItem();
        }
    }

    private void Interaction() //가구등 상호작용
    {
        if (hitInfo.transform != null)
        {
            if (item.type == IItem.ItemType.interacted)
            {
                item.Use(transform.parent.gameObject);
            }
        }
    }
}
