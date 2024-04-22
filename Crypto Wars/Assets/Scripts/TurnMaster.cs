using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TurnMaster : MonoBehaviour
{
    private static int currTurn;
    private float turnTimer;
    private float maxTurnTime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        currTurn = 1;
        turnTimer = 0f;
    }

    void Update()
    {
        // May not need this especially due to Hotseating
        // Check the completion of phases or if max time has elapsed each frame
        if (AllPhasesDone() /* ||  turnTimer >= maxTurnTime */)
        {
            turnTimer = 0f;
        }
        else
        {
            turnTimer += Time.deltaTime;
        }
    }

    // Checks if all players are done with their turns
    public static bool AllPhasesDone()
    {
        foreach (Player player in PlayerController.players) {
            if (!player.IsPlayerTurnFinished())
            {
                return false;
            }
        }
        return true;
    }

    // Starts a new turn for all players
    public static void StartNewTurn()
    {
        foreach (Player player in PlayerController.players)
        {
            player.PlayerStartTurn();
            player.ResetPhase();
        }
    }

    // Advances a player to the next phase and checks for turn completion
    public static void AdvancePlayerPhase(Player player)
    {
        if (player.GetCurrentPhase() == Player.Phase.Build){
            player.PlayerFinishTurn();
            PlayerController.NextPlayer();
            if (AllPhasesDone()) {
                StartNewTurn();
                currTurn++;
            }
        }
        else {
            player.NextPhase();
        }
        
    }

    // Returns the current turn number
    public static int GetCurrentTurn()
    {
        return currTurn;
    }
}
