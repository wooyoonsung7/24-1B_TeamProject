using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject item;
    public bool isOn = true;
    private void Start()
    {
        StartCoroutine(CheckItem());
    }

    private IEnumerator CheckItem()
    {
        while (true)
        {
            if (isOn)
            {
                if (item == null)
                {
                    SoundDetector.instance.G_level = 3;
                    SoundDetector.instance.SoundPos.Add(transform.position); //레벨3사운드발생
                    SoundManager.instance.PlaySound("Emergency");
                    break;
                }
            }
            else
            {
                if (item == null)
                {
                    Debug.Log("귀걸이가 없어졌다");
                    SoundManager.instance.PlaySound("Emergency");
                    EventManager.instance.StartCoroutine("Day5Event");
                    break;
                }
            }

            yield return null;
        }
    }
}
