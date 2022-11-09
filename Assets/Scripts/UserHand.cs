using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHand : MonoBehaviour
{

    public CardScript cardScript;
    public DeckManager deckScript;

    public int handValue;

    public GameObject[] hand;
    public int cardIndex;
    
    void StartHand()
    {
        GetCard();
    }

    public int GetCard()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        handValue += cardValue;
        cardIndex++;

        return handValue;
    }

}
