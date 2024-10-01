using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;

public class SpeedPack : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; } // 아이템을 사용가능한지를 판단

    

    [SerializeField]
    private Sprite itemImage_2;
    [SerializeField]
    private GameObject itemPrefab_2;

    private float timer = 0f;
    private float fullTime = 5f;

    private float walkSpeed;
    private float runSpeed;
    private float crouchSpeed;
    private void Start()
    {
        type = ItemType.Consumed;
        itemName = "진통제";
        itemImage = itemImage_2;
        itemPrefab = itemPrefab_2;
        isCanUse = false;
    }
    public void Use(GameObject target)
    {
        Debug.Log("진통제 사용");
        PlayerController p_Controller = target.GetComponent<PlayerController>();

        crouchSpeed = p_Controller.crouchSpeed;
        runSpeed = p_Controller.runSpeed;
        walkSpeed = p_Controller.walkSpeed;

        p_Controller.crouchSpeed *= 2;
        p_Controller.runSpeed *= 2;
        p_Controller.walkSpeed *= 2;
        timer += Time.deltaTime;

        if (timer >= fullTime)
        {
            p_Controller.crouchSpeed = crouchSpeed;
            p_Controller.runSpeed = runSpeed;
            p_Controller.walkSpeed = walkSpeed;

            Destroy(gameObject);
        }

    }
}
