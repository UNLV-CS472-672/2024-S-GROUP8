using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateDefenseSystem : MonoBehaviour
{
    public GameObject defendIcon;

    private Battles battle;
    private Player defendingPlayer;
    private bool activeStash;
    private bool updateDefense;
    private static List<GameObject> defendObjects;
    private static List<Vector2> needDefensePositions;

    public static bool IsDefendable(Vector2 pos) {
        foreach (Vector2 needDefence in needDefensePositions) {
            if (needDefence == pos) { 
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateDefense = true;
        defendObjects = new List<GameObject>();
        needDefensePositions = new List<Vector2>();
        //TEMP
        //PlayerController.CurrentPlayer.SetPhase(Player.Phase.Attack);
    }

    // Update is called once per frame
    void Update()
    {
        // when it's the player's defend phase, it checks all the attacked tiles in battle
        // needs to check if the tile is owned by the player to instantiate the defend button
        // clears previous defense array
        defendingPlayer = PlayerController.CurrentPlayer;
        if (checkDefensePhase(defendingPlayer) && updateDefense){
            //Debug.Log("Beginning Defence");
            List<GameManager.Battle> battles = GameManager.OnlyDefenderBattles(PlayerController.CurrentPlayer);
            //Debug.Log("Battle: " + battles[0].attack.destinationTilePos);
            List<Tile.TileReference> ownedTiles = defendingPlayer.getTiles();
            //Debug.Log("OwnedTile: " + ownedTiles[0].tilePosition);

            GameObject Canvas = GameObject.Find("Button Canvas");
            foreach (GameManager.Battle battle in battles){
                Vector2 battlePos = battle.attack.destinationTilePos;
                if (checkPlayerTiles(battlePos, ownedTiles)){
                    GameObject defendButton = Instantiate(defendIcon, new Vector3(battlePos.x, 2.5f, battlePos.y), Quaternion.identity) as GameObject;
                    defendButton.transform.localScale = new Vector3(0.032f, 0.032f, 0.032f);
                    defendButton.transform.eulerAngles = new Vector3(90, 0, 0);
                    defendButton.transform.SetParent(Canvas.transform);
                    defendObjects.Add(defendButton);
                    needDefensePositions.Add(new Vector2(battlePos.x, battlePos.y));
                    Debug.Log("Creating a Defend Button");
                }
            }
            // wont constantly run during defense phase
            updateDefense = false;
        }

        // will start to check for defense phase once the next phase begins
        if(!checkDefensePhase(defendingPlayer)){
            updateDefense = true;
        }
    }

    // check for defense phase
    public bool checkDefensePhase(Player pl){
        if(pl.GetCurrentPhase() == Player.Phase.Defense)
        {
            return true;
        }
        return false;
    }

    // Refactoring
    /* 
    // pull attack objects and check which tiles they're on
    public List<Vector2> getAttackTiles(Battles bat){
        List<Vector2> attTiles = new List<Vector2>();        
        //List<Battles.AttackObject> attackArray = bat.getAttackArray();

        // foreach(Battles.AttackObject attObj in attackArray){
        //     attTiles.Add(attObj.destinationTilePos);
        // }

        return attTiles;
    }
    */

    // checks if the coordinates are in the defendingPlayers owned tile
    public bool checkPlayerTiles(Vector2 coords, List<Tile.TileReference> tileRef){
        foreach(Tile.TileReference tile in tileRef){
            if(tile.tilePosition == coords){
                return true;
            }
        }
        return false;
    }

    // Refactoring
    /*
    // get cards from stash and tile that is currently selected
    // can possibly overload Accept to handle Battles.DefendObjects and Battles.AttackObjects 
    public void setDefenderStash(Battles.DefendObject defObj, Vector2 defTile){
        stash.Activate(activeStash);
        //stash.Accept(defObj, defTile);
        stash.Activate(!activeStash);
    }
    */

}
