using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BuildButtonScriptTest
{
    private GameObject go, canvas, buildButton, destroyButton, cancelButton;

    private BuildButtonScript testBuildButton;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject("BuildMenu");
        canvas = new GameObject("Canvas");

        buildButton = new GameObject("BuildButton");
        buildButton.transform.SetParent(canvas.transform);

        destroyButton = new GameObject("DestroyButton");
        destroyButton.transform.SetParent(canvas.transform);

        cancelButton = new GameObject("CancelButton");
        cancelButton.transform.SetParent(canvas.transform);

        go.AddComponent<BuildButtonScript>();
        testBuildButton = go.GetComponent<BuildButtonScript>();

        // Use the public properties to assign mock objects to script
        typeof(BuildButtonScript).GetProperty("BuildButton").SetValue(testBuildButton, buildButton, null);
        typeof(BuildButtonScript).GetProperty("DestroyButton").SetValue(testBuildButton, destroyButton, null);
    }

    [Test]
    public void TestCreateBuildButton()
    {
        Assert.IsNotNull(testBuildButton.buildButton.GetComponentInChildren<Text>());
    }

    [Test]
    public void TestCreateDestroyButton()
    {
        Assert.IsNotNull(testBuildButton.destroyButton.GetComponentInChildren<Text>());
    }

    [Test]
    public void TestCreateCancelButton()
    {
        Assert.IsNotNull(testBuildButton.cancelButton.GetComponentInChildren<Text>());
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(go);
        Object.DestroyImmediate(canvas);
        Object.DestroyImmediate(buildButton);
        Object.DestroyImmediate(destroyButton);
        Object.DestroyImmediate(cancelButton);
    }
}
