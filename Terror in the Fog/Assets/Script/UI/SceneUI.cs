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
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf)
            {
                settingsMenu.SetActive(false);
            }
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

    public void GoToStreet()
    {
        SceneManager.LoadScene("Street");
    }

}
