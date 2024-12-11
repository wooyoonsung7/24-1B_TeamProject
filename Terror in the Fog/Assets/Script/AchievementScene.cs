using System;
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

    public Sprite cloverkeyIcon;
    public Sprite diamondKeyIcon;
    public Sprite spaceKeyIcon;

    public Achievement[] achievements;
    public GameObject achievementPrefab;  // 업적 UI 프리팹
    public Transform achievementsContainer; // UI에 업적을 넣을 컨테이너

    void Start()
    {
        // 업적 데이터 설정
        achievements = new Achievement[]
        {
            new Achievement { name = "클로버 키", description = "클로버 키 획득!", isUnlocked = true, icon = cloverkeyIcon },
            new Achievement { name = "다이아몬드 키", description = "다이아몬드 키 획득!", isUnlocked = false, icon = diamondKeyIcon },
            new Achievement { name = "스페이스 키", description = "스페이스 키 획득!", isUnlocked = true, icon = spaceKeyIcon }
        };

        // 각 업적에 대해 UI 생성
        foreach (Achievement achievement in achievements)
        {
            CreateAchievementUI(achievement);
        }
    }

    void CreateAchievementUI(Achievement achievement)
    {
        // 업적 UI를 프리팹에서 인스턴스화하여 추가
        GameObject achievementUI = Instantiate(achievementPrefab, achievementsContainer);

        // 업적 UI의 컴포넌트 가져오기
        Image iconImage = achievementUI.transform.Find("Icon").GetComponent<Image>();
        Text nameText = achievementUI.transform.Find("Name").GetComponent<Text>();
        

        // 업적 정보로 UI 업데이트
        if (iconImage != null) iconImage.sprite = achievement.icon;
        if (nameText != null) nameText.text = achievement.name;
        

        // 업적 잠금 여부에 따라 아이콘 상태 변경
        if (iconImage != null && !achievement.isUnlocked)
        {
            iconImage.color = new Color(1, 1, 1, 0.5f); // 반투명 처리
        }
    }
}
    

