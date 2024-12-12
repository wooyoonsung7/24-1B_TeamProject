using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneUI : MonoBehaviour
{
    public GameObject settingsMenu;
    private static bool isGameStart = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;        
        Cursor.visible = true;
        if(settingsMenu != null) settingsMenu.SetActive(false);

        if (!isGameStart)
        {
            UnlockAchievements();
            //Debug.Log("작동한다ㅏㅏ");
            isGameStart = true;
        }

        PlayerPrefs.SetInt("FirstStart", 1);
    }

    private void Update()
    {
        if (settingsMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (settingsMenu.activeSelf)
                {
                    settingsMenu.SetActive(false);
                }
            }
        }

        if (ButtonUI.isEnd)
        {
            GoToMain();
            ButtonUI.isEnd = false;
        }
    }
    public void LoadTestScene()
    {
        SceneManager.LoadScene("LoadingUI");
    }

   
    public void LoadAchievementsScene()
    {
        
        PlayerPrefs.SetString("NextScene", "AchievementUI");
        
        SceneManager.LoadScene("AchievementUI");
    }


    public void GameExit()
    {
        Application.Quit();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void UnlockAchievements()
    {
        // 게임 시작 시 업적 달성
        PlayerPrefs.SetInt("FirstStart", 1);  // 첫 게임 시작 업적 달성
        PlayerPrefs.SetInt("FirstSleep", 0);  // 잠자는 업적은 아직 달성하지 않음
        PlayerPrefs.SetInt("GameEnding", 0);  // 엔딩 업적은 아직 달성하지 않음

        // 업적 씬으로 이동
        
    }

    
}
