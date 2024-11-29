using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Toy : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]private bool isUnLocked = false; 

    [SerializeField]private GameObject generatedItem;
    [SerializeField] private bool isKey;
    public bool isGen = false;

    //Vector3 itemPos;
    Quaternion quaternion;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "오르골";
        index = 2;
        isCanUse = isUnLocked;
        if (!isKey) StartCoroutine("CheckUse");


        Vector3 itemRot = Vector3.zero;
        itemRot.x -= 90f;
        quaternion = Quaternion.Euler(itemRot);
    }

    private IEnumerator CheckUse()
    {
        while (true)
        {
            if (isCanUse)
            {
                isCanUse = false;
                SoundDetector.instance.G_level = 3;
                SoundDetector.instance.SoundPos.Add(transform.position); //레벨3사운드발생
                SoundManager.instance.PlaySound("Toy");
                Instantiate(generatedItem, transform.position - transform.right * 0.3f, quaternion);
                StopCoroutine("CheckUse");
            }

            yield return null;
        }
    }
    public void Use(GameObject target)
    {
        if (isCanUse && isKey)
        {
            isCanUse = false;
            SoundDetector.instance.G_level = 3;
            SoundDetector.instance.SoundPos.Add(transform.position); //레벨3사운드발생
            SoundManager.instance.PlaySound("Toy");
            StartCoroutine(GenKey(transform.position - transform.right * 0.1f, quaternion));
        }
    }

    private IEnumerator GenKey(Vector3 itemPos, Quaternion quaternion)
    {
        Debug.Log("기다린다");
        yield return new WaitUntil(() => isGen);
        Debug.Log("된다");
        Instantiate(generatedItem, itemPos, quaternion);
        StopCoroutine("CheckUse");
    }
}
