using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using static UnityEngine.GraphicsBuffer;

public class BookCase : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    public Transform[] slotPositions; // 여러 슬롯 위치
    [SerializeField] private GameObject[] generatedItem;
    
    private bool OneTime = true;
    private int count = 0;

    [SerializeField]
    private GameObject generatedItem_2;
    void Start()
    {
        type = ItemType.interacted;
        itemName = "책장";
        isCanUse = false;
        index = 5;

        StartCoroutine("CheckPuzz"); //성능의 우려로 인해서 코루틴으로 변경
    }

    public void Use(GameObject target)
    {
        if (isCanUse && OneTime)
        {
            StartCoroutine(UseItem(target));
            index++;
            if (count >= 3)
            {
                gameObject.layer = 0;
                OneTime = false;
            }
        }
    }

    IEnumerator UseItem(GameObject target)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject temp = Instantiate(generatedItem[count], target.transform.position, Quaternion.identity);
        generatedItem[count].GetComponent<BookController>().PlaceBook(slotPositions[count]);
        temp.gameObject.layer = 0;
        count++;
    }
    
    IEnumerator CheckPuzz()
    {
        while (true)
        {
            //index = 5; Debug.Log(index);
            //index = 6; Debug.Log(index);
            //index = 7; Debug.Log(index);

            if (count >= 3)
            {
                Debug.Log("퍼즐완");
                Vector3 itemPos = transform.position - transform.forward * 0.35f;
                itemPos.y -= (transform.localScale.y / 2 - generatedItem_2.transform.localScale.y / 2);

                Vector3 itemRot = Vector3.zero;
                itemRot.y += 55f;
                Quaternion quaternion = Quaternion.Euler(itemRot);

                Instantiate(generatedItem_2, itemPos, quaternion);
                StopCoroutine("CheckPuzz");
            }

            yield return null;
        }
    }
}
