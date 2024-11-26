using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite changedImage;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private bool isPauseMenu = false;

    [SerializeField] private Sprite currentSprite;
    [SerializeField] private bool isFirst = false;
    public static bool isEnd = false;
    public static bool isEnd_2 = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(settingUI != null) settingUI.SetActive(false);
        }
    }
    private void OnEnable()
    {
        isEnd = false;
        if (isPauseMenu)
        {
            image.color = new Color32(84, 84, 84, 255);
            image.sprite = currentSprite;
        }
    }

    public void ChangeColor()
    {
        StartCoroutine(ChangColor());
    }

    public void Off()
    {
        StartCoroutine(OffHighlight());
    }

    private IEnumerator OffHighlight()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        if (settingUI != null) settingUI.SetActive(true);
        yield return null;

        if (!isPauseMenu)
        {
            image.color = new Color32(100, 100, 100, 255);
            image.sprite = null;
        }
        else
        {
            image.color = new Color32(84, 84, 84, 255);
            image.sprite = currentSprite;
        }
    }

    private IEnumerator ChangColor()
    {
        image.color = Color.white;
        currentSprite = image.sprite;
        image.sprite = changedImage;
        yield return new WaitForSecondsRealtime(0.15f);
        if (settingUI == null && isFirst) isEnd = true;
        if (settingUI == null && !isFirst) isEnd_2 = true;
    }
}
