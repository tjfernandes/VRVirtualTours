using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndState : BaseState
{
    public override void EnterState(StateManager stateManager)
    {
        Debug.Log("Entered EndState");
    }

    public override void UpdateState(StateManager stateManager)
    {
    }

    public override void ExitState(StateManager stateManager)
    {
    }
}
