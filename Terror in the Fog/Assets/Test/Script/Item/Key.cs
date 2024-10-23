using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class Key : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private Sprite _itemImage;
    [SerializeField]
    private GameObject _itemPrefab;
    private void Start()
    {
        type = ItemType.Used;
        itemName = "¿­¼è";
        itemImage = _itemImage;
        itemPrefab = _itemPrefab;
    }
    public void Use(GameObject target)
    {
        Debug.Log("¿­¼è »ç¿ë");
    }
}
