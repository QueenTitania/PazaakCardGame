using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoseState : CardGameState
{
    [SerializeField] GameObject gameLosePanel = null;

    public override void Enter()
    {
        Debug.Log("enter game lose state");
        gameLosePanel.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        Debug.Log("exit game lose state");
        gameLosePanel.gameObject.SetActive(false);

    }
}
