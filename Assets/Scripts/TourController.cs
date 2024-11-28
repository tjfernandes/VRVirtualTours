using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ButtonArray
{
    public List<Button> buttons = new();
}

public class TourController : MonoBehaviour
{
    public GameObject guider;
    public Transform[] guiderPositions;
    public GameObject[] tourSpheres;
    
    //[Tooltip("Assign the buttons that will trigger transitions between spheres. Each sub-array represents a group of buttons for a specific sphere.")]
    public List<ButtonArray> transitionButtons;
    //public Material[] backgroundMaterials;

    private int currentSphereIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transitionButtons.Count; i++)
        {
            int index = i;

            for (int j = 0; j < transitionButtons[i].buttons.Count; j++)
            {
                transitionButtons[i].buttons[j].onClick.AddListener(() => TransitionToSphere(index));
            }
        }

        // Set the initial background
        TransitionToSphere(currentSphereIndex);
    }

    // Transition to the sphere at the specified index
    void TransitionToSphere(int sphereIndex)
    {       
        currentSphereIndex = sphereIndex;


        // Set the camera position to the sphere's position
        Vector3 cameraPosition = tourSpheres[sphereIndex].transform.position;
        cameraPosition.y -= 1.5f;
        Camera.main.transform.position = cameraPosition;

        // Set the guider's position to the sphere's position
        Vector3 guiderPosition = guiderPositions[sphereIndex].position;
        guider.transform.position = guiderPosition;

        Camera.main.transform.LookAt(guider.transform);

        Vector3 directionToCamera = Camera.main.transform.position - guider.transform.position;
        directionToCamera.y = 0;
        Quaternion rotation = Quaternion.LookRotation(directionToCamera);
        guider.transform.rotation = rotation;

        // Set the background material
        UpdateActiveSphere();
    }

    void UpdateActiveSphere()
    {
        for (int i = 0; i < tourSpheres.Length; i++)
        {
            tourSpheres[i].SetActive(i == currentSphereIndex);       
        }
    }
}
