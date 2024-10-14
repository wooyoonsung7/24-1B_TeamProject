using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
