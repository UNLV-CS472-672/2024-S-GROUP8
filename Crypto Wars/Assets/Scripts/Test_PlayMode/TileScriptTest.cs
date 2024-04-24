using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.TestTools;
using System.Collections;

public class TileScriptTest
{
    public GameObject tileGameObject;
    public Tile tile;
    public Material testMaterial;

    [SetUp]
    public void SetUp()
    {
        GameObject tileGameObject = new GameObject("Tile");
        tileGameObject.AddComponent<MeshRenderer>();
        tileGameObject.AddComponent<Tile>();

        // Initialize Tile GameObject and Tile script before each test
        tile = tileGameObject.GetComponent<Tile>();
        tile.SetRender(tileGameObject.GetComponent<MeshRenderer>());

        // Set up a test material
        testMaterial = new Material(Shader.Find("Standard"));
    }

    [UnityTest]
    public IEnumerator TileStartTest() {
        yield return new WaitForSeconds(0.5f);
        Tile StashScript = tile.GetComponent<Tile>();
        Assert.IsNotNull(StashScript);
    }


    [Test]
    public void Tile_SetPlayer_AssignsPlayerIndexCorrectly()
    {
        // Test to ensure the player index is set correctly
        int testPlayerIndex = 0;
        PlayerController.players = new List<Player>
        {
            new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"))
        };
        tile.SetPlayer(testPlayerIndex);
        Assert.AreEqual(testPlayerIndex, tile.GetPlayer());
    }

    [Test]
    public void Tile_SetMaterial_AssignsMaterialCorrectly()
    {
        // Test to ensure the material is set correctly
        tile.SetMaterial(testMaterial);
        Material actualMaterial = tile.GetMaterial();

        string expectedName = testMaterial.name.Split(' ')[0];
        string actualName = actualMaterial.name.Split(' ')[0];

        Assert.AreEqual(expectedName, actualName);
    }

    [Test]
    public void Tile_GetTilePosition_ReturnsCorrectPosition()
    {
        // Test to ensure the position is retrieved correctly
        Vector2 expectedPosition = new Vector2(5, 10);
        tile.SetTilePosition(5, 10);
        Assert.AreEqual(expectedPosition, tile.GetTilePosition());
    }

    [Test]
    public void Tile_GetBuilding_ReturnsCorrectBuilding()
    {
        // Test to ensure the building is retrieved correctly
        Building building = tile.GetBuilding();
        string buildingName = building.GetName();
        Assert.AreEqual("Nothing", building.GetName());

    }
    [Test]
    public void Tile_SetBuilding_AssignsBuildingCorrectly()
    {
        // Test to ensure the building is set correctly
        Building testBuilding = new Building("TestBuilding", 1, 1);
        tile.SetBuilding(testBuilding);
        Building actualBuilding = tile.GetBuilding();
        Assert.AreEqual(testBuilding, actualBuilding);
    }

    [Test]
    public void Tile_SetBuilding_AssignsNothing()
    {
        // Test to ensure the building is set correctly
        tile.SetBuilding(null);
        Building actualBuilding = tile.GetBuilding();
        Assert.AreEqual("Nothing", actualBuilding.GetName());
    }

    [Test]
    public void Tile_IsAdjacent_ReturnsTrue()
    {
        // Test to ensure the tile is adjacent to the player
        // Create a player and add tiles to it
        Player player = new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"));
        player.AddTiles(new Tile.TileReference { tilePosition = new Vector2(0,0), tileName = "Tile"});
        player.AddTiles(new Tile.TileReference { tilePosition = new Vector2(1,0), tileName = "Tile2"});

        Tile adjTile = new Tile();
        adjTile.SetTilePosition(0,1);

        // Check if the tile is adjacent to the player
        // Right
        bool checkTile = Tile.IsAdjacent(player, adjTile);
        Assert.IsTrue(checkTile);

        adjTile.SetTilePosition(1,0);

        // Left
        checkTile = Tile.IsAdjacent(player, adjTile);
        Assert.IsTrue(checkTile);

        // Is self tile
        adjTile.SetTilePosition(0,0);
        checkTile = Tile.IsAdjacent(player, adjTile);
        Assert.IsFalse(checkTile);

        adjTile.SetTilePosition(1, -1);

        // Center
        checkTile = Tile.IsAdjacent(player, adjTile);
        Assert.IsTrue(checkTile);

        adjTile.SetTilePosition(2, 2);

        // Not adjacent
        checkTile = Tile.IsAdjacent(player, adjTile);
        Assert.IsFalse(checkTile);
    }

    [Test]
    public void Tile_GetTileAtPosition_ReturnsTileRef()
    {
        PlayerController.players = new List<Player>
        {
            new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"))
        };
        Tile.TileReference tileRef = new Tile.TileReference();
        tileRef.tilePosition = new Vector2(0, 0);
        PlayerController.players[0].AddTiles(tileRef);
        Tile.TileReference tileRef2 = Tile.GetTileAtPostion(new Vector2(0, 0), PlayerController.players[0].GetTiles());
        Assert.NotNull(tileRef2);
    }

    [Test]
    public void Tile_GetTileAtPosition_ReturnsEmpty()
    {
        PlayerController.players = new List<Player>
        {
            new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"))
        };
        Tile.TileReference tileRef = new Tile.TileReference();
        tileRef.tilePosition = new Vector2(10, 0);
        PlayerController.players[0].AddTiles(tileRef);
        Tile.TileReference tileRef2 = Tile.GetTileAtPostion(new Vector2(0, 0), PlayerController.players[0].GetTiles());
        Assert.Null(tileRef2);
    }

    [Test]
    public void Tile_GetRender_ReturnsRender()
    {
        Assert.NotNull(tile.GetRender());
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(tile);
    }
}

