using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class StashTest
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Project", LoadSceneMode.Single);
        yield return null;
        yield return new EnterPlayMode();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        yield return new ExitPlayMode();
    }

    [UnityTest]
    public IEnumerator SceneTest()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject Stash = GameObject.Find("Stash");
        GameObject Inventory = GameObject.Find("Inventory Bar");
        GameObject Camera = GameObject.Find("Main Camera");
        
        Assert.IsNotNull(Stash);
        Assert.IsNotNull(Camera);
        Assert.IsNotNull(Inventory);


        Stash StashScript = Stash.GetComponent<Stash>();
        Assert.IsNotNull(StashScript);

        PlayerController PlayerController = Camera.GetComponent<PlayerController>();
        Assert.IsNotNull(PlayerController);

    }

    [UnityTest]
    public IEnumerator TestHandtoStash()
    {
        yield return new WaitForSeconds(0.5f);
        Card TestCard = new Card(null, "John");
        PlayerController.CurrentPlayer.GetInventory().GetHand().AddCardtoHand(TestCard);
        GameObject Stash = GameObject.Find("Stash");
        Stash StashScript = Stash.GetComponent<Stash>();
        StashScript.Clear();
        StashScript.HandtoStash();
        
        Assert.AreEqual(StashScript.GetStashSize(), 1);
    }

    [UnityTest]
    public IEnumerator TestStashToInventory()
    {
        yield return new WaitForSeconds(0.5f);
        Card TestCard = new Card(null, "John");
        PlayerController.CurrentPlayer.GetInventory().GetHand().AddCardtoHand(TestCard);
        GameObject Stash = GameObject.Find("Stash");
        Stash StashScript = Stash.GetComponent<Stash>();
        StashScript.HandtoStash();

        StashScript.StashtoInventory();
        Assert.AreEqual(PlayerController.CurrentPlayer.GetInventory().GetStack(0).GetSize(), 1);
        PlayerController.CurrentPlayer.GetInventory().RemoveCardFromStack(TestCard);
    }


    [UnityTest]
    public IEnumerator TestCancel()
    {
        yield return new WaitForSeconds(0.5f);
        Card TestCard = new Card(null, "John");
        PlayerController.CurrentPlayer.GetInventory().GetHand().AddCardtoHand(TestCard);
        GameObject Stash = GameObject.Find("Stash");
        Stash StashScript = Stash.GetComponent<Stash>();
        StashScript.HandtoStash();

        StashScript.Cancel();
        PlayerController.CurrentPlayer.GetInventory().RemoveCardFromStack(TestCard);
        Assert.AreEqual(StashScript.GetStashSize(), 0);
    }

    [UnityTest]
    public IEnumerator TestAccept()
    {
        yield return new WaitForSeconds(0.5f);
        // When accept is finished
    }

    [UnityTest]
    public IEnumerator TestActivate()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject Stash = GameObject.Find("Stash");
        Stash StashScript = Stash.GetComponent<Stash>();
        StashScript.Activate(true);

        Assert.True(Stash.activeSelf);
    }

    [UnityTest]
    public IEnumerator TestDeactivate()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject Stash = GameObject.Find("Stash");
        Stash StashScript = Stash.GetComponent<Stash>();
        StashScript.Activate(false);

        Assert.False(Stash.activeSelf);
    }

}