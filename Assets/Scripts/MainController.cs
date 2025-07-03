using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Inworld;
using Unity.VisualScripting;
using Inworld.AEC;
using Inworld.Interactions;
using Inworld.Packet;
using System;
using UnityEngine.SceneManagement;
using Inworld.Assets;
using Inworld.Sample.RPM;

/**
* This controller is responsible to manage the user interactions
* and send them to the InworldController.
*/
public class MainController : MonoBehaviour
{
    public GameObject audioCapture;
    private StateManager stateManager;
    private UIManager uiManager;

    private bool isMicToggleAllowed = true;
    private float micToggleCooldown = 0.5f; // 500 milliseconds

    void Awake()
    {
        stateManager = GetComponent<StateManager>();
        uiManager = GetComponent<UIManager>();
    }
    

    void Start()
    {
        StartCoroutine(GreetPlayer());
    }

    private IEnumerator GreetPlayer()
    {
        Debug.Log("Greeting Player");
        while(InworldController.CurrentCharacter == null)
        {
            yield return null;
        }
        Debug.Log("Character Found");
        InworldController.CurrentCharacter.SendTrigger("greeting", false);
    }

    #region Inworld Custom Event Listeners
        public void OnBeginSpeaking(string characterName)
        {
            uiManager.DeactivateChatComponents();
            audioCapture.SetActive(false);
        }

        public void OnEndSpeaking(string characterName)
        {
            uiManager.ActivateChatComponents();
        }

        public void OnGoalComplete(string brainName, string trigger)
        {
            if (trigger == "greeting")
            {
                Debug.Log("Current Scene: " + InworldController.Instance.CurrentScene);
            }
            if (trigger == "start_game")
            {
                InworldController.CurrentCharacter.SendTrigger("start_questions", false);
            }
            if (trigger == "start_questions")
            {
                StartCoroutine(SwitchToGameMode());
            }
            if (trigger == "forfeit_game")
            {
                uiManager.AskEndQuizConfirmation();
            }
            if (trigger == "forfeit_confirm")
            {
                uiManager.ConfirmEndQuiz();
                
                // Change the state to the talking state
                stateManager.SwitchState(stateManager.talkingState);
            }
            if (trigger == "finish_stage")
            {
                stateManager.SwitchState(stateManager.endState);
            }

            if (trigger == "point_behind")
            {
                Debug.Log("GOAL ACTIVATED: " + trigger + " .... Pointing behind");
                InworldController.CurrentCharacter.Animator.SetTrigger("PointBackTrigger");
            }

            if (trigger == "point_front")
            {
                Debug.Log("Pointing front");
                InworldController.CurrentCharacter.Animator.SetTrigger("PointFrontTrigger");
            }
        }
    #endregion

    private IEnumerator SwitchToGameMode()
    {
        // Wait until character is not speaking
        yield return new WaitUntil(() => !InworldController.CurrentCharacter.IsSpeaking);

        // Now that isSpeaking is false, switch to the game state
        stateManager.SwitchState(stateManager.gameState);    
    }


    private IEnumerator MicToggleCooldown()
    {
        isMicToggleAllowed = false;
        yield return new WaitForSeconds(micToggleCooldown);
        isMicToggleAllowed = true;
    }

    public void OnPlayerStartSpeaking()
    {
        if (isMicToggleAllowed && audioCapture != null)
        {
            audioCapture.SetActive(true);
            StartCoroutine(MicToggleCooldown());
        }
    }

    public void OnPlayerStopSpeaking()
    {
        if (isMicToggleAllowed && audioCapture != null)
        {
            audioCapture.SetActive(false);
            StartCoroutine(MicToggleCooldown());
        }
    }

}

