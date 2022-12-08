using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideDeck : MonoBehaviour
{
    public Sprite[] cardSprites;
    public int[] cardValues = new int[10];
    public GameObject[] hand;
    public GameObject card = null;

    public DeckManager deck;
    public PlayerTurnCardGameState playerTurn;
    public EnemyTurnCardGameState opponentTurn;

    int currentIndex = 0;
    int currentCard;
    public bool handCardPlayed;
    public GameObject playCardWarningTextUI;
    
    void Start()
    {
        GetCardValues();
    }

    void GetCardValues()
    {
        int num = 0;
        for(int i=0; i < cardSprites.Length; i++)
        {
            num = i-5;
            if(num >= 0)
            {
                num++;
            } 
            cardValues[i] = num;
            num++;
        }
        currentIndex = 0;
    }

    public void Shuffle()
    {
        for(int i = cardValues.Length - 1; i>0; i--)
        {
            int j = Mathf.FloorToInt(Random.Range(0f,1f) * cardValues.Length-1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }
    }

    public void DealHand()
    {
        for(int i=0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().SetSprite(cardSprites[currentIndex]);
            hand[i].GetComponent<CardScript>().SetCardValue(cardValues[currentIndex]);
            currentIndex++;
        }
        //DealCard(card, this.gameObject);
        //DealCard(card, this.gameObject);
        //DealCard(card, this.gameObject);
        //DealCard(card, this.gameObject);
    }

    public GameObject DealCard(GameObject cardPrefab, GameObject user)
    {
        GameObject cardInstance = Instantiate(cardPrefab, user.transform , false);
        cardInstance.GetComponent<CardScript>().SetSprite(cardSprites[currentIndex]);
        cardInstance.GetComponent<CardScript>().SetCardValue(cardValues[currentIndex]);
        //Debug.Log(gameObject.ToString() + " " +currentIndex+ cardSprites[currentIndex].ToString() + " " + cardValues[currentIndex].ToString());
        currentIndex++;
        return cardInstance;

    }

    public void SetCurrentCard(int selectedCard)
    {
        currentCard = selectedCard;
    }

    public void PlayHandCard(GameObject targetHand)
    {
        if(!handCardPlayed)
        {
            targetHand.GetComponent<UserHand>().GetHandCard(hand[currentCard], targetHand);
            //targetHand.GetComponent<UserHand>().handValue += hand[currentCard].GetComponent<CardScript>().value;
            playerTurn.UpdateHandValueText();
            opponentTurn.UpdateHandValueText();
            hand[currentCard].GetComponent<Image>().sprite = null;
            hand[currentCard].GetComponent<Button>().interactable = false;
            handCardPlayed = true;
        }
        else
        {
            playCardWarningTextUI.SetActive(true);
            StartCoroutine(ReadTimeRoutine(2));
        }

    }

    public void ClearHand()
    {
        //if(this.gameObject.transform.childCount != 0)
        //for(int x=this.gameObject.transform.childCount - 1; x>=0; x--)
        //{
        //    Destroy(this.gameObject.transform.GetChild(x).gameObject);
        //}

        for(int i=0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
        }
    }

    private IEnumerator ReadTimeRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        playCardWarningTextUI.SetActive(false);
    }
}
