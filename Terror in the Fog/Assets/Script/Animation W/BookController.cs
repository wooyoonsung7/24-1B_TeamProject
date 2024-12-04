using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookController : MonoBehaviour
{
    public bool isPlaced = false; // 책이 배치되었는지 여부 확인
    public Transform setPlace;

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
                //Debug.Log("된다된다");
                transform.DOMove(setPlace.position, 0.5f).SetEase(Ease.OutQuad);
                transform.DOLocalRotate(new Vector3(-90, 90, 0), 0.5f).SetEase(Ease.OutQuad); // Y축 기준 90도 회전
                isPlaced = false;
            }
            yield return null;
        }
    }

}
