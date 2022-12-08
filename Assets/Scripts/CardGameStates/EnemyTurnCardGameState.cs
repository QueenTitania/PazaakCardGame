using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;
    
    [SerializeField] Text opponentHandValueTextUI = null;
    [SerializeField] public GameObject tieTextUI = null;
    public PlayerTurnCardGameState player;

    public UserHand opponentHand;
    public SideDeck opponentSide;

    public UserHand playerHand;

    int currentPossibleTotal;
    int playerTotal;

    public int enemyCardTotal = 0;
    public bool opponentStand = false;

    [SerializeField] float pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("enter enemy turn");
        if(!opponentStand)
            StartTurn();
        else
            EndTurn();
        
    }

    public override void Tick()
    {
        UpdateHandValueText();
        if(opponentStand)
        {
            EndTurn();
        }
    }

    public override void Exit()
    {
        Debug.Log("exit enemy turn");
    }

    public void StartTurn()
    {
        EnemyTurnBegan?.Invoke();

        if(opponentHand.hand[8] == null)
            opponentHand.GetCard();

        StartCoroutine(EnemyThinkingRoutine(pauseDuration));

        UpdateHandValueText();
    }

    public void EndTurn()
    {
        EnemyTurnEnded?.Invoke();
        playerTotal = playerHand.handValue;
        enemyCardTotal = opponentHand.handValue;

        if(opponentHand.handValue > playerTotal && player.playerStand)
            SetOpponentStand(true);

        if(enemyCardTotal > 20)
            StateMachine.ChangeState<RoundWinState>();
        
        else if(opponentStand && player.playerStand)
        {
            if(playerTotal > enemyCardTotal)
                StateMachine.ChangeState<RoundWinState>();
            else if(playerTotal < enemyCardTotal)
                StateMachine.ChangeState<RoundLoseState>();
            else if(playerTotal == enemyCardTotal)
            {
                tieTextUI.SetActive(true);
                StartCoroutine(ReadTimeRoutine(2));
                StateMachine.ChangeState<SetupCardGameState>();
            }
        }
        else
        StateMachine.ChangeState<PlayerTurnCardGameState>();
    }

    private IEnumerator EnemyThinkingRoutine(float duration)
    {
        //Debug.Log("enemy thinking");
        
        yield return new WaitForSeconds(duration);
        if(CalcMove())
            SetOpponentStand(true);
        //Debug.Log("enemy performs action");
        EndTurn();
    }

    public void UpdateHandValueText()
    {
        opponentHandValueTextUI.text = "" + opponentHand.GetHandValue().ToString();
    }

    public bool CalcMove()
    {
        playerTotal = playerHand.handValue;
        currentPossibleTotal = opponentHand.handValue;

        if(playerTotal > opponentHand.handValue)
            return false;
        if(opponentHand.handValue == 20)
            return true;
        if(player.playerStand && playerTotal < opponentHand.handValue)
            return true;
        if(opponentHand.handValue >= 18 && playerTotal < opponentHand.handValue)
            return true;
        //else if(opponentHand.handValue > playerTotal && player.playerStand)
          //  return true;

        //play a side hand card?
        /*for(int i=0; i<=opponentSide.hand.Length-1; i++)
        {
            opponentSide.SetCurrentCard(i);
            currentPossibleTotal += opponentSide.hand[i].GetComponent<CardScript>().value;

            if(opponentHand.handValue > 20 && currentPossibleTotal <= 20)
            {
                opponentSide.PlayHandCard(opponentHand.gameObject);
                if(opponentHand.handValue >= 18 && playerTotal < opponentHand.handValue)
                    return true;
                else
                    return false;
            }

            if(currentPossibleTotal == 20)
            {
                opponentSide.PlayHandCard(opponentHand.gameObject);
                return true;
            }
            else if(currentPossibleTotal == 19 && playerTotal != 20)
            {
                opponentSide.PlayHandCard(opponentHand.gameObject);
                return true;
            }
            else if(currentPossibleTotal == 18 && playerTotal != 19)
            {
                opponentSide.PlayHandCard(opponentHand.gameObject);
                return true;
            }

        }*/
        return false;
    }

    public void SetOpponentStand(bool stand)
    {
        opponentStand = stand;
    }

    public IEnumerator ReadTimeRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        tieTextUI.SetActive(false);
    }

}
