using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    private SoundData soundData;
    void Start()
    {
        soundData = GetComponent<SoundData>();
        Debug.Log(soundData.soundLevel);

        SoundManager.instance.PlaySound("Run");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

    }
}
