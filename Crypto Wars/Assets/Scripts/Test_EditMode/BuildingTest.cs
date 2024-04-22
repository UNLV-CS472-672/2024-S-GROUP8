using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BuildingTest
{
    [Test]
    public void testBuildingCreate()
    {
        Building build = new Building("testBuilding", 1, 2);
        Assert.IsNotNull(build);
    }
    
    [Test]
    public void testBuildingOwner()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer rend = cube.GetComponent<Renderer>();
        Material playerColor = new Material(Shader.Find("Specular"));
        Player tim = new Player("Tim", playerColor);
        Building build = new Building("testBuilding", 1, 2);
        build.setOwner(tim);
        Player buildingOwner = build.getOwner();
        Assert.AreEqual(tim, buildingOwner);
    }


    [Test]
    public void testBuildingTile()
    {  
        Tile tile = new Tile();
        Building build = new Building("testBuilding", 1, 2);
        build.setTile(tile);
        Tile buildTile = build.getTile();
        Assert.AreEqual(tile, buildTile);
    }

    [Test]
    public void testBuildingProduction()
    {  
        Building build = new Building("testBuilding", 1, 4);
        build.didNotProduce();
        int buildTurnsSinceLast = build.getTurnsSinceLast();
        Assert.AreEqual(1, buildTurnsSinceLast);
    }

    [Test]
    public void testBuildingAmount()
    {  
        Building build = new Building("testBuilding", 2, 2);
        build.setAmount(3);
        int buildAmount = build.getAmount();
        Assert.AreEqual(3, buildAmount);
    }

    [Test]
    public void testBuildingName()
    {  
        Building build = new Building("testBuilding", 2, 2);
        string buildName = build.getName();
        Assert.AreEqual("testBuilding", buildName);
    }

    [Test]
    public void testBuildingCard()
    {  
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 2);
        build.setCard(card);
        Card buildCard = build.getCard();
        Assert.AreEqual(card, buildCard);
    }

    [Test]
    public void testAddCardsToInventory()
    {
        Material playerColor = new Material(Shader.Find("Specular"));
        Player player = new Player("Test",playerColor);
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 1);

        build.didNotProduce();
        build.didNotProduce();

        build.addCardsToInventory(player.GetInventory());
        Assert.AreEqual(0, build.getTurnsSinceLast());
    }
}
