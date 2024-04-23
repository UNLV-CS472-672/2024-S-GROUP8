using System.Collections.Generic;
using UnityEngine;

public class CardRegistry
{
    // A list to hold all the cards
    private static List<Card> cardList = new List<Card>();

    // Initialized before buildingRegistry
    public static void Load()
    {
        // Create the cards
        CreateCard("Python", 5, 55, 20);
        CreateCard("Java", 8, 21, 30);
        CreateCard("C", 1, 34, 40);

        // To test, log the names of the cards that were created
        foreach (var card in cardList)
        {
            Debug.Log("Created card: " + card.GetName());
        }
    }

    // Method to create a card and add it to the list
    private static void CreateCard(string name, int offense, int defense, int staminaCost)
    {
        
        Sprite cardSprite = null; // Card sprite placeholder 

        Card newCard = new Card(cardSprite, name);
        newCard.setOffense(offense);
        newCard.setDefense(defense);
        newCard.setStaminaCost(staminaCost);

        cardList.Add(newCard);
    }

    // Method to get a card by name
    // Might be helpful
    public static Card GetCardByName(string cardName)
    {
        Card card = cardList.Find(card => card.GetName() == cardName);
        Card newCard = new Card(card.GetSprite(), card.GetName());
        newCard.setOffense(card.getOffense());
        newCard.setDefense(card.getDefense());
        newCard.setStaminaCost(card.getStaminaCost());
        return newCard;
    }

    public static Card GetCardByIndex(int index)
    {
        Card card = cardList[index];
        Card newCard = new Card(card.GetSprite(), card.GetName());
        newCard.setOffense(card.getOffense());
        newCard.setDefense(card.getDefense());
        newCard.setStaminaCost(card.getStaminaCost());
        return newCard;
    }
}
