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
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private GameObject chestCap;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "주방상자";
        isCanUse = false;
        index = 3;
    }
    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            Vector3 capPos = chestCap.transform.position + chestCap.transform.right * 1.1f;

            chestCap.transform.DOMove(capPos, 3f);
        }
        else
        {
            Debug.Log("열쇠가 없음");
        }
    }
}
