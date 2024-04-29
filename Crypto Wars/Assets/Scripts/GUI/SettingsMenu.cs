using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    // Menu Options
    GameObject settingsMenu;
    GameObject playerMenu;
    GameObject multiplayerOptions;
    GameObject winConditionsMenu;

        
    // Game Setting Variables
    private int playerCount;
    private bool hotseat;
    private float timerWinCondition;    
    private float tileWinCondition;

    public string newGame;
    public string main;


    void Start()
    {
        // Grabs different submenus and sets them to not be shown in the scene
        settingsMenu = GameObject.Find("SettingsMenu");
        // playerMenu = GameObject.Find("PlayerMenu");
        // multiplayerOptions = GameObject.Find("MultiplayerMenu");
        // winConditionsMenu = GameObject.Find("WinConMenu");
        // playerMenu.SetActive(false);
        
        // Default Game Settings
        playerCount = 2;
        hotseat = true;
        timerWinCondition = 45.5f;
        tileWinCondition = 0.50f;
    }


    // Switches to Submenus
    // public void PlayerMenu()
    // {
    //     settingsMenu.SetActive(false);
    //     playerMenu.SetActive(true);
    // }

    // public void MultiplayerMenu()
    // {
    //     settingsMenu.SetActive(false);
    //     multiplayerOptions.SetActive(true);
    // }

    // public void WinConditionsMenu()
    // {
    //     settingsMenu.SetActive(false);
    //     winConditionsMenu.SetActive(true);
    // }

    public void LoadGame() {
        SceneManager.LoadScene(newGame);
    }


    public void ReturnToMain()
    {
        SceneManager.LoadScene(main);
    }

    // Player Menu Buttons
    public void setTwoPlayer()
    {
        playerCount = 2;
        PlayerController.playersToGen = 2;
        Debug.Log("Player Count set to 2!");
    }

    public void setThreePlayer()
    {
        playerCount = 3;
        PlayerController.playersToGen = 3;
        Debug.Log("Player Count set to 3!");
    }

    public void setFourPlayer()
    {
        playerCount = 4;
        PlayerController.playersToGen = 4;
        Debug.Log("Player Count set to 4!");
    }

    // Multiplayer Settings
    // moved functionality of SetActive to the Inspector due to test failures
    public void hotSeatEnable()
    {
        hotseat = true;
        Debug.Log("Hotseat Enabled");
        //settingsMenu.SetActive(true);
    }

    public void hotseatDisable()
    {
        hotseat = false;
        Debug.Log("Hotseat Disabled");
        //settingsMenu.SetActive(true);
    }



    // Win Condition Settings
    public void gameTimeSettings(int sel)
    {
        if(sel == 0)
        {
            timerWinCondition = 45.5f;
            Debug.Log("Timer Set to 45s");
        }

        if(sel == 1)
        {
            timerWinCondition = 60f;
            Debug.Log("Timer Set to 60s");
        }

        if(sel == 2)
        {
            timerWinCondition = 300f;
            Debug.Log("Timer Set to 5min");
        }
    }

    public void tilePercentageSettings(int sel)
    {
        if(sel == 0)
        {
            tileWinCondition = 0.5f;
            Debug.Log("Tile Percentage set to 50%");
        }

        if(sel == 1)
        {
            tileWinCondition = 0.75f;
            Debug.Log("Tile Percentage set to 75%");
        }

        if(sel == 2)
        {
            tileWinCondition = 1f;
            Debug.Log("Tile Percentage set to 100%");
        }
    }



    // Number of Players
    public int getPlayerCount()
    {
        return playerCount;
    }

    // Local Multiplayer
    public bool getHotseat()
    {
        return hotseat;
    }



    // Win Conditions
    public float getTimerWinCondition()
    {
        return timerWinCondition;
    }

   public float getTileWinCondition()
   {
        return tileWinCondition;
   }

}
