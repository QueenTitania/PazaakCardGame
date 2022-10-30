using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] Text playerTurnTextUI = null;
    int playerCardTotal = 0;

    int playerTurnCount = 0;

    bool playerWin = false;
    bool playerLose = false;
    bool playerStand = false;

    public override void Enter()
    {
        Debug.Log("enter player turn");
        playerTurnTextUI.gameObject.SetActive(true);

        playerTurnCount++;
        playerTurnTextUI.text = "Player turn count: " + playerTurnCount.ToString();

        //StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.EndTurn += OnEndTurn;
        StateMachine.Input.PressedStand += OnStand;
        StateMachine.Input.PressWin += OnWin;
        StateMachine.Input.PressLose += OnLose;
        
    }

    public override void Tick()
    {
        if(playerStand)
            StateMachine.ChangeState<EnemyTurnCardGameState>();

    }

    public override void Exit()
    {
        playerTurnTextUI.gameObject.SetActive(false);
        Debug.Log("exiting player turn");

        //StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.EndTurn -= OnEndTurn;
        StateMachine.Input.PressedStand -= OnStand;
        StateMachine.Input.PressWin -= OnWin;
        StateMachine.Input.PressLose -= OnLose;
    }

    void OnEndTurn()
    {
        //Debug.Log("attempt to enter enemy turn");
        if(playerCardTotal > 20)
            StateMachine.ChangeState<RoundLoseState>();
        else
            StateMachine.ChangeState<EnemyTurnCardGameState>();
    }

    void OnStand()
    {
        StateMachine.ChangeState<RoundLoseState>();

    }

    void OnWin()
    {
        StateMachine.ChangeState<RoundWinState>();
    }
    void OnLose()
    {
        StateMachine.ChangeState<RoundLoseState>();
    }
    
}
