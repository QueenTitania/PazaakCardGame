using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    public SideDeck opponentSideDeck;
    public SideDeck playerSideDeck;

    public UserHand playerHand;
    public UserHand opponentHand;

    public WinTracker playerWinTracker;
    public WinTracker opponentWinTracker;


    bool activated = false;
    bool firstRound = true;

    public override void Enter()
    {
        //Debug.Log("entering");
        playerHand.ClearHand();
        opponentHand.ClearHand();

        if(firstRound)
        {
            GameObject.Find("Deck").GetComponent<DeckManager>().Shuffle();
            //opponentSideDeck.ClearHand();
            //playerSideDeck.ClearHand();

            playerWinTracker.ResetWinUI();
            opponentWinTracker.ResetWinUI();

            opponentSideDeck.Shuffle();
            playerSideDeck.Shuffle();

            opponentSideDeck.DealHand();
            playerSideDeck.DealHand();

            firstRound = false;
        }
        
        GameObject.Find("StateController").GetComponent<PlayerTurnCardGameState>().SetPlayerStand(false);
        GameObject.Find("StateController").GetComponent<EnemyTurnCardGameState>().SetOpponentStand(false);
        //opponentHand.handValue = 0;
        GameObject.Find("StateController").GetComponent<EnemyTurnCardGameState>().UpdateHandValueText();
        
        activated = false;
    }

    public override void Tick()
    {
        if(activated == false)
        {
            activated = true;
            StateMachine.ChangeState<PlayerTurnCardGameState>();
        }
    }

    public override void Exit()
    {
        activated = false;
        //Debug.Log("exiting");
    }
    
    
}
