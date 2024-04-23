using NUnit.Framework;
using UnityEngine;
using UnityEditor;

public class AttackButtonScriptTest
{
    public GameObject go;
    public AttackButtonScript attackButton;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject("AttackButton");
        go.AddComponent<AttackButtonScript>();
        attackButton = go.GetComponent<AttackButtonScript>();
        go.transform.position = new Vector3(0, 0, 0);
    }

    [Test]
    public void TestOutOfFrame()
    {
        Vector3 expectedPosition = new Vector3(0,-375,0);
        attackButton.Deactivate();
        Assert.AreEqual(expectedPosition, go.transform.position);
    }
}
