using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(StateManager stateManager);
    public abstract void UpdateState(StateManager stateManager);
    public abstract void ExitState(StateManager stateManager);
}
