using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    Sequence sequence2;
    void Start()
    {
        sequence2 = DOTween.Sequence();

        sequence2.Prepend(transform.DOLocalRotate(new Vector3(0, -180, 0), 1.5f))
         .Append(transform.DOLocalRotate(new Vector3(0, 0, 0), 1.5f))
         .SetAutoKill(false)
         .Pause();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //sequence2.Restart();
            Debug.Log(Mathf.Ceil(134.54f) / 100f);
        }
    }
}
