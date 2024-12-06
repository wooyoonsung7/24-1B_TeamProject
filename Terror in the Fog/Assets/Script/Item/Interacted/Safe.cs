using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Safe : MonoBehaviour
{
    private int selectCount = 0;
    private int count = 0;
    private int order = 0;
    private SafeButton[] safeButtons;
    [SerializeField]private GameObject item;

    public bool isUnLocked = false;
    private void Awake()
    {
        safeButtons = GetComponentsInChildren<SafeButton>();
    }
    void Start()
    {
        StartCoroutine("CheckPuzz"); //성능의 우려로 인해서 코루틴으로 변경
        if (item != null) item.layer = 0;

    }
    IEnumerator CheckPuzz()
    {
        while (true)
        {
            for (int i = 0; i < safeButtons.Length; i++)
            {
                //Debug.Log(i + "번째" + safeButtons[i].selected + "이다");
                if (selectCount >= 4)
                {
                    if (count >= 4)
                    {
                        SoundManager.instance.PlaySound("OpenSafe");
                        transform.DORotate(new Vector3(0, 60f, 0), 1f, RotateMode.LocalAxisAdd);

                        safeButtons[i].gameObject.layer = 0;
                        item.gameObject.layer = 6;
                        isUnLocked = true; //금고열림 확인용 불값
                        StopCoroutine("CheckPuzz");
                    }
                    else
                    {
                        StartCoroutine(Clear());
                        //틀렸을 때, 사운드 추가
                    }
                }
                else
                {
                    if (safeButtons[i].selected && safeButtons[i].isCanUse)
                    {
                        SoundManager.instance.PlaySound("SafeButton_" + selectCount);
                        selectCount++;
                        safeButtons[i].isCanUse = false;

                        if (safeButtons[i].isCollect && safeButtons[i].ListIndex == order)
                        {
                            order++;
                            count++;
                        }
                    }
                }
            }
            yield return null;
        }
    }

    private IEnumerator Clear()
    {
        for (int i = 0; i < safeButtons.Length; i++)
        {
            if (safeButtons[i].selected)
            {
                safeButtons[i].ResetValue();
            }
            safeButtons[i].isCanUse = true;
            safeButtons[i].selected = false;
            safeButtons[i].ResetValue();
        }
        yield return null;
        selectCount = 0;
        count = 0;
        order = 0;
        yield return null;
    }
}
