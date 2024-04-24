using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

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
        GameObject attackButtonButton = new GameObject();
        attackButtonButton.AddComponent<Button>();
        attackButton.SetAttackButton(attackButtonButton);
        GameObject thing = new GameObject();
        thing.AddComponent<Stash>();
        attackButton.SetStash(thing.GetComponent<Stash>());
    }

    [Test]
    public void TestDeactivate()
    {
        attackButton.Deactivate();
        Assert.True(!attackButton.GetAttackButton().activeSelf);
    }
}
