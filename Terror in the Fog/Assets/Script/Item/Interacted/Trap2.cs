using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    public bool isCanUse = true;
    [SerializeField] private int index = 0;
    private float currentSpeed = 0f;
    private Enemy enemy;

    private static bool IsCanUse = true;
    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isCanUse && IsCanUse)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SoundDetector.instance.G_level = 3;
                SoundDetector.instance.SoundPos.Add(transform.position); //레벨3사운드발생
                SoundManager.instance.PlaySound("Emergency_Trap" + index);
                currentSpeed = enemy.navMeshAgent.speed;
                enemy.navMeshAgent.speed *= 1.5f;
                isCanUse = false;
                IsCanUse = false;
            }
        }
        else if(!isCanUse)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                SoundManager.instance.PauseSound("Emergency_Trap" + index);
                enemy.navMeshAgent.speed = currentSpeed;
                SoundDetector.instance.ResetHurry();
                isCanUse = true;
                IsCanUse = true;
            }
        }
    }
}
