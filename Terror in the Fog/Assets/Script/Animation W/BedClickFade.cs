using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BedClickFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // 페이드용 이미지
    [SerializeField] private float fadeDuration = 1.0f; // 페이드 지속 시간
    [SerializeField] private float holdDuration = 2.0f; // 검은 화면 유지 시간
    [SerializeField] private TextMeshProUGUI dDayText;
    [SerializeField] private Transform spawnPos;

    public bool isFading = false;
    private int dDay = 1; // 디데이 초기값 (1일차부터 시작)

    public void BedAnimation()
    {
        if (isFading || fadeImage == null || dDayText == null) return;

        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        isFading = true;

        // 페이드 인
        yield return fadeImage.DOFade(1, fadeDuration).WaitForCompletion();

        // D-day 업데이트 및 표시
        dDayText.text = $"{dDay}일차";
        dDayText.gameObject.SetActive(true);
        dDayText.DOFade(1, fadeDuration).SetEase(Ease.InOutQuad).Play(); // 텍스트 페이드 인
        yield return new WaitForSeconds(holdDuration);
        dDayText.DOFade(0, fadeDuration).SetEase(Ease.InOutQuad).Play(); // 텍스트 페이드 아웃

        // 검은 화면 유지
        PlayerController player = FindObjectOfType<PlayerController>();
        player.transform.position = spawnPos.position;
        player.Theta += 180;
        yield return new WaitForSeconds(holdDuration);

        // 텍스트 숨기기
        dDayText.gameObject.SetActive(false);

        // 페이드 아웃
        yield return fadeImage.DOFade(0, fadeDuration).WaitForCompletion();

        isFading = false;

        // D-day 증가 (5일차까지 제한)
        dDay = Mathf.Min(dDay + 1, 5);
    }
}
