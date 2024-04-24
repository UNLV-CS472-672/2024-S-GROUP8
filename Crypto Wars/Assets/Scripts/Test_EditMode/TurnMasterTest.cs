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
    }

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
    public void TestAllPhasesDone_False()
    {
        TurnMaster.AdvancePlayerPhase(PlayerController.players[0]);
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

