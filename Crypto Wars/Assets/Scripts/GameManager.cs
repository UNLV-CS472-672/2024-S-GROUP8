using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    /* Battle object is an instance of a single battle */
    public class Battle { 
        public Player attacker; // Player who is attacking
        public Player defender; // null until winner is chosen in DoAllBattles
        public Battles.AttackObject attack;
        public Battles.DefendObject defence;
        public Tile tile;
        public bool defenderHasCards; // Has defender selected cards?
        public Battle(Player attacker, Player defender, Battles.AttackObject attack, Tile tile){ // Constructor
            this.attacker = attacker;
            this.attack = attack;
            this.defender = defender;
            defence = null;
            defenderHasCards = false;
            this.tile = tile;
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
        CardRegistry.Load();
        BuildingRegistry.Load();
        PlannedBattles = new List<Battle>();
        FinalBattles = new List<Battle>();
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.CurrentPlayer.GetCurrentPhase() == Player.Phase.Build) {
            if (PlannedBattles.Count > 0) {
                for (int i = PlannedBattles.Count - 1; i > -1; i--){
                    if(PlayerController.CurrentPlayer.GetName().Equals(PlayerController.players[PlannedBattles[i].tile.GetPlayer()].GetName()))
                        AddDefenderToBattle(PlayerController.CurrentPlayer, new Battles.DefendObject(new List<Card>(), PlannedBattles[i].attack.destinationTilePos));
                }
            
            }
            DoAllBattles();
        }
    }

    public static void AddAttackerToBattle(Player From, Player To, Battles.AttackObject attack, Tile tile)
    {
        PlannedBattles.Add(new Battle(From, To, attack, tile));
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
                battle.defence = defend;
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
         for (int i = FinalBattles.Count - 1; i > -1 ; i--){
            // Calculate outcome for battle i
            if(FinalBattles[i].defenderHasCards){
                // Defender has selected cards
            }
            //FinalBattles[i].winner = FinalBattles[i].attacker; // return attacker as victor for now
            Player originalAttacker = FinalBattles[i].attacker;
            Player winner = calcBattles.CalculateWinner(FinalBattles[i].attack.cardList, FinalBattles[i].defence.cardList, FinalBattles[i].attacker, FinalBattles[i].defender); 
            //if winner is attacker remove the tile from the defenders tiles owned and add it to the attackers
            if (string.Equals(winner.GetName(), originalAttacker.GetName())){
                Tile.TileReference tile = Tile.GetTileAtPostion(FinalBattles[i].defence.originTilePos, FinalBattles[i].defender.GetTiles());
                FinalBattles[i].defender.RemoveTiles(tile);
                winner.AddTiles(tile);
                Debug.Log("Index of winner is " + PlayerController.players.IndexOf(winner));
                int index = -1;
                if (PlayerController.players.IndexOf(winner) >= 0){
                    index = PlayerController.players.IndexOf(winner);
                    FinalBattles[i].tile.SetPlayer(index);
                    FinalBattles[i].tile.SetMaterial(PlayerController.players[index].GetColor());
                }
                else {
                    FinalBattles[i].tile.SetPlayer(index);
                }
                returnWinnersRemainingCardsToInventory(winner, FinalBattles[i].attack.cardList);
                
            }
            else {
                // if winner was not attacker then nothing happens defender won and will keep tile 
                Debug.Log("Player " + winner.GetName() + " has won the battle. Since the defender has won " + originalAttacker.GetName()  + " will keep their tile.");
                returnWinnersRemainingCardsToInventory(winner, FinalBattles[i].defence.cardList);
            }
            FinalBattles.Remove(FinalBattles[i]);
            // if winner was not attacker then nothing happens defender won and will keep tile 
        }

    }


    public void returnWinnersRemainingCardsToInventory(Player winner, List<Card> remainingCards){
         Debug.Log(winner.GetName() + " stil has " + remainingCards.Count + " cards after the battle.");
         for (int i = 0; i < remainingCards.Count; i++){
            Debug.Log("Adding card " + remainingCards[i].GetName() + " back to player " + winner.GetName()  + "'s card stack.");
            winner.GetInventory().AddToCardToStack(remainingCards[i]);
         }
    }
}
