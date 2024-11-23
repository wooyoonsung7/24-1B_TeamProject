using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    Vector3 targetRot;
    float currentTime;
    int count = 0;
    void Start()
    {
        targetRot = transform.rotation.eulerAngles;
        targetRot.y += 90;
    }

    // Update is called once per frame
    void Update()
    {
        Tests();
    }

    private void Tests()
    {
        //Vector3.Lerp(transform.rotation.eulerAngles, targetRot, currentTime * 10)
        currentTime += Time.deltaTime;
        if (currentTime <= 1.3)
        {
            transform.Rotate(0, 1, 0);
        }
        else if(currentTime <= 3.4)
        {
            transform.Rotate(0, -1, 0);
        }
    }
}
