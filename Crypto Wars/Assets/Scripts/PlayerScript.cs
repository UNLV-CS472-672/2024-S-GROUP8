using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private bool isDone;
    private string playerName;
    private Material playerColor;
    private double percentControlled;
    private int tilesControlled;
    private List<Tile.TileReference> tilesOwned;

    private Inventory inventory;
    private Hand hand;

    private Phase currentPhase; // Tracks current phase

     // Enum to represent the different phases each player can be in
    public enum Phase
    {
        Defense,
        Attack,
        Build
    }

    // Advance to the next phase and loop back to the first phase after the last one
    public void NextPhase()
    {
        currentPhase = (Phase)(((int)currentPhase + 1) % 3);
    }

    // Sets the current phase for the player
    public void SetPhase(Phase phase)
    {
        currentPhase = phase;
    }

    // Resets the phase to the start (useful at the start of a new turn)
    public void ResetPhase()
    {
        currentPhase = Phase.Defense; // Defense Phase is now first
    }

    // Method to check the current phase
    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }

    // Constructs the player, used a reference when hotswapping 
    public Player(string name, Material color)
    {
        isDone = false;
        playerName = name;
        playerColor = color;
        percentControlled = 0;
        inventory = new Inventory();
        tilesOwned = new List<Tile.TileReference>();
    }

    // Return the inventory from this player
    public Inventory GetInventory()
    {
        return inventory;
    }

    // Return the hand from this player
    public Hand GetHand()
    {
        return hand;
    }

    // Returns whether the player is done with there turn
    // Turn master will refer to this when calculating whether a full turn will elapse
    public bool IsPlayerTurnFinished()
    {
        return isDone;
    }

    // Sets the player's turn to finished
    // Not finalized, may have further functionality 
    public void PlayerFinishTurn()
    {
        isDone = true;
    }

    // Sets the player's turn to a new turn
    // Not finalized, may have further functionality 
    public void PlayerStartTurn()
    {
        isDone = false;
    }

    // Add one tile to a player's ownership
    // Simple reference for calculating victory, etc
    public void AddTiles(Tile.TileReference tile)
    {
        tilesControlled += 1;
        if (!tilesOwned.Contains(tile))
        {
            tilesOwned.Add(tile);
            Debug.Log("Tile added to player's ownership");
            Debug.Log(tilesOwned.GetHashCode());
        }
    }

    // Overloaded adding tiles for if amount is more than 1
    // Simple reference for calculating victory
    public void AddTiles(List<Tile.TileReference> tiles, int amount)
    {
        tilesControlled += amount;
        foreach (Tile.TileReference tile in tiles)
        {
            if (!tilesOwned.Contains(tile))
            {
                tilesOwned.Add(tile);
                
                Debug.Log("Tile added to player's ownership");
            }
        }
    }

    // Remove one tile to a player's ownership
    // Simple reference for calculating victory, etc
    public void RemoveTiles(Tile.TileReference tile)
    {
        tilesControlled -= 1;
        bool isRemoved = tilesOwned.Remove(tile);
        if (!isRemoved)
            Debug.Log("A tile cannot be removed without prior ownership");
    }

    // Overloaded removing tiles for if amount is more than 1
    // Simple reference for calculating victory, etc
    public void RemoveTiles(List<Tile.TileReference> tiles, int amount)
    {
        tilesControlled -= amount;
        foreach (Tile.TileReference tile in tiles)
        {
            bool isRemoved = tilesOwned.Remove(tile);
            if (!isRemoved)
                Debug.Log("A tile cannot be removed without prior ownership");
        }
    }

    // Simple Getters and Setters
    public Material GetColor()
    {
        return playerColor;
    }

    public string GetName()
    {
        return playerName;
    }

    public void SetColor(Material color)
    {
        playerColor = color;
    }

    public void SetName(string name)
    {
        playerName = name;
    }

    public List<Tile.TileReference> GetTiles()
    {
        return this.tilesOwned;
    }

    public int getTilesControlledCount()
    {
        return tilesOwned.Count;
    }

    // This will be called after tiles are added or removed from player's control
    // And if a turn has elapsed, to make all win conditions checked after the turn is over
    public double CalculatePercentage()
    {
        return 0;
    }
}