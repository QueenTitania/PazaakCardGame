using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] GameObject playerTurnUI = null;
    [SerializeField] Text playerTurnTextUI = null;
    [SerializeField] Text playerHandValueTextUI = null;
    int playerCardTotal = 0;

    int playerTurnCount = 0;

    public UserHand playerHand;

    public bool playerStand = false;

    public override void Enter()
    {
        playerStand = false;
        //Debug.Log("enter player turn");
        playerTurnUI.gameObject.SetActive(true);

        playerTurnCount++;
        playerTurnTextUI.text = "Player turn count: " + playerTurnCount.ToString();

        

        if(playerHand.hand[8] == null)
            playerHand.GetCard();

        playerHandValueTextUI.text = "" + playerHand.handValue.ToString();
        //StateMachine.Input.PressedConfirm += OnPressedConfirm;
        /*StateMachine.Input.EndTurn += OnEndTurn;
        StateMachine.Input.PressedStand += OnStand;
        StateMachine.Input.PressWin += OnWin;
        StateMachine.Input.PressLose += OnLose;*/
        
    }

    public override void Tick()
    {
        if(playerStand)
        {
            StateMachine.ChangeState<EnemyTurnCardGameState>();
        }
            

    }

    public override void Exit()
    {
        playerTurnUI.gameObject.SetActive(false);
        Debug.Log("exiting player turn");
        

        //StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        /*StateMachine.Input.EndTurn -= OnEndTurn;
        StateMachine.Input.PressedStand -= OnStand;
        StateMachine.Input.PressWin -= OnWin;
        StateMachine.Input.PressLose -= OnLose;*/
    }

    public void EndTurn()
    {
        //Debug.Log("attempt to enter enemy turn");
        if(playerCardTotal > 20)
            StateMachine.ChangeState<RoundLoseState>();
        else
            StateMachine.ChangeState<EnemyTurnCardGameState>();
    }

    public void SetPlayerStand(bool stand)
    {
        playerStand = stand;
    }

    /*
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
    */
    
    
}
