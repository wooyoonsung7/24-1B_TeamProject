using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    private Camera playerCam;

    [SerializeField]
    private float distance = 1.5f;

    private bool pickupActivated = false;

    public LayerMask whatIsTarget;
    public LayerMask obstacleMask;

    private RaycastHit hitInfo;

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private float scrollSpeed = 2f;
    float scrollPoint = 0f;
    int i = 0;

    public IItem item;

    void Start()
    {
        playerCam = Camera.main;
        playerCam = GetComponentInChildren<Camera>();
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
            InsideInventory.Instance.CheckSlotFull();
        }
    }
    private void CheckItem() //아이템유무 확인
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDir = transform.forward;

        Debug.DrawRay(rayOrigin, rayDir * distance, Color.red);

        //오브젝트일 때, 획득 X는 실제로 바닥에 아이템이 있을 때, 아이템과 오브젝트가 곂치기 때문에 제거, 다른 방법사용
        if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, distance, whatIsTarget)) //해당 아이템이 아이템임을 인식
        {

            item = hitInfo.collider.GetComponent<IItem>();
            ItemInfoAppear();
        }
        else
        {
            item = null;
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear() //아이템임을 표시 하기
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = item.itemName+ "상화작용" + "<color=yellow>" + "(F)" + "</color>";
    }
    private void ItemInfoDisappear() //아이템임을 표시 끄기
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void CanPickUp() //아이템 획득
    {
        if (pickupActivated && !InsideInventory.Instance.isFull)
        {
            if (hitInfo.transform != null && hitInfo.collider.gameObject.layer == 6)
            {
                if (item.type == IItem.ItemType.Used)
                {
                    SoundManager.instance.PlaySound("GetItem");
                    Debug.Log(item.itemName + " 획득 했습니다.");
                    InsideInventory.Instance.AcuquireItem(item);
                    Destroy(hitInfo.transform.gameObject);
                    ItemInfoDisappear();
                    Debug.Log("아이템이름은" + item.itemName);
                    EventManager.instance.CheckIventoryItem(item.itemName);
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
            else if (-5f >= scrollPoint && scrollPoint >= -6f)
            {
                i = 5;
            }
            else if (scrollPoint < -6f)
            {
                scrollPoint = 0f;
            }
            else
            {
                scrollPoint = -6f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            i = 0; scrollPoint = -0.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            i = 1; scrollPoint = -1.5f;
        }
        else if( Input.GetKeyDown(KeyCode.Alpha3))
        {
            i = 2; scrollPoint = -2.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            i = 3; scrollPoint = -3.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            i = 4; scrollPoint = -4.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            i = 5; scrollPoint = -5.5f;
        }
        InsideInventory.Instance.CheckCanUse(i);
    }

    private void UseItem() //인벤안의 아이템 사용
    {
        if (Input.GetButtonDown("Use Item"))
        {
            if (item != null)
            {
                InsideInventory.Instance.UsingItem();
            }

        }
    }

    private void Interaction() //가구등 상호작용
    {
        if (hitInfo.transform != null && item != null)
        {
            if (item.type == IItem.ItemType.interacted)
            {
                item.Use(transform.parent.gameObject);
            }
        }
    }

    public void isDead()  //사망시, 옷장밖으로 내보내기
    {
        Interaction();
    }
}
