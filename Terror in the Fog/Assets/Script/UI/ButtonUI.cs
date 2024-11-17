using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]private TextMeshProUGUI text;  //버튼을 눌렀을 때, 텍스트 색변경용

    [SerializeField]private Button button;  //버튼인터랙션 감지해서 하위오브젝트 색변경

    [SerializeField] bool isUseHighLight = false;

    private bool isHighlighted = false;
    private Color defaultColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHighlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHighlighted = false;
    }

    private void Update()
    {
        if (isUseHighLight)
        {
            //ChangeColorSetting();
        }
    }
    public void ChangeColor()
    {
        if (text == null)
        {
            Debug.Log("없다");
            return;
        }
        text.color = Color.black;
    }
    /*
    public void ChangeColorSetting()
    {
        //defaultColor = text.color;
        if (isHighlighted)
        {
            Debug.Log("된다");
            text.color = Color.black;
        }
        else
        {
            text.color = new Color(191, 191, 191, 255);
        }
    }*/
}
