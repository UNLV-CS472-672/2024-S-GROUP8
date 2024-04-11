using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EndTurnTest
{
    public GameObject go;
    int startNum;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject("EndTurnButton");
        go.AddComponent<EndTurn>();
        go.AddComponent<TMPro.TextMeshProUGUI>();
        go.GetComponent<EndTurn>().turnOutput = go.GetComponent<TMPro.TextMeshProUGUI>();
    }

    [Test]
    public void TestTurnNumberIncrement()
    {
        go.GetComponent<EndTurn>().endPlayerTurn();
        Assert.AreNotEqual(startNum, go.GetComponent<EndTurn>().turnNum);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(go);
    }
}