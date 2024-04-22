using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryManager;


public class InventoryTests
{

    [SetUp]
    public void SetUp()
    {
        // set up inventory slots for each test
        // Inventory.manager.SetupSlot(0);
        // Inventory.manager.SetupSlot(1);
        // Inventory.manager.SetupSlot(2);
        // Inventory.manager.SetupSlot(3);
        // Inventory.manager.SetupSlot(4);
         //inventory.manager = managerMock;

        for (int i = 0; i < 5; i++){
            GetManager().SetupSlot(i);
        }

    }


    [Test]
    public void TestAddCardToStack_WhenNoStackExists()
    {
    
        Inventory inventory = new Inventory();

        // Create a new card
        Card newCard = new Card(null, "New Card");
        Assert.AreEqual(0, inventory.GetStacksListSize());

         //Add new card to inventory
        inventory.AddToCardToStack(newCard);



        // Check if card is in now in inventory
        Assert.AreEqual(1, inventory.GetStacksListSize());
        Assert.AreEqual(1, inventory.GetStack(0).GetSize());
        Assert.AreEqual(newCard.getName(), inventory.GetStack(0).GetCardinStack().getName());
    }


    [Test]
    public void TestAddCardToStack_WhenStackAlreadyExists()
    {

        Inventory inventory = new Inventory();

        // Create a new cards
        Card exisitingCard = new Card(null, "Existing Card");
        Card newCard = new Card(null, "New Card");

        inventory.GetStacks().Add(new CardStack(exisitingCard, 5));

        //Add card to inventory
        inventory.AddToCardToStack(newCard);

        // Check if card is in now in inventory
        Assert.AreEqual(2, inventory.GetStacksListSize());
        //Assert.AreEqual(newCard.getName(), inventory.GetStack(1).GetCardinStack().getName());
        }



    [Test]
    public void TestRemoveCardFromStackNoIndex_OneCardRemaining()
    {

        Inventory inventory = new Inventory();

        Card newCard = new Card(null, "New Card");
       // List<CardStack> cardStacks = inventory.GetStacks();

        inventory.GetStacks().Add(new CardStack(newCard, 5));

        //Remove card from inventory
        inventory.RemoveCardFromStack(newCard);

        // Check if card is no longer in inventory
        Assert.AreEqual(0, inventory.GetStacksListSize());
    }


    [Test]
    public void TestRemoveCardFromStackNoIndex_MutipleCardsRemaining()
    {
        Inventory inventory = new Inventory();
         
        // Create cards
        Card card1 = new Card(null, "Card1");
        Card card2 = new Card(null, "Card2");
        Card card3 = new Card(null, "Card3");
        //List<CardStack> cardStacks = inventory.GetStacks();
        //Add cards to inventory
        inventory.GetStacks().Add(new CardStack(card1, 5));
        inventory.AddToCardToStack(card2);
        inventory.AddToCardToStack(card3);


        //Remove card from inventory
        inventory.RemoveCardFromStack(card3);

        // Check if card is no longer in inventory
        Assert.AreEqual(2, inventory.GetStacksListSize());
        //Assert.AreEqual(2, inventory.GetStack(0).GetSize());
    }


    [Test]
    public void TestRemoveCardFromStackWithValidIndex()
    {
        Inventory inventory = new Inventory();
        // Create cards
        Card card1 = new Card(null, "Card1");
        Card card2 = new Card(null, "Card2");
        Card card3 = new Card(null, "Card3");

        //Add cards to inventory
        inventory.GetStacks().Add(new CardStack(card1, 5));
        inventory.AddToCardToStack(card2);
        inventory.AddToCardToStack(card3);
        //Remove card from inventory
        inventory.RemoveCardFromStack(card2, 1);

        // Check that card is still in inventory
        Assert.AreEqual(2, inventory.GetStacksListSize());
        //Assert.AreEqual(2, inventory.GetStack(0).GetSize());
    }



    // [Test]
    // public void TestRemoveCardFrominventoryWithInvalidIndex()
    // {
    //     Inventory inventory = new Inventory();

    //     // Create cards
    //     Card card1 = new Card(null, "Card1");
    //     Card card2 = new Card(null, "Card2");
    //     Card card3 = new Card(null, "Card3");
    //     //List<CardStack> cardStacks = inventory.GetStacks();
    //     //Add cards to inventory
    //     inventory.GetStacks().Add(new CardStack(card1, 5));
    //     inventory.AddCardToStack(card2);
    //     inventory.AddCardToStack(card3);
    //     //Remove card from inventory
    //     inventory.RemoveCardFromStack(card2, 50);

    //     // Check that card is still in inventory
    //     Assert.AreEqual(3, inventory.GetStacksListSize());
    //     //Assert.AreEqual(3, inventory.GetStack(0).GetSize());
    // }




    [Test]
    public void TestMoveCardToHand()
    {

        Inventory inventory = new Inventory();

        Card card1 = new Card(null, "Card1");
        Card card2 = new Card(null, "Card2");
        inventory.GetStacks().Add(new CardStack(card1, 5));
        inventory.AddToCardToStack(card2);

        //Add card to inventory
        inventory.MoveCardToHand(1, card2, 1);

        // Check if card is in now in inventory
         Assert.AreEqual(1, inventory.GetStacksListSize());
    }


    [Test]
    public void TestMoveCardFromHand()
    {

        Card card1 = new Card(null, "Card1");
        Card card2 = new Card(null, "Card2");
        Inventory inventory = new Inventory();
        Hand hand = new Hand();
        inventory.GetStacks().Add(new CardStack(card1, 5));
        hand.AddCardtoHand(card2);
        inventory.MoveCardFromHand(1, card2);
        Assert.AreEqual(2, inventory.GetStacksListSize());
    }



    [TearDown]
    public void TearDown(){
        Inventory inventory = new Inventory();
        InventoryManager.ClearSlots();
        inventory.GetStacks().Clear();
    }

}
