using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    public UserHand opponentHand;

    int enemyCardTotal = 0;

    [SerializeField] float pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("enter enemy turn");
        EnemyTurnBegan?.Invoke();

        opponentHand.GetCard();

        StartCoroutine(EnemyThinkingRoutine(pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("exit enemy turn");
    }

    private IEnumerator EnemyThinkingRoutine(float duration)
    {
        Debug.Log("enemy thinking");
        yield return new WaitForSeconds(duration);

        Debug.Log("enemy performs action");
        EnemyTurnEnded?.Invoke();

        if(enemyCardTotal > 20)
            StateMachine.ChangeState<RoundWinState>();
        else
            StateMachine.ChangeState<PlayerTurnCardGameState>();
    }

}
