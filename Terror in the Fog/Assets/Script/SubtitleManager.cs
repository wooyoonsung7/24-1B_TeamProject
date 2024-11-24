using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText; 
    public float subtitleDuration = 3f;

    private void Start()
    {
        HideSubtitle();
    }

    
    public void ShowSubtitle(string message)
    {
        subtitleText.text = message;
        subtitleText.gameObject.SetActive(true);
        Invoke(nameof(HideSubtitle), subtitleDuration);
    }

    
    private void HideSubtitle()
    {
        subtitleText.gameObject.SetActive(false);
    }
}
