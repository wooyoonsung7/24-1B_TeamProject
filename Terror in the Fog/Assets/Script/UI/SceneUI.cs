using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;        
        Cursor.visible = true;
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
        Debug.Log("나간다");
    }

    public void GoToStreet()
    {
        SceneManager.LoadScene("Street");
    }

}
