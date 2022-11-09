using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[41];

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
            num = i;
            num %= 10;
            cardValues[i] = num++;
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

    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetCardValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetCardValue();
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }

}