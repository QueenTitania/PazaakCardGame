using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    public SideDeck opponentSideDeck;
    public SideDeck playerSideDeck;
    public UserHand playerHand;
    public UserHand opponentHand;


    bool activated = false;
    bool firstRound = true;

    public override void Enter()
    {
        //Debug.Log("entering");
        playerHand.ClearHand();
        opponentHand.ClearHand();

        GameObject.Find("Deck").GetComponent<DeckManager>().Shuffle();

        if(firstRound)
        {
            //opponentSideDeck.ClearHand();
            //playerSideDeck.ClearHand();

            opponentSideDeck.Shuffle();
            playerSideDeck.Shuffle();

            opponentSideDeck.DealHand();
            playerSideDeck.DealHand();

            firstRound = false;
        }
        
        GameObject.Find("StateController").GetComponent<PlayerTurnCardGameState>().SetPlayerStand(false);
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
