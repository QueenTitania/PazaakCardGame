using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    [SerializeField] int startingCardNumber = 10;
    [SerializeField] int numberOfPlayers = 2;

    bool activated = false;

    public override void Enter()
    {
        Debug.Log("entering");
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
