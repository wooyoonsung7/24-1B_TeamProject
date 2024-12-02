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
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "빈책장";
        isCanUse = false;
        index = slotIndex;

        itemRot = Vector3.zero;
        itemRot.x -= 75.4f;
        quaternion = Quaternion.Euler(itemRot);
    }
    public void Use(GameObject target)
    {
        if (isCanUse && !isUsing)
        {
            SoundManager.instance.PlaySound("SetToken");
            isCanUse = false;
            isUsing = true;
            Debug.Log("작동한다");
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
                GameObject temp = Instantiate(tokenPrf[i], transform.position + transform.forward * 0.05f, quaternion);
                Debug.Log("생성된다");
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
