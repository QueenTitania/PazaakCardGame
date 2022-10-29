using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState => currentState;
    protected bool InTransition { get; private set;}

    State currentState;
    protected State previousState;

    public void ChangeState<T>() where T : State
    {
        T targetState = GetComponent<T>();
        if(targetState == null)
        {
            Debug.Log("Cannot change to stste");
            return;
        }

        InitiateStateChange(targetState);
    }

    public void RevertState()
    {
        if(previousState != null)
            InitiateStateChange(previousState);
    }

    public void InitiateStateChange(State targetState)
    {
        if(currentState != targetState && !InTransition)
        {
            Transition(targetState);
        }
    }

    void Transition(State newState)
    {
        InTransition = true;
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
        InTransition = false;
    }

    private void Update()
    {
        if(CurrentState != null && !InTransition)
        {
            CurrentState.Tick();
        }
    }

}
