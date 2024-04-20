using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMasterTest
{
    private TurnMaster turnMaster;
    private Player player1, player2;

    [SetUp]
    public void Setup()
    {
        // Setup method to initialize objects before each test
        turnMaster = new TurnMaster();
        player1 = new Player("Player1", null);
        player2 = new Player("Player2", null);

        // Initialize TurnMaster with two players
        Player[] players = new Player[] { player1, player2 };
        turnMaster.SetPlayers(players); // Implemented in TurnMaster to set the Players array
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
    public void TestNewTurn_ResetsPlayers()
    {
        player1.PlayerFinishTurn();
        turnMaster.StartNewTurn();
        Assert.IsFalse(player1.IsPlayerTurnFinished());
        Assert.AreEqual(Player.Phase.Defense, player1.GetCurrentPhase());
    }

    [Test]
    public void TestAllPhasesDone_True()
    {
        player1.PlayerFinishTurn();
        player1.NextPhase(); // Attack
        player1.NextPhase(); // Build
        player2.PlayerFinishTurn();
        player2.NextPhase(); // Attack
        player2.NextPhase(); // Build
        Assert.IsTrue(turnMaster.AllPhasesDone());
    }

    [Test]
    public void TestAllPhasesDone_False()
    {
        player1.PlayerFinishTurn();
        player1.NextPhase(); // Attack
        // player2 is still in Defense phase
        Assert.IsFalse(turnMaster.AllPhasesDone());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhases()
    {
        turnMaster.AdvancePlayerPhase(player1);
        Assert.AreEqual(Player.Phase.Attack, player1.GetCurrentPhase());
        turnMaster.AdvancePlayerPhase(player1);
        turnMaster.AdvancePlayerPhase(player1);
        Assert.AreEqual(Player.Phase.Defense, player1.GetCurrentPhase());
        Assert.IsTrue(player1.IsPlayerTurnFinished());
    }

    [Test]
    public void TestGetCurrTurn_Initial()
    {
        Assert.AreEqual(0, turnMaster.GetCurrentTurn());
    }
}

