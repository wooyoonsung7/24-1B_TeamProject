using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class VoidCase : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private int caseIndex = 0;
    [SerializeField]
    private GameObject generatedItem;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "∫Û√•¿Â";
        isCanUse = false;
        index = caseIndex;
    }
    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            Instantiate(generatedItem, transform.position - transform.forward * 0.15f, Quaternion.identity);
            isCanUse = false;
        }
    }
}
