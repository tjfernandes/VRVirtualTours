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

    private int numberOfColumns = 3; // Number of columns for the grid layout
    private float spacing = 50f; // Spacing value between each panel

    void Start()
    {
        ListAreas();
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));
    }

    void ListAreas()
    {
        int totalPanels = SceneManager.sceneCountInBuildSettings;
        int numberOfRows = Mathf.CeilToInt((float)totalPanels / numberOfColumns);

        for (int i = 0; i < totalPanels; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            Sprite sceneSprite = sceneSprites[i];

            // Instantiate the panel object with listParent as its parent
            GameObject panelObj = Instantiate(scenePanelPrefab, listParent, false);

            // Calculate the position based on the index, number of columns, and spacing
            int row = i / numberOfColumns;
            int column = i % numberOfColumns;
            float xPos = column * (scenePanelPrefab.GetComponent<RectTransform>().rect.width + spacing);
            float yPos = -row * scenePanelPrefab.GetComponent<RectTransform>().rect.height;
            panelObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

            // Set the text and image
            TMP_Text textComponent = panelObj.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = sceneName;
            }

            Image imageComponent = panelObj.GetComponentInChildren<Image>();
            if (imageComponent != null)
            {
                Sprite originalSprite = imageComponent.sprite;
                imageComponent.sprite = sceneSprite;

                // Ensure the image fits inside the original imageComponent sprite and maintains its size
                AspectRatioFitter aspectRatioFitter = imageComponent.GetComponent<AspectRatioFitter>();
                if (aspectRatioFitter == null)
                {
                    aspectRatioFitter = imageComponent.gameObject.AddComponent<AspectRatioFitter>();
                }
            aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            aspectRatioFitter.aspectRatio = originalSprite.rect.width / originalSprite.rect.height;
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
        SceneManager.LoadScene(sceneIndex);

        InworldController.Instance.GameData = (InworldGameData) sceneGameDatas[sceneIndex];
        InworldController.Instance.LoadData((InworldGameData) sceneGameDatas[sceneIndex]);
        InworldController.Instance.LoadScene(((InworldGameData) sceneGameDatas[sceneIndex]).sceneFullName);
    }
}
