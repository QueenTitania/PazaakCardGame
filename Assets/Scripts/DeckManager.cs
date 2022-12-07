using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Sprite[] cardSprites;
    public int[] cardValues = new int[40];

    public int currentIndex;
    
    void Start()
    {
        GetCardValues();
    }

    void GetCardValues()
    {
        int num = 0;
        for(int i=0; i < cardSprites.Length; i++)
        {
            num = i+1;
            num %= 10;
            if(num == 0)
                num = 10;
            cardValues[i] = num;
            num++;
        }
        currentIndex = 0;
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
    
    public GameObject DealCard(GameObject cardPrefab, GameObject user)
    {
        GameObject cardInstance = Instantiate(cardPrefab, user.transform , false);
        cardInstance.GetComponent<CardScript>().SetSprite(cardSprites[currentIndex]);
        cardInstance.GetComponent<CardScript>().SetCardValue(cardValues[currentIndex]);
        Debug.Log(gameObject.ToString() + " " +currentIndex+ cardSprites[currentIndex].ToString() + " " + cardValues[currentIndex].ToString());
        currentIndex++;
        return cardInstance;
    }

    public GameObject HandCard(GameObject handCard, GameObject user)
    {
        GameObject cardInstance = Instantiate(handCard, user.transform , false);
        return cardInstance;
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }

}
