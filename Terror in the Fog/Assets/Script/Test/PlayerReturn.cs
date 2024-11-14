using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReturn : MonoBehaviour
{
    // 플레이어가 되돌아갈 위치를 저장할 변수
    public Vector3[] returnPositions = new Vector3[4]; // 배열 크기를 4로 설정

    void Start()
    {
        // 기본 위치를 첫 번째 위치로 설정
        returnPositions[0] = new Vector3(-40, 2, 130); // 첫 번째 위치
        returnPositions[1] = new Vector3(160, 2, -180); // 두 번째 위치
        returnPositions[2] = new Vector3(-230, 2, 180); // 세 번째 위치
        returnPositions[3] = new Vector3(-40, 2, -470); // 네 번째 위치

        // 초기 위치 설정 (첫 번째 위치로 이동)
        transform.position = returnPositions[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        // 박스 콜라이더의 태그를 체크
        if (other.CompareTag("Boundary1")) // 첫 번째 콜라이더
        {
            transform.position = returnPositions[0]; // 첫 번째 위치로 이동
        }
        else if (other.CompareTag("Boundary2")) // 두 번째 콜라이더
        {
            transform.position = returnPositions[1]; // 두 번째 위치로 이동
        }
        else if (other.CompareTag("Boundary3")) // 세 번째 콜라이더
        {
            transform.position = returnPositions[2]; // 세 번째 위치로 이동
        }
        else if (other.CompareTag("Boundary4")) // 네 번째 콜라이더
        {
            transform.position = returnPositions[3]; // 네 번째 위치로 이동
        }
    }
}
