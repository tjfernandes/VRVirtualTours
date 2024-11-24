using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inworld;

public class TalkingState : BaseState
{
    public override void EnterState(StateManager stateManager)
    {
        // Send Inworld AI greeting trigger in order to start the conversation
        
        Debug.Log("Entered Talking State");
        // Set start button and its panel to inactive
    }

    public override void UpdateState(StateManager stateManager)
    {
        // TODO: When the player asks to play the game, switch to the game state
        // stateManager.SwitchState(stateManager.gameState);
    }

    public override void ExitState(StateManager stateManager)
    {
        // Set start button and its panel to active
        // GameObject buttonObj = stateManager.startButton.gameObject;
        // buttonObj.SetActive(true);
        // buttonObj.transform.parent.gameObject.SetActive(true);
        
        // stateManager.chat.SetActive(false);
    }
}
