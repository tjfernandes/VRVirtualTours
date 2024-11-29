using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourController : MonoBehaviour
{
    public GameObject guider;
    public GameObject buttonPrefab;
    public GameObject[] spots;
    public GameObject xROrigin;
    public Material[] skyboxMaterials;

    private Transform[] guiderPositions;
    private List<Transform>[] buttonsPositions;
    private int currentSpotIndex = 0;

    void Awake()
    {
        guiderPositions = new Transform[spots.Length];
        buttonsPositions = new List<Transform>[spots.Length];

        for (int i = 0; i < spots.Length; i++)
        {
            // Find the guider's position for each spot
            guiderPositions[i] = spots[i].transform.Find("MockCharacter");

            // Create the buttons for each spot
            buttonsPositions[i] = new List<Transform>();
            // Find the mockButtons GameObject
            Transform mockButtons = spots[i].transform.Find("MockButtons");
            if (mockButtons != null)
            {
                // Find and add button positions for each spot
                foreach (Transform child in mockButtons)
                {
                    Debug.Log("Found button at position: " + child.position + " for spot " + i);
                    buttonsPositions[i].Add(child);
                }
            }
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TourController Start");
        CreateButtons(0);
        
        // Set the initial background
        MoveToPosition(0);
    }

    private void CreateButtons(int index)
    {
        // Get the list of button positions for the specified spot index
        List<Transform> spotButtonPositions = buttonsPositions[index];
        
        for (int i = 0; i < spotButtonPositions.Count; i++)
        {
            // Find the Canvas component inside the current spot
            Transform spotCanvas = spots[index].transform.Find("Canvas");

            if (spotCanvas != null)
            {
                // Instantiate the button prefab at the current spot position
                GameObject button = Instantiate(buttonPrefab, spotButtonPositions[i].position, Quaternion.identity);

                // button should face the xrOrigin
                button.transform.LookAt(xROrigin.transform);
                // Set the Canvas as the parent of the button
                button.transform.SetParent(spotCanvas, false);

                // Set the button's onClick event to move to the corresponding position
                // The button's index is the digit in "MockButton" GameObject name
                currentSpotIndex = Int32.Parse(spotButtonPositions[i].gameObject.name.Substring(10))-1;
                button.GetComponent<Button>().onClick.AddListener(() => MoveToPosition(currentSpotIndex));
            }
            else
            {
                Debug.LogWarning("Canvas not found in spot " + i);
            }
        }
    }

        // Transition to the position in the room at the specified index
        void MoveToPosition(int spotIndex)
        {       
            Debug.Log("Moving to position " + spotIndex);
            // Destroy the current buttons
            foreach (Transform child in spots[currentSpotIndex].transform.Find("Canvas"))
            {
                Destroy(child.gameObject);
            }

            // Set the guider's new position
            Vector3 guiderPosition = guiderPositions[spotIndex].position;
            guider.transform.position = guiderPosition;

            xROrigin.transform.LookAt(guider.transform);

            Vector3 directionToRig = xROrigin.transform.position - guider.transform.position;
            directionToRig.y = 0;
            Quaternion rotation = Quaternion.LookRotation(directionToRig);
            guider.transform.rotation = rotation;

            // Set the background material
            RenderSettings.skybox = skyboxMaterials[spotIndex];
        }
    }
