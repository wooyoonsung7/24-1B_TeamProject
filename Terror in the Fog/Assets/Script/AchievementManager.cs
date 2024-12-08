using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;




public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;          //싱글톤 화
    public List<Achievement> achievements;              //Achievement 클래스를 List로 관리

    public Text[] AchievementTexts = new Text[3];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);              //다른 Scene에서도 적용 하기 위해서 파괴 되지 않게 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void UpdateAchievementUI()
    {


        for (int i = 0; i < achievements.Count; i++)
        {
            var achievement = achievements[i];
            Image iconImage = AchievementTexts[i].transform.Find("Icon").GetComponent<Image>(); // Icon 이미지 가져오기

            if (achievement.isUnlocked)
            {
                // 업적이 해제된 경우 선명하게
                iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 1f);
            }
            else
            {
                // 업적이 잠긴 경우 흐릿하게
                iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 0.8f); // 알파값 0.5로 흐릿하게
            }

            // 텍스트 업데이트
            AchievementTexts[0].text = achievement.name;
            AchievementTexts[1].text = achievement.description;
            AchievementTexts[2].text = $"{achievement.currentProgress}/{achievement.goal}";
            AchievementTexts[3].text = achievement.isUnlocked ? "달성" : "미달성";
        }

    }


    public void AddProgress(string achievementName, int amount) //업적 진행 상활 갱신 함수
    {
        Achievement achievement = achievements.Find(a => a.name == achievementName);         //인수에서 받아온 이름으로 업적 리스트에서 찾아서 반환
        if (achievement != null)                                                            //반환된 업적이 있을 경우
        {
            achievement.AddProgress(amount);                                                //프로그래스를 증가 시킨다.
        }
    }

    //새로운 업적 추가 함수
    public void AddAchievement(Achievement achievement)
    {
        //Achievement temp = new Achievement("이름", "설명", 5);
        achievements.Add(achievement);                              //List에 업적 추가
    }
   
}
