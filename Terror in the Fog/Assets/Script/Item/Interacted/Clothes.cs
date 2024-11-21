using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using DG.Tweening;
using static PlayerController;

public class Clothes : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField] bool isHide = false;
    [SerializeField] float m_duration = 0.3f;
    [SerializeField] float r_duration = 0.1f;
    private Transform transObject;

    private bool endMove = true;
    private Vector3 currentRot;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "ø ¿Â";
        
        transObject = transform.GetChild(0);

    }

    public void Use(GameObject target)
    {
        if (endMove)
        {
            endMove = false;
            isHide = !isHide;
        }
        PlayerController playerController = target.GetComponent<PlayerController>();
        if (isHide)
        {
            currentRot = target.transform.localEulerAngles;
            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.y += -90;

            target.transform.DOMove(transObject.position, m_duration).OnComplete(() => endMove = true);
            target.transform.DOLocalRotate(currentRotation, r_duration);
            playerController.isCanMove = false;
            playerController.isHide = true;
            playerController.isOneTime_3 = true;
        }
        else
        {
            target.transform.position = transObject.position + transform.right * -1.5f;
            playerController.isHide = false;
            playerController.isCanMove = true;
            endMove = true;
        }
    }
}
