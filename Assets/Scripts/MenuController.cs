using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject enterButton;
    [SerializeField] private GameObject exitButton;

    private void Start()
    {
        enterButton.GetComponent<Button>().onClick.AddListener(OnEnterButtonClicked);
        exitButton.GetComponent<Button>().onClick.AddListener(OnExitButtonClicked);
    }
    

    private void OnEnterButtonClicked()
    {
        // Load the initial scene with index 1
        SceneManager.LoadScene(1);
    }

    private void OnExitButtonClicked()
    {
        // Exit the application
        Application.Quit();
    }
}
