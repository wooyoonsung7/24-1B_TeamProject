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

    // Update is called once per frame

    public void LoadTestScene()
    {
        // TestSceneÀ¸·Î ÀÌµ¿
        SceneManager.LoadScene("TestScene2");
    }



    public void GameExit()
    {
        Application.Quit();
    }
}
