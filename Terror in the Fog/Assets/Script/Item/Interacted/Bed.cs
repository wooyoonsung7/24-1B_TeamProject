using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static IItem;

public class Bed : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]private Door door;
    private void Awake()
    {

    }
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Bed";
        isCanUse = true;
    }

    public void Use(GameObject target)
    {
        Debug.Log("사용시도");
        if (isCanUse)
        {
            Debug.Log("침대사용");
            isCanUse = false;

            //자고 일어나는 애니메이션
            //저장시스템추가
            door.isCanUse = true;
            GameManager.Instance.PassDay();
        }
    }
}
