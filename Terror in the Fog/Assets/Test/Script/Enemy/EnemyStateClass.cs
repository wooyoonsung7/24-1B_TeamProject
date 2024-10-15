using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Idle : IState
{
    private static Idle Instance = new Idle();
    private Idle() { }
    public static Idle GetInstance() { return Instance; }
    public void StateEnter(Enemy enemy)
    {

    }
    public void StateFixUpdate(Enemy enemy)
    {

    }
    public void StateUpdate(Enemy enemy)
    {

    }
    public void StateExit(Enemy enemy)
    {

    }
}


//플레이어를 찾기 위해 탐색하는 상태
public class Research : IState
{
    private static Research Instance = new Research();
    private Research() { }
    public static Research GetInstance() { return Instance; }

    private bool isFindPlayer = true;
    public void StateEnter(Enemy enemy)
    {

        if (enemy.navMeshAgent != null)                          //몬스터 활동다시 시작
        {
            enemy.navMeshAgent.isStopped = false;
        }
        isFindPlayer = true;

        enemy.ResetSound();                                         //사운드감지 값초기화
        enemy.ResetResearch();                                       //탐색 값 초기화
    }
    public void StateFixUpdate(Enemy enemy)
    {
        enemy.CheckObject();                                        //시야에서 플레이어감지
        enemy.DetectToSound();                                      //사운드감지
    }
    public void StateUpdate(Enemy enemy)
    {
        enemy.ResearchArea();                                       //맵탐색

        if (enemy != null)                                         
        {
            if (enemy.hitTargetList.Count > 0 && isFindPlayer)      //플레이이어 시야내 감지(1순위)
            {
                enemy.navMeshAgent.updateRotation = true;
                enemy.stateMachine.SetState(enemy, JudgeChase.GetInstance());
            }
            
            if (SoundDetector.instance.SoundPos.Count > 0)         //플레이어의 사운드감지(2순위)
            {
                enemy.navMeshAgent.updateRotation = true;
                enemy.pauseResearch = true;
            }
            else
            {
                enemy.pauseResearch = false;
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        isFindPlayer = false;
    }
}


//추적하다가 놓쳤을 때, 주위를 둘러보는 상태
public class FeelStrage : IState
{
    private static FeelStrage Instance = new FeelStrage();
    private FeelStrage() { }
    public static FeelStrage GetInstance() { return Instance; }
    public void StateEnter(Enemy enemy)
    {
        enemy.CheckAround();
        enemy.navMeshAgent.updateRotation = false;
    }
    public void StateFixUpdate(Enemy enemy)
    {

    }
    public void StateUpdate(Enemy enemy)
    {
         if (enemy != null && !enemy.isCheckAround)
        {
            if (enemy.hitTargetList.Count <= 0)
            {
                enemy.stateMachine.SetState(enemy, Research.GetInstance());
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        enemy.navMeshAgent.updateRotation = true;
    }
}



//플레이어를 발견하고 쫓기를 판단하는 상태
public class JudgeChase : IState
{
    private static JudgeChase Instance = new JudgeChase();
    private JudgeChase() { }
    public static JudgeChase GetInstance() { return Instance; }

    private float detectTime = 1f;
    private float detectStartTime = 0f;
    public void StateEnter(Enemy enemy)
    {
        if (enemy != null)
        {
            //이상함 감지 애니메이션
            detectStartTime = Time.time;
            //enemy.FeelStrage() 추가예정
        }
    }
    public void StateFixUpdate(Enemy enemy)
    {

    }
    public void StateUpdate(Enemy enemy)
    {
        if (enemy != null)
        {
            if (enemy.stateMachine.currentState != ChaseState.GetInstance())
            {
                bool canChase = Time.time >= detectTime + detectStartTime;
                if (canChase)                                                       //1초 경과후에 추적시작
                {
                    enemy.stateMachine.SetState(enemy, ChaseState.GetInstance());
                }
            }
        }
    }
    public void StateExit(Enemy enemy)
    {
        //적이 이상함을 느끼는 애니메이션 종료
    }
}


//적을 추적하는 상태
public class ChaseState : IState
{
    private static ChaseState Instance = new ChaseState();
    private ChaseState() { }
    public static ChaseState GetInstance() { return Instance; }

    //숨었때의 변수
    float findTime = 1.5f;                       //플레이어가 숨었을 경우, 어그로가 풀릴 때까지의 시간
    float f_timer = 0f;
    float seizeRadius = 1f;

    //숨지 않았을 때의 변수
    float notChaseTime = 10f;

    PlayerController playerController;
    public void StateEnter(Enemy enemy)
    {
        playerController = enemy.playerController;
        enemy.navMeshAgent.isStopped = false;
        enemy.isFindPlayer = true;
    }

    public void StateFixUpdate(Enemy enemy)
    {
        enemy.CheckObject();
        enemy.ChasePlayer();                      //플레이어를 쫓는다.
    }

    public void StateUpdate(Enemy enemy)
    {
        enemy.StopFinding();                      //시선안에 없어진 후 쿨타임

        if (playerController.isHide)
        {
            f_timer += Time.deltaTime;
            if (findTime <= f_timer)
            {
                enemy.seizeRadius = 0.5f;         //사망용 감지범위 초기화
                enemy.stateMachine.SetState(enemy, FeelStrage.GetInstance());
                Debug.Log("숨기 성공");
            }
            else
            {
                enemy.seizeRadius = seizeRadius;  //사망용 감지범위 늘리기
                enemy.CheckDeath();
                Debug.Log("숨기 실패");
            }
        }
        else
        {
            enemy.CheckDeath();
            if (enemy.timer >= notChaseTime)
            {
                enemy.stateMachine.SetState(enemy, FeelStrage.GetInstance());
            }
        }
    }

    public void StateExit(Enemy enemy)
    {
        f_timer = 0f;                              //타이머초기화
        enemy.timer = 0f;
        enemy.isFindPlayer = false;
    }
}
