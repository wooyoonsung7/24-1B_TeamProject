using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Camera playerCam;

    [SerializeField]
    private float distance = 50f;

    public LayerMask whatIsTarget;

    private RaycastHit hitInfo;

    private Vector3 camRotation;

    public float test = 0;
    public float test1 = 0;
    public float test2 = 1;
    void Start()
    {
        playerCam = Camera.main;
        playerCam = GetComponentInChildren<Camera>();

        //camRotation = new Vector3(playerCam.transform.localEulerAngles.x, 0.0f, 0.0f);   //X축만 받으면 됨.
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rayDir = transform.forward;

        Debug.DrawRay(rayOrigin, rayDir * 100f, Color.red);

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, distance, whatIsTarget))
            {
                Debug.Log("상호작용 실시");

                IItem item = hitInfo.collider.GetComponent<IItem>();

                if (item != null)
                {
                    item.Use(gameObject);
                }
            }
        }

    }
}
