using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate{};
    public event Action PressedCancel = delegate{};
    public event Action PressedLeft = delegate{};
    public event Action PressedRight = delegate{};

    public event Action EndTurn = delegate{};
    public event Action PressedStand = delegate{};

    public event Action PressWin = delegate{};
    public event Action PressLose = delegate{};

    void Update()
    {
        DetectConfirm();
        DetectCancel();
        DetectLeft();
        DetectRight();
        DetectEndTurn();
        DetectStand();
        DetectWin();
        DetectLose();
    }

    private void DetectRight()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            PressedRight?.Invoke();
        }
    }

    private void DetectLeft()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PressedLeft?.Invoke();
        }
    }

    private void DetectCancel()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PressedCancel?.Invoke();
        }
    }

    private void DetectConfirm()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PressedConfirm?.Invoke();
        }
    }

    private void DetectEndTurn()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            EndTurn?.Invoke();
        }
    }

    private void DetectStand()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            PressedStand?.Invoke();
        }
    }

    private void DetectWin()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            PressWin?.Invoke();
        }
    }

    private void DetectLose()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            PressLose?.Invoke();
        }
    }

}
