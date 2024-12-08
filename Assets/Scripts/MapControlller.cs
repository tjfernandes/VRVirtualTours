using System.Collections;
using Inworld;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapControlller : MonoBehaviour
{
    public GameObject scenePanelPrefab;
    public Transform listParent;
    public Button closeButton;
    public ScriptableObject[] sceneGameDatas;
    public Sprite[] sceneSprites;

    void Start()
    {
        ListAreas();
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));
    }

    void ListAreas()
    {
        GridLayoutGroup gridLayout = listParent.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            // Set padding (left, right, top, bottom)
            gridLayout.padding = new RectOffset(100, 100, 100, 100);

            // Set spacing (x, y)
            gridLayout.spacing = new Vector2(150, 200);
        }

        int totalPanels = SceneManager.sceneCountInBuildSettings;

        for (int i = 1; i < totalPanels; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            
            // Check if the scene is not the current scene
            if (scenePath == SceneManager.GetActiveScene().path)
            {
                continue;
            }

            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            Sprite sceneSprite = sceneSprites[i-1]; // Subtract 1 because the first scene is the menu scene

            // Instantiate the panel object with listParent as its parent
            GameObject panelObj = Instantiate(scenePanelPrefab, listParent, false);

            // Set the text and image
            TMP_Text textComponent = panelObj.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = sceneName;
            }

            Image imageComponent = panelObj.GetComponentInChildren<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = sceneSprite;
            }

            panelObj.name = sceneName + " Panel";
            AddClickEvent(panelObj, i);
        }
    }

    void AddClickEvent(GameObject panelObj, int sceneIndex)
    {
        Button button = panelObj.GetComponent<Button>();
        if (button != null)
        {
            Debug.Log("Adding click event to " + panelObj.name);
            button.onClick.AddListener(() => LoadScene(sceneIndex));
        }
    }

    void LoadScene(int sceneIndex)
    {
        InworldController.Instance.LoadScene(((InworldGameData) sceneGameDatas[sceneIndex-1]).sceneFullName);
        SceneManager.LoadScene(sceneIndex);
    }

}
