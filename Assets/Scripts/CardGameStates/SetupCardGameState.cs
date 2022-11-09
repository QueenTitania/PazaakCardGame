using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    

    bool activated = false;

    public override void Enter()
    {
        Debug.Log("entering");
        GameObject.Find("Deck").GetComponent<DeckManager>().Shuffle();
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
        Debug.Log("exiting");
    }
    
    
}
