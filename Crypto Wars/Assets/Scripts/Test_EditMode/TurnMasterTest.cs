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
    public void TestGetCurrTurn_Initial()
    {
        Assert.AreEqual(0, TurnMaster.GetCurrentTurn());
    }

    [Test]
    public void TestNewTurn_ResetsTurnsFinsished()
    {
        PlayerController.players[0].PlayerFinishTurn();
        TurnMaster.StartNewTurn();
        Assert.IsFalse(PlayerController.players[0].IsPlayerTurnFinished());
    }

    [Test]
    public void TestNewTurn_ResetsPhase()
    {
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
        Assert.AreEqual(Player.Phase.Build, PlayerController.players[0].GetCurrentPhase());
    }

    [Test]
    public void TestAllPhasesDone_False()
    {
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        //PlayerController.players[0].PlayerFinishTurn();
        //PlayerController.players[0].NextPhase(); // Attack
        // PlayerController.players[1] is still in Defense phase
        Assert.IsFalse(TurnMaster.AllPhasesDone());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesAttack()
    {
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        Assert.AreEqual(Player.Phase.Attack, PlayerController.players[0].GetCurrentPhase());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesFinishTurn()
    {
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        Assert.IsTrue(PlayerController.players[0].IsPlayerTurnFinished());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesBuild()
    {
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
        Assert.AreEqual(Player.Phase.Build, PlayerController.players[0].GetCurrentPhase());
    }

    
}

