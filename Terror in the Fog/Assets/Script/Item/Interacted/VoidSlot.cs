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
    public bool isCanUse { get; set; }

    [SerializeField]
    private int slotIndex = 0;

    [SerializeField]
    private GameObject[] tokenPrf = new GameObject[5];

    Vector3 itemRot;
    Quaternion quaternion;
    bool isUsing = false;
    public bool isSettingAtHome = false;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "∫Û√•¿Â";
        isCanUse = false;
        index = slotIndex;

        if (!isSettingAtHome)
        {
            itemRot = Vector3.zero;
            itemRot.x -= 75.4f;
            quaternion = Quaternion.Euler(itemRot);
        }
        else
        {
            itemRot = Vector3.zero;
            itemRot.x -= 90f;
            itemRot.y -= 180f;
            quaternion = Quaternion.Euler(itemRot);
        }
    }
    public void Use(GameObject target)
    {
        if (isCanUse && !isUsing)
        {
            SoundManager.instance.PlaySound("SetToken");
            isCanUse = false;
            isUsing = true;
            StartCoroutine(SetToken());
        }
        CheckCorrect();
    }

    private IEnumerator SetToken()
    {
        for (int i = 0; i < tokenPrf.Length; i++)
        {
            Token token = tokenPrf[i].GetComponent<Token>();
            if (getIndex == token.tokenIndex)
            {
                if (!isSettingAtHome)
                {
                    Instantiate(tokenPrf[i], transform.position + transform.forward * 0.05f, quaternion);
                }
                else
                {
                    Instantiate(tokenPrf[i], transform.position - transform.forward * 0.03f, quaternion);
                }
            }
        }
        yield return new WaitForSeconds(0.2f);
        isUsing = false;
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
