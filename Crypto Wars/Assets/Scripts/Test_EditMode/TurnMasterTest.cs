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
        playerController = new PlayerController();
        

        // Initialize TurnMaster with two players
        Player[] players = playerController.GetPlayerList();

        player1 = players[0];
        player2 = players[1];
        
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
        player1.PlayerFinishTurn();
        TurnMaster.StartNewTurn();
        Assert.IsFalse(player1.IsPlayerTurnFinished());
    }

    [Test]
    public void TestNewTurn_ResetsPhase()
    {
        TurnMaster.AdvancePlayerPhase(player1);
        TurnMaster.StartNewTurn();
        Assert.AreEqual(Player.Phase.Defense, player1.GetCurrentPhase());
    }

    [Test]
    public void TestAllPhasesDone_True()
    {
        //TurnMaster.AdvancePlayerPhase(player1);
        //TurnMaster.AdvancePlayerPhase(player1);
        //TurnMaster.AdvancePlayerPhase(player1);
        //TurnMaster.AdvancePlayerPhase(player2);
        //TurnMaster.AdvancePlayerPhase(player2);
        //TurnMaster.AdvancePlayerPhase(player2);
        //player1.NextPhase(); // Attack
        //player1.NextPhase(); // Build
        //player2.PlayerFinishTurn();
        //player2.NextPhase(); // Attack
        //player2.NextPhase(); // Build
        Assert.IsTrue(TurnMaster.AllPhasesDone());
    }

    [Test]
    public void TestAllPhasesDone_False()
    {
        player1.PlayerFinishTurn();
        player1.NextPhase(); // Attack
        // player2 is still in Defense phase
        Assert.IsFalse(TurnMaster.AllPhasesDone());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesAttack()
    {
        TurnMaster.AdvancePlayerPhase(player1);
        Assert.AreEqual(Player.Phase.Attack, player1.GetCurrentPhase());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesFinishTurn()
    {
        TurnMaster.AdvancePlayerPhase(player1);
        TurnMaster.AdvancePlayerPhase(player1);
        TurnMaster.AdvancePlayerPhase(player1);
        Assert.IsTrue(player1.IsPlayerTurnFinished());
    }

    [Test]
    public void TestAdvancePlayerPhase_CyclesThroughPhasesBuild()
    {
        TurnMaster.AdvancePlayerPhase(player1);
        TurnMaster.AdvancePlayerPhase(player1);
        Assert.AreEqual(Player.Phase.Build, player1.GetCurrentPhase());
    }

    [Test]
    public void TestGetCurrTurn_Initial()
    {
        Assert.AreEqual(0, TurnMaster.GetCurrentTurn());
    }
}

