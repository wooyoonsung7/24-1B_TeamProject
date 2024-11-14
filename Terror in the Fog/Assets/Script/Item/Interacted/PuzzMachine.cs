using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzMachine : MonoBehaviour
{
    [SerializeField]
    private GameObject[] voidSlot = new GameObject[5];
    [SerializeField]
    private GameObject drawer;

    private int stack = 0;
    void Start()
    {
        StartCoroutine("CheckPuzz"); //성능의 우려로 인해서 코루틴으로 변경
    }

    IEnumerator CheckPuzz()
    {
        while (true)
        {
            foreach (GameObject obj in voidSlot)
            {
                IItem item = obj.GetComponent<IItem>();
                if (item.isCanUse)
                {
                    Debug.Log(stack);
                    stack++;
                }
            }
            if (stack >= 5)
            {
                Debug.Log("퍼즐완");
                Vector3 moveToPos = drawer.transform.position + transform.forward * 0.5f;
                moveToPos.y = drawer.transform.position.y;
                drawer.transform.DOMove(moveToPos, 3f);
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
