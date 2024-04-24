using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BuildButtonScriptTest
{
    private GameObject go;
    private BuildButtonScript testBuildButton;
    private GameObject canvas;
    
    [SetUp]
    public void SetUp()
    {
        // Create main GameObject and add the BuildButtonScript
        go = new GameObject("TestObject");
        testBuildButton = go.AddComponent<BuildButtonScript>();
        
        // Create a canvas and assign it as a parent to the build and destroy buttons
        canvas = new GameObject("Canvas");
        GameObject buildButton = new GameObject("BuildButton");
        buildButton.transform.SetParent(canvas.transform);
        buildButton.AddComponent<Button>();
        buildButton.SetActive(false); // Start with an inactive button to test activation
        
        GameObject buildMenu = new GameObject("BuildMenu");
        buildMenu.transform.SetParent(canvas.transform);
        buildMenu.SetActive(false); // Start with an inactive menu to test activation

        // Mock properties setup
        testBuildButton.GetType().GetProperty("BuildButton").SetValue(testBuildButton, buildButton, null);
        testBuildButton.GetType().GetProperty("DestroyButton").SetValue(testBuildButton, new GameObject("DestroyButton"), null);
        testBuildButton.buildMenu = buildMenu;
    }

    [Test]
    public void BuildButton_ActivatesCorrectly()
    {
        testBuildButton.ActivateMenu();
        Assert.IsTrue(testBuildButton.buildMenu.activeSelf, "Build menu should be active.");
    }

    [Test]
    public void BuildButton_DeactivatesCorrectly()
    {
        testBuildButton.DeactivateMenu();
        Assert.IsFalse(testBuildButton.buildMenu.activeSelf, "Build menu should be inactive.");
    }

    [TearDown]
    public void TearDown()
    {
        // Destroy the GameObjects created for the test
        Object.DestroyImmediate(go);
        Object.DestroyImmediate(canvas);
    }
}
