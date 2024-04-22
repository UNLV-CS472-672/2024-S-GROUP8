using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class VictoryScreenTest
{
    GameObject panel;
    VictoryScreen vs;

    [SetUp]
    public void SetUp()
    {
        panel = new GameObject("VictoryScreen");
        panel.AddComponent<VictoryScreen>();
        vs = panel.GetComponent<VictoryScreen>();
        vs.transform.localPosition = Vector3.zero;
    }

    [Test]
    public void TestGUIdisplay()
    {
        Assert.AreEqual(Vector3.zero, panel.transform.localPosition);
    }
}
