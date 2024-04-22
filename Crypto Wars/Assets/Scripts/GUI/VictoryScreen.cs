using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public WinConditions wcScript;
    private GameObject Panel;
    private Boolean gameOver;
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver){ // prevent infinite insantiation of victory screen
            if(wcScript.winningPlayer != null){
                gameOver = true;
                displayVictoryGUI();
            }
        }        
    }


    void displayVictoryGUI(){
        GameObject Canvas = GameObject.Find("Canvas");
        Player winner = wcScript.winningPlayer;

        // create the panel which displays all needed information
        Panel = new GameObject("VictoryPanel");
        Panel.transform.parent = Canvas.transform;
        Panel.AddComponent<CanvasRenderer>();
        Panel.AddComponent<GraphicRaycaster>();
        image = Panel.AddComponent<Image>();
        image.color = new Color(0, 0, 0, 1);

        // fix panel to middle of screen
        Panel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 350);
        Panel.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        // Create/align/display winner text
        GameObject text = new GameObject("WinnerText");
        text.transform.parent = Panel.transform;
        TextMeshProUGUI winnerText = text.AddComponent<TextMeshProUGUI>();
        winnerText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 125);
        winnerText.alignment = TextAlignmentOptions.Center;
        winnerText.color = winner.GetColor().color;
        winnerText.text = "Player " + winner.GetName() + " Wins!";

        // Create/align/display stats text
        text = new GameObject("StatsText");
        text.transform.parent = Panel.transform;
        TextMeshProUGUI statsText = text.AddComponent<TextMeshProUGUI>();
        statsText.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 350);
        statsText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
        statsText.alignment = TextAlignmentOptions.Center;
        statsText.text = "Tiles Controlled:\t\t% Controlled\n" + winner.getTilesControlledCount() + "\t\t\t\t" + winner.CalculatePercentage() + "%";
    }
}
