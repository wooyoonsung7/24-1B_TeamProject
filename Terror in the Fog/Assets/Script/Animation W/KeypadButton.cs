using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class KeypadButton : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 pressedPosition;
    private static int buttonPressCount = 0;
    private const int maxPressCount = 4;
    private bool isPressed = false; // 버튼이 눌렸는지 여부

    public GameObject door; // 문 오브젝트 연결

    private void Start()
    {
        originalPosition = transform.localPosition;
        pressedPosition = originalPosition + new Vector3(-0.001f, 0, 0); // 눌린 위치 계산
    }

    private void OnMouseDown()
    {
        if (!isPressed) // 버튼이 눌리지 않은 경우에만 동작
        {
            AnimateButtonPress();
        }
    }

    private void AnimateButtonPress()
    {
        isPressed = true;
        buttonPressCount++;

        // 버튼을 눌린 상태로 유지
        transform.DOLocalMove(pressedPosition, 0.2f);

        if (buttonPressCount >= maxPressCount)
        {
            OpenDoor(); // 문 열기
            RestoreAllButtons(); // 버튼 초기화
        }
    }

    private void OpenDoor()
    {
        if (door != null)
        {
            // 문을 Y축으로 -60도 회전
            door.transform.DORotate(new Vector3(0, 60f, 0), 1f, RotateMode.LocalAxisAdd);
        }
    }

    private void RestoreAllButtons()
    {
        // 모든 버튼 초기화
        KeypadButton[] allButtons = FindObjectsOfType<KeypadButton>();

        foreach (KeypadButton button in allButtons)
        {
            button.RestorePosition();
        }

        buttonPressCount = 0;
    }

    public void RestorePosition()
    {
        isPressed = false;
        transform.DOLocalMove(originalPosition, 0.2f); // 원래 위치로 복귀
    }
}