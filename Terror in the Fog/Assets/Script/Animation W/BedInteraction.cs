using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BedInteraction : MonoBehaviour
{
    public Transform player;                //플레이어 오브젝트
    public Transform bedPosition;           //침대 위 누울 위치
    public float moveDuration = 1.0f;       //이동 시간
    public float rotationDuration = 0.5f;   //회전 시간

    private bool isLyingDown = false;       //플레이어가 누워 있는 상태 체크

    public Vector3 lyingRotation = new Vector3(90f, 0f, 0f); // x축 회전으로 가로로 눕기 
    private void OnMouseDown()
    {
        if (isLyingDown)        //플레이어가 누워있지 않을 때만 실행
        {
            LieDownOnBed();
        }
        else
        {
            GetUpFromBed();
        }
    }
    void LieDownOnBed()
    {
        isLyingDown = true;

        // 기존 애니메이션 정리
        DOTween.Kill(player);

        // 이동 애니메이션
        player.DOMove(bedPosition.position, moveDuration).SetEase(Ease.InOutQuad);

        // 회전 애니메이션
        player.DORotate(lyingRotation, rotationDuration).SetEase(Ease.InOutQuad);

        // 플레이어 컨트롤 비활성화
        var controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;
    }
    void GetUpFromBed()
    {
        isLyingDown = false;

        // 원래 위치나 초기 위치로 복귀 (예시로 Vector3.zero 사용)
        player.DOMove(Vector3.zero, moveDuration).SetEase(Ease.InOutQuad);

        // 원래 회전 복귀
        player.DORotate(Vector3.zero, rotationDuration).SetEase(Ease.InOutQuad);

        // 플레이어 컨트롤 다시 활성화
        var controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
