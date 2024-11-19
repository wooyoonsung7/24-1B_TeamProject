using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingBarController : MonoBehaviour
{
    public Slider progressBar;
    public Image fillImage;
    float fakeProgress = 0f;




    private void Start()
    {
        // 로딩 시작
        StartCoroutine(LoadTutorialScene());
    }

    public IEnumerator LoadTutorialScene()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync("TutorialScene");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.05f);
            progressBar.value = progress;

            fakeProgress += Time.deltaTime * 0.03f;  
            fakeProgress = Mathf.Clamp01(fakeProgress);  

            fillImage.fillAmount = progress;

            
            if (operation.progress >= 0.9f)
            {

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
