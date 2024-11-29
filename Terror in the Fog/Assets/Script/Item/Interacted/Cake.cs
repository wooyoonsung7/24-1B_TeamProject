using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class Cake : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    public bool isOpen = false;
    public int Index = 0;     //문아이디

    [SerializeField] private GameObject cake_1;
    [SerializeField] private GameObject cake_2;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Cake";
        isCanUse = false;
        index = Index;

        StartCoroutine(CheckUse());
    }

    public void Use(GameObject target) { }

    private IEnumerator CheckUse()
    {
        while (true)
        {
            if (isCanUse)
            {
                //케이크가 짤리는 사운드
                yield return new WaitForSeconds(0.1f);
                cake_1.SetActive(false);
                cake_2.SetActive(true);
                gameObject.layer = 0;
                isCanUse = false;
            }
            yield return null;
        }
    }
}
