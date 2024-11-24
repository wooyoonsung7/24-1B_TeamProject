using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    public bool isCanUse = true;
    [SerializeField] private int index = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (isCanUse)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SoundDetector.instance.G_level = 3;
                SoundDetector.instance.SoundPos.Add(transform.position); //레벨3사운드발생
                SoundManager.instance.PlaySound("Emergency_Trap" + index);
                isCanUse = false;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                SoundManager.instance.PauseSound("Emergency_Trap" + index);
            }
        }
    }
}
