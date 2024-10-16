using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public bool Paused = false;
    bool isquick = false;
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

    }
    public void OpenSettingsMenu()
    {
        settingsPanel.SetActive(true);
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f; // 게임 시간 재개
        }
        Paused = true;
    }

    public void CloseSettingsMenu()
    {
        settingsPanel.SetActive(false);
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f; // 게임 시간 재개
        }
        Paused = false;
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
    }
}
