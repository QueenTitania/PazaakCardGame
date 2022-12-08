using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] GameObject playerTurnUI = null;
    [SerializeField] Text playerTurnTextUI = null;
    [SerializeField] Text playerHandValueTextUI = null;
    [SerializeField] public GameObject StandTextUI = null;
    int playerCardTotal = 0;

    int playerTurnCount = 0;

    public UserHand playerHand;
    public SideDeck sideDeck;
    public EnemyTurnCardGameState opponent;

    public bool playerStand = false;

    public override void Enter()
    {
        if(!playerStand)
            StartTurn();
        else
            EndTurn();
    }

    public override void Tick()
    {
        if(playerStand)
        {
            StandTextUI.SetActive(true);
            StateMachine.ChangeState<EnemyTurnCardGameState>();
        }
        if(playerHand.GetHandValue() == 20)
        {
            playerStand = true;
            EndTurn();
        }

    }

    public override void Exit()
    {
        playerTurnUI.gameObject.SetActive(false);
        //Debug.Log("exiting player turn");
        

        //StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        /*StateMachine.Input.EndTurn -= OnEndTurn;
        StateMachine.Input.PressedStand -= OnStand;
        StateMachine.Input.PressWin -= OnWin;
        StateMachine.Input.PressLose -= OnLose;*/
    }

    public void StartTurn()
    {
        sideDeck.handCardPlayed = false;
        StandTextUI.SetActive(false);
        playerTurnUI.gameObject.SetActive(true);

        playerTurnCount++;
        playerTurnTextUI.text = "Player turn count: " + playerTurnCount.ToString();

        if(playerHand.hand[8] == null)
            playerHand.GetCard();

        playerHandValueTextUI.text = "" + playerHand.handValue.ToString();
    }

    public void EndTurn()
    {
        playerCardTotal = playerHand.handValue;

        if(playerCardTotal > 20)
            StateMachine.ChangeState<RoundLoseState>();
        else
            StateMachine.ChangeState<EnemyTurnCardGameState>();
    }

    public void SetPlayerStand(bool stand)
    {
        playerStand = stand;
    }

    public void UpdateHandValueText()
    {
        playerHandValueTextUI.text = "" + playerHand.GetHandValue().ToString();
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
