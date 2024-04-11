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

        // add text to panel
        for(int i = 0; i < Controller.GetNumberOfPlayers(); i++){
            Player CurrentPlayer = PlayerController.CurrentPlayer; // get next player from controller
            GameObject temp = new GameObject("Player" + CurrentPlayer.GetName() + "_Text");
            TextMeshProUGUI playerText = temp.AddComponent<TextMeshProUGUI>();
            playerText.transform.parent = panel.transform;
            // align text within panel
            playerText.GetComponent<RectTransform>().localPosition = Vector3.zero;
            playerText.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
            playerText.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            playerText.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
            playerText.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 82);
            if(i > 0){ // moves text further down panel dependant on number of players
                playerText.GetComponent<RectTransform>().localPosition = new Vector3(50, -40*i, 0);
            }
            // change how text is displayed
            playerText.text = "Player " + CurrentPlayer.GetName() + "\n<info>";
            playerText.color = CurrentPlayer.GetColor().color;
            playerText.font = font;
            playerText.fontSize = 10;

            Controller.NextPlayer();
        }
    }

    // Update is called once per frame
    /* Update() should be used to change any displayed text during the game */
    void Update()
    {
        
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
