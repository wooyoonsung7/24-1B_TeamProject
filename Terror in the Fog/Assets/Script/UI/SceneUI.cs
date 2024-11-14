using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;          //마우스 커서를 잠그고 숨긴다
        Cursor.visible = true;
    }

    public void LoadTestScene()
    {
        SceneManager.LoadScene("Home");
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
