using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ButtonUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI text;  //버튼을 눌렀을 때, 텍스트 색변경용

    public void ChangeColor()
    {
        if (text == null)
        {
            Debug.Log("없다");
            return;
        }
        text.color = Color.black;
    }

}
