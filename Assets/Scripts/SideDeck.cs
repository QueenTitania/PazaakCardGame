using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDeck : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[10];
    public GameObject card = null;

    int currentIndex;
    
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
            cardValues[i] = num;
            num++;
        }
        currentIndex = 1;
    }

    public void Shuffle()
    {
        for(int i = cardSprites.Length - 1; i>0; i--)
        {
            int j = Mathf.FloorToInt(Random.Range(0f,1f) * cardSprites.Length-1) + 1;
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
        DealCard(card, this.gameObject);
        DealCard(card, this.gameObject);
        DealCard(card, this.gameObject);
        DealCard(card, this.gameObject);
    }

    public GameObject DealCard(GameObject cardPrefab, GameObject user)
    {
        GameObject cardInstance = Instantiate(cardPrefab, user.transform , false);
        cardPrefab.GetComponent<CardScript>().SetSprite(cardSprites[currentIndex]);
        cardPrefab.GetComponent<CardScript>().SetCardValue(cardValues[currentIndex]);
        currentIndex++;
        return cardInstance;
    }

    public void ClearHand()
    {
        if(this.gameObject.transform.childCount != 0)
        for(int x=this.gameObject.transform.childCount - 1; x>=0; x--)
        {
            Destroy(this.gameObject.transform.GetChild(x).gameObject);
        }
    }
}
