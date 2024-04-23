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
        GameObject Stash = GameObject.Find("Stash Bar").transform.Find("Stash").gameObject;
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
        Stash StashScript = GameObject.Find("Stash Bar").transform.Find("Stash").gameObject.GetComponent<Stash>();
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
        Stash StashScript = GameObject.Find("Stash Bar").transform.Find("Stash").gameObject.GetComponent<Stash>();
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
        Stash StashScript = GameObject.Find("Stash Bar").transform.Find("Stash").gameObject.GetComponent<Stash>();
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
        GameObject StashObj = GameObject.Find("Stash Bar").transform.Find("Stash").gameObject;
        Stash StashScript = StashObj.GetComponent<Stash>();
        StashScript.Activate(true);

        Assert.True(StashObj.activeSelf);
    }

    [UnityTest]
    public IEnumerator TestActiveForAttack()
    {
        yield return new WaitForSeconds(0.5f);

        PlayerController.players = new List<Player>
        {
            new Player("Phil", null),
            new Player("John", null)
        };
        PlayerController.CurrentPlayer = PlayerController.players[0];
        PlayerController.CurrentPlayer.SetPhase(Player.Phase.Attack);
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<Tile>();
        gameObject.GetComponent<Tile>().SetTilePosition(1, 1);
        gameObject.GetComponent<Tile>().SetPlayer(0);
        PlayerController.SetSelectedTile(gameObject.GetComponent<Tile>());

        Card TestCard = new Card(null, "John");
        PlayerController.CurrentPlayer.GetInventory().GetHand().AddCardtoHand(TestCard);
        GameObject StashObj = GameObject.Find("Stash Bar").transform.Find("Stash").gameObject;
        Stash StashScript = StashObj.GetComponent<Stash>();
        StashScript.Activate(true);
        StashScript.Clear();
        StashScript.HandtoStash();

        StashScript.Accept();

        Assert.True(GameManager.PlannedBattles.Count > 0);
    }

    [UnityTest]
    public IEnumerator TestActiveForDefense()
    {
        yield return new WaitForSeconds(0.5f);

        PlayerController.players = new List<Player>
        {
            new Player("Phil", null),
            new Player("John", null)
        };
        PlayerController.CurrentPlayer = PlayerController.players[0];
        List<Card> cards = new List<Card>
        {
            new Card(null, "Steve")
        };
        Battles.AttackObject makeAttack = new Battles.AttackObject(cards, new Vector2(0, 0), new Vector2(0, 0));
        GameManager.AddAttackerToBattle(PlayerController.CurrentPlayer, PlayerController.players[1], makeAttack, null);

        PlayerController.CurrentPlayer = PlayerController.players[1];
        PlayerController.CurrentPlayer.SetPhase(Player.Phase.Defense);
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<Tile>();
        gameObject.GetComponent<Tile>().SetTilePosition(0, 0);
        gameObject.GetComponent<Tile>().SetPlayer(0);
        PlayerController.SetSelectedTile(gameObject.GetComponent<Tile>());

        Card TestCard = new Card(null, "John");
        PlayerController.CurrentPlayer.GetInventory().GetHand().AddCardtoHand(TestCard);
        GameObject StashObj = GameObject.Find("Stash Bar").transform.Find("Stash").gameObject;
        Stash StashScript = StashObj.GetComponent<Stash>();
        StashScript.Activate(true);
        StashScript.Clear();
        StashScript.HandtoStash();

        StashScript.Accept();

        Assert.True(GameManager.FinalBattles.Count > 0);
    }

    [UnityTest]
    public IEnumerator TestDeactivate()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject StashObj = GameObject.Find("Stash Bar").transform.Find("Stash").gameObject;
        Stash StashScript = StashObj.GetComponent<Stash>();
        StashScript.Activate(false);

        Assert.False(StashObj.activeSelf);
    }

    
}