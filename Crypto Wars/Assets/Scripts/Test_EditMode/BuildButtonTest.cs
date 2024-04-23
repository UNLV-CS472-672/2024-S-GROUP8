using NUnit.Framework;
using UnityEngine;
using UnityEditor;

public class BuildButtonTest
{
    public GameObject go;
    public BuildButtonScript buildButton;


    [SetUp]
    public void SetUp()
    {
        go = new GameObject("Build Button");
        go.AddComponent<BuildButtonScript>();
        buildButton = go.GetComponent<BuildButtonScript>();
        go.transform.position = new Vector3(0, 0, 0);
    }

    [Test]
    public void TestOutOfFrame()
    {
        Vector3 expectedPosition = new Vector3(0,-375,0);
        buildButton.DeactivateMain();
        Assert.AreEqual(expectedPosition, go.transform.position);
    }
}