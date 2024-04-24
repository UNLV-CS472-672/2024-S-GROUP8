using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;


public class SettingsTest
{
    public GameObject go;
    public SettingsMenu settTest;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject();
        go.AddComponent<SettingsMenu>();
        settTest = go.GetComponent<SettingsMenu>();
    }

    [Test]
    public void TestPlayerCountSettings()
    {
        settTest.setTwoPlayer();
        Assert.AreEqual(settTest.getPlayerCount(), 2);
        settTest.setThreePlayer();
        Assert.AreEqual(settTest.getPlayerCount(), 3);
        settTest.setFourPlayer();
        Assert.AreEqual(settTest.getPlayerCount(), 4);
    }

    [Test]
    public void TestHotseatSettings()
    {
        settTest.hotSeatEnable();
        Assert.AreEqual(settTest.getHotseat(), true);
        settTest.hotseatDisable();
        Assert.AreEqual(settTest.getHotseat(), false);
    }

    [Test]
    public void TestGameTimeSettings()
    {
        settTest.gameTimeSettings(0);
        Assert.AreEqual(settTest.getTimerWinCondition(), 45.5f);

        settTest.gameTimeSettings(1);
        Assert.AreEqual(settTest.getTimerWinCondition(), 60f);

        settTest.gameTimeSettings(2);
        Assert.AreEqual(settTest.getTimerWinCondition(), 300f);
    }

    [Test]
    public void TestTilePercentageSettings()
    {
        settTest.tilePercentageSettings(0);
        Assert.AreEqual(settTest.getTileWinCondition(), 0.5f);
        settTest.tilePercentageSettings(1);
        Assert.AreEqual(settTest.getTileWinCondition(), 0.75f);
        settTest.tilePercentageSettings(2);
        Assert.AreEqual(settTest.getTileWinCondition(), 1f);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(go);
    }

}
