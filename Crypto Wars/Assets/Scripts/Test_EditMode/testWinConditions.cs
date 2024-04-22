using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class testWinConditions

{
    private WinConditions winConditions;
    private GameObject winConditionsGameObject;
    private float timer;                // Starting timer
    private float maxTime = 45.5f;      // Time limit of 45.5 sec (for now)
    public GameObject gameOver;         // Unity game over object
    //public TextMeshProUGUI gameWinner;  // Output text for overall game winner
    private Player winningPlayer;       // Find/output the winner
    private PlayerController playerCtrl;
    private GameObject playerCtrlGameObject;

    // Create a new game object
    // also add the WinConditions to said object
    [SetUp]
    public void SetUp()
    {
        winConditionsGameObject = new GameObject();
        winConditions = winConditionsGameObject.AddComponent<WinConditions>();

        /************************************************************
         * Set testWinConditions variables to match WinConditions.cs
         ***********************************************************/
        timer = 0f;
        //gameWinner = GetComponent<TextMeshProUGUI>();
        //gameWinner.text = "Winner ";
        playerCtrlGameObject = new GameObject();
        playerCtrl = playerCtrlGameObject.AddComponent<PlayerController>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(winConditionsGameObject);
        Object.DestroyImmediate(playerCtrlGameObject);
    }

    /******************************************************
     *
     * TEST 
     * 
     *******************************************************/


    [Test]
    public void testGameOver()
    {
        // Create the winning player
        Material m = new Material(Resources.Load<Material>("Materials/PlayerTileColor"));
        Player winner = new Player("Bobby", m);

        // Clear list so that there is only one player
        PlayerController.players = new List<Player>
        {
            winner
        };

        winConditions.gameIsOver(winner);
    }

    [Test]
    public void testFindWinner()
    {
        // Create the winning player
        Material m = new Material(Resources.Load<Material>("Materials/PlayerTileColor"));
        Player winner = new Player("Bobby",m);

        // Clear list so that there is only one player
        PlayerController.players = new List<Player>
        {
            winner
        };

        // timer < maxTime
        winConditions.findWinner(playerCtrl);
        Assert.AreEqual(null,winConditions.winningPlayer);


        // timer > maxTime
        winConditions.timer += winConditions.maxTime;
        winConditions.findWinner(playerCtrl);
        Assert.AreEqual(winner.GetName(), winConditions.winningPlayer.GetName());
    }

}





