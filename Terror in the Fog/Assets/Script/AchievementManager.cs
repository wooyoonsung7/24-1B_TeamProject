using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    [System.Serializable]
    public class Achievement
    {
        public string name;
        public string description;
        public bool isUnlocked;
        public bool showAlert; 



        // UI 연결
        private AchievementManager  achievementManager;

        public void Initialize(AchievementManager system)
        {
            achievementManager = system;
        }


        public void Unlock()
        {
            if (!isUnlocked)
            {
                isUnlocked = true;
                //Debug.Log($"업적 달성: {name}");

                if (showAlert)
                {
                    ShowAchievementAlert();
                }
            }
        }

        private void ShowAchievementAlert()
        {
            
            //Debug.Log($"알람: {name} 달성!");
        }
    }

    public Achievement[] achievements;

   
    public GameObject alertPanel;
    public Text alertTitleText;
    public Text alertDescriptionText;


    public void CheckAchievements(string condition)
    {
        
        foreach (Achievement achievement in achievements)
        {
            if (!achievement.isUnlocked && condition == achievement.name)
            {
                achievement.Unlock();
                ShowAchievementAlert(achievement.name, achievement.description);
            }
        }
    }


    public void ShowAchievementAlert(string title, string description)
    {
        if (alertPanel != null)
        {
            alertPanel.SetActive(true);
            alertTitleText.text = title;
            alertDescriptionText.text = description;

           
            Invoke(nameof(HideAchievementAlert), 3f);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        alertPanel = GameObject.Find("alertPanel");
        alertTitleText = alertPanel.transform.GetChild(0).GetComponent<Text>();
        alertDescriptionText = alertPanel.transform.GetChild(1).GetComponent<Text>();
        HideAchievementAlert();

        achievements = new Achievement[]
        {
            new Achievement
            {
                name = "클로버열쇠",
                description = "클로버열쇠를 획득했습니다.",
                isUnlocked = false,
                showAlert = true
            },
            new Achievement
            {
                name = "다이아몬드열쇠",
                description = "다이아몬드열쇠를 획득했습니다.",
                isUnlocked = false,
                showAlert = true
            },
            new Achievement
            {
                name = "스페이드열쇠",
                description = "스페이드열쇠를 획득했습니다.",
                isUnlocked = false,
                showAlert = true
            }
        };
    }

    private void HideAchievementAlert()
    {
        if (alertPanel != null)
        {
            alertPanel.SetActive(false);
        }
    }
}

    
