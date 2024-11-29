using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using DG.Tweening;
using UnityEngine;
using static IItem;

public class Bank : MonoBehaviour,IItem
{

    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField] private GameObject bankDoor;
    Vector3 itemRot;
    int order = 0;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Àú±ÝÅë";

        itemRot = Vector3.zero;
        itemRot.z -= 90f;
    }
    public void Use(GameObject target)
    {
        switch(order)
        {
            case 0:
                transform.DORotate(itemRot, 0.1f);
                order++;
                break;
            case 1:
                bankDoor.transform.DORotate(new Vector3(-180, 90, -90), 0.5f).SetEase(Ease.OutQuad);
                gameObject.layer = 0;
                break;
        }
        transform.DORotate(itemRot, 0.1f);
    }
}
