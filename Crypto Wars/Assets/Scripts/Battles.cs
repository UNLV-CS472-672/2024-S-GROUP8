using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Battles;

public class Battles
{

    private List<AttackObject> attackArray = new List<AttackObject>();
    private List<DefendObject> defenseArray = new List<DefendObject>();

    public class AttackObject {
        public List<Card> cardList;
        public Vector2 originTilePos;
        public Vector2 destinationTilePos;

        public AttackObject(List<Card> cards, Vector2 origin, Vector2 desination){
            cardList = cards;
            originTilePos = origin;
            destinationTilePos = desination;
        }
    }


    public class DefendObject {
        public List<Card> cardList;
        public Vector2 originTilePos;


        public DefendObject(List<Card> cards, Vector2 origin){
            cardList = cards;
            originTilePos = origin;
        }
    }

    public Battles(){
        // private List<AttackObject> atackArray = new List<AttackObject>();
        // private List<DefendObject> defenseArray = new List<DefendObject>();
    }


    // REFACTORINGS FOR NEW SYSTEM
    /*
    // used to access battles arrays from CreateDefenseSystem
    public List<AttackObject> getAttackArray(){
        return atackArray;
    }

    public List<DefendObject> getDefenseArray(){
        return defenseArray;
    }

    public void addDefense(DefendObject def){
        defenseArray.Add(def);
    }

    public void clearDefense(){
        defenseArray.Clear();
    }
    */

    
    bool promptForDefCards;
    // PlayerScript player1, player2;
    public static List<Player> players;  //players array for testing

    // private class playerAction {
    //     Cards[] playerCards = new Cards[]{};
    //     int tileX = 0;
    //     int tileY = 0;
    // }



    // Start is called before the first frame update
    void Start()
    {
        promptForDefCards = false;
        players = PlayerController.players;
        // intialize two new players (placeholder)
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // get players tiles
        TileScript t1 = getTile(players[0]);
        TileScript t2 = getTile(players[1]);
        //check if player tiles are adjacent for special case
        if (t1.isAdjacent(t2)){
            //specialBattle();
        }
        //if not adjacent
        else {
            for (int i = 0; i < players.Count; i++){
                //prompt player for defense cards if not prompted already
                if (!players[i].promptForDefCards){
                    Debug.Log("Hello" + players[0].GetName() + ", please select cards for defense!");
                    players[i].promptForDefCards = true;

                    //get defensive cards
                    }
                //reset if player was prompted for next battle
                players[i].promptForDefCards = false;
            }
            //call battle and perform calculations
             //Battle();
        }
        //*/


    }

    
    // bool isAdjacent(Tile TileFrom, Tile TileTo)
    // {
    //     bool adjacent;

    //     int currX = TileFrom.BoardXPos;
    //     int currY = TileFrom.BoardYPos;

    //     int desiredX = TileTo.BoardXPos;
    //     int desiredY = TileTo.BoardYPos;

    //     adjacent = Mathf.Abs(currX - desiredX) <= 1 && Mathf.Abs(currY - desiredY) <= 1;
    //     return adjacent;
    // }


    public Player CalculateWinner(List<Card> Attacker, List<Card> Defender, Player playerAttacking, Player playerDefending){
  
    //    if (isSpecialBattle()){
    //         List<Card> Attacker1 = new List<Card>();
    //         List<Card> Attacker2 = new List<Card>();
    //         SpecialAttack(Attacker1, Attacker2);            // need to still implement way to get two attackers cards to pass into function
    //     }


        //store references to orginal attacker and defender 
        Player orginalAttacker = playerAttacking;
        Player orginalDefender = playerDefending;
       
       //determine who attacks first
         //no defenders to attack so attacking player wins by default
        if (Defender.Count == 0){   
            Debug.Log("Player " + playerDefending.GetName() + " has no defensive cards so " + playerAttacking.GetName() + " has won the battle.");     
            return playerAttacking;
        }
        else if (Attacker.Count > Defender.Count){
            Debug.Log("Attacking players card count is greater so player " + playerAttacking.GetName() + " will attack first.");
            Attack(Attacker, Defender);
        }
        else if (Attacker.Count < Defender.Count){
            Debug.Log("Defensive players card count is greater so player " + playerDefending.GetName() + " will attack first.");
            Attack(Defender, Attacker);
        }
        else if (Attacker.Count == Defender.Count){
            Debug.Log("Card count is equal so player " + playerAttacking.GetName() + " will attack first.");
             Attack(Attacker, Defender);
        }


        // swap who is attacker and defender until someone runs out of cards
        while (Attacker.Count != 0 && Defender.Count != 0){
            swap(ref Attacker, ref Defender, ref playerAttacking, ref playerDefending);
            Attack(Attacker, Defender);
        }

        
        if (Defender.Count == 0){
            Debug.Log("Player " + playerAttacking.GetName() + " has won the battle.");
            return playerAttacking;
        }
        else if (Attacker.Count == 0) {
            Debug.Log("Player " + playerDefending.GetName() + " has won the battle.");
            return playerDefending;
        }  

        return null;
    }



    public void Attack(List<Card> Attacker, List<Card> Defender){
         int minCount = Mathf.Min(Attacker.Count, Defender.Count);
         int i = 0;


         while (i < minCount){
            Card attackerCard = Attacker[i];
            Card defenderCard = Defender[i];

            if (DodgeAttack(defenderCard)){
                attackerCard.setOffense(attackerCard.getOffense() - 1);  // update attack -1 since defender dodged and attack was not successful
                Debug.Log("Card " + attackerCard.getName() + " had an unsuccessful attack. Attack is now " + attackerCard.getOffense() + ".");
                Debug.Log("Card " + defenderCard.getName() + " has sucessfully dodged an attack.");
                if (attackerCard.getOffense() == 0){
                    Attacker.RemoveAt(i); 
                    Debug.Log("Attack stat for card " + attackerCard.getName() + " has reached 0. Card has been defeated.");
                    minCount--;
                    continue;
                }
            }
            else {
                if (attackerCard.getOffense() >= defenderCard.getDefense()){
                    Defender.RemoveAt(i);    // attack was successful so remove defensive card
                    Debug.Log("Attack sucessful, card " + defenderCard.getName() + " has been defeated.");
                    minCount--;

                    // if (KeepAttack(attackerCard)){
                    //     attackerCard.setOffense(attackerCard.getOffense() + 2);  // update attack +2 since attack successful and efficency chance
                    //     Debug.Log("Card " + attackerCard.getName() + " has sucessfully retained their attack upon a sucessful attack. Attack is now " + attackerCard.getOffense() + ".");
                    // }
                    attackerCard.setOffense(attackerCard.getOffense() + 1);  // update attack +1 since attack successful
                    Debug.Log("Card " + attackerCard.getName() + " had a sucessful attack. Attack is now " + attackerCard.getOffense() + ".");
                }
                else if (attackerCard.getOffense() < defenderCard.getDefense()){   // if attacker stat is not more than defender stat then card still has health
                    defenderCard.setDefense(defenderCard.getDefense() - attackerCard.getOffense());   // subtract players attack stat (damage) from the defense stat (health)
                    Debug.Log("Card " + defenderCard.getName() + " has been attacked. Defense is now " + defenderCard.getDefense() + ".");
                    if (!KeepAttack(attackerCard)){
                        attackerCard.setOffense(attackerCard.getOffense() - 1);  // update attack -1 since attack was not successful
                        Debug.Log("Card " + attackerCard.getName() + " had an unsuccessful attack. Attack is now " + attackerCard.getOffense() + ".");
                        if (attackerCard.getOffense() == 0){
                            Attacker.RemoveAt(i); 
                            Debug.Log("Attack stat for card " + attackerCard.getName() + " has reached 0. Card has been defeated.");
                            minCount--;
                            continue;
                        }
                    }
                    else {
                        Debug.Log("Card " + attackerCard.getName() + " has sucessfully retained their attack. Attack is stil " + attackerCard.getOffense() + ".");
                    }
                }
            }
            i++;
        }

    }   


    // Fuction that swaps roles of attacker and defender in a battle 
    public void swap(ref List<Card> list1, ref List<Card> list2, ref Player atk, ref Player def){
        List<Card> temp = list1;
        list1 = list2; //new Attacker
        list2 = temp; //new Defender
        //Attacks are ordered Highest attackstat to lowest
        list1 = list1.OrderByDescending(card => card.getOffense()).ToList();
        //Defense are ordered Highest defensestat to lowest
        list2 = list2.OrderByDescending(card => card.getDefense()).ToList();

        // make sure that player references are correct so winner can be determined correctly
        Player temp2 = atk;
        atk = def;
        def = temp2;
    }



    // bool isSpecialBattle(){
    //     HashSet<Vector2> uniqueDestinations = new HashSet<Vector2>();

    //     // check if attackers destination is already the desitnation of another attacker
    //     foreach (AttackObject attack in atackArray){
    //         if (!uniqueDestinations.Add(attack.destinationTilePos)){
    //             return true; 
    //         }
    //     }
    //     return false; 
    // }



    // void SpecialAttack(List<Card> Attacker1, List<Card> Attacker2){
    //     if (Attacker1.Count > Attacker2.Count){
    //         Attack(Attacker1, Attacker2);
    //     }
    //     else if (Attacker1.Count < Attacker2.Count){
    //          Attack(Attacker2, Attacker1);
    //     }
    //     else {
    //         int rand = UnityEngine.Random.Range(0,1);
    //         if (rand == 0){
    //             Attack(Attacker1, Attacker2);
    //         }
    //         else {
    //             Attack(Attacker2, Attacker1);
    //         }
    //     }
    // }

    public bool DodgeAttack(Card defenderCard){
           float rand = UnityEngine.Random.Range(0.0f,1.0f);

           return rand <= defenderCard.getImmunityChance();   
    }


    public bool KeepAttack(Card attackerCard){
           float rand = UnityEngine.Random.Range(0.0f,1.0f);

           return rand <= attackerCard.getEfficencyChance();   
    }





}