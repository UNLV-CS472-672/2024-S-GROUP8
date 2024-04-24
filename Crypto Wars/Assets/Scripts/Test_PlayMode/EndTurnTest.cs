using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class EndTurnTest
{
    public GameObject turn;
    public EndTurn end;
    int startNum = 1;
    string phaseText;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Project", LoadSceneMode.Single);
        yield return null;
        yield return new EnterPlayMode();

        turn = GameObject.Find("Canvas").transform.Find("TurnCounter").gameObject;
        EndTurn thing = turn.GetComponent<EndTurn>();
        end = thing;
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

        Assert.IsNotNull(turn);
        Assert.IsNotNull(end);

    }

    [UnityTest]
    public IEnumerator TestPhaseBuild()
    {
        yield return new WaitForSeconds(0.5f);
        phaseText = "Phase: " + "Build";
        end.Advance();
        end.Advance();
        Assert.AreEqual(phaseText, end.phaseOutput.text);
    }

    [UnityTestAttribute]
    public IEnumerator TestPhaseIncrement()
    {
        yield return new WaitForSeconds(0.5f);
        phaseText = "Phase: " + "Attack";
        end.Advance();
        Assert.AreEqual(phaseText, end.phaseOutput.text);
    }
}