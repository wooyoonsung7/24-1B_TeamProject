using System;
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
    private Inventory theInventory;

    IItem item;

    void Start()
    {
        playerCam = Camera.main;
        playerCam = GetComponentInChildren<Camera>();

    }

    void Update()
    {
        CheckItem();
        TryPickUp();
    }

    private void TryPickUp() //æ∆¿Ã≈€ ¡›±‚
    {
        if (Input.GetButtonDown("Interaction"))
        {
            CheckItem();
            CanPickUp();
        }
    }
    private void CheckItem() //æ∆¿Ã≈€¿Øπ´ »Æ¿Œ
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDir = transform.forward;

        Debug.DrawRay(rayOrigin, rayDir * distance, Color.red);

        if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, distance, whatIsTarget))
        {

            item = hitInfo.collider.GetComponent<IItem>();

            ItemInfoAppear();
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear() //æ∆¿Ã≈€»πµÊ «•Ω√ «œ±‚
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = item.itemName+ "»πµÊ" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear() //æ∆¿Ã≈€»πµÊ «•Ω√ ≤Ù±‚
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void CanPickUp() //æ∆¿Ã≈€ »πµÊ
    {
       if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(item.itemName + " »πµÊ «ﬂΩ¿¥œ¥Ÿ.");
                theInventory.AcuquireItem(item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }

    private void UseItem()
    {
        if (Input.GetButtonDown("Use Item"))
        {

        }
    }
}
