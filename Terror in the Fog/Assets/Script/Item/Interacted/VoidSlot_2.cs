using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class VoidSlot_2 : MonoBehaviour, IItem
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
    private GameObject[] itemPrf = new GameObject[5];

    private Valuable saveValuable;

    Vector3 itemRot;
    Quaternion quaternion;
    bool isUsing = false;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "√•ªÛ<±Õ¡ﬂ«∞>";
        isCanUse = false;
        index = slotIndex;

        itemRot = Vector3.zero;
        itemRot.x -= 90f;
        itemRot.y += 180f;
        quaternion = Quaternion.Euler(itemRot);

        if (SaveData.instance.data.ContainsKey("±Õ¡ﬂ«∞ΩΩ∑‘" + index))
        {
            GameObject temp = Instantiate(itemPrf[SaveData.instance.data["±Õ¡ﬂ«∞ΩΩ∑‘" + index]], transform.position - transform.forward * 0.03f, quaternion);
            saveValuable = temp.GetComponent<Valuable>();
            StartCoroutine(CheckSave());
        }
        else
        {
            StartCoroutine(CheckSave());
        }
    }
    public void Use(GameObject target)
    {
        if (isCanUse && !isUsing)
        {
            isCanUse = false;
            isUsing = true;
            gameObject.layer = 0;
            StartCoroutine(SetValuable());
        }
    }

    private IEnumerator SetValuable()
    {

        for (int i = 0; i < itemPrf.Length; i++)
        {
            Valuable item = itemPrf[i].GetComponent<Valuable>();
            if (getIndex == item.Index)
            {
                SoundManager.instance.PlaySound("SetItem");
                GameObject temp = Instantiate(itemPrf[i], transform.position - transform.forward * 0.03f, quaternion);
                saveValuable = temp.GetComponent<Valuable>();
                SaveData.instance.data.Add("±Õ¡ﬂ«∞ΩΩ∑‘" + index, i);
            }
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = 6;
        isUsing = false;
    }

    private IEnumerator CheckSave()
    {
        while (true)
        {
            if (SaveData.instance.data.ContainsKey("±Õ¡ﬂ«∞ΩΩ∑‘" + index))
            {
                if (saveValuable == null)
                {
                    SaveData.instance.data.Remove("±Õ¡ﬂ«∞ΩΩ∑‘" + index);
                }
            }
            yield return null;
        }
    }
}
