using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void StateEnter(Enemy enemy);     //상태입장
    void StateFixUpdate(Enemy enemy); //현상태(물리연산, 로직) 업데이트
    void StateUpdate(Enemy enemy);    //현상태 업데이트
    void StateExit(Enemy enemy);      //현상태 나가기
}
public class StateMachine
{
    public IState currentState;

    public void Statemachine(Enemy enemy, IState defaultState)
    {
        currentState = defaultState;
        currentState.StateEnter(enemy);
    }
    public void SetState(Enemy enemy, IState state)
    {
        if (currentState == null || currentState == state)
        {
            Debug.Log("상태변경불가");
            return;
        }

        currentState.StateExit(enemy);
        currentState = state;
        //플레이어 사운드타입변경
        //배경음악 변경
        currentState.StateEnter(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        currentState.StateUpdate(enemy);
    }

    public void FixedUpdateState(Enemy enemy)
    {
        currentState.StateFixUpdate(enemy);
    }
}
