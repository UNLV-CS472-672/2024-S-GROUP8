using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack 
{
    private Card card;
    private int maxSize;
    private int currentSize;
    private bool full;

    public CardStack(Card card, int maxSize) { 
        this.card = card;
        this.maxSize = maxSize;
        currentSize = 1;
    }

    public Card GetCardinStack() { 
        return card;
    }

    public bool CanAddtoStack(Card card) {
        if (card.GetName().Equals(this.card.GetName()))
        {
            return true;
        }
        return false;
    }

    public bool AddCardtoStack(Card card) {
        if(!CanAddtoStack(card)){
            return false;
        }
        if (currentSize < maxSize)
            currentSize++;
        CheckFullness();
        return true;
    }

    public bool RemoveCardFromStack(Card card){
        if(!CanAddtoStack(card)){
            return false;
        }
        if (currentSize < maxSize)
            currentSize--;
        CheckFullness();
        return true;
    }

    public void CheckFullness() {
        if (currentSize + 1 > maxSize)
        {
            full = true;
        }
        else {
            full = false;
        }
    }

    public bool IsFull() { 
        return full;
    }

    public int GetSize()
    {
        return currentSize;
    }
}
