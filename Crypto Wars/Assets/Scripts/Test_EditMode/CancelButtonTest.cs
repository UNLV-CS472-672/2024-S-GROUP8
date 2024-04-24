using NUnit.Framework;
using UnityEngine;
using UnityEditor;

public class CancelButtonTest
{
    public PlayerController testController;

    [SetUp]
    public void SetUp()
    {
        GameObject camera = GameObject.Find("Main Camera");
        PlayerController PlayerController = camera.GetComponent<PlayerController>();
        testController = PlayerController;
    }

    [Test]
    public void TestDeactivate()
    {
        testController.GetCancelButton().GetComponent<CancelButton>().Deactivate();
        Assert.AreEqual(testController.GetCancelButton().activeSelf, false);
    }
}