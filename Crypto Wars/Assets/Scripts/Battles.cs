using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battles
{

    private List<AttackObject> atackArray = new List<AttackObject>();
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


    Player CalculateWinner(List<Card> Attacker, List<Card> Defender, Player playerAttacking, Player playerDefending){
  
       if (isSpecialBattle()){
            List<Card> Attacker1 = new List<Card>();
            List<Card> Attacker2 = new List<Card>();
            SpecialAttack(Attacker1, Attacker2);            // need to still implement way to get two attackers cards to pass into function


       }
       
       //determine who attacks first
        if (Attacker.Count > Defender.Count){
            Attack(Attacker, Defender);
        }
        else if (Attacker.Count < Defender.Count){
              Attack(Defender, Attacker);
        }
        else if (Attacker.Count == Defender.Count){
            Attack(Attacker, Defender);
        }
        //if defender never selected cards for defense then attacker automatically wins
        else if (Defender.Count == 0){        
            return playerAttacking;
        }


        // swap who is attacker and defender until someone runs out of cards
        while (Attacker.Count != 0 && Defender.Count != 0){
            swap(ref Attacker, ref Defender);
            Attack(Attacker, Defender);
        }

        
        if (Defender.Count == 0){
            return playerAttacking;
        }
        else if (Attacker.Count == 0) {
            return playerDefending;
        }  

        return null;
    }



    void Attack(List<Card> Attacker, List<Card> Defender){
         int minCount = Mathf.Min(Attacker.Count, Defender.Count);


         for (int i = 0; i < minCount; i++){
            Card attackerCard = Attacker[i];
            Card defenderCard = Defender[i];

            if (attackerCard.getOffense() >= defenderCard.getDefense()){
                Defender.RemoveAt(i);
            }
            else if (attackerCard.getOffense() < defenderCard.getDefense())
            {
                defenderCard.setOffense(defenderCard.getOffense() - attackerCard.getOffense());
            }
        }

    }   


    // Fuction that swaps roles of attacker and defender in a battle 
    void swap(ref List<Card> list1, ref List<Card> list2){
        List<Card> temp = list1;
        list1 = list2;
        list2 = temp;
    }



    bool isSpecialBattle(){
        HashSet<Vector2> uniqueDestinations = new HashSet<Vector2>();

        // check if attackers destination is already the desitnation of another attacker
        foreach (AttackObject attack in atackArray){
            if (!uniqueDestinations.Add(attack.destinationTilePos)){
                return true; 
            }
        }
        return false; 
    }



    void SpecialAttack(List<Card> Attacker1, List<Card> Attacker2){
        if (Attacker1.Count > Attacker2.Count){
            Attack(Attacker1, Attacker2);
        }
        else if (Attacker1.Count < Attacker2.Count){
             Attack(Attacker2, Attacker1);
        }
        else {
            int rand = UnityEngine.Random.Range(0,1);
            if (rand == 0){
                Attack(Attacker1, Attacker2);
            }
            else {
                Attack(Attacker2, Attacker1);
            }
        }
    }

    public void AddAttackObject(AttackObject attack){
        atackArray.Add(attack);
    }

}