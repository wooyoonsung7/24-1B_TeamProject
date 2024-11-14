using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class Toy : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private GameObject generatedItem;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "오르골";
        index = 6;
        isCanUse = false;
        itemPrefab = generatedItem;
    }
    public void Use(GameObject target)
    {
        Vector3 itemPos = transform.position + transform.forward * 0.3f;
        itemPos.y -= (transform.localScale.y - itemPrefab.transform.localScale.y / 2);

        Vector3 itemRot = Vector3.zero;
        itemRot.y += 55f;
        Quaternion quaternion = Quaternion.Euler(itemRot);
        if (isCanUse)
        {
            Instantiate(itemPrefab, itemPos, quaternion);
        }
        else
        {
            Debug.Log("오르골을 작동시킬 수 없다");
        }
    }

}
