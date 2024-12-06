using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneUI : MonoBehaviour
{
    public GameObject settingsMenu;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;        
        Cursor.visible = true;
        if(settingsMenu != null) settingsMenu.SetActive(false);
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
}
