using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //플레이어의 움직임 속도를 설정하는 변수
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float walkSpeed = 2.5f;
    public float runSpeed = 5.0f;
    public float crouchSpeed = 1.1f;
    public float playerHigh = 0.8f;
    public float crouchDgree = 0.3f;

    //카메라 설정 변수
    [Header("Camera Settings")]
    public Camera firstPersonCamera;      //1인칭 카메라

    public float radius = 5.0f;          //3인칭 카메라와 플레이어 간의 거리
    public float minRadius = 1.0f;       //카메라 최소 거리
    public float maxRadius = 10.0f;      //카메라 최대 거리


    public float yMinLimit = 30;         //카메라 수직 회전 최소각
    public float yMaxLimit = 90;         //카메라 수직 회전 최대각        

    private float theta = 0.0f;                  //카메라 수평회전 각도
    private float phi = 0.0f;                    //카메라의 수직회전 각도
    private float targetVerticalRotation;         //목표 수직 회전 각도
    private float RotationSpeed = 240f;           //수직 회전 속도

    public float mouseSenesitivity = 2f;  //마우스 감도

    //내부 변수들
    private bool isCrouching = false;     //1인치 모드 인지 여부
    private Rigidbody rb;

    public GameObject head;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;          //마우스 커서를 잠그고 숨긴다
        UnityEngine.Cursor.visible = false;
        SetupCameras();
        SetActiveCamera();
    }


    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleRun();
        HandleCrouch();
    }

    //활성화 카메라를 설정하는 함수
    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(true);
    }

    //카메라 및 캐릭터 회전처리하는 함수
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

        //수평 회전(theta 값)
        theta += mouseX;
        theta = Mathf.Repeat(theta, 360f);

        //수직 회전 처리
        targetVerticalRotation -= mouseY;
        targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, yMinLimit, yMaxLimit);

        phi = Mathf.MoveTowards(phi, targetVerticalRotation, RotationSpeed * Time.deltaTime);

        //플레이어, 머리회전 처리
        head.gameObject.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

    }

    //카메라 초기 위치 및 회전을 설정하는 함수
    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0f, 0f, 0f);
        firstPersonCamera.transform.localRotation = Quaternion.identity;
    }

    //플레이어 행동처리 함수
    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //캐릭터 기준으로 이동
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);  //물리기반 이동

    }

    void HandleRun()
    {
        if (Input.GetButton("Run") && !isCrouching)
        {
            moveSpeed = runSpeed;
            Debug.Log("뛰다.");
            Debug.Log(moveSpeed);
        }
        else if (!isCrouching)
        {
            moveSpeed = walkSpeed;
        }
    }

    void HandleCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                Debug.Log("앉다");
                gameObject.transform.localScale = new Vector3(0.0f, crouchDgree, 0.0f);
                moveSpeed = crouchSpeed;
                Debug.Log(moveSpeed);
            }
            else
            {
                Debug.Log("일어남");
                gameObject.transform.localScale = new Vector3(0.0f, playerHigh, 0.0f);
                moveSpeed = walkSpeed;
                Debug.Log(moveSpeed);
            }
        }

    }
}
