using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfController : MonoBehaviour
{
    public Transform[] slotPositions; // 여러 슬롯 위치
    public BookController[] books; // 여러 책

    private int currentSlotIndex = 0; // 현재 사용할 슬롯 인덱스

    void OnMouseDown()
    {
        if (currentSlotIndex < books.Length)
        {
            // 현재 책을 다음 슬롯에 배치
            //books[currentSlotIndex].PlaceBook(slotPositions[currentSlotIndex]);
            currentSlotIndex++;
        }
    }
}
