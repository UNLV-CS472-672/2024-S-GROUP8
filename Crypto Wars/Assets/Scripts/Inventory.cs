using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Inventory
{
    private List<CardStack> CardStacks = new List<CardStack>();
    private const int maxSize = 5;
    private const int maxCardStack = 16;
    private Hand hand = new Hand();

    // Add a card to the UI inventory
    public void AddToCardToStack(Card card)
    {
        // Get Manager
        InventoryManager manager = InventoryManager.GetManager();

        // No stacks currently exist
        if (CardStacks.Count < 1)
        {
            CardStacks.Add(new CardStack(card, maxCardStack));
            Debug.Log(CardStacks[0].GetCardinStack().GetName().Equals(""));
            manager.SetText("CardName", 0, "" + CardStacks[0].GetCardinStack().GetName(), "CardName Bar");
            manager.SetText("Amount", 0, "" + 1);
        }
        // Stacks already exist
        else
        {
            // No new stack is necassary
            bool hasBeenAdded = false;
            for (int i = 0; i < CardStacks.Count; i++)
            {
                if (!CardStacks[i].IsFull())
                {
                    if (CardStacks[i].AddCardtoStack(card))
                    {
                        hasBeenAdded = true;
                        manager.SetText("Amount", i, "" + CardStacks[i].GetSize());
                    }
                }
            }

            // Prior Stacks are filled
            if (!hasBeenAdded && CardStacks.Count < maxSize)
            {
                CardStacks.Add(new CardStack(card, maxCardStack));
                manager.SetText("CardName", CardStacks.Count - 1, "" + CardStacks[CardStacks.Count - 1].GetCardinStack().GetName(), "CardName Bar");
                manager.SetText("Amount", CardStacks.Count - 1, "" + CardStacks[CardStacks.Count - 1].GetSize());
            }
            else
            {
                // Card cannot be added in any way
            }
        }
    }


    // Method for quick removable if player doesn't want to select which stack
    // the player wants to draw from
    public void RemoveCardFromStack(Card card)
    {
        CardStack selectedStack = null;
        // Grab the first stack in the array
        if (CardStacks.Count > 0)
            selectedStack = CardStacks[0];
        int index = 0;
        if (selectedStack != null)
        {
            // Find stack with an existing card name
            foreach (CardStack existing in CardStacks)
            {
                if (existing.GetCardinStack().GetName().Equals(card.GetName()))
                {
                    selectedStack = existing;
                    break;
                }
                else {
                    selectedStack = null;
                }
                index++;
            }
            if(selectedStack != null)
                RemoveCardFromStack(card, index);
        }
    }

    // Method for if the player chooses the specific stack to remove from
    public void RemoveCardFromStack(Card card, int index)
    {
        // Get Manager
        InventoryManager manager = InventoryManager.GetManager();
        // Cardstack has one card remaining
        if (CardStacks[index].GetSize() < 2)
        {
            // Delete cardstack slot
            CardStacks.RemoveAt(index);
            manager.ReorderSlots(); // Reorder slots leftward
        }
        // Cardstack has more than one card remaining
        else
        {
            if (!CardStacks[index].RemoveCardFromStack(card))
                Debug.Log("Card to be removed does not exist in CardStack");
            else
                manager.SetText("Amount", index, "" + CardStacks[index].GetSize());
        }
    }

    // Get the inventory's hand
    public Hand GetHand() { return hand; }

    // Get a stack in the inventory
    public CardStack GetStack(int index) { return CardStacks[index]; }

    // Get size of the inventory
    public int GetStacksListSize()
    {
        return CardStacks.Count;
    }

    // Get the entire list of cardstacks
    public List<CardStack> GetStacks()
    {
        return CardStacks;
    }
    
    // Transfers a amount of cards from the inventory to the hand
    public void MoveCardToHand(int amount, Card card, int index = -1)
    {
        if (index > -1) {
            if (!CardStacks[index].GetCardinStack().GetName().Equals(card.GetName())) {
                return;
            }
        }
        for (int i = amount; i > 0; i--)
        {
            hand.AddCardtoHand(card);
            if(index > -1)
                RemoveCardFromStack(card, index);
            else
                RemoveCardFromStack(card);
        }
    }

    // Transfers a amount of cards from the hand to the inventory
    public void MoveCardFromHand(int amount, Card card)
    {
        for (int i = amount; i > 0; i--)
        {
            // Refactor to allow for adding to stack index
            hand.RemoveCardfromHand(card);
            AddToCardToStack(card);
        }
    }

    // Gives each player their starting cards
    public static void StartingInventory(int[] cardArray) {
        for (int i = 0; i < cardArray.Length; i++) {
            for (int j = 0; j < cardArray[i]; j++) {
                foreach (Player player in PlayerController.players) {
                    Inventory inventory = player.GetInventory();
                    inventory.AddToCardToStack(CardRegistry.GetCardByIndex(i));
                }
            }
        }
    }
}

