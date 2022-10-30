using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinState : CardGameState
{
    [SerializeField] GameObject gameWinPanel = null;
    
    public override void Enter()
    {
        Debug.Log("enter game win state");
        gameWinPanel.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        Debug.Log("exit game win state");
        gameWinPanel.gameObject.SetActive(false);
    }


}
