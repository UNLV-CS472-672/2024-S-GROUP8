using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    private bool inPauseMenu = false;
    private GameObject escMenu;
    private GameObject escButton;
    public Sprite bar;
    // Start is called before the first frame update
    void Start()
    {
        // Add various components to the "Menu" button and the escape menu itself to allow for UI rendering and interaction
        escButton = new GameObject("EscapeButton");
        Canvas canvas = escButton.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        escButton.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        escButton.AddComponent<GraphicRaycaster>();
        escButton.AddComponent<CanvasRenderer>();

        GameObject buttonPanel = CreatePanel("Panel", escButton.transform, new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 1), new Color(0, 0, 0, 0.5f));
        buttonPanel.AddComponent<Image>().color = new Color(255, 255, 255, 255);
        GameObject menuToggleButton = CreateButton(buttonPanel.transform, new Vector2(39, -17), "Menu");

        escMenu = new GameObject("EscapeMenu");
        canvas = escMenu.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        escMenu.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        escMenu.AddComponent<GraphicRaycaster>();
        escMenu.AddComponent<CanvasRenderer>();
        
        GameObject menuPanel = CreatePanel("Panel", escMenu.transform, new Vector2(300, 350), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Color(0, 0, 0, 0.5f));
        menuPanel.AddComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, 255);
        menuPanel.GetComponent<Image>().sprite = bar;
        menuPanel.GetComponent<Image>().type = Image.Type.Sliced;

        // Create text for the escape menu and attach it to escape menu panel
        GameObject text = CreatePanel("Text", menuPanel.transform, new Vector2(200, 200), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Color(0, 0, 0, 0.5f));
        TextMeshProUGUI textMesh = text.AddComponent<TextMeshProUGUI>();
        // Customize the text font size, alightment, color, etc.
        textMesh.text = "Game Paused";
        textMesh.fontSize = 24;
        textMesh.alignment = TextAlignmentOptions.Top;
        textMesh.color = Color.white;

        menuToggleButton.GetComponent<Button>().onClick.AddListener(() => ToggleMenu());

        // Create buttons for the escape menu
        GameObject resumeButton = CreateButton(menuPanel.transform, new Vector2(0, 40), "Resume");
        resumeButton.GetComponent<Button>().onClick.AddListener(() => ToggleMenu());

        // Currently there is no settings menu, so this button does nothing
        GameObject settingsButton = CreateButton(menuPanel.transform, new Vector2(0, -10), "Settings"); 
        settingsButton.GetComponent<Button>().onClick.AddListener(() => {Debug.Log("Settings button clicked");});

        // Quit to main menu is not available yet as we do not have a main menu currently.
        // Otherwise this button will allow the player to return to the main menu from a battle.
        GameObject quitButton = CreateButton(menuPanel.transform, new Vector2(0, -60), "Main Menu");
        quitButton.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("Main Menu");
            Debug.Log("Quit to main menu button clicked");
        });

        // If this button is clicked, close the entire game.
        GameObject exitGameButton = CreateButton(menuPanel.transform, new Vector2(0, -110), "Exit Game");
        exitGameButton.GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("Exit game button clicked");
            Application.Quit();
        });

        escMenu.SetActive(false); // Hide menu by default until the player presses escape
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses escape, open the escape menu until the player either chooses an option or closes the menu with escape again
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
    }
    GameObject CreateButton(Transform parent, Vector2 anchoredPosition, string text)
    {
        // Create the button and customize its background and clickbox size
        GameObject button = new GameObject(text + "Button");
        button.transform.SetParent(parent, false);
        RectTransform buttonRT = button.AddComponent<RectTransform>();
        buttonRT.sizeDelta = new Vector2(80, 35);
        buttonRT.anchoredPosition = anchoredPosition;
        button.AddComponent<Image>().color = new Color(255f, 255f, 255f, 1);
        button.GetComponent<Image>().sprite = bar;
        button.GetComponent<Image>().type = Image.Type.Sliced;

        // Add and customize the text that is held inside of the button
        GameObject textObject = new GameObject(text);
        textObject.transform.SetParent(button.transform, false);
        RectTransform textRT = textObject.AddComponent<RectTransform>();
        textRT.sizeDelta = Vector2.zero;
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;

        // Customize the text font size, alightment, color, etc.
        TextMeshProUGUI uiText = textObject.AddComponent<TextMeshProUGUI>();
        uiText.text = text;
        uiText.fontSize = 14;
        uiText.alignment = TextAlignmentOptions.Center;
        uiText.color = Color.white;

        // Add a button compononent to listen for and manage for click events
        button.AddComponent<Button>();
        return button;
    }
    // Configures a panel to attach menu options/buttons to
    GameObject CreatePanel(string name, Transform parent, Vector2 sizeDelta, Vector2 ancMin, Vector2 ancMax, Color color)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent, false);
        RectTransform rectTransform = panel.AddComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.anchorMin = ancMin;
        rectTransform.anchorMax = ancMax;
        rectTransform.sizeDelta = sizeDelta;
        return panel;
    
    }
    public void ToggleMenu()
    {
        inPauseMenu = !inPauseMenu; // Toggle escape menu when esc is pressed
        escMenu.SetActive(inPauseMenu);
        Time.timeScale = inPauseMenu ? 0 : 1;  // Pause game when in escape menu
    }
}