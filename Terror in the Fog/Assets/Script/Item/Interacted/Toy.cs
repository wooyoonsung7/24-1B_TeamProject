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
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    [SerializeField]private bool isUnLocked = false; 

    [SerializeField]private GameObject generatedItem;
    [SerializeField] private bool isKey;
    public bool isGen = false;

    int targetMask = (1 << 8);

    private void Start()
    {
        type = ItemType.interacted;
        itemName = "오르골";
        index = 6;
        isCanUse = isUnLocked;
    }
    public void Use(GameObject target)
    {
        Vector3 itemPos = transform.position + transform.forward * 0.3f;
        itemPos.y -= (transform.localScale.y - generatedItem.transform.localScale.y / 2);

        Vector3 itemRot = Vector3.zero;
        itemRot.y += 55f;
        Quaternion quaternion = Quaternion.Euler(itemRot);
        if (isCanUse)
        {
            isCanUse = false;
            SoundDetector.instance.G_level = 3;
            SoundDetector.instance.SoundPos.Add(transform.position); //레벨3사운드발생
            SoundManager.instance.PlaySound("Toy");

            if (isKey)
            {
                StartCoroutine(GenKey(itemPos, quaternion));
            }
            else
            {
                Instantiate(generatedItem, itemPos, quaternion);
            }
        }
        else
        {
            Debug.Log("오르골을 작동시킬 수 없다");
        }
    }

    private IEnumerator GenKey(Vector3 itemPos, Quaternion quaternion)
    {
        Debug.Log("기다린다");
        yield return new WaitUntil(() => SoundDetector.instance.isLevel3End);
        Debug.Log("된다");
        Instantiate(generatedItem, itemPos, quaternion);
    }
}
