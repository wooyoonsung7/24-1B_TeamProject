using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandController : MonoBehaviour
{
    private Camera playerCam;

    [SerializeField]
    private float distance = 50f;

    private bool pickupActivated = false;

    public LayerMask whatIsTarget;

    private RaycastHit hitInfo;

    private TextMeshProUGUI actionText;

    IItem item;
    void Start()
    {
        playerCam = Camera.main;
        playerCam = GetComponentInChildren<Camera>();

        //camRotation = new Vector3(playerCam.transform.localEulerAngles.x, 0.0f, 0.0f);   //X√‡∏∏ πﬁ¿∏∏È µ .
    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryPickUp();
    }

    private void TryPickUp()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            CheckItem();
            CanPickUp(item);
        }
    }
    private void CheckItem()
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDir = transform.forward;

        Debug.DrawRay(rayOrigin, rayDir * 100f, Color.red);

        if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, distance, whatIsTarget))
        {

            item = hitInfo.collider.GetComponent<IItem>();

            ItemInfoAppear(item);
        }
    }

    private void ItemInfoAppear(IItem item)
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        //actionText.text = item.itemName+ "»πµÊ" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void CanPickUp(IItem item)
    {
       if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(item.itemName + " »πµÊ «ﬂΩ¿¥œ¥Ÿ.");
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }


}
