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
    private TextMeshProUGUI player1Text;
    private TextMeshProUGUI player2Text;

    // Start is called before the first frame update
    /* Start creates all objects and components needed to create */
    void Start()
    {
        TMP_FontAsset font = Resources.Load<TMP_FontAsset>("LiberationSans.ttf");
        GameObject Canvas = GameObject.Find("Canvas");

        // create the panel which displays all needed information
        panel = new GameObject("PlayerList");
        panel.transform.parent = Canvas.transform;
        panel.AddComponent<CanvasRenderer>();
        image = panel.AddComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
        // EventTrigger is used to toggle visibility of PlayerList
        EventTrigger et = panel.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { ToggleVisibility(); });
        et.triggers.Add(entry);

        // fix panel position to middle right of screen
        panel.GetComponent<RectTransform>().localPosition = Vector3.zero;
        panel.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        panel.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        panel.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);

        // add text for player 1 to panel
        Player CurrentPlayer = PlayerController.CurrentPlayer; // get next player from controller
        GameObject temp = new GameObject("Player" + CurrentPlayer.GetName() + "_Text");
        player1Text = temp.AddComponent<TextMeshProUGUI>();
        player1Text.transform.parent = panel.transform;
        // align text within panel
        player1Text.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        player1Text.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        player1Text.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
        player1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 82);
        player1Text.GetComponent<RectTransform>().localPosition = new Vector3(50, 0, 0);
        // change how text is displayed
        player1Text.text = "Player " + CurrentPlayer.GetName() + "\nTiles: " + CurrentPlayer.getTilesControlledCount();
        player1Text.color = CurrentPlayer.GetColor().color;
        player1Text.font = font;
        player1Text.fontSize = 10;

        // add text for player 2 to panel
        Controller.NextPlayer();
        CurrentPlayer = PlayerController.CurrentPlayer; // get next player from controller
        temp = new GameObject("Player" + CurrentPlayer.GetName() + "_Text");
        player2Text = temp.AddComponent<TextMeshProUGUI>();
        player2Text.transform.parent = panel.transform;
        // align text within panel
        player2Text.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        player2Text.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        player2Text.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
        player2Text.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 82);
        player2Text.GetComponent<RectTransform>().localPosition = new Vector3(50, -40, 0);
        // change how text is displayed
        player2Text.text = "Player " + CurrentPlayer.GetName() + "\nTiles: " + CurrentPlayer.getTilesControlledCount();
        player2Text.color = CurrentPlayer.GetColor().color;
        player2Text.font = font;
        player2Text.fontSize = 10;
    }

    // Update is called once per frame
    /* Update() should be used to change any displayed text during the game */
    void Update()
    {
        Controller.NextPlayer();
        player1Text.text = "Player " + PlayerController.CurrentPlayer.GetName() + "\nTiles: " + PlayerController.CurrentPlayer.getTilesControlledCount();
        Controller.NextPlayer();
        player2Text.text = "Player " + PlayerController.CurrentPlayer.GetName() + "\nTiles: " + PlayerController.CurrentPlayer.getTilesControlledCount();
    }

    public void ToggleVisibility(){
        if(visible){ // move panel out-of-view
            panel.GetComponent<RectTransform>().localPosition = new Vector3(466, 0, 0);
            player1Text.GetComponent<RectTransform>().localPosition = new Vector3(1000, 1000, 1000);
            player2Text.GetComponent<RectTransform>().localPosition = new Vector3(1000, 1000, 1000);
            image.color = new Color(0, 0, 0, 0.5f);
            visible = false;
        }
        else{ // move panel in-view
            panel.GetComponent<RectTransform>().localPosition = new Vector3(400, 0, 0);
            player1Text.GetComponent <RectTransform>().localPosition = new Vector3(50, 0, 0);
            player2Text.GetComponent <RectTransform>().localPosition = new Vector3(50, -40, 0);
            image.color = new Color(0, 0, 0, 1);
            visible = true;
        }
    }
}
