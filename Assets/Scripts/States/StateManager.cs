using System;
using Inworld;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{

    #region State management
        [NonSerialized] public BaseState currentState;

        // Create instances of the states
        [NonSerialized] public InitialState initialState = new();
        [NonSerialized] public GameState gameState = new();
        [NonSerialized] public TalkingState talkingState = new();
        [NonSerialized] public EndState endState = new();
    #endregion

    [NonSerialized] public UIManager uIManager;

    void Awake()
    {
        uIManager = GetComponent<UIManager>();
        
        currentState = initialState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

}
