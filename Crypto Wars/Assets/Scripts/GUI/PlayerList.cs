using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerList : MonoBehaviour
{
    public PlayerController Controller;
    public Boolean visible = true;
    public Image image;
    private GameObject panel;
    public Sprite bar;
    public GameObject element;
    public List<GameObject> elementList;
    public static bool updateMenu = false;
    public static bool updateCurrentPlayer = false;
    public Sprite goldBar;
    public Sprite selectBar;

    // Start is called before the first frame update
    /* Start creates all objects and components needed to create */
    void Start()
    {
        elementList = new List<GameObject>();

        TMP_FontAsset font = Resources.Load<TMP_FontAsset>("LiberationSans.ttf");
        GameObject Canvas = GameObject.Find("Canvas");

        // create the panel which displays all needed information
        panel = new GameObject("PlayerList");
        panel.transform.parent = Canvas.transform;
        panel.AddComponent<CanvasRenderer>();
        image = panel.AddComponent<Image>();
        image.color = new Color(255, 255, 255, 255);
        image.sprite = bar;
        image.type = Image.Type.Sliced;
        // EventTrigger is used to toggle visibility of PlayerList
        EventTrigger et = panel.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { ToggleVisibility(); });
        et.triggers.Add(entry);

        // fix panel position to middle right of screen
        panel.GetComponent<RectTransform>().localPosition = new Vector3(0, 40, 0);
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2 (120, 200);
        panel.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        panel.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        panel.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
        panel.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);

        panel.AddComponent<GridLayoutGroup>();
        RectOffset rect = new RectOffset();
        rect.left = -10;
        rect.right = 0;
        rect.top = 3;
        rect.bottom = 0;
        panel.GetComponent<GridLayoutGroup>().padding = rect;
        panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(140f, 45f);

        // add text to panel
        for (int i = 0; i < Controller.GetNumberOfPlayers(); i++){
            elementList.Add(Instantiate(element));
            elementList[i].gameObject.transform.parent = panel.transform;
            elementList[i].GetComponent<RectTransform>().localScale = new Vector3(.75f, .75f, .75f);
            elementList[i].name = "ListElement" + (i + 1);
            elementList[i].transform.Find("PlayerIcon").GetComponent<Image>().color = PlayerController.players[i].GetColor().color;
            elementList[i].transform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = PlayerController.players[i].GetName();
            elementList[i].transform.Find("TileInfo").GetComponent<TextMeshProUGUI>().text = "Tiles: 0";
            if (PlayerController.players[i].GetName().Equals(PlayerController.CurrentPlayer.GetName())){
                elementList[i].GetComponent<Image>().sprite = selectBar;
            } 
        }
    }

    // Update is called once per frame
    /* Update() should be used to change any displayed text during the game */
    void Update()
    {
        if (updateMenu) {
            for (int i = 0; i < Controller.GetNumberOfPlayers(); i++)
            {
                elementList[i].transform.Find("TileInfo").GetComponent<TextMeshProUGUI>().text = "Tiles: " + PlayerController.players[i].getTilesControlledCount();
            }
            updateMenu = false;
        }
        if (updateCurrentPlayer) {
            for (int i = 0; i < Controller.GetNumberOfPlayers(); i++)
            {
                if (PlayerController.players[i].GetName().Equals(PlayerController.CurrentPlayer.GetName())){
                    elementList[i].GetComponent<Image>().sprite = selectBar;
                }
                else {
                    elementList[i].GetComponent<Image>().sprite = goldBar;
                }
            }
            updateCurrentPlayer = false;
        }
    }

    public void ToggleVisibility(){
        if(visible){ // move panel out-of-view
            panel.GetComponent<RectTransform>().localPosition = new Vector3(400, 0, 0);
            image.color = new Color(0, 0, 0, 1);
            visible = false;
        }
        else{ // move panel in-view
            panel.GetComponent<RectTransform>().localPosition = new Vector3(448, 0, 0);
            image.color = new Color(0, 0, 0, 0.5f);
            visible = true;
        }
    }
}
