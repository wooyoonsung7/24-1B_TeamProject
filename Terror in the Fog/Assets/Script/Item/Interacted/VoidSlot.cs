using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class VoidSlot : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]
    private int slotIndex = 0;

    [SerializeField]
    private GameObject[] tokenPrf = new GameObject[5];

    Vector3 itemRot;
    Quaternion quaternion;
    bool isUse = true;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "∫Û√•¿Â";
        isCanUse = false;
        index = slotIndex;

        itemRot = Vector3.zero;
        itemRot.x += 72f;
        Quaternion quaternion = Quaternion.Euler(itemRot);
        StartCoroutine(SetToken());
    }
    public void Use(GameObject target)
    {
        isUse = true;
        CheckCorrect();
    }

    private IEnumerator SetToken()
    {
        while(true)
        {
            if (isUse)
            {
                isUse = false;
                for (int i = 0; i < tokenPrf.Length; i++)
                {
                    Token token = tokenPrf[i].GetComponent<Token>();
                    if (getIndex == token.tokenIndex)
                    {
                        Instantiate(tokenPrf[i], transform.position + transform.forward * 0.05f, quaternion);
                    }
                }
            }
            yield return null;
        }
    }

    private void CheckCorrect()
    {
        if (getIndex == index)
        {
            isCanUse = true;
        }
        else
        {
            isCanUse = false;
        }
    }
}
