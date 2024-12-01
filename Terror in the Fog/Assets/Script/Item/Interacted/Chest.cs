using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static IItem;

public class Chest : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private OpenBox chestCap;
    [SerializeField]
    private GameObject knife;
    [SerializeField]
    private GameObject Locked;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "주방상자";
        isCanUse = false;
        index = 4;

        if(knife != null) knife.layer = 0;
    }
    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            chestCap.ToggleBox();
            Locked.SetActive(false);
            knife.layer = 6;
        }
        else
        {
            Debug.Log("열쇠가 없음");
        }
    }
}
