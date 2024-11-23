using UnityEngine;
using DG.Tweening;

public class DrawerController : MonoBehaviour
{
    public Transform drawer; // 서랍의 Transform
    public float openPositionZ = -1f; // 서랍을 열었을 때의 Z 위치
    public float closePositionZ = 0f; // 서랍을 닫았을 때의 Z 위치
    private bool isOpen = false; // 서랍이 열려있는지 여부

    void Start()
    {
        // 서랍을 시작할 때 닫힌 상태로 설정
        drawer.localPosition = new Vector3(drawer.localPosition.x, drawer.localPosition.y, closePositionZ);
    }

    void Update()
    {
        // 마우스 좌클릭을 감지
        if (Input.GetMouseButtonDown(0)) // 0은 좌클릭
        {
            ToggleDrawer(); // 서랍 열기/닫기 토글
        }
    }

    public void ToggleDrawer()
    {
        if (isOpen)
        {
            // 서랍이 열려 있으면 닫기
            drawer.DOLocalMoveZ(closePositionZ, 0.5f).SetEase(Ease.OutQuad);
        }
        else
        {
            // 서랍이 닫혀 있으면 열기
            drawer.DOLocalMoveZ(openPositionZ, 0.5f).SetEase(Ease.OutQuad);
        }

        // 상태를 반전시킴
        isOpen = !isOpen;
    }
}
