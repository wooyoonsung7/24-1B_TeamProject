using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUIManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string name;        // 업적 이름
        public string description; // 업적 설명
        public Sprite icon;        // 업적 아이콘
        public bool isUnlocked;    // 잠금/해제 상태
    }

    [Header("Achievements Data")]
    public List<Achievement> achievements; // 업적 데이터 리스트

    [Header("UI References")]
    public GameObject achievementItemPrefab; // Prefab 템플릿
    public Transform achievementsParent;     // Grid Layout Group 또는 Content

    void Start()
    {
        PopulateAchievements();
    }

    // 업적 UI 생성
    void PopulateAchievements()
    {
        foreach (var achievement in achievements)
        {

            GameObject item = Instantiate(achievementItemPrefab, achievementsParent);

            // Prefab의 UI 요소 설정
            item.transform.Find("Icon").GetComponent<Image>().sprite = achievement.icon;
            item.transform.Find("Name").GetComponent<Text>().text = achievement.name;
            item.transform.Find("Description").GetComponent<Text>().text = achievement.description;


            var statusText = item.transform.Find("Status").GetComponent<Text>();
            if (achievement.isUnlocked)
            {
                statusText.text = "Unlocked";
                statusText.color = Color.green;
            }
            else
            {
                statusText.text = "Locked";
                statusText.color = Color.red;
            }
        }
    }


    public void UnlockAchievement(string achievementName)
    {
        foreach (var achievement in achievements)
        {
            if (achievement.name == achievementName && !achievement.isUnlocked)
            {
                achievement.isUnlocked = true;
                RefreshUI();
                Debug.Log($"Achievement Unlocked: {achievementName}");
                break;
            }
        }
    }
    void RefreshUI()
    {
        // 기존 UI 삭제
        foreach (Transform child in achievementsParent)
        {
            Destroy(child.gameObject);
        }

        // 업데이트된 데이터로 다시 생성
        PopulateAchievements();
    }
}
