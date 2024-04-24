using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building 
{ 

    private int amount;
    private int turnsToProduce;
    private int turnsSinceLastProdction;
    private Card producedCard;
    private Player owner;
    private Tile currentTile;
    private string name;
    private Vector2 pos;
    
    public Building(string nme, int amt, int ttProduce)
    {
        name = nme;
        amount = amt;
        turnsToProduce = ttProduce;
        turnsSinceLastProdction = 0;
        //Get the correct material for the type of building that is being created   
    }

    public Player GetOwner()
    {
        return owner;
    }

    public void SetOwner(Player owner)
    {
        this.owner = owner;
    }

    public Tile GetTile()
    {
        return currentTile;
    }

    public void SetTile(Tile newTile)
    {
        currentTile = newTile;
    }

    public int GetTurnsSinceLast()
    {
        return turnsSinceLastProdction;
    }

    public float GetPercentageFilled()
    {
        return (float)turnsSinceLastProdction / turnsToProduce;
    }

    public int GetTimeToProduce()
    {
        return turnsToProduce;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void SetAmount(int inAmount)
    {
        amount = inAmount;
    }

    public void SetCard(Card cardProd)
    {
        producedCard = cardProd;
    }

    public Card GetCard()
    {
        return producedCard;
    }

    public string GetName()
    {
        return name;
    }

    public void SetPosition(Vector2 pos)
    {
        this.pos = pos;
    }

    public Vector2 GetPosition()
    {
        return pos;
    }


    public void SetName(string newName)
    {
        name = newName;
    }

    public void AddCardsToInventory(Inventory inv)
    {
        if (turnsSinceLastProdction >= turnsToProduce)
        {
            for (int i = 0; i < amount; i++)
            {
                inv.AddToCardToStack(producedCard);
            }
            turnsSinceLastProdction = 0;
        }
        else {
            DidNotProduce();
        }
        
    }

    public void DidNotProduce()
    {
        turnsSinceLastProdction++;
    }
}
