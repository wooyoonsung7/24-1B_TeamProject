using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookController : MonoBehaviour
{
    public bool isPlaced = false; // 책이 배치되었는지 여부 확인

    // 책을 지정된 위치로 이동시키는 함수
    public void PlaceBook(Transform targetSlot)
    {
        if (!isPlaced)
        {
            isPlaced = true;
            // 책을 이동하고 로컬 회전 적용
            transform.DOMove(targetSlot.position, 0.5f).SetEase(Ease.OutQuad);
            transform.DOLocalRotate(new Vector3(-90, 90, 0), 0.5f).SetEase(Ease.OutQuad); // Y축 기준 90도 회전
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
