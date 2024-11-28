using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenBox : MonoBehaviour
{
    public Transform lid; // 상자 뚜껑 (뚜껑의 Transform)
    public float openAngle = 90f; // 열리는 각도
    public float duration = 1f; // 애니메이션 지속 시간

    private bool isOpen = false; // 상자가 열렸는지 여부 확인

    public void ToggleBox()
    {
        lid.DOLocalRotate(new Vector3(-openAngle, 0, 0), duration); // 위로 열기
        /*
        if (isOpen)
        {
            // 열기 애니메이션
            lid.DOLocalRotate(new Vector3(-openAngle, 0, 0), duration); // 위로 열기
            isOpen = false;
        }*/
    }
}
