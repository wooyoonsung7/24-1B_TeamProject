using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IItem;
using DG.Tweening;
using static UnityEditor.PlayerSettings;
using Unity.VisualScripting;

public class Door : MonoBehaviour, IItem
{
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public int index { get; set; }
    public int getIndex { get; set; }
    public Sprite itemImage { get; set; }
    public GameObject itemPrefab { get; set; }
    public bool isCanUse { get; set; }

    public bool isOpen = false;
    private bool canOpen = true;
    private bool checkState = false;

    public bool isOpened = true; //잠긴문인지 확인
    public int doorIndex = 0;     //문아이디

    Vector3 doorRot;
    Vector3 doorPos;
    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask TargetMask;
    private bool isOneTime = true;
    private void Start()
    {
        type = ItemType.interacted;
        itemName = "Door";
        isCanUse = isOpened; //몬스터전용 불값
        index = doorIndex;
        doorRot = transform.rotation.eulerAngles;
        doorPos = transform.position + transform.right * 0.3f;

        StartCoroutine(CheckEnemy());
    }

    public void Use(GameObject target)
    {
        if (isCanUse)
        {
            StartCoroutine(CheckUse());
        }
        else
        {
            Debug.Log("문이 잠겼습니다");
        }
    }

    private IEnumerator CheckUse()
    {
        if (canOpen)
        {
            canOpen = false;

            checkState = isOpen;
            isOpen = !isOpen;
            if (isOpen != checkState)
            {
                StartCoroutine(OpenDoor());
                StartCoroutine(CloseDoor());
            }
        }
        yield return null;
    }
    private IEnumerator OpenDoor()
    {
        if (isOpen)
        {
            doorRot += new Vector3(0f, 90f, 0f);
            transform.DOLocalRotate(doorRot, 0.5f).OnComplete(()=>canOpen = true);
        }
        yield return null;
    }
    private IEnumerator CloseDoor()
    {
        if (!isOpen)
        {
            doorRot -= new Vector3(0f, 90f, 0f);
            transform.DOLocalRotate(doorRot, 0.5f).OnComplete(() => canOpen = true);
        }
        yield return null;
    }

    private IEnumerator CheckEnemy()
    {
        while (true)
        {
            Collider[] targets = Physics.OverlapSphere(doorPos, detectRadius, TargetMask);

            if (targets.Length > 0 && isOneTime)
            {
                isOneTime = false;
                if (!isOpen)
                {
                    Use(gameObject);
                    yield return new WaitForSeconds(1f);
                    Use(gameObject);
                    isOneTime = true;
                }
            }
            yield return null;
        }
    }
}
