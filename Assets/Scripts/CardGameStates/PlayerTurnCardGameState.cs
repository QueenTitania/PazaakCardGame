using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] Text playerTurnTextUI = null;

    int playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("enter player turn");
        playerTurnTextUI.gameObject.SetActive(true);

        playerTurnCount++;
        playerTurnTextUI.text = "Player turn count: " + playerTurnCount.ToString();

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        
    }

    public override void Exit()
    {
        playerTurnTextUI.gameObject.SetActive(false);
        Debug.Log("exiting player turn");

        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        //Debug.Log("attempt to enter enemy turn");
        StateMachine.ChangeState<EnemyTurnCardGameState>();
    }
    
}
