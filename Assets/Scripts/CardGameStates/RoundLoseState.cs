using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundLoseState : CardGameState
{
    [SerializeField] Text roundLoseTextUI = null;
    [SerializeField] float waitTime = 1f;

    public WinTracker winTracker;

    int playerLoses = 0;
    
    public override void Enter()
    {
        Debug.Log("enter round lose state");
        roundLoseTextUI.gameObject.SetActive(true);
        playerLoses++;
        winTracker.UpdateWinUI(playerLoses);
        StartCoroutine(ReadTimeRoutine(waitTime));
        
    }

    public override void Exit()
    {
        Debug.Log("exit round lose state");
        roundLoseTextUI.gameObject.SetActive(false);
    }

    private IEnumerator ReadTimeRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (playerLoses >= 3)
            StateMachine.ChangeState<GameLoseState>();
        else
            StateMachine.ChangeState<SetupCardGameState>();
    }

}
