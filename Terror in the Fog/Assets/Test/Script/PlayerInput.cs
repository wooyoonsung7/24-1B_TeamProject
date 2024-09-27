using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName_01 = "Vertical";
    public string moveAxisName_02 = "Horizontal";
    public string rotateAxisName_01 = "MouseX";
    public string rotatieAxisName_02 = "MouseY";

    public float mouseSenesitivity { get; private set; }  //마우스 감도



    // Update is called once per frame
    void Update()
    {
        
    }
}
