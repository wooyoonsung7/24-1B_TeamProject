using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public bool Paused = false; // 게임이 일시정지 상태인지 확인하는 변수

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private HandController handController;
    [SerializeField]
    private Enemy enemy;


    void Start()
    {
        settingsPanel.SetActive(false);
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused == true)
            {
                CloseSettingsMenu();
            }
            else
            {
                OpenSettingsMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadTestScene2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            EnemyStartMove();
        }

    }
    public void OpenSettingsMenu()
    {
        Time.timeScale = 0f;
        settingsPanel.SetActive(true);
        Paused = true;

        playerController.enabled = false;  //playerController 스크립트 비활성화
        handController.enabled = false;    //handController 스크립트 비활성화
        Cursor.lockState = CursorLockMode.None;              //마우스조작 활성화
        Cursor.visible = true;
    }

    public void CloseSettingsMenu()
    {
        settingsPanel.SetActive(false); 
        Time.timeScale = 1f; 
        Paused = false;

        playerController.enabled = true;   //playerController 스크립트 활성화
        handController.enabled = true;     //handController 스크립트 활성화
        Cursor.lockState = CursorLockMode.Locked;              //마우스조작 비활성화
        Cursor.visible = true;
    }



    public void QuitGame()
    {
        SoundManager.instance.PlaySound("Click");
        SoundManager.instance.StopSound("GameBGM");
        SoundManager.instance.StopSound("FeverBGM");
        // 게임 종료
        Application.Quit();
    }


    public void ReturnToMainMenu()
    {
        SoundManager.instance.PlaySound("Click");
        SoundManager.instance.StopSound("GameBGM");
        SoundManager.instance.StopSound("FeverBGM");
        // 메인 메뉴로 이동
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f; // 게임 시간 재개
        }
        SceneManager.LoadScene("TestScene3");
        Time.timeScale = 1f;
    }

    public void LoadTestScene2()
    {
        SceneManager.LoadScene("TestScene2");
    }

    public void EnemyStartMove()
    {
        enemy.gameObject.SetActive(true);
    }
}
