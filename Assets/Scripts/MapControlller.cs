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
        int spriteIndex = 0; // Counter for sprite array indexing

        for (int i = 0; i < totalPanels; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);

            // Check if the scene is not the current scene
            if (scenePath == SceneManager.GetActiveScene().path)
            {
                spriteIndex++; // Skip the sprite for the current scene as well
                continue;
            }

            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            Sprite sceneSprite = sceneSprites[spriteIndex]; // Use sprite index that corresponds to this scene
            spriteIndex++; // Increment sprite counter for next scene

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
        // Find the correct gameData index by counting non-current scenes before this index
        int gameDataIndex = 0;
        for (int i = 1; i < sceneIndex; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if (scenePath != SceneManager.GetActiveScene().path)
            {
                gameDataIndex++;
            }
        }
        
        InworldController.Instance.LoadScene(((InworldGameData) sceneGameDatas[gameDataIndex]).sceneFullName);
        SceneManager.LoadScene(sceneIndex);
    }

}
