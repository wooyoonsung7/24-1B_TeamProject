using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class AchievementManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string name;
        public string description;
        public bool isUnlocked;
        public bool showAlert;  // 알람을 띄울지 여부
        



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
                Debug.Log($"업적 달성: {name}");

                if (showAlert)
                {
                    ShowAchievementAlert();
                }
            }
        }

        private void ShowAchievementAlert()
        {
            // 알람을 띄우는 로직 추가 (예: UI 알람, 팝업 등)
            Debug.Log($"알람: {name} 달성!");
        }
    }

    public Achievement[] achievements;

    // UI 알림 텍스트 및 패널
    public GameObject alertPanel;
    public Text alertTitleText;
    public Text alertDescriptionText;

 


    public void CheckAchievements(string condition)
    {
        // 특정 조건을 만족하는 경우 해당 업적 달성
        foreach (Achievement achievement in achievements)
        {
            if (!achievement.isUnlocked && condition == achievement.name)
            {
                achievement.Unlock();
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

            // 일정 시간 후 알림 숨기기
            Invoke(nameof(HideAchievementAlert), 3f);
        }
    }

    private void Awake()
    {
        // 업적 배열 초기화
        achievements = new Achievement[]
        {
            new Achievement
            {
                name = "box key",
                description = "I got the box key.",
                isUnlocked = false,
                showAlert = true
            },
            new Achievement
            {
                name = "diamond key",
                description = "Obtained the Diamond Key.",
                isUnlocked = false,
                showAlert = true
            },
            new Achievement
            {
                name = "spade key",
                description = "Obtained the spade key.",
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

    
