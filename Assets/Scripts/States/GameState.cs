using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : BaseState
{
    private GameObject quizPanel;
    private QuizController quizController;

    public override void EnterState(StateManager stateManager)
    {
        Debug.Log("Entered Game State");

        // UI elements management when entering the game state
        stateManager.uIManager.ChangeToQuizMode();

        // Quiz Controller should reset
        quizController = stateManager.transform.GetComponent<QuizController>();
        quizController.ResetQuiz();
    }

    public override void UpdateState(StateManager stateManager)
    {
    }

    public override void ExitState(StateManager stateManager)
    {
        stateManager.uIManager.ExitQuizMode();
    }
}
