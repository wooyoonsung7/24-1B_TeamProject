using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementScene : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string name; 
        public string description;
        public bool isUnlocked;
        public Sprite icon; 
    }

    public Achievement[] achievements; 

    public GameObject achievementPrefab; 
    public Transform achievementsContainer;

    void Start()
    {
       
        foreach (Achievement achievement in achievements)
        {
            CreateAchievementUI(achievement);
        }
    }

    void CreateAchievementUI(Achievement achievement)
    {
        
        GameObject AchievementUI = Instantiate(achievementPrefab, achievementsContainer);

       
        Image iconImage = AchievementUI.transform.Find("Icon").GetComponent<Image>();
        Text nameText = AchievementUI.transform.Find("Name").GetComponent<Text>();
        Text descriptionText = AchievementUI.transform.Find("Description").GetComponent<Text>();

       
        if (iconImage != null) iconImage.sprite = achievement.icon;
        if (nameText != null) nameText.text = achievement.name;
        if (descriptionText != null) descriptionText.text = achievement.description;

      
        if (!achievement.isUnlocked)
        {
            iconImage.color = new Color(1, 1, 1, 0.5f); // 반투명 처리
        }
    }
}

