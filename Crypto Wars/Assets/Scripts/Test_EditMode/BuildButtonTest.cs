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
        // Create the main GameObject that the BuildButtonScript is attached to
        go = new GameObject("Build Button");
        go.AddComponent<BuildButtonScript>();
        buildButton = go.GetComponent<BuildButtonScript>();

        // Create a mock build button GameObject and assign it
        var mockBuildButton = new GameObject("Mock Build Button");
        buildButton.buildButton = mockBuildButton;

        go.transform.position = new Vector3(0, 0, 0);
    }

    [Test]
    public void TestOutOfFrame()
    {
        Vector3 expectedPosition = new Vector3(0, -375, 0);
        buildButton.DeactivateMain();
        Assert.AreEqual(expectedPosition, go.transform.position);
    }
}