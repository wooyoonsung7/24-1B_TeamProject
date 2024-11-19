using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private SoundData soundData;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Tests();
    }

    private void Tests()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SoundDetector.instance.G_level = 3;
            SoundDetector.instance.SoundPos.Add(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundDetector.instance.G_level = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundDetector.instance.G_level = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gameObject.layer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            gameObject.layer = 3;
        }
    }
}
