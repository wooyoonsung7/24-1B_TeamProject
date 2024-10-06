using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using DG.Tweening;

public class Door : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    public float tweenDuration = 1f;
    public bool isOpen = false;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Door";
        isCanUse = false; //몬스터전용 불값
    }
    private void Update()
    {

    }
    public void Use(GameObject target)
    {
        Vector3 doorPos = transform.position;
        isOpen = !isOpen;
        isCanUse = true;
        if (isOpen)
        {
            doorPos += new Vector3(1.2f, 0f, 0f);
            Debug.Log("문 열기");
            transform.DOLocalMove(doorPos, tweenDuration).OnComplete(()=> isCanUse = false);
        }
        else
        {
            doorPos += new Vector3(-1.2f, 0.0f, 0.0f);
            Debug.Log("문 닫기");
            transform.DOLocalMove(doorPos, tweenDuration);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (!isOpen)
                {
                    Use(gameObject);
                    enemy.navMeshAgent.isStopped = true;
                    StartCoroutine(CloseDoor(enemy));
                }
            }
        }
    }
    IEnumerator CloseDoor(Enemy _enemy)
    {
        Enemy enemy = _enemy;
        if (!isCanUse)
        {
            yield return null;
            Use(gameObject);
            enemy.navMeshAgent.isStopped = false; //오루수정필요
        }
    }
}
