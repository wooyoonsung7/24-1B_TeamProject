using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxToken : MonoBehaviour
{
    public Transform token;         // 토큰 오브젝트
    public Transform tokenHole;     // 토큰이 나오는 위치
    public float animationDuration = 1f; // 애니메이션 지속 시간

    private bool isTokenReleased = false; // 토큰이 이미 나왔는지 확인

    private void OnMouseDown()
    {
        if (isTokenReleased) return; // 토큰이 이미 나오면 동작하지 않음

        ReleaseToken();
    }

    private void ReleaseToken()
    {
        isTokenReleased = true;
        token.gameObject.SetActive(true); // 토큰 활성화

        // 토큰이 올라오는 애니메이션 (회전 없음)
        token.DOMove(tokenHole.position, animationDuration)
             .SetEase(Ease.OutBack); // 부드러운 애니메이션
    }
}
