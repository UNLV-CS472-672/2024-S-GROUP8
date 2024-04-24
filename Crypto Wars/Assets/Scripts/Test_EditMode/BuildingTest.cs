using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BuildingTest
{
    [Test]
    public void TestBuildingCreate()
    {
        Building build = new Building("testBuilding", 1, 2);
        Assert.IsNotNull(build);
    }
    
    [Test]
    public void TestBuildingOwner()
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
    public void TestBuildingTile()
    {  
        Tile tile = new Tile();
        Building build = new Building("testBuilding", 1, 2);
        build.SetTile(tile);
        Tile buildTile = build.GetTile();
        Assert.AreEqual(tile, buildTile);
    }

    [Test]
    public void TestBuildingProduction()
    {  
        Building build = new Building("testBuilding", 1, 4);
        build.DidNotProduce();
        int buildTurnsSinceLast = build.GetTurnsSinceLast();
        Assert.AreEqual(1, buildTurnsSinceLast);
    }

    [Test]
    public void TestBuildingAmount()
    {  
        Building build = new Building("testBuilding", 2, 2);
        build.SetAmount(3);
        int buildAmount = build.GetAmount();
        Assert.AreEqual(3, buildAmount);
    }

    [Test]
    public void TestBuildingName()
    {  
        Building build = new Building("testBuilding", 2, 2);
        build.SetName("testBuilding2");
        Assert.AreEqual("testBuilding2", build.GetName());
    }

    [Test]
    public void TestBuildingCard()
    {  
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 2);
        build.SetCard(card);
        Card buildCard = build.GetCard();
        Assert.AreEqual(card, buildCard);
    }

    [Test]
    public void TestAddCardsToInventory()
    {
        Material playerColor = new Material(Shader.Find("Specular"));
        Player player = new Player("Test",playerColor);
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 1);
        build.SetCard(card);

        build.DidNotProduce();
        build.DidNotProduce();

        InventoryManager.GetManager().SetupSlot(0);
        build.AddCardsToInventory(player.GetInventory());
        Assert.AreEqual(0, build.GetTurnsSinceLast());
    }

    [Test]
    public void TestAddCardsToInventory_NoProduction()
    {
        Material playerColor = new Material(Shader.Find("Specular"));
        Player player = new Player("Test", playerColor);
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 1);
        build.SetCard(card);

        InventoryManager.GetManager().SetupSlot(0);
        build.AddCardsToInventory(player.GetInventory());
        Assert.AreEqual(1, build.GetTurnsSinceLast());
    }

    [Test]
    public void TestBuildingPercentage()
    {
        Material playerColor = new Material(Shader.Find("Specular"));
        Player player = new Player("Test", playerColor);
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 1);

        build.DidNotProduce();
        Assert.AreEqual(1f, build.GetPercentageFilled());
    }

    [Test]
    public void TestBuildingTimeToProduce()
    {
        Material playerColor = new Material(Shader.Find("Specular"));
        Player player = new Player("Test", playerColor);
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 1);
        Assert.AreEqual(1, build.GetTimeToProduce());
    }

    [Test]
    public void TestBuildingPosition()
    {
        Material playerColor = new Material(Shader.Find("Specular"));
        Player player = new Player("Test", playerColor);
        Card card = new Card(null, "testCard");
        Building build = new Building("testBuilding", 2, 1);
        build.SetPosition(new Vector2(1, 1));
        Assert.AreEqual(new Vector2(1, 1), build.GetPosition());
    }
}
