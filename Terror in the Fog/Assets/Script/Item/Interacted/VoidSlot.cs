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

    private Token saveToken;

    Vector3 itemRot;
    Quaternion quaternion;
    bool isUsing = false;
    public bool isSettingAtHome = false;
    private void Start()
    {
        type = ItemType.interacted;
        isCanUse = false;
        index = slotIndex;

        if (!isSettingAtHome)
        {
            itemName = "빈책장";
            itemRot = Vector3.zero;
            itemRot.x -= 75.4f;
            quaternion = Quaternion.Euler(itemRot);
        }
        else
        {
            itemName = "책상<토큰>";
            itemRot = Vector3.zero;
            itemRot.x -= 90f;
            itemRot.y -= 180f;
            quaternion = Quaternion.Euler(itemRot);

            if (SaveData.instance.data.ContainsKey("토큰슬롯" + index))
            {
                GameObject temp = Instantiate(tokenPrf[SaveData.instance.data["토큰슬롯" + index]], transform.position - transform.forward * 0.03f, quaternion);
                saveToken = temp.GetComponent<Token>();
                StartCoroutine(CheckSave());
            }
            else
            {
                StartCoroutine(CheckSave());
            }

            if (GameManager.Days == 5 && SaveData.instance.data.ContainsKey("토큰슬롯_5일차용" + index))
            {
                GameObject temp = Instantiate(tokenPrf[SaveData.instance.data["토큰슬롯_5일차용" + index]], transform.position - transform.forward * 0.03f, quaternion);
                saveToken = temp.GetComponent<Token>();
            }
        }
    }
    public void Use(GameObject target)
    {
        if (isCanUse && !isUsing)
        {
            SoundManager.instance.PlaySound("SetToken");
            isCanUse = false;
            isUsing = true;
            gameObject.layer = 0;
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
                    GameObject temp = Instantiate(tokenPrf[i], transform.position - transform.forward * 0.03f, quaternion);
                    saveToken = temp.GetComponent<Token>();
                    if (!SaveData.instance.data.ContainsKey("토큰슬롯" + index)) SaveData.instance.data.Add("토큰슬롯" + index, i);
                }
            }
        }
        yield return null;
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

    private IEnumerator CheckSave()
    {
        while (true)
        {
            if (SaveData.instance.data.ContainsKey("토큰슬롯" + index))
            {

                if (!SaveData.instance.data.ContainsKey("토큰슬롯_5일차용" + index))
                {
                    if (GameManager.Days == 5 || GameManager.Days == 4)
                    {
                        SaveData.instance.data.Add("토큰슬롯_5일차용" + index, SaveData.instance.data["토큰슬롯" + index]);
                    }
                }

                if (saveToken == null)
                {
                    gameObject.layer = 6;
                    isUsing = false;
                    SaveData.instance.data.Remove("토큰슬롯" + index);
                }
                else
                {
                    //Debug.Log("없어졌다");
                }
            }
            yield return null;
        }
    }
}
