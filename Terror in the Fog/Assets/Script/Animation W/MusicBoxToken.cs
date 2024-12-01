using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxToken : MonoBehaviour
{
    public Transform token;         // 토큰 오브젝트
    public Transform tokenHole;     // 토큰이 나오는 위치
    public float animationDuration = 1f; // 애니메이션 지속 시간


    public void ReleaseToken()
    {
        token.gameObject.SetActive(true); // 토큰 활성화
        gameObject.layer = 0;
        // 토큰이 올라오는 애니메이션 (회전 없음)
        token.DOMove(tokenHole.position, animationDuration)
             .SetEase(Ease.OutBack); // 부드러운 애니메이션
    }
}
