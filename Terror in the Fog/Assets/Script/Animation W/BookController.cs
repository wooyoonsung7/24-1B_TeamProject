using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookController : MonoBehaviour
{
    public bool isPlaced = false; // 책이 배치되었는지 여부 확인
    public Transform setPlace;

    /*
    // 책을 지정된 위치로 이동시키는 함수
    public void PlaceBook(Transform targetSlot)
    {
        if (!isPlaced)
        {
            Debug.Log("들어간다");
            isPlaced = true;
            // 책을 이동하고 로컬 회전 적용
            transform.DOMove(targetSlot.position, 0.5f).SetEase(Ease.OutQuad);
            transform.DOLocalRotate(new Vector3(-90, 90, 0), 0.5f).SetEase(Ease.OutQuad); // Y축 기준 90도 회전
        }

    }*/
    private void Start()
    {
        StartCoroutine(PlaceBook());
    }
    private IEnumerator PlaceBook()
    {
        while (true)
        {
            if (isPlaced)
            {
                Debug.Log("된다된다");
                transform.DOMove(setPlace.position, 0.5f).SetEase(Ease.OutQuad);
                transform.DOLocalRotate(new Vector3(-90, 90, 0), 0.5f).SetEase(Ease.OutQuad); // Y축 기준 90도 회전
                isPlaced = false;
            }
            yield return null;
        }
    }

}
