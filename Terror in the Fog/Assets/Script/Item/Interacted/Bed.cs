using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static IItem;

public class Bed : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]private Door door;

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Bed";
        isCanUse = true;
    }

    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            Debug.Log("침대사용");
            isCanUse = false;
            StartCoroutine(UseBed());
            //자고 일어나는 애니메이션
            
            door.isCanUse = true;
            GameManager.Instance.PassDay();
        }
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
