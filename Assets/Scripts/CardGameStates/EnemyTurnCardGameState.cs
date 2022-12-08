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
    [SerializeField] public GameObject StandTextUI = null;
    public PlayerTurnCardGameState player;

    public UserHand opponentHand;
    public SideDeck opponentSide;

    public UserHand playerHand;

    int currentPossibleTotal;
    int playerTotal;

    public int enemyCardTotal = 0;
    public bool opponentStand = false;
        int bestCardIndex = -1;
        int bestTotal = 0;

    [SerializeField] float pauseDuration = 1.5f;

    public override void Enter()
    {
        //Debug.Log("enter enemy turn");
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
        //Debug.Log("exit enemy turn");
    }

    public void StartTurn()
    {
        EnemyTurnBegan?.Invoke();

        opponentSide.handCardPlayed = false;
        StandTextUI.SetActive(false);

        if(opponentHand.hand[8] == null)
            opponentHand.GetCard();

        StartCoroutine(EnemyThinkingRoutine(pauseDuration));

        UpdateHandValueText();
    }

    public void EndTurn()
    {
        EnemyTurnEnded?.Invoke();
        
        Debug.Log("index " + bestCardIndex.ToString() + " total " + bestTotal.ToString());

        playerTotal = playerHand.handValue;
        enemyCardTotal = opponentHand.handValue;

        if(opponentStand)
            StandTextUI.SetActive(true);

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
        bool playCard = false;
        bestCardIndex = -1;
        bestTotal = 0;

        if(playerTotal > opponentHand.handValue)
            return false;
        if(opponentHand.handValue == 20)
            return true;
        if(player.playerStand && playerTotal < opponentHand.handValue)
            return true;
        if((opponentHand.handValue >= 18 && opponentHand.handValue <= 20) && playerTotal < opponentHand.handValue)
            return true;
        
        //check possible totals
        for(int i=0; i<opponentSide.hand.Length; i++)
        {
            if(bestCardIndex != -1)
                bestTotal = opponentSide.hand[bestCardIndex].GetComponent<CardScript>().GetCardValue();

            currentPossibleTotal += opponentSide.hand[i].GetComponent<CardScript>().GetCardValue();
            Debug.Log("current total " + currentPossibleTotal.ToString());
            if(opponentHand.handValue > 20)
            {
            Debug.Log("neg");
                if(currentPossibleTotal <= 20)
                    {
                        playCard = true;
                        bestCardIndex = i;
                        Debug.Log("play neg");
                    }
            }
            else
            {

            if(bestTotal < 20)
                if(currentPossibleTotal > bestTotal)
                {
                    if(currentPossibleTotal == 20)
                    {
                        playCard = true;
                        bestCardIndex = i;
                        bestTotal = currentPossibleTotal;
                        Debug.Log("new best total " + bestTotal.ToString());
                    }
                    else if(currentPossibleTotal == 19 && playerTotal < 20)
                    {
                        playCard = true;
                        bestCardIndex = i;
                        bestTotal = currentPossibleTotal;
                        Debug.Log("new best total " + bestTotal.ToString());
                    }
                    else if(currentPossibleTotal == 18 && playerTotal < 19)
                    {
                        playCard = true;
                        bestCardIndex = i;
                        bestTotal = currentPossibleTotal;
                        Debug.Log("new best total " + bestTotal.ToString());
                    }
                    else
                        Debug.Log("no new best total");
                }
            else if(bestTotal == 20)
            {
                Debug.Log("already best total");
                playCard = true;
            }
            else
            {
                Debug.Log("no new best total");
            }
            }
            currentPossibleTotal = opponentHand.handValue;
        }
        
        /*
            for(int i=0; i<opponentSide.hand.Length; i++)
            {
                if(currentPossibleTotal > 20)
                {
                    currentPossibleTotal += opponentSide.hand[i].GetComponent<CardScript>().GetCardValue();

                    if(currentPossibleTotal <= 20)
                    {
                        playCard = true;
                        bestCardIndex = i;
                        Debug.Log("play neg");
                    }
                }
                    
            }
        */
        if(playCard)
        {
            //play card
            opponentSide.SetCurrentCard(bestCardIndex);
            opponentSide.PlayHandCard(opponentHand.gameObject);
            if(opponentHand.handValue >= 18)
                SetOpponentStand(true);
            
        }

        

        return false;

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

        }
        return false;*/
    }

    public void SetOpponentStand(bool stand)
    {
        opponentStand = stand;
        if(!stand)
            StandTextUI.SetActive(false);
    }

    public IEnumerator ReadTimeRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        tieTextUI.SetActive(false);
    }

}
