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
                    StartCoroutine(Day5Event());
                    break;
                }
            }

            yield return null;
        }
    }
    IEnumerator Day5Event()
    {
        while (true)
        {
            Debug.Log("이벤트진행중");
            Enemy enemy = FindObjectOfType<Enemy>();
            enemy.stopResearch = true;
            enemy.gameObject.GetComponent<SoundDetector>().isDetectOFF = true;
            enemy.navMeshAgent.SetDestination(new Vector3(11.15f, 5.57f, -41.88f));
            enemy.transform.rotation = Quaternion.Euler(0f, 90, 0);
            yield return new WaitForSeconds(1f);
            if (enemy.transform.position == new Vector3(11.15f, 5.57f, -41.88f)) break;

            yield return null;
        }
    }
}
