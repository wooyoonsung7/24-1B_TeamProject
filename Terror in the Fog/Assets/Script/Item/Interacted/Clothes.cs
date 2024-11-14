using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using DG.Tweening;

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

    private void Awake()
    {
        
    }
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "ø ¿Â";
        
        transObject = transform.GetChild(0);

    }

    public void Use(GameObject target)
    {
        isHide = !isHide;
        PlayerController playerController = target.GetComponent<PlayerController>();
        if (isHide)
        {
            Debug.Log("ø ¿Â ø≠±‚");
            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.y += -90;

            target.transform.DOMove(transObject.position, m_duration);
            target.transform.DOLocalRotate(currentRotation, r_duration);
            playerController.isCanMove = false;
            playerController.isHide = true;
        }
        else
        {
            Debug.Log("ø ¿Â ¥›±‚");
            target.transform.position = transObject.position + transform.right * -1.5f;
            playerController.isHide = false;
            playerController.isCanMove = true;
        }
    }
}
