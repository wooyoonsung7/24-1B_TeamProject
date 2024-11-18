using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCase : MonoBehaviour
{
    public GameObject[] voidCase = new GameObject[3];

    private int stack = 0;
    [SerializeField]
    private GameObject generatedItem;
    void Start()
    {
        StartCoroutine("CheckPuzz"); //성능의 우려로 인해서 코루틴으로 변경
    }

    IEnumerator CheckPuzz()
    {
        while (true)
        {
            foreach (GameObject obj in voidCase)
            {
                VoidCase item = obj.GetComponent<VoidCase>();
                if (item.isCanUse)
                {
                    Debug.Log(stack);
                    stack++;
                }
            }
            if (stack >= 3)
            {
                Debug.Log("퍼즐완");
                Vector3 itemPos = transform.position - transform.forward * 0.35f;
                itemPos.y -= (transform.localScale.y / 2 - generatedItem.transform.localScale.y / 2);

                Vector3 itemRot = Vector3.zero;
                itemRot.y += 55f;
                Quaternion quaternion = Quaternion.Euler(itemRot);

                Instantiate(generatedItem, itemPos, quaternion);
                StopCoroutine("CheckPuzz");
            }
            else
            {
                stack = 0;
            }

            yield return null;
        }
    }
}
