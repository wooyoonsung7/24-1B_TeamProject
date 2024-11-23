using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloor : MonoBehaviour //윗층의 레벨3사운드를 듣고 집주인이 지나갈 때, 감지
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (SoundDetector.instance.SoundPos.Count > 0)
            {
                
            }
        }
    }
}
