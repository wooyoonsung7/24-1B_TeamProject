using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject item;
    public bool isOn = true;
    private void Start()
    {
        if(isOn)StartCoroutine(CheckItem());
    }

    private IEnumerator CheckItem()
    {
        while (true)
        {
            if (item == null)
            {
                SoundDetector.instance.G_level = 3;
                SoundDetector.instance.SoundPos.Add(transform.position); //레벨3사운드발생
                SoundManager.instance.PlaySound("Emergency");
                break;
            }

            yield return null;
        }
    }
}
