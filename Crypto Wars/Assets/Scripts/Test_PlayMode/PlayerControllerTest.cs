using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerTest
{
    public PlayerController testController;
    public GameObject camera;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Project", LoadSceneMode.Single);
        yield return null;
        yield return new EnterPlayMode();

        camera = GameObject.Find("Main Camera");
        PlayerController PlayerController = camera.GetComponent<PlayerController>();
        testController = PlayerController;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        yield return new ExitPlayMode();
    }

    [UnityTest]
    public IEnumerator SceneTest()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject Inventory = GameObject.Find("Inventory Bar");
        GameObject Camera = GameObject.Find("Main Camera");

        Assert.IsNotNull(camera);
        Assert.IsNotNull(Inventory);

        Assert.IsNotNull(testController);

    }

    [UnityTest]
    public IEnumerator TestNextPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.NextPlayer();
        Assert.AreEqual(1, PlayerController.GetCurrentPlayerIndex());
    }

    [UnityTest]
    public IEnumerator TestNextPlayer_Overloaded()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.NextPlayer(1);
        Assert.AreEqual(1, PlayerController.GetCurrentPlayerIndex());
    }

    [UnityTest]
    public IEnumerator TestPlayerList()
    {
        yield return new WaitForSeconds(0.5f);
        List<Player> players = PlayerController.GetPlayerList();
        Assert.AreEqual(2, players.Count);
    }

    [UnityTest]
    public IEnumerator TestCurrentPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        Player player = PlayerController.GetCurrentPlayer();
        Assert.AreEqual("One", player.GetName());
    }

    [UnityTest]
    public IEnumerator TestSetTile()
    {
        yield return new WaitForSeconds(0.5f);
        Tile tile = new Tile(); 
        tile.SetTilePosition(0, 0);
        PlayerController.SetSelectedTile(tile);
        Assert.AreEqual(new Vector2(), PlayerController.GetSelectedTile().GetTilePosition());
    }

    [UnityTest]
    public IEnumerator TestEnableCancel()
    {
        yield return new WaitForSeconds(0.5f);
        testController.EnableCancel();
        Assert.True(testController.GetCancelButton().activeSelf);
    }

    [UnityTest]
    public IEnumerator TestMoveProgress()
    {
        yield return new WaitForSeconds(0.5f);
        Tile.TileReference tile = new Tile.TileReference();
        tile.tilePosition = new Vector2(0, 0);
        testController.MoveProgress(tile);
        Vector3 pos = testController.GetProgress().transform.position;
        Assert.AreEqual(tile.tilePosition, new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.z)));
    }

    [UnityTest]
    public IEnumerator TestMoveBuild()
    {
        yield return new WaitForSeconds(0.5f);
        Tile tile = new Tile();
        tile.SetTilePosition(0, 0);
        testController.MoveBuild(tile);
        Vector3 pos = testController.GetBuild().transform.position;
        Assert.AreEqual(tile.GetTilePosition(), new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.z)));
    }

    [UnityTest]
    public IEnumerator TestMoveAttack()
    {
        yield return new WaitForSeconds(0.5f);
        Tile tile = new Tile();
        tile.SetTilePosition(0, 0);
        testController.MoveAttack(tile);
        Vector3 pos = testController.GetAttack().transform.position;
        Assert.AreEqual(tile.GetTilePosition(), new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.z)));
    }

    [UnityTest]
    public IEnumerator TestMoveDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Tile tile = new Tile();
        tile.SetTilePosition(0, 0);
        testController.MoveDestroy(tile);
        Vector3 pos = testController.GetDestroy().transform.position;
        Assert.AreEqual(tile.GetTilePosition(), new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.z)));
    }

    [UnityTest]
    public IEnumerator TestSetupAttack()
    {
        yield return new WaitForSeconds(0.5f);
        Tile tile = new Tile();
        tile.SetTilePosition(0, 0);
        testController.SetupAttackButton(tile);
        testController.SetAdj(true);
        Assert.True(testController.GetCancelButton().activeSelf);
    }

    [UnityTest]
    public IEnumerator TestSetupBuild_Nothing()
    {
        yield return new WaitForSeconds(0.5f);
        Tile tile = new Tile();
        tile.SetTilePosition(0, 0);
        Building building = new Building("Nothing", 0, 0);
        tile.SetBuilding(building);
        testController.SetupBuildButton(tile);
        Assert.True(testController.GetCancelButton().activeSelf);
        Assert.True(testController.GetBuild().activeSelf);
    }

    [UnityTest]
    public IEnumerator TestSetupBuild_Something()
    {
        yield return new WaitForSeconds(0.5f);
        Tile tile = new Tile();
        tile.SetTilePosition(0, 0);
        Building building = new Building("TEST", 0, 0);
        tile.SetBuilding(building);
        testController.SetupBuildButton(tile);
        Assert.True(testController.GetCancelButton().activeSelf);
        Assert.True(testController.GetDestroy().activeSelf);
    }

    [UnityTest]
    public IEnumerator TestAttackInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.players = new List<Player>
        {
            new Player("Phil", null),
            new Player("John", null)
        };
        Player player = PlayerController.players[1];
        player.AddTiles(new Tile.TileReference { tilePosition = new Vector2(1, 0), tileName = "Tile" });

        GameObject holder = new GameObject();
        holder.AddComponent<Tile>();
        holder.AddComponent<MeshRenderer>();
        Tile tile = holder.GetComponent<Tile>();

        GameObject holder2 = new GameObject();
        holder2.AddComponent<Tile>();
        holder2.AddComponent<MeshRenderer>();
        Tile tile2 = holder2.GetComponent<Tile>();

        tile.SetTilePosition(0, 0);
        tile.SetPlayer(0);

        tile2.SetTilePosition(1, 0);
        tile2.SetPlayer(1);

        PlayerController.CurrentPlayer = PlayerController.players[0];
        PlayerController.players[0].SetPhase(Player.Phase.Attack);
        testController.TileInteractions(tile2);
        Assert.True(testController.GetAttack().activeSelf);
    }

    [UnityTest]
    public IEnumerator TestBuildInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.players = new List<Player>
        {
            new Player("Phil", null)
        };


        GameObject holder = new GameObject();
        holder.AddComponent<Tile>();
        holder.AddComponent<MeshRenderer>();
        Tile tile = holder.GetComponent<Tile>();

        tile.SetTilePosition(0, 0);
        tile.SetPlayer(0);

        PlayerController.CurrentPlayer = PlayerController.players[0];
        PlayerController.players[0].SetPhase(Player.Phase.Build);
        testController.TileInteractions(tile);
        Assert.True(testController.GetBuild().activeSelf);
    }

    [UnityTest]
    public IEnumerator TestDefenseInteraction_NoNeed()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.players = new List<Player>
        {
            new Player("Phil", null)
        };


        GameObject holder = new GameObject();
        holder.AddComponent<Tile>();
        holder.AddComponent<MeshRenderer>();
        Tile tile = holder.GetComponent<Tile>();

        tile.SetTilePosition(0, 0);
        tile.SetPlayer(0);

        PlayerController.CurrentPlayer = PlayerController.players[0];
        PlayerController.players[0].SetPhase(Player.Phase.Defense);
        testController.TileInteractions(tile);
        Assert.True(!testController.GetStash().gameObject.activeSelf);
    }

    [UnityTest]
    public IEnumerator TestDefenseInteraction_Needed()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.players = new List<Player>
        {
            new Player("Phil", null)
        };


        GameObject holder = new GameObject();
        holder.AddComponent<Tile>();
        holder.AddComponent<MeshRenderer>();
        Tile tile = holder.GetComponent<Tile>();

        tile.SetTilePosition(0, 0);
        tile.SetPlayer(0);

        PlayerController.CurrentPlayer = PlayerController.players[0];
        PlayerController.players[0].SetPhase(Player.Phase.Defense);
        CreateDefenseSystem.GetDefensePositions().Add(new Vector2(0, 0));
        testController.TileInteractions(tile);
        Assert.True(testController.GetStash().gameObject.activeSelf);
    }

    [UnityTest]
    public IEnumerator TestBuildingInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject building = new GameObject();
        building.tag = "IsBuilding";

        PlayerController.players = new List<Player>
        {
            new Player("Phil", null)
        };
        Player player = PlayerController.players[0];
        Tile.TileReference tile = new Tile.TileReference { tilePosition = new Vector2(0, 0), tileName = "Tile" };
        player.AddTiles(tile);
        tile.currBuilding = BuildingRegistry.GetBuildingByIndex(0);
        tile.currBuilding.SetOwner(PlayerController.players[0]);
        PlayerController.CurrentPlayer = PlayerController.players[0];
        testController.BuildingInteractions(building);
        Assert.True(testController.GetProgress().gameObject.activeSelf);

    }
}