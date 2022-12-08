using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTracker : MonoBehaviour
{

    public int userWins;

    public GameObject win1Img;
    public GameObject win2Img;
    public GameObject win3Img;

    public void UpdateWinUI(int wins)
    {
        if(wins >= 1)
        {
            win1Img.SetActive(true);
        }
        if(wins >= 2)
        {
            win2Img.SetActive(true);
        }
        if(wins >= 3)
        {
            win3Img.SetActive(true);
        }
    }

    public void ResetWinUI()
    {
        win1Img.SetActive(false);
        win2Img.SetActive(false);
        win3Img.SetActive(false);
    }

}
