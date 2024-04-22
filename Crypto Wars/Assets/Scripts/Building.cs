using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private GameObject inv;
    private int amount;
    private int turnsToProduce;
    private int turnsSinceLastProdction;
    private Card producedCard;
    private Player owner;
    private Tile currentTile;
    private string name;

    private MeshRenderer rendererReference;
    private Material PurpleMaterial;
    private Material YellowMaterial;
    private Material GreenMaterial;

    
    void Start()
    {
        rendererReference = GetComponent<MeshRenderer>();
        // Materials are loaded with the generic typecast
        GreenMaterial = Resources.Load<Material>("Materials/JavaBuildingColor");
        YellowMaterial = Resources.Load<Material>("Materials/PythonBuildingColor");
        PurpleMaterial = Resources.Load<Material>("Materials/CBuildingColor");
    }
    
    public Building(string nme, int amt, int ttProduce)
    {
        name = nme;
        amount = amt;
        turnsToProduce = ttProduce;
        turnsSinceLastProdction = 0;
        //Get the correct material for the type of building that is being created
        if(name == "Python Factory")
        {
            rendererReference.material = YellowMaterial;
        }
        else if(name == "Java Junction")
        {
            rendererReference.material = GreenMaterial;
        }
        else if(name == "C Workshop")
        {
            rendererReference.material = Resources.Load<Material>("Materials/CBuildingColor");;
        }
    }

    public Player getOwner()
    {
        return owner;
    }

    public void setOwner(Player newOwner)
    {
        owner = newOwner;
    }

    public Tile getTile()
    {
        return currentTile;
    }

    public void setTile(Tile newTile)
    {
        currentTile = newTile;
    }

    public int getTurnsSinceLast()
    {
        return turnsSinceLastProdction;
    }

    public int getAmount()
    {
        return amount;
    }

    public void setAmount(int inAmount)
    {
        amount = inAmount;
    }

    public void setCard(Card cardProd)
    {
        producedCard = cardProd;
    }

    public Card getCard()
    {
        return producedCard;
    }

    public string getName()
    {
        return name;
    }

    public void setName(string newName)
    {
        name = newName;
    }

    public void addCardsToInventory(Inventory inv)
    {
        if(turnsSinceLastProdction >= turnsToProduce)
        {
            for (int i = 0; i < amount; i++)
            {
                inv.AddToCardToStack(producedCard);
            }
            turnsSinceLastProdction = 0;
        }
    }

    public void didNotProduce()
    {
        turnsSinceLastProdction++;
    }
}
