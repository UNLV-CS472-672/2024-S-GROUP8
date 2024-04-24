using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Battles;
using static InventoryManager;

public class GameManagerTest
{
    public GameObject go;
    public GameManager testManager;
    // new Battle testBattle;

    // [SetUp]
    // public void SetUp()
    // {
    //     go = new GameObject("Manager");
    //     testManager = go.AddComponent<GameManager>();
    // }

    // I don't think we can do tests involving Battle objects
    // without refactoring
    // [Test]
    // public void TestAddBattle()
    // {

    // }

     [Test]
    public void DoAllBattles_AttackerWins_TileTransferred()
    {
        // Arrange
        GameManager gameManager = new GameObject().AddComponent<GameManager>(); // Creating a GameManager instance
        InventoryManager inventoryManager = new GameObject().AddComponent<InventoryManager>();
        PlayerController playerController = new GameObject().AddComponent<PlayerController>();
        
        GameObject tileGameObject = new GameObject("Tile");
        Tile tile = tileGameObject.AddComponent<Tile>();
        MeshRenderer renderer = tileGameObject.AddComponent<MeshRenderer>();


        Player attacker = new Player("Bob", null);
        Player defender = new Player("John", null);
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();

        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(6);
        Attacker[0].setDefense(10);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Attacker.Add(new Card(null, "Card2A"));
        Attacker[1].setOffense(3);
        Attacker[1].setDefense(18);
        Attacker[1].setImmunityChance(0.0f);
        Attacker[1].setEfficency(0.0f);


        Attacker.Add(new Card(null, "Card3A"));
        Attacker[2].setOffense(8);
        Attacker[2].setDefense(7);
        Attacker[2].setImmunityChance(0.0f);
        Attacker[2].setEfficency(0.0f);


        Defender.Add(new Card(null, "Card1D"));
        Defender[0].setOffense(6);
        Defender[0].setDefense(10);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Card2D"));
        Defender[1].setOffense(6);
        Defender[1].setDefense(10);
        Defender[1].setImmunityChance(0.0f);
        Defender[1].setEfficency(0.0f);

        // Mocking tile ownership
        Tile.TileReference tileAttacker = new Tile.TileReference();
        tileAttacker.tilePosition = new Vector2(1, 1);
        attacker.AddTiles(tileAttacker); // Attacker owns a tile at position (1,1)
        Tile.TileReference tileDefender = new Tile.TileReference();
        tileDefender.tilePosition = new Vector2(2, 2);
        defender.AddTiles(tileDefender); // Attacker owns a tile at position (2,2)

        // Adding a battle where attacker wins
        GameManager.Battle battle = new GameManager.Battle(attacker, defender, new AttackObject(Attacker, tileAttacker.tilePosition, tileDefender.tilePosition), tile);
        battle.defence = new DefendObject(Defender, tileDefender.tilePosition);
        //battle.defence.cardList.Add(new Card(null, "Card1")); // Adding a dummy card for the defender
        GameManager.FinalBattles.Add(battle);

        // Act
        gameManager.DoAllBattles();

        // Assert
        // Assert that attacker now owns the tile at (2,2)
        Assert.IsTrue(attacker.GetTiles().Exists(tile => tile.tilePosition == new Vector2(2, 2)));
        // Assert that defender no longer owns the tile at (2,2)
        Assert.IsFalse(defender.GetTiles().Exists(tile => tile.tilePosition  == new Vector2(2, 2)));
    }



    [Test]
    public void DoAllBattles_DefenderWins_NoTileTransferred()
    {
        // Arrange
        GameManager gameManager = new GameObject().AddComponent<GameManager>(); // Creating a GameManager instance
        InventoryManager inventoryManager = new GameObject().AddComponent<InventoryManager>();
        PlayerController playerController = new GameObject().AddComponent<PlayerController>();

        GameObject tileGameObject = new GameObject("Tile");
        Tile tile = tileGameObject.AddComponent<Tile>();
        MeshRenderer renderer = tileGameObject.AddComponent<MeshRenderer>();

        Player attacker = new Player("Bob", null);
        Player defender = new Player("John", null);
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();


        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(10);
        Attacker[0].setDefense(100);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Attacker.Add(new Card(null, "Card2A"));
        Attacker[1].setOffense(4);
        Attacker[1].setDefense(50);
        Attacker[1].setImmunityChance(0.0f);
        Attacker[1].setEfficency(0.0f);


        Defender.Add(new Card(null, "Card1D"));
        Defender[0].setOffense(10);
        Defender[0].setDefense(50);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Card2D"));
        Defender[1].setOffense(2);
        Defender[1].setDefense(100);
        Defender[1].setImmunityChance(0.0f);
        Defender[1].setEfficency(0.0f);


        Defender.Add(new Card(null, "Card3D"));
        Defender[2].setOffense(15);
        Defender[2].setDefense(40);
        Defender[2].setImmunityChance(0.0f);
        Defender[2].setEfficency(0.0f);

         // Mocking tile ownership
        Tile.TileReference tileAttacker = new Tile.TileReference();
        tileAttacker.tilePosition = new Vector2(1, 1);
        attacker.AddTiles(tileAttacker); // Attacker owns a tile at position (1,1)
        Tile.TileReference tileDefender = new Tile.TileReference();
        tileDefender.tilePosition = new Vector2(2, 2);
        defender.AddTiles(tileDefender); // Attacker owns a tile at position (2,2)
        // Adding a battle where attacker wins
        GameManager.Battle battle = new GameManager.Battle(attacker, defender, new Battles.AttackObject(Attacker, tileAttacker.tilePosition, tileDefender.tilePosition), tile);
        battle.defence = new Battles.DefendObject(Defender, tileDefender.tilePosition);
        //battle.defence.cardList.Add(new Card(null, "Card1")); // Adding a dummy card for the defender
        GameManager.FinalBattles.Add(battle);

        // Act
        gameManager.DoAllBattles();
        // Assert
        // Assert that attacker still doesn't own the tile at (1,1)
        Assert.IsFalse(attacker.GetTiles().Exists(t => t.tilePosition == new Vector2(2, 2)));
        // Assert that defender still owns the tile at (2,2)
        Assert.IsTrue(defender.GetTiles().Exists(t => t.tilePosition == new Vector2(2, 2)));
    }


    [Test]
    public void returnWinnersRemainingCardsToInventoryTest()
    {

        Inventory inventory = new Inventory();

        Card stackCard = new Card(null, "Stack Card");
        Assert.AreEqual(0, inventory.GetStacksListSize());

        GameManager gameManager = new GameObject().AddComponent<GameManager>(); // Creating a GameManager instance
        //InventoryManagerManager invManager = new GameObject().AddComponent<InventoryManager>();
        Player attacker = new Player("Bob", null);
        Player defender = new Player("John", null);
        List<Card> Attacker = new List<Card>();
        List<Card> Defender = new List<Card>();

         //Add new card to inventory
        attacker.GetInventory().AddToCardToStack(stackCard);

        Attacker.Add(new Card(null, "Card1A"));
        Attacker[0].setOffense(6);
        Attacker[0].setDefense(10);
        Attacker[0].setImmunityChance(0.0f);
        Attacker[0].setEfficency(0.0f);

        Attacker.Add(new Card(null, "Card2A"));
        Attacker[1].setOffense(3);
        Attacker[1].setDefense(18);
        Attacker[1].setImmunityChance(0.0f);
        Attacker[1].setEfficency(0.0f);


        Attacker.Add(new Card(null, "Card3A"));
        Attacker[2].setOffense(8);
        Attacker[2].setDefense(7);
        Attacker[2].setImmunityChance(0.0f);
        Attacker[2].setEfficency(0.0f);


        Defender.Add(new Card(null, "Card1D"));
        Defender[0].setOffense(6);
        Defender[0].setDefense(10);
        Defender[0].setImmunityChance(0.0f);
        Defender[0].setEfficency(0.0f);

        Defender.Add(new Card(null, "Card2D"));
        Defender[1].setOffense(6);
        Defender[1].setDefense(10);
        Defender[1].setImmunityChance(0.0f);
        Defender[1].setEfficency(0.0f);

        gameManager.returnWinnersRemainingCardsToInventory(attacker, Attacker);
        Assert.AreEqual(4, attacker.GetInventory().GetStacksListSize());

    }


     [Test]
    public void OnlyDefenderBattlesTest()
    {
        GameManager gameManager = new GameObject().AddComponent<GameManager>();

        GameManager.Battle battle1 = new GameManager.Battle(new Player("Atacker1", null), new Player("Defender", null), null, null);
        GameManager.Battle battle2 = new GameManager.Battle(new Player("Atacker2", null), new Player("Defender", null), null, null);
        GameManager.Battle battle3 = new GameManager.Battle(new Player("Atacker3", null), new Player("Attacker", null), null, null);

        GameManager.PlannedBattles.AddRange(new GameManager.Battle[] { battle1, battle2, battle3 });

        List<GameManager.Battle> defenderBattles = GameManager.OnlyDefenderBattles(new Player("Defender", null));

        // assert that only battle 1 and 2 were added since there was a defender
        Assert.AreEqual(2, defenderBattles.Count);
        Assert.Contains(battle1, defenderBattles);
        Assert.Contains(battle2, defenderBattles);
        Assert.IsFalse(defenderBattles.Contains(battle3));

    }


    [Test]
    public void AddDefenderToBattle_Test()
    {
    // Arrange
    GameManager gameManager = new GameObject().AddComponent<GameManager>();
    Player attacker = new Player("Attacker", null);
    Player attacker2 = new Player("Attacker2", null);
    Player defender = new Player("Defender", null);
    Player defender2 = new Player("Defender2", null);
    GameManager.Battle battle1 = new GameManager.Battle(attacker, null, new AttackObject(new List<Card>(), new Vector2(1, 1), new Vector2(2, 2)), null);
    GameManager.Battle battle2 = new GameManager.Battle(attacker, null, new AttackObject(new List<Card>(), new Vector2(2, 2), new Vector2(3, 3)), null);
    GameManager.Battle battle3 = new GameManager.Battle(attacker2, null, new AttackObject(new List<Card>(), new Vector2(4, 4), new Vector2(5, 5)), null);

    GameManager.PlannedBattles.AddRange(new[] { battle1, battle2, battle3 });

    // Create a DefendObject for testing
    List<Card> cardList = new List<Card> { new Card(null, "Card1"), new Card(null, "Card2") };
    Vector2 originTilePos = new Vector2(2, 2);
    DefendObject defendObject = new DefendObject(cardList, originTilePos);

    // Create a DefendObject for testing
    List<Card> cardList2 = new List<Card> { new Card(null, "Card1"), new Card(null, "Card2") };
    Vector2 originTilePos2 = new Vector2(5, 5);
    DefendObject defendObject2 = new DefendObject(cardList2, originTilePos2);

    // Act
    GameManager.AddDefenderToBattle(defender, defendObject);
    GameManager.AddDefenderToBattle(defender2, defendObject2);

    // Assert
    Assert.AreEqual(2, GameManager.FinalBattles.Count); 
    Assert.AreEqual(defender, battle1.defender); //first battle should have defender set to the provided player
    Assert.AreEqual(defendObject, battle1.defence); //first battle should have defence set to the given defendObject
    Assert.IsTrue(battle1.defenderHasCards); 
    Assert.IsFalse(GameManager.PlannedBattles.Contains(battle1)); 
    Assert.IsFalse(battle2.defenderHasCards); 
    Assert.IsTrue(GameManager.PlannedBattles.Contains(battle2)); //second battle should still be in PlannedBattles  

    }  


     [Test]
    public void AddAttackerToBattle_Test()
    {
        GameManager gameManager = new GameObject().AddComponent<GameManager>();


        GameObject tileGameObject = new GameObject("Tile");
        Tile tile = tileGameObject.AddComponent<Tile>();
        MeshRenderer renderer = tileGameObject.AddComponent<MeshRenderer>();

        Player attacker = new Player("Attacker", null);
        Player defender = new Player("Defender", null);

        Battles.AttackObject atkObj = new Battles.AttackObject(new List<Card>(), new Vector2(1,1), new Vector2(2,2));

        GameManager.AddAttackerToBattle(attacker, defender, atkObj, tile);

        Assert.AreEqual(1, GameManager.PlannedBattles.Count);
        Assert.AreEqual(attacker, GameManager.PlannedBattles[0].attacker);
        Assert.AreEqual(defender, GameManager.PlannedBattles[0].defender);

    }
    
    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(go);
        GameManager.PlannedBattles.Clear(); // Clear the PlannedBattles list after each test
        GameManager.FinalBattles.Clear();
    }
}