using NUnit.Framework;
using UnityEngine;
using UnityEditor;

public class CancelButtonTest
{
    public GameObject go;
    public CancelButton cancelButton;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject("Cancel Button");
        go.AddComponent<CancelButton>();
        cancelButton = go.GetComponent<CancelButton>();
        go.transform.position = new Vector3(0, 0, 0);
    }

    [Test]
    public void TestDeactivate()
    {
        cancelButton.Deactivate();
        Assert.AreEqual(cancelButton.isActiveAndEnabled, false);
    }
}