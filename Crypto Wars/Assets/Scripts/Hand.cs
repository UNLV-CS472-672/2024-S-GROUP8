using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// The hand holds cards to be placed in a stash
// FUTURE: Allow players to visually see the cards they are holding
public class Hand 
{
    private List<Card> heldCards;

    public Hand() { 
        heldCards = new List<Card>();
    }

    // Gets the cards inside the hand
    public List<Card> GetHandCards() { 
        return heldCards;
    }

    // Add a card to the hand
    public void AddCardtoHand(Card Card) {
        heldCards.Add(Card);
        Debug.Log("Hand has " + heldCards.Count + " Cards");
    }

    // Removes a card from the hand
    public void RemoveCardfromHand(Card Card){
        heldCards.Remove(Card);
        Debug.Log("Hand has " + heldCards.Count + " Cards");
    }

    // Finds all the cards of a certain type in the hand
    public int CountofCardType(Card card) { 
        int i = 0;
        foreach (Card oldCard in heldCards) {
            if (oldCard.GetName().Equals(card.GetName())) {
                i++;
            }
        }
        return i;
    }

    // True if the hand contains no cards
    public bool IsEmpty() { 
        return heldCards.Count < 1;
    }
}
