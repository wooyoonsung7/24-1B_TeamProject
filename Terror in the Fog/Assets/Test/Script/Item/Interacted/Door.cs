using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using DG;
using DG.Tweening;

public class Door : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    //private float tweenDuration = 3f;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "문";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Use(gameObject);
        }
    }
    public void Use(GameObject target)
    {
        Debug.Log("문 열기");
        //transform.DORotate(new Vector3(0, -90, 0), tweenDuration);
    }
}
