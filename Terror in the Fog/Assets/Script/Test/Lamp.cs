using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private Light lampLight;
    public float activationRange = 5f; // 플레이어가 범위 안에 있을 때 라이트가 켜지는 범위

    void Start()
    {
        lampLight = GetComponent<Light>();
        lampLight.enabled = false; // 시작할 때 라이트를 끔
    }

    void Update()
    {
        // 플레이어와 Lamp 오브젝트 간의 거리 측정
        GameObject player = GameObject.FindWithTag("player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            // 범위 내에 있을 경우 라이트 켜기
            if (distance <= activationRange)
            {
                lampLight.enabled = true;
            }
            else
            {
                lampLight.enabled = false;
            }
        }
    }
}
