using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BedClickFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // 페이드용 이미지
    [SerializeField] private float fadeDuration = 1.0f; // 페이드 지속 시간
    [SerializeField] private float holdDuration = 2.0f; // 검은 화면 유지 시간
    [SerializeField] private Transform spawnPos;

    public bool isFading = false;

    public void BedAnimation()
    {
        if (isFading || fadeImage == null) return;

        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        isFading = true;

        // 페이드 인
        yield return fadeImage.DOFade(1, fadeDuration).WaitForCompletion();

        // 검은 화면 유지
        PlayerController player = FindObjectOfType<PlayerController>();
        player.transform.position = spawnPos.position;
        player.Theta += 180;
        yield return new WaitForSeconds(holdDuration);

        // 페이드 아웃
        yield return fadeImage.DOFade(0, fadeDuration).WaitForCompletion();

        isFading = false;
    }
}
