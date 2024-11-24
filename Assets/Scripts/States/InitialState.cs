using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialState : BaseState
{

    public override void EnterState(StateManager stateManager)
    {
        Debug.Log("Entered Initial State");
    }

    public override void UpdateState(StateManager stateManager)
    {
    }

    public override void ExitState(StateManager stateManager)
    {
    }

    
}
