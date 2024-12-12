using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject settingsMenu;
    public bool Paused = false; // 게임이 일시정지 상태인지 확인하는 변수

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private HandController handController;

    private Enemy enemy;
    /*
    private void Awake()
    {
        AchievementManager achievementManager = AchievementManager.instance;
        if(achievementManager != null && GameObject.Find("alertPanel") != null) achievementManager.alertPanel = GameObject.Find("alertPanel");
        achievementManager.alertTitleText = GameObject.Find("alertTitleText").GetComponent<Text>();
        achievementManager.alertDescriptionText = GameObject.Find("alertDescriptionText").GetComponent<Text>();
        if(AchievementManager.instance.alertPanel != null) achievementManager.alertPanel.SetActive(false);
    }*/
    
    void Start()
    {
        AchievementManager achievementManager = AchievementManager.instance;
        if (achievementManager != null && GameObject.Find("alertPanel") != null) achievementManager.alertPanel = GameObject.Find("alertPanel");
        achievementManager.alertTitleText = GameObject.Find("alertTitleText").GetComponent<Text>();
        achievementManager.alertDescriptionText = GameObject.Find("alertDescriptionText").GetComponent<Text>();
        if (AchievementManager.instance.alertPanel != null) achievementManager.alertPanel.SetActive(false);

        pausemenu.SetActive(false);
        settingsMenu.SetActive(false);
        if(FindAnyObjectByType<Enemy>() != null) enemy = FindAnyObjectByType<Enemy>();
    }

    void Update() 
    {
        if (EventManager.instance == null) return;

        if (EventManager.instance.isGameOver)
        {
            playerController.enabled = false;  //playerController 스크립트 비활성화
            handController.enabled = false;
            Cursor.lockState = CursorLockMode.None;              //마우스조작 활성화
            Cursor.visible = true;

            if (ButtonUI.isEnd)
            {
                GameOver();
                ButtonUI.isEnd = false;
            }
            if (ButtonUI.isEnd_2)
            {
                ReturnToMainMenu();
                ButtonUI.isEnd_2 = false;
            }
            return;
        }

        if (Paused == true)
        {
            if (ButtonUI.isEnd)
            {
                CloseSettingsMenu();
                ButtonUI.isEnd = false;
            }
            if (ButtonUI.isEnd_2)
            {
                ReturnToMainMenu();
                ButtonUI.isEnd_2 = false;
            }
        }

        if(enemy != null) if (!enemy.isOneTime4) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf)
            {
                settingsMenu.SetActive(false);
            }
            else
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
        }
    }
    public void OpenSettingsMenu()
    {
        PauseSounds();
        Time.timeScale = 0f;
        pausemenu.SetActive(true);
        Paused = true;

        playerController.enabled = false;  //playerController 스크립트 비활성화
        handController.enabled = false;    //handController 스크립트 비활성화
        Cursor.lockState = CursorLockMode.None;              //마우스조작 활성화
        Cursor.visible = true;
    }

    public void CloseSettingsMenu()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;

        playerController.enabled = true;   //playerController 스크립트 활성화
        handController.enabled = true;     //handController 스크립트 활성화
        Cursor.lockState = CursorLockMode.Locked;              //마우스조작 비활성화
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        //SoundManager.instance.PlaySound("Click");
        //SoundManager.instance.StopSound("GameBGM");
        //SoundManager.instance.StopSound("FeverBGM");
        // 게임 종료
        Application.Quit();
    }


    public void ReturnToMainMenu()
    {
        //SoundManager.instance.PlaySound("Click");
        //SoundManager.instance.StopSound("GameBGM");
        //SoundManager.instance.StopSound("FeverBGM");
        // 메인 메뉴로 이동
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f; // 게임 시간 재개
        }
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        //Debug.Log("현재날짜는 " + GameManager.Days);
        if (GameManager.Days == 0)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            EventManager.playerdead = true;
            SceneManager.LoadScene("Home");
        }
    }

    private void PauseSounds()
    {

        string[] soundnames = {"Walk", "Run", "EnemyMove" };
        foreach (string str in soundnames)
        {
            SoundManager.instance.PauseSound(str);
        }
    }
}
