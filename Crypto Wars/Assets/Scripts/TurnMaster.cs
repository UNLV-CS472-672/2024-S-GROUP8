using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMaster : MonoBehaviour
{
    private int currTurn;
    private float turnTimer;
    private float maxTurnTime = 30f;
    private Player[] players; // Private field to encapsulate the player array

    // Property to access players array for testing 
    public Player[] Players 
    {
        get { return players; }
        private set { players = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        currTurn = 0;
        turnTimer = 0f;
    }

    void Update()
    {
        // Check the completion of phases or if max time has elapsed each frame
        if (AllPhasesDone() || turnTimer >= maxTurnTime)
        {
            StartNewTurn();
            currTurn++;
            turnTimer = 0f;
        }
        else
        {
            turnTimer += Time.deltaTime;
        }
    }

    // Set players dynamically
    public void SetPlayers(Player[] newPlayers)
    {
        Players = newPlayers;
    }

    // Checks if all players are done with their turns
    public bool AllPhasesDone()
    {
        foreach (Player player in Players)
        {
            if (!(player.GetCurrentPhase() == Player.Phase.Build && player.IsPlayerTurnFinished()))
            {
                return false;
            }
        }
        return true;
    }

    // Starts a new turn for all players
    public void StartNewTurn()
    {
        foreach (Player player in Players)
        {
            player.PlayerStartTurn();
            player.ResetPhase();
        }
    }

    // Advances a player to the next phase and checks for turn completion
    public void AdvancePlayerPhase(Player player)
    {
        player.NextPhase();
        if (player.GetCurrentPhase() == Player.Phase.Defense)
        {
            player.PlayerFinishTurn();
        }
    }

    // Returns the current turn number
    public int GetCurrentTurn()
    {
        return currTurn;
    }
}
