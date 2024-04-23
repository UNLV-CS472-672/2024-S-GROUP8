using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static PlasticPipe.Server.MonitorStats;
using static Battles;

public class GameManager : MonoBehaviour
{
    /* Battle object is an instance of a single battle */
    public class Battle { 
        public Player attacker; // Player who is attacking
        public Player defender; // null until winner is chosen in DoAllBattles
        public Battles.AttackObject attack;
        public Battles.DefendObject defence;
        public bool defenderHasCards; // Has defender selected cards?
        public Battle(Player attacker, Player defender, Battles.AttackObject attack){ // Constructor
            this.attacker = attacker;
            this.attack = attack;
            this.defender = defender;
            defence = null;
            defenderHasCards = false;
        }
    }
    public static List<Battle> PlannedBattles = new List<Battle>();
    public static List<Battle> FinalBattles = new List<Battle>();
    Battles calcBattles = new Battles();

    public static List<Battle> OnlyDefenderBattles(Player player) {
        List<Battle> Battles = new List<Battle>();
        foreach (Battle battle in PlannedBattles) {
            if (battle.defender.GetName().Equals(player.GetName())) {
                Battles.Add(battle);
            }
        }
        return Battles;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlannedBattles = new List<Battle>();
        FinalBattles = new List<Battle>();
    }
    // Update is called once per frame
    void Update()
    {
        if (FinalBattles.Count > 0) {
            if (PlayerController.CurrentPlayer.GetCurrentPhase() == Player.Phase.Build) {
                DoAllBattles();
            }
        }
    }

    public static void AddAttackerToBattle(Player From, Player To, Battles.AttackObject attack)
    {
        PlannedBattles.Add(new Battle(From, To, attack));
        Debug.Log("Battle - Player: " + From.GetName() + " Position: " + attack.destinationTilePos.ToString());
        foreach (Card card in attack.cardList) {
            Debug.Log("BattleCard: " + card.GetName());
        }
    }

    public static void AddDefenderToBattle(Player For, Battles.DefendObject defend)
    {
        foreach (Battle battle in PlannedBattles)
        {
            if (battle.attack.destinationTilePos == defend.originTilePos) {
                PlannedBattles.Remove(battle);
                battle.defender = For;
                if (defend.cardList.Count > 0) {
                    battle.defenderHasCards = true; 
                }
                FinalBattles.Add(battle);
                Debug.Log("Battle - Def Player: " + For.GetName() + " Att Player: " + battle.attacker.GetName() + " Position: " + defend.originTilePos.ToString());
                foreach (Card card in defend.cardList)
                {
                    Debug.Log("BattleCard: " + card.GetName());
                }
                break;
            }
        }
        
        
    }

   public void DoAllBattles(){
        for(int i = 0; i < FinalBattles.Count; i++){
            // Calculate outcome for battle i
            if(FinalBattles[i].defenderHasCards){
                // Defender has selected cards
            }
            //FinalBattles[i].winner = FinalBattles[i].attacker; // return attacker as victor for now
            Player originalAttacker = FinalBattles[i].attacker;
            Player winner = calcBattles.CalculateWinner(FinalBattles[i].attack.cardList, FinalBattles[i].defence.cardList, FinalBattles[i].attacker, FinalBattles[i].defender); 
            //if winner is attacker remove the tile from the defenders tiles owned and add it to the attackers
            if (String.Equals(winner.GetName(), originalAttacker.GetName())){
                Tile.TileReference tile = Tile.GetTileAtPostion(FinalBattles[i].defence.originTilePos, FinalBattles[i].defender.GetTiles());
                FinalBattles[i].defender.RemoveTiles(tile);
                winner.AddTiles(ref tile);
                returnWinnersRemainingCardsToInventory(winner, FinalBattles[i].attack.cardList);
            }
            else {
                // if winner was not attacker then nothing happens defender won and will keep tile 
                Debug.Log("Player " + winner.GetName() + " has won the battle. Since the defender has won " + originalAttacker.GetName()  + " will keep their tile.");
                returnWinnersRemainingCardsToInventory(winner, FinalBattles[i].defence.cardList);
            }
        }
    }

    public void BattlesComplete(){
        // Called by TurnMaster waiting until all battles are calculated
    }

    public void returnWinnersRemainingCardsToInventory(Player winner, List<Card> remainingCards){
         Debug.Log(winner.GetName() + " stil has " + remainingCards.Count + " cards after the battle.");
         for (int i = 0; i < remainingCards.Count; i++){
            Debug.Log("Adding card " + remainingCards[i].GetName() + " back to player " + winner.GetName()  + "'s card stack.");
            winner.GetInventory().AddToCardToStack(remainingCards[i]);
         }
    }
}
