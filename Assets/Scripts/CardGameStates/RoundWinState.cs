using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundWinState : CardGameState
{
    [SerializeField] Text roundWinTextUI = null;
    [SerializeField] float waitTime = 1f;
    public WinTracker winTracker;

    int playerWins = 0;
    
    public override void Enter()
    {
        Debug.Log("enter round win state");
        roundWinTextUI.gameObject.SetActive(true);
        playerWins++;
        winTracker.UpdateWinUI(playerWins);
        StartCoroutine(ReadTimeRoutine(waitTime));
    }


    public override void Exit()
    {
        Debug.Log("exit round win state");
        roundWinTextUI.gameObject.SetActive(false);
    }

    private IEnumerator ReadTimeRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (playerWins >= 3)
        {
            StateMachine.ChangeState<GameWinState>();
        }
        else
            StateMachine.ChangeState<SetupCardGameState>();
    }


}
