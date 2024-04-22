using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WinConditions : MonoBehaviour
{
    public float timer { get; set; }    // Starting timer
    public float maxTime { get; set; }  // Time limit of 45.5 sec (for now)
    public GameObject gameOver;         // Unity game over object
    public TextMeshProUGUI gameWinner;  // Output text for overall game winner
    public Player winningPlayer { get; private set; } // Find/output the winner
    private PlayerController playerCtrl;
    private GameObject playerCtrlGameObject;


    void Start() {
        timer = 0f;
        maxTime = 45.5f;
        gameWinner = GetComponent<TextMeshProUGUI>();
        gameWinner.text = "Winner ";
        playerCtrlGameObject = new GameObject();
        playerCtrl = playerCtrlGameObject.AddComponent<PlayerController>();
    } 

    void Update()
    {
        // Check/find a winner
        findWinner(playerCtrl);

        // Increment game timer
        timer += Time.deltaTime;
    }

    /*******************************************************
     * 
     * Game scenes based on wins
     * 
    *******************************************************/

    // Added in case we want a restart game screen
    public void restartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Displays gameOver object & scene
    // Added in case we want a game over screen
    public void gameIsOver(Player winner)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game is Over");

        // Add player name to winner text
        gameWinner.text += winner.GetName();

        // Set gameOver to be displayed
        gameOver.SetActive(true);

        // Destroy objects
        Object.Destroy(playerCtrlGameObject);
    }

    /***********************************************************************************************
     * 
     * findWinner - find a winner based on game win conditions:
     *      1. a player has reached the tile percentage controlled necessary to win (>=50%)
     *      
     *      or
     *      
     *      2. time runs out and a winner must be found based on tile count
     * 
     ***********************************************************************************************/
    public void findWinner(PlayerController playerController)
    {
        Player ply; // temp - stores a player

        winningPlayer = null;

        // Check if any player has won based on time or tiles
        if (timer < maxTime) // game time limit not yet reached
        {
            // Check if any player's tile controlled  % >= 50%
            for (int i = 0; i < PlayerController.players.Count; i++)
            {
                ply = PlayerController.CurrentPlayer;

                // Player needs at least 50% overall tile control to win
                if (ply.CalculatePercentage() >= 0.50)
                    winningPlayer = ply;

                PlayerController.NextPlayer();
            }

            // Declare game over if winner was found
            if (winningPlayer != null)
                gameIsOver(winningPlayer);
        }
        else // game time limit reached
        {
            // force game to end
            Debug.Log("Game Time Limit Reached");

            // Default winner is currentPlayer to prevent winningPlayer = null
            winningPlayer = PlayerController.CurrentPlayer;

            // Find max of all players TilesControlledCount
            for (int i = 0; i < PlayerController.players.Count; i++)
            {
                PlayerController.NextPlayer();
                ply = PlayerController.CurrentPlayer;

                if (winningPlayer.getTilesControlledCount() < ply.getTilesControlledCount())
                    winningPlayer = ply;
            }

            // Declare game over
            gameIsOver(winningPlayer);
        }
    }
}
