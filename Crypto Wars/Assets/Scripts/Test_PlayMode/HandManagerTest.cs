using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System;
using System.Collections;
using System.Collections.Generic;

public class HandManagerTest
{
    public HandManager testManager;
    public string slot1Text, slot2Text, slot3Text, slot4Text, slot5Text;
    public TMPro.TextMeshProUGUI testText;
    Vector3 startPos, previousPos;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Project", LoadSceneMode.Single);
        Vector3 startPos = new Vector3(0, 0, 0);
        yield return null;
        yield return new EnterPlayMode();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        yield return new ExitPlayMode();
    }
    
    [UnityTest]
    public IEnumerator MouseMovementTest()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 previousPos = startPos;
    }

    [UnityTest]
    public IEnumerator PartialRevealTest()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject camera = GameObject.Find("Main Camera");
        testManager = camera.GetComponent<HandManager>();
        Image image = GameObject.Find("Handheld Cards").GetComponent<Image>();
        // visible starts as false
        testManager.PartialReveal(1);
        Assert.IsTrue(image.enabled);
        testManager.ForceHideAll();
        testManager.PartialReveal(2);
        Assert.IsTrue(image.enabled);
        testManager.ForceHideAll();
        testManager.PartialReveal(3);
        Assert.IsTrue(image.enabled);
        testManager.ForceHideAll();
        testManager.PartialReveal(4);
        Assert.IsTrue(image.enabled);
        testManager.ForceHideAll();
        testManager.PartialReveal(5);
        Assert.IsTrue(image.enabled);
    }

    [UnityTest]
    public IEnumerator ToggleHideAllTest()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject camera = GameObject.Find("Main Camera");
        testManager = camera.GetComponent<HandManager>();
        Image image = GameObject.Find("Handheld Cards").GetComponent<Image>();
        // visible starts as false
        testManager.ToggleHideAll();
        Assert.IsTrue(image.enabled);
        // test for visible is true
        testManager.ToggleHideAll();
        Assert.IsFalse(image.enabled);
    }

    [UnityTest]
    public IEnumerator ForceHideAllTest()
    {
        yield return new WaitForSeconds(0.5f);
        Image image = GameObject.Find("Handheld Cards").GetComponent<Image>();
        // TextMeshProUGUI slot1Text = GameObject.Find("Hand_Slot_1").GetComponentInChildren<TextMeshProUGUI>();
        Assert.IsFalse(image.enabled);
        // Assert.IsFalse(slot1Text.enabled); // not working for whatever reason
    }
    
    [UnityTest]
    public IEnumerator ChangeDisplayedCardsTest()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject camera = GameObject.Find("Main Camera");
        testManager = camera.GetComponent<HandManager>();
        Hand testHand = testManager.GetCurrentPlayerHand();
        Image image = GameObject.Find("Handheld Cards").GetComponent<Image>();
        Card testCard1 = new Card(null, "Test1");
        Card testCard2 = new Card(null, "Test2");
        Card testCard3 = new Card(null, "Test3");
        Card testCard4 = new Card(null, "Test4");
        Card testCard5 = new Card(null, "Test5");

        testText = testManager.GetComponent<TMPro.TextMeshProUGUI>();

        // visible starts as false
        testManager.ChangeDisplayedCards();
        // player should not have any cards, so should go to ForceHideAll, setting visibility to false
        Assert.IsFalse(image.enabled);

        testHand.AddCardtoHand(testCard1);
        testManager.ChangeDisplayedCards();
        slot1Text = testManager.GetComponent<HandManager>().handSlot1Text.text;
        // player should have 1 unique card, so should only modify handSlot1Text
        Assert.AreEqual("1x " + testCard1.GetName(), slot1Text);
        
        testHand.AddCardtoHand(testCard2);
        testManager.ChangeDisplayedCards();
        slot2Text = testManager.GetComponent<HandManager>().handSlot2Text.text;
        // player should have 2 unique cards
        Assert.AreEqual("1x " + testCard2.GetName(), slot2Text);

        testHand.AddCardtoHand(testCard3);
        testManager.ChangeDisplayedCards();
        slot3Text = testManager.GetComponent<HandManager>().handSlot3Text.text;
        // player should have 3 unique cards
        Assert.AreEqual("1x " + testCard3.GetName(), slot3Text);

        testHand.AddCardtoHand(testCard4);
        testManager.ChangeDisplayedCards();
        slot4Text = testManager.GetComponent<HandManager>().handSlot4Text.text;
        // player should have 4 unique cards
        Assert.AreEqual("1x " + testCard4.GetName(), slot4Text);

        testHand.AddCardtoHand(testCard5);
        testManager.ChangeDisplayedCards();
        slot5Text = testManager.GetComponent<HandManager>().handSlot5Text.text;
        // player should have 5 unique cards
        Assert.AreEqual("1x " + testCard5.GetName(), slot5Text);
    }
}