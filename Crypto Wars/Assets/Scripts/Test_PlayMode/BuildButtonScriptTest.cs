using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BuildButtonScriptTest
{
    private GameObject go, canvas;
    private BuildButtonScript testBuildButton;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject("BuildMenu");
        canvas = new GameObject("Canvas");

        var buildButton = new GameObject("BuildButton");
        buildButton.transform.SetParent(canvas.transform);

        var destroyButton = new GameObject("DestroyButton");
        destroyButton.transform.SetParent(canvas.transform);

        go.AddComponent<BuildButtonScript>();
        testBuildButton = go.GetComponent<BuildButtonScript>();
    }

    [Test]
    public void TestCreateBuildButton()
    {
        // Ensure the build button is properly instantiated and has an Image component
        Assert.IsNotNull(testBuildButton.BuildButton);
        Assert.IsNotNull(testBuildButton.BuildButton.GetComponent<Image>());
    }

    [Test]
    public void TestCreateDestroyButton()
    {
        // Ensure the destroy button is properly instantiated and has an Image component
        Assert.IsNotNull(testBuildButton.DestroyButton);
        Assert.IsNotNull(testBuildButton.DestroyButton.GetComponent<Image>());
    }

    // CancelButton??

}