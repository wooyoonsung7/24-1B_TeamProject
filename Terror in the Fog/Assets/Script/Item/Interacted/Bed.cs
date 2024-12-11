using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static IItem;

public class Bed : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    private bool reUse = true;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "침대";
        isCanUse = false;
    }

    public void Use(GameObject target)
    {
        if (reUse)
        {
            reUse = false;
            StartCoroutine(CheckCanUse());
        }
    }

    private IEnumerator CheckCanUse()
    {
        //Debug.Log(isCanUse);
        if (InsideInventory.Instance.CheckInventory())
        {
            isCanUse = true;
        }
        yield return null;

        if (isCanUse)
        {
            //Debug.Log("침대사용");
            isCanUse = false;
            StartCoroutine(UseBed());
            //자고 일어나는 애니메이션

            EventManager.instance.isCanOpen = true;
            if (GameManager.Days != 5) GameManager.Instance.PassDay();
        }
        yield return new WaitForSeconds(1.5f);
        reUse = true;

    }

    private IEnumerator UseBed()
    {
        SoundManager.instance.PlaySound("GoToSleep");
        BedClickFade bed = GetComponent<BedClickFade>();
        bed.BedAnimation();
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => !bed.isFading);
        SoundManager.instance.PlaySound("WakeUp");
    }

   
}
