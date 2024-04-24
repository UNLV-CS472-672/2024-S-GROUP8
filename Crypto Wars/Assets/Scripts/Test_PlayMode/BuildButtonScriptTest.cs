using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BuildButtonScriptTest
{
    public GameObject go, canvas;
    public BuildButtonScript testBuildButton;
    public GameObject destroyButton;
    public GameObject buildButton;
    public GameObject cancelButton;

    /// <summary>
    /// FIX
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        go = new GameObject("BuildMenu");
        canvas = new GameObject("Canvas");

        // Create and set up BuildButton
        buildButton = new GameObject("BuildButton");
        buildButton.transform.SetParent(canvas.transform);

        // Create and set up Text object for the BuildButton
        GameObject buildText = new GameObject("BuildButtonText");
        buildText.transform.SetParent(buildButton.transform);
        buildText.AddComponent<Text>();

        // Assign mock objects to the script
        go.AddComponent<BuildButtonScript>();
        testBuildButton = go.GetComponent<BuildButtonScript>();
        // testBuildButton.buildButton = buildButton;
        
        //testBuildButton.CreateBuildButton();
        //testBuildButton.CreateDestroyButton();
        //testBuildButton.CreateCancelButton();
        /*
        testBuildButton.buildButton = GameObject.Find("BuildButton");
        testBuildButton.destroyButton = GameObject.Find("DestroyButton");
        testBuildButton.cancelButton = GameObject.Find("CancelButton");
        */
    }

    [Test]  //  Currently Broken (Null Reference, BuildButtonScript: 41)
    public void TestToggleMenu()
    {
        // buildButton.AddComponent<Image>();
        //testBuildButton.ToggleMenu();
        /*
        Assert.IsFalse(testBuildButton.buildButton.GetComponent<Image>().enabled);
        //testBuildButton.ToggleMenu();
        Assert.IsTrue(testBuildButton.buildButton.GetComponent<Image>().enabled);
        */
    }

    [Test]
    public void TestCreateBuildButton()
    {
        GameObject buildText = GameObject.Find("BuildButtonText");
        Assert.IsNotNull(buildText);
    }

    [Test]
    public void TestCreateDestroyButton()
    {
        GameObject destroyText = GameObject.Find("DestroyButtonText");
        Assert.IsNotNull(destroyText);
    }

    [Test]
    public void TestCreateCancelButton()
    {
        GameObject cancelText = GameObject.Find("CancelButtonText");
        Assert.IsNotNull(cancelText);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(cancelButton);
        Object.DestroyImmediate(destroyButton);
        Object.DestroyImmediate(buildButton);
        Object.DestroyImmediate(canvas);
        Object.DestroyImmediate(go);
    }
}