using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CardRegistryTest
{
    [SetUp]
    public void SetUp()
    {
        // Implement Clear() method to reset list??
        CardRegistry.Load();
    }

    [Test]
    public void CardRegistry_Load_ShouldCreateSpecificCards()
    {
        // The Load method should create and log the cards
        Assert.IsNotNull(CardRegistry.GetCardByName("Python"));
        Assert.IsNotNull(CardRegistry.GetCardByName("Java"));
        Assert.IsNotNull(CardRegistry.GetCardByName("C"));
    }

    [Test]
    public void CardRegistry_GetCardByName_ShouldReturnCardWithCorrectAttributes()
    {
        // Test if the GetCardByName returns the correct card with the expected attributes
        var pythonCard = CardRegistry.GetCardByName("Python");
        Assert.IsNotNull(pythonCard);
        Assert.AreEqual("Python", pythonCard.GetName());
        Assert.AreEqual(5, pythonCard.getOffense());
        Assert.AreEqual(55, pythonCard.getDefense());
        Assert.AreEqual(20, pythonCard.getStaminaCost());

        var javaCard = CardRegistry.GetCardByName("Java");
        Assert.IsNotNull(javaCard);
        Assert.AreEqual("Java", javaCard.GetName());
        Assert.AreEqual(8, javaCard.getOffense());
        Assert.AreEqual(21, javaCard.getDefense());
        Assert.AreEqual(30, javaCard.getStaminaCost());

        var cCard = CardRegistry.GetCardByName("C");
        Assert.IsNotNull(cCard);
        Assert.AreEqual("C", cCard.GetName());
        Assert.AreEqual(1, cCard.getOffense());
        Assert.AreEqual(34, cCard.getDefense());
        Assert.AreEqual(40, cCard.getStaminaCost());
    }
}
