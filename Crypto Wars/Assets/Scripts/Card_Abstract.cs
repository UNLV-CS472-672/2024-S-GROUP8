using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/*
 * CLASS CardClass - holds values/details for individual cards
 *
 *      contains:
 *          GameObject card 
 *          method to set GameObject card to active
 *          struct cardStats 
 *          cardStats stats (offense,defense,staminaCost)
 *          getters/setters for changing stats
 * 
 */
public class Card
{
    private Sprite card;        // for displaying card
    private cardStats stats;    // contains stats for card (offense,defense,stamina) 
    private string name;         // name of the card

    public Card(Sprite card, string name) { 
        this.name = name;
        this.card = card;   
    }

    public string getName() { 
        return name;
    }
    /*
     * // displays the card in Unity
    void displayCard() {
        // check if card was initialized
        if (card != null) {
            card.SetActive(true);
        }
    }
    */

    //////////////////////////
    //////////////////////////
    //  GETTERS 
    //////////////////////////
    //////////////////////////

    public short getDefense() {
        return stats.defense;
    }

    public short getOffense() {
        return stats.offense;
    }

    public short getStaminaCost() {
        return stats.staminaCost;
    }

    public float getImmunityChance(){
        return stats.immunity;
    }

    public float getEfficencyChance(){
        return stats.efficency;
    }

    //////////////////////////
    //////////////////////////
    //  SETTERS 
    //////////////////////////
    //////////////////////////

    public void setDefense(int defense) {
        // prevent user from misassigning an invalid value
        if(defense < 0 || defense > 150)
            Debug.LogError("ERROR: stat value must be between 0-2000"); ;

        stats.defense = (short)defense;
    }

    public void setOffense(int offense) {
        // prevent user from misassigning an invalid value
        if (offense < 0 || offense > 30)
            Debug.LogError("ERROR: stat value must be between 0-2000"); ;

        stats.offense = (short)offense;
    }

    public void setStaminaCost(int stamCost) {
        // prevent user from misassigning an invalid value
        if (stamCost < 0 || stamCost > 2000)
            Debug.LogError("ERROR: stat value must be between 0-2000"); ;

        stats.staminaCost = (short)stamCost;
    }

    public void setImmunityChance(float chance) {
        // prevent user from misassigning an invalid value
        if (chance < 0.0f || chance > 0.4f)
            Debug.LogError("ERROR: stat value must be between 0.0-0.4"); ;

        stats.immunity = chance;
    }

     public void setEfficency(float chance) {
        // prevent user from misassigning an invalid value
        if (chance < 0.0f || chance > 0.4f)
            Debug.LogError("ERROR: stat value must be between 0.0-0.4"); ;

        stats.efficency = chance;
    }

    //////////////////////////
    // Struct contains the stats of the card
    //////////////////////////
    public struct cardStats {
        public short defense;
        public short offense;
        public short staminaCost; // can be renamed at a later date
        public float immunity;
        public float efficency;
    };
}



