using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHand : MonoBehaviour
{

    public CardScript cardScript;
    public DeckManager deckScript;
    [SerializeField] public GameObject cardPrefab;

    public int handValue;

    public GameObject[] hand;
    public int cardIndex = 0;
    

    public void GetCard()
    {
        //int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        hand[cardIndex] = deckScript.DealCard(cardPrefab, this.gameObject);
        //int cardValue = hand[cardIndex].GetComponent<CardScript>().GetCardValue();

        //hand[cardIndex].GetComponent<Renderer>().enabled = true;
        cardPrefab.GetComponent<Renderer>().enabled = true;
        handValue += hand[cardIndex].GetComponent<CardScript>().GetCardValue();
        cardIndex++;
        Debug.Log("hand value: " + handValue);
    }

}
