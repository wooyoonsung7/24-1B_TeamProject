using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementScene : MonoBehaviour
{
    public Image firstStartImage;       // 업적 이미지
    public Image firstSleepImage;       // 업적 이미지
    public Image gameEndingImage;      // 업적 이미지
    public Image lockImageFirstStart;  // 잠금 이미지 (첫 게임 시작)
    public Image lockImageFirstSleep;  // 잠금 이미지 (첫 잠)
    public Image lockImageGameEnding; // 잠금 이미지 (게임 엔딩)

    public Text firstStartText;         // 텍스트
    public Text firstSleepText;         // 텍스트
    public Text gameEndingText;        // 텍스트

    void Start()
    {
        if (PlayerPrefs.GetInt("FirstStart", 0) == 1)
        {
            firstStartImage.gameObject.SetActive(true);
            firstStartText.text = "첫 게임 시작!";
            lockImageFirstStart.gameObject.SetActive(false);  // 잠금 이미지 숨기기
        }
        else
        {
            lockImageFirstStart.gameObject.SetActive(true);  // 잠금 이미지 보이기
        }

        if (PlayerPrefs.GetInt("FirstSleep", 0) == 1)
        {
            firstSleepImage.gameObject.SetActive(true);
            firstSleepText.text = "첫 잠!";
            lockImageFirstSleep.gameObject.SetActive(false);  // 잠금 이미지 숨기기
        }
        else
        {
            lockImageFirstSleep.gameObject.SetActive(true);  // 잠금 이미지 보이기
        }

        if (PlayerPrefs.GetInt("GameEnding", 0) == 1)
        {
            gameEndingImage.gameObject.SetActive(true);
            gameEndingText.text = "게임 엔딩!";
            lockImageGameEnding.gameObject.SetActive(false);  // 잠금 이미지 숨기기
        }
        else
        {
            lockImageGameEnding.gameObject.SetActive(true);  // 잠금 이미지 보이기
        }
    }
    void Update()
    {
        // ESC 키를 눌렀을 때 메인 화면으로 돌아가기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // "MainMenu" 씬으로 로드 (MainMenu 씬 이름에 맞게 수정)
            SceneManager.LoadScene("MainScene");
        }
    }
}




