using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementScene : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string name; // 업적 이름
        public string description; // 업적 설명
        public bool isUnlocked; // 잠금 해제 여부
        public Sprite icon; // 업적 아이콘
    }

    public Achievement[] achievements; // 업적 데이터 배열

    public GameObject achievementPrefab; // 업적을 표시할 프리팹
    public Transform achievementsContainer; // UI 컨테이너 (Grid Layout Group이 붙어 있어야 함)

    void Start()
    {
        // 업적 UI 초기화
        foreach (Achievement achievement in achievements)
        {
            CreateAchievementUI(achievement);
        }
    }

    void CreateAchievementUI(Achievement achievement)
    {
        // 프리팹을 인스턴스화
        GameObject AchievementUI = Instantiate(achievementPrefab, achievementsContainer);

        // 프리팹 내 컴포넌트 가져오기
        Image iconImage = AchievementUI.transform.Find("Icon").GetComponent<Image>();
        Text nameText = AchievementUI.transform.Find("Name").GetComponent<Text>();
        Text descriptionText = AchievementUI.transform.Find("Description").GetComponent<Text>();

        // 업적 데이터 적용
        if (iconImage != null) iconImage.sprite = achievement.icon;
        if (nameText != null) nameText.text = achievement.name;
        if (descriptionText != null) descriptionText.text = achievement.description;

        // 잠금 상태에 따라 투명도 조정
        if (!achievement.isUnlocked)
        {
            iconImage.color = new Color(1, 1, 1, 0.5f); // 반투명 처리
        }
    }
}

