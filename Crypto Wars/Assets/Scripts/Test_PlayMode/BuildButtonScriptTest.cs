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
        buildButton.AddComponent<Image>();  // Image component??

        GameObject buildText = new GameObject("BuildButtonText");
        buildText.transform.SetParent(buildButton.transform);
        buildText.AddComponent<Text>();

        destroyButton = new GameObject("DestroyButton");
        destroyButton.transform.SetParent(canvas.transform);
        destroyButton.AddComponent<Image>();

        GameObject destroyText = new GameObject("DestroyButtonText");
        destroyText.transform.SetParent(destroyButton.transform);
        destroyText.AddComponent<Text>();

        cancelButton = new GameObject("CancelButton");
        cancelButton.transform.SetParent(canvas.transform);
        cancelButton.AddComponent<Image>();

        GameObject cancelText = new GameObject("CancelButtonText");
        cancelText.transform.SetParent(cancelButton.transform);
        cancelText.AddComponent<Text>();

        go.AddComponent<BuildButtonScript>();
        testBuildButton = go.GetComponent<BuildButtonScript>();
        testBuildButton.buildButton = buildButton;
        testBuildButton.destroyButton = destroyButton;
        testBuildButton.cancelButton = cancelButton;
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
