using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText; 
    
    public void ShowSubtitle(int day)
    {
        subtitleText.text = "Day " + day;
        StartCoroutine(FadeAnimation());
        //subtitleText.gameObject.SetActive(true);
        //Invoke(nameof(HideSubtitle), subtitleDuration);
    }

    private IEnumerator FadeAnimation()
    {
        Debug.Log("작동된다");
        yield return subtitleText.DOFade(1, 2).WaitForCompletion();


        yield return new WaitForSeconds(1.5f);

        Debug.Log("작동된다2");
        yield return subtitleText.DOFade(0, 2).WaitForCompletion();
    }
}
