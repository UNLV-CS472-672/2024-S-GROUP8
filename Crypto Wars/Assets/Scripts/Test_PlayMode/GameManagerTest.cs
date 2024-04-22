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
        GameManager.Battle battle = new GameManager.Battle(attacker, defender, new Battles.AttackObject(Attacker, tileAttacker.tilePosition, tileDefender.tilePosition));
        battle.defence = new Battles.DefendObject(Defender, tileDefender.tilePosition);
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
        GameManager.Battle battle = new GameManager.Battle(attacker, defender, new Battles.AttackObject(Attacker, tileAttacker.tilePosition, tileDefender.tilePosition));
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
        //InventoryManager manager = InventoryManager.GetManager();
        // InventoryManager manager = new GameObject().AddComponent<InventoryManager>();
        // manager.SetupSlot(0);
        // manager.SetupSlot(1);
        // manager.SetupSlot(2);
        // manager.SetupSlot(3);
        // manager.SetupSlot(4);
        // manager.SetupSlot(5);

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











    
    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(go);
    }
}