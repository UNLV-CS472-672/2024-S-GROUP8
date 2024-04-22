using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerControllerTest
{
    public PlayerController testController;

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
        GameObject Inventory = GameObject.Find("Inventory Bar");
        GameObject Camera = GameObject.Find("Main Camera");

        Assert.IsNotNull(Camera);
        Assert.IsNotNull(Inventory);

        PlayerController PlayerController = Camera.GetComponent<PlayerController>();
        testController = PlayerController;
        Assert.IsNotNull(PlayerController);

    }

    [UnityTest]
    public IEnumerator TestNextPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerController.NextPlayer();
        Assert.AreEqual(1, PlayerController.GetCurrentPlayerIndex());
    }
    

}