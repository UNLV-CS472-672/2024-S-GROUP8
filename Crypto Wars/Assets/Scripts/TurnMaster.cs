using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private static void BuildingRewards(Player player) { 
        List<Tile.TileReference> tiles = player.GetTiles();
        foreach (Tile.TileReference tile in tiles) {
            if (!tile.currBuilding.GetName().Equals("Nothing")) {
                if(tile.currBuilding.GetOwner() != null)
                    tile.currBuilding.AddCardsToInventory(player.GetInventory());
            }
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
            if (player.GetCurrentPhase() == Player.Phase.Attack) {
                foreach (Player rewarded in PlayerController.players)
                {
                    BuildingRewards(rewarded);
                }
            }
            player.NextPhase();
        }
        
    }

    // Returns the current turn number
    public static int GetCurrentTurn()
    {
        return currTurn;
    }
}
