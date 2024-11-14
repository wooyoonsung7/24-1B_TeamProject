using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class HintPaper : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private GameObject _itemPrefab;
    private void Start()
    {
        type = ItemType.ETC;
        itemName = "힌트종이";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
    }
    public void Use(GameObject target)
    {
        Debug.Log("힌트종이 사용");
    }
}
