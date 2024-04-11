using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingforTests
{
    [Test]
    public void Testing() { 
        Card card = new Card(null, "Smith");
        Inventory inventory = new Inventory();
        Inventory.manager.SetupSlot(0);

        inventory.AddToCardToStack(card);
        Assert.AreEqual(inventory.GetStacksListSize(), 1);
    }
}
