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
        build.SetOwner(tim);
        Player buildingOwner = build.GetOwner();
        Assert.AreEqual(tim, buildingOwner);
    }


    [Test]
    public void testBuildingTile()
    {  
        Tile tile = new Tile();
        Building build = new Building("testBuilding", 1, 2);
        build.SetTile(tile);
        Tile buildTile = build.GetTile();
        Assert.AreEqual(tile, buildTile);
    }

    [Test]
    public void testBuildingProduction()
    {  
        Building build = new Building("testBuilding", 1, 4);
        build.DidNotProduce();
        int buildTurnsSinceLast = build.GetTurnsSinceLast();
        Assert.AreEqual(1, buildTurnsSinceLast);
    }

    [Test]
    public void testBuildingAmount()
    {  
        Building build = new Building("testBuilding", 2, 2);
        build.SetAmount(3);
        int buildAmount = build.GetAmount();
        Assert.AreEqual(3, buildAmount);
    }

    [Test]
    public void testBuildingName()
    {  
        Building build = new Building("testBuilding", 2, 2);
        string buildName = build.GetName();
        Assert.AreEqual("testBuilding", buildName);
    }

    [Test]
    public void testBuildingCard()
    {  
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 2);
        build.SetCard(card);
        Card buildCard = build.GetCard();
        Assert.AreEqual(card, buildCard);
    }

    [Test]
    public void testAddCardsToInventory()
    {
        Material playerColor = new Material(Shader.Find("Specular"));
        Player player = new Player("Test",playerColor);
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 1);

        build.DidNotProduce();
        build.DidNotProduce();

        build.AddCardsToInventory(player.GetInventory());
        Assert.AreEqual(0, build.GetTurnsSinceLast());
    }
}
