using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

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
    public int stamina_Time = 5;
    public int recover_stamina_Time = 7;
    public bool isHide = false;

    //카메라 설정 변수
    [Header("Camera Settings")]
    public Camera firstPersonCamera;      //1인칭 카메라

    public float yMinLimit = 30;         //카메라 수직 회전 최소각
    public float yMaxLimit = 90;         //카메라 수직 회전 최대각        

    private float theta = 0.0f;                  //카메라 수평회전 각도
    private float phi = 0.0f;                    //카메라의 수직회전 각도
    private float targetVerticalRotation;         //목표 수직 회전 각도
    private float RotationSpeed = 240f;           //수직 회전 속도

    public static float mouseSenesitivity = 2f;  //마우스 감도

    private bool isCrouching = false;     //1인치 모드 인지 여부
    private Rigidbody rb;

    [Header("Ext Setting")]
    public GameObject head;
    public Slider s_slider;        //플레이어의 기력을 나타나탤 UI
    public GameObject s_Fill;
    public float Fade_Duration = 5f;    //스태미너 흐릿하게 하는 지속시간
    public float speedPackDuration = 3f;
    private bool playerCanRun = true;
    private Image[] s_Image = new Image[2];
    private bool isFadeIn = false;
    public bool isFadeOut = true;
    private float timer = 0f;

    public bool isCanMove = true;   //옷장용
    public bool isCanMove_2 = true; //금고용

    private SoundData soundData;
    private bool isOneTime = false;
    private bool isOneTime_2 = true;
    public bool isOneTime_3 = false;

    private Vector3 movement2;
    [SerializeField]private Slider M_Slider;

    public enum SoundState
    {
        Idle, Walk, Run, Crouch
    }
    SoundState soundstate;

    private void Awake()
    {
        FindSlider();
    }
    void Start()
    {
        soundData = GetComponent<SoundData>();

        rb = GetComponent<Rigidbody>();
        s_Image[0] = s_slider.GetComponentInChildren<Image>();
        s_Image[1] = s_slider.transform.GetChild(1).GetComponentInChildren<Image>();

        Cursor.lockState = CursorLockMode.Locked;          //마우스 커서를 잠그고 숨긴다
        Cursor.visible = false;
        SetupCameras();
        SetActiveCamera();
    }


    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleRun();
        HandleCrouch();
        ETC();
        SetSound();
    }

    //활성화 카메라를 설정하는 함수
    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(true);
    }

    //카메라 및 캐릭터 회전처리하는 함수
    void HandleRotation()
    {
        if (isCanMove)
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
    }

    //카메라 초기 위치 및 회전을 설정하는 함수
    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0f, 0f, -0.55f);
        firstPersonCamera.transform.localRotation = Quaternion.identity;
    }

    //플레이어 행동처리 함수
    void HandleMovement()
    {
        if (isCanMove && isCanMove_2)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            //캐릭터 기준으로 이동
            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
            movement2 = new Vector3(moveHorizontal, 0f, moveVertical);
            if (CheckHitWall(movement2))
                movement = Vector3.zero;

            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);  //물리기반 이동

            if ((moveHorizontal != 0f || moveVertical != 0f) && !isCrouching && !Input.GetButton("Run")) 
            {
                ChangeState(SoundState.Walk);
            }
            
            if(movement == Vector3.zero && !isCrouching)
            {
               ChangeState(SoundState.Idle);
            }
        }
    }

    void HandleRun()
    {

        if (Input.GetButton("Run") && !isCrouching && playerCanRun && !CheckHitWall(movement2))
        {
            moveSpeed = runSpeed;

            s_slider.value -= Time.deltaTime / stamina_Time;
            if (s_slider.value <= 0.01f)
            {
                playerCanRun = false;
                s_Fill.SetActive(false);
                timer = Time.time;
            }

            FadeIn();                  //페이드 한번만 처리하기 위한 불값

            ChangeState(SoundState.Run);
        }
        else
        {
            if (Time.time >= timer + 0.3f)
            {
                s_Fill.SetActive(true);

                if (!isCrouching)
                {
                    moveSpeed = walkSpeed;

                    if (!playerCanRun)
                    {
                        ChangeState(SoundState.Walk);
                    }
                }

                s_slider.value += Time.deltaTime / recover_stamina_Time;
                if (s_slider.value == 1)
                {
                    playerCanRun = true;
                    FadeOut();

                }
            }

        }
    }

    void HandleCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                ChangeState(SoundState.Crouch);
                gameObject.transform.localScale = new Vector3(0.0f, crouchDgree, 0.0f);
                moveSpeed = crouchSpeed;
                //Debug.Log(moveSpeed);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(0.0f, playerHigh, 0.0f);
                moveSpeed = walkSpeed;
                //Debug.Log(moveSpeed);
            }
        }

    }

    private void FadeIn()
    {
        if (isFadeIn)
        {
            for (int i = 0; i < s_Image.Length; i++)
            {
                s_Image[i].DOFade(1f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(false).OnComplete(() => isFadeOut = true);
            }
            isFadeIn = false;
        }
    }
    private void FadeOut()
    {
        if (isFadeOut)
        {
            for (int i = 0; i < s_Image.Length; i++)
            {
                s_Image[i].DOFade(0f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(false).OnComplete(() => isFadeIn = true);
            }
            isFadeOut = false;
        }
    }

    public void ETC()
    {
        if (isHide) //플레이가 숨었을 때, 기본 상태로 변경
        {
            if (soundstate != SoundState.Idle)
            {
                ChangeState(SoundState.Idle);
                Debug.Log("확인중");
            }
        }

        if (transform.position.y < -10f) //낙사
        {
            PlayerDead();
        }
    }
    public void PlayerDead()
    {
        StartCoroutine(SetSounds());
        //gameObject.SetActive(false);
    }
    private IEnumerator SetSounds()
    {
        SoundManager.instance.PauseAllSound();
        yield return new WaitUntil(()=> SoundManager.instance.isEnd);
        EventManager.instance.PlayerDead();
    }

    private void SetSound()
    {
        if (isOneTime)
        {
            switch (soundstate)
            {
                case SoundState.Walk:
                    SetState(SoundState.Walk);
                    break;

                case SoundState.Run:
                    SetState(SoundState.Run);
                    break;

                case SoundState.Crouch:
                    SetState(SoundState.Crouch);
                    break;

                case SoundState.Idle:
                    SetState(SoundState.Idle);
                    break;

            }
            isOneTime = false;
        }
    }

    private void SetState(SoundState state)
    {
        SoundState _soundstate = state;
        SoundManager.instance.PlaySound(_soundstate.ToString());

        if (SoundDetector.instance != null)
        {
            StartCoroutine(SetLevel(_soundstate.ToString()));
        }
        //Debug.Log(state.ToString());
    }

    public void ChangeState(SoundState state)
    {
        if (soundstate != state)
        {
            isOneTime_2 = true;
        }
        if (isOneTime_2)
        {
            //SoundDetector.instance.ResetPos();

            isOneTime = true;
            SoundState r_soundstate = soundstate;
            SoundManager.instance.PauseSound(r_soundstate.ToString());
            soundstate = state;

            isOneTime_2 = false;
        }
    }

    IEnumerator SetLevel(string name)
    {
        string _name = name;
        while (true)
        {
            if (soundstate == SoundState.Idle || soundstate == SoundState.Crouch)
            {
                SoundDetector.instance.G_level = 0;
                Debug.Log("전달 레벨은 " + SoundDetector.instance.G_level);
            }
            if (soundstate == SoundState.Walk || soundstate == SoundState.Run)
            {
                for (int i = 0; i < soundData.soundLevel.Count; i++)
                {
                    if (soundData.soundname[i] == _name)
                    {
                        SoundDetector.instance.G_level = soundData.soundLevel[i];
                        Debug.Log("전달 레벨은 " + SoundDetector.instance.G_level);
                    }
                }
            }
            yield return new WaitForSeconds(50f*Time.deltaTime);

            //Debug.Log("코루틴 중");
        }
    }



    //벽에 부딛혔는지 체킹-->벽에 부딪혔을 때, 속도를 0로 처리

    private bool CheckHitWall(Vector3 movement)
    {
        movement = transform.TransformDirection(movement);
        float scope = 0.5f;

        List<Vector3> rayPositions = new List<Vector3>();
        rayPositions.Add(transform.position + Vector3.up * 0.7f);
        rayPositions.Add(transform.position - Vector3.up * transform.localScale.y /2 *1.5f);
        rayPositions.Add(transform.position - Vector3.up * 0.1f);

        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        foreach (Vector3 pos in rayPositions)
        {
            if (Physics.Raycast(pos, movement, out RaycastHit hit, scope))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ChangMouseSensitivity(float value)
    {
        mouseSenesitivity = value;
    }

    public void FindSlider()
    {
        M_Slider = GameObject.FindGameObjectWithTag("M_Slider").GetComponent<Slider>();
    }
    public void FixedUpdate()
    {
        M_Slider.value = mouseSenesitivity;
    }
}
