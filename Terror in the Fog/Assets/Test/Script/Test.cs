using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    //Sequence sequence2;
    bool iscan = false;
    void Start()
    {
        /*
        sequence2 = DOTween.Sequence();

        sequence2.Prepend(transform.DOLocalRotate(new Vector3(0, -180, 0), 1.5f))
         .Append(transform.DOLocalRotate(new Vector3(0, 0, 0), 1.5f))
         .SetAutoKill(false)
         .Pause();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Quaternion quaternion = Quaternion.identity;
            iscan = !iscan;

            if (iscan)
            {

            }

            //sequence2.Restart();
            Debug.Log(Mathf.Ceil(134.54f) / 100f);
        }
        //float targetpos_y;
        float currentTime = 0f;
        Vector3 startRot;
        Vector3 endRot;

        Vector3[] act = new Vector3[2];
        Vector3 act_2;

        int transCount = 0;
        /*int[] transCount= new int[3];

        for (int i = 0; i < transCount.Length; i++)
        {
            transCount[i] =  i;
        }*/

        act[0] = new Vector3(0.0f, 90.0f, 0.0f);
        act[1] = new Vector3(0.0f, -90.0f, 0.0f);

        startRot = transform.localRotation.eulerAngles;
        endRot = new Vector3(0f, 90f, 0f);

        currentTime += Time.deltaTime;

        if (transform.localRotation.eulerAngles == endRot)
        {
            transCount++;
            endRot += act[transCount];
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, endRot, currentTime * 1.8f));
        }

        //Debug.Log(transform.localRotation.eulerAngles);
        //float startpos_y = transform.rotation.y;

        //targetpos_y = Mathf.Lerp(startpos_y, startpos_y + 90f, 1 * Time.deltaTime);
        //quaternion.eulerAngles.y = targetpos_y;

        //targetpos_y = Mathf.Lerp(0f, 90f, currentTime * 1/50f);
        //Debug.Log(targetpos_y);

        //gameObject.transform.localRotation = Quaternion.Euler(0.0f, targetpos_y, 0.0f);
    }
}
