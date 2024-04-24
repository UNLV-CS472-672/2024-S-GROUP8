using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMasterTest
{
    private TurnMaster turnMaster;
    //private Player players[0], player2;
    private PlayerController playerController;

    [SetUp]
    public void Setup()
    {
        // Setup method to initialize objects before each test
        turnMaster = new TurnMaster();
        playerController = new PlayerController();
        PlayerController.players = new List<Player>
        {
            new Player("One", Resources.Load<Material>("Materials/PlayerTileColor")),
            new Player("Two", Resources.Load<Material>("Materials/EnemyTileColor"))
        };
        
        
        // Initialize TurnMaster with two players
        //Player[] players = PlayerController.players;
        //player1 = players[0];
        //player2 = players[1];
    }

    // [Test]
    // public void TestAllAreDone_True()
    // {
    //     player1.PlayerFinishTurn();
    //     player2.PlayerFinishTurn();
    //     Assert.IsTrue(turnMaster.allAreDone(turnMaster.Players));
    // }

    // [Test]
    // public void TestAllAreDone_False()
    // {
    //     player1.PlayerFinishTurn();
    //     // player2 does not finish the turn
    //     Assert.IsFalse(turnMaster.allAreDone(turnMaster.Players));
    // }

    [Test]
    public void TestNewTurn_ResetsTurnsFinsished()
    {
        players[0] = new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"));
        PlayerController.players[0].PlayerFinishTurn();
        TurnMaster.StartNewTurn();
        Assert.IsFalse(PlayerController.players[0].IsPlayerTurnFinished());
    }

    [Test]
    public void TestNewTurn_ResetsPhase()
    {
        players[0] = new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"));
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.StartNewTurn();
        Assert.AreEqual(Player.Phase.Defense, PlayerController.players[0].GetCurrentPhase());
    }

    [Test]
    public void TestAllPhasesDone_True()
    {
        
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[1]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[1]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[1]);
        //PlayerController.players[0].NextPhase(); // Attack
        //PlayerController.players[0].NextPhase(); // Build
        //PlayerController.players[1].PlayerFinishTurn();
        //PlayerController.players[1].NextPhase(); // Attack
        //PlayerController.players[1].NextPhase(); // Build
        Assert.IsTrue(TurnMaster.AllPhasesDone());
    }

    [Test]
    public void TestAllPhasesDone_False()
    {
        players[0] = new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"));
        PlayerController.players[0].PlayerFinishTurn();
        PlayerController.players[0].NextPhase(); // Attack
        // PlayerController.players[1] is still in Defense phase
        Assert.IsFalse(TurnMaster.AllPhasesDone());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesAttack()
    {
        players[0] = new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"));
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        Assert.AreEqual(Player.Phase.Attack, PlayerController.players[0].GetCurrentPhase());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesFinishTurn()
    {
        players[0] = new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"));
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        Assert.IsTrue(PlayerController.players[0].IsPlayerTurnFinished());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesBuild()
    {
        players[0] = new Player("One", Resources.Load<Material>("Materials/PlayerTileColor"));
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        Assert.AreEqual(Player.Phase.Build, PlayerController.players[0].GetCurrentPhase());
    }

    [Test]
    public void TestGetCurrTurn_Initial()
    {
        playerController = new PlayerController();
        Assert.AreEqual(0, TurnMaster.GetCurrentTurn());
    }
}

