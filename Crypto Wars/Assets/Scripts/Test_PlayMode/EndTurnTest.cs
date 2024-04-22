using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EndTurnTest
{
    public GameObject go;
    int startNum;
    string phaseText;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject("EndTurnButton");
        go.AddComponent<EndTurn>();
        go.AddComponent<TMPro.TextMeshProUGUI>();
        go.GetComponent<EndTurn>().turnOutput = go.GetComponent<TMPro.TextMeshProUGUI>();
        go.GetComponent<EndTurn>().phaseOutput = go.GetComponent<TMPro.TextMeshProUGUI>();
    }

    [Test]
    public void TestTurnNumberIncrement()
    {
        go.GetComponent<EndTurn>().Advance();
        Assert.AreNotEqual(startNum, go.GetComponent<EndTurn>().turnNum);
    }

    [Test]
    public void TestPhaseIncrement()
    {
        phaseText = "Phase: " + "Defense";
        go.GetComponent<EndTurn>().Advance();
        Assert.AreNotEqual(phaseText, go.GetComponent<EndTurn>().phaseOutput.text);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(go);
    }
}