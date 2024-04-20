using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Temp players
    public static List<Player> players { get; set; }
    private static int CurrentPlayerIndex;

    // Tracks the player who is currently playing
    public static Player CurrentPlayer { get; set; }
    public static bool Switching = false;
    // Store the most recently selected tile
    private static Tile selectedTile;
    private static Stash stash;
    private static GameObject cancelButton;

    // Get number of players in game
    public int GetNumberOfPlayers(){
        return players.Count;
    }

    // Start is called before the first frame update
    void Awake()
    {
        players = new List<Player>
        {
            new Player("One", Resources.Load<Material>("Materials/PlayerTileColor")),
            new Player("Two", Resources.Load<Material>("Materials/EnemyTileColor"))
        };

        CurrentPlayer = players[0];
        CurrentPlayerIndex = 0;
        stash = FindObjectOfType<Stash>();
        cancelButton = GameObject.Find("Cancel Button");
    }

    // Update is called once per frame
    void Update()
    {
        // Detects input of the player inside the camera
        // Right now it just does some basic stuff
        // Will have more functionality in future
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f)) {
                if (hit.transform != null) {
                    Tile tile = hit.transform.GetComponent<Tile>();
                    if (tile != null) {
                        SetSelectedTile(tile);
                        if(tile.GetPlayer() > -1)
                        {
                            // If the tile clicked on is not controlled by the current player
                            if(tile.GetPlayer() != CurrentPlayerIndex && CurrentPlayer.GetCurrentPhase() == Player.Phase.Attack){
                                SetupAttackButton(tile);
                            }
                            if(tile.GetPlayer() == CurrentPlayerIndex && CurrentPlayer.GetCurrentPhase() == Player.Phase.Build){
                                SetupBuildButton(tile);
                            }
                            if (tile.GetPlayer() == CurrentPlayerIndex && CurrentPlayer.GetCurrentPhase() == Player.Phase.Defense)
                            {
                                if (CreateDefenseSystem.IsDefendable(tile.GetTilePosition())){
                                    stash.Activate(true);
                                }
                                else {
                                    stash.Activate(true);
                                }
                            }
                        } 
                        else
                        {
                            tile.SetPlayer(CurrentPlayerIndex);
                            tile.SetMaterial(players[CurrentPlayerIndex].GetColor());
                        }
                    }
                }
            }
        }
        // Temp player switching until TurnMaster additions can be made
        if (Input.GetKeyDown(KeyCode.K)) {
            NextPlayer();
            Debug.Log("Next: Player Index is now: " + CurrentPlayerIndex);
        }

        // DEBUG: allows current player to take tile regardless of who owns it
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f)) {
                if (hit.transform != null) {
                    Tile tile = hit.transform.GetComponent<Tile>();

                    if (tile != null) {
                        tile.SetPlayer(CurrentPlayerIndex);
                        tile.SetMaterial(players[CurrentPlayerIndex].GetColor());
                    }
                }
            }
        }
           
    }

    // Moves to the next player in line
    public static void NextPlayer() {
        Switching = true;
        if (players.Count > (CurrentPlayerIndex + 1))
        {
            CurrentPlayerIndex++; 
        }
        else {
            CurrentPlayerIndex = 0;
        }
        CurrentPlayer = players[CurrentPlayerIndex];
    }

    // Grabs a player based on position in players array
    public void NextPlayer(int index)
    {
        Switching = true;
        if (players.Count >= (index + 1))
        {
            CurrentPlayerIndex = index;
            CurrentPlayer = players[index];
        }
    }

    public int GetCurrentPlayerIndex()
    {
        return CurrentPlayerIndex;
    }

    public List<Player> GetPlayerList()
    {
        return players;
    }

    public static Tile GetSelectedTile()
    {
        return selectedTile;
    }

    public static void SetSelectedTile(Tile tile)
    {
        selectedTile = tile;
    }

    public void SetupAttackButton(Tile tile) {
        //Get the attack button and cancel button
        GameObject attackButton = GameObject.Find("Attack Button");
        
        attackButton.transform.position = new Vector3(tile.GetTilePosition().x, 2.5f, tile.GetTilePosition().y);
        attackButton.transform.localScale = new Vector3(0.055f, 0.055f, 0.055f);
        attackButton.transform.eulerAngles = new Vector3(90, 0, 0);
        cancelButton.SetActive(true);
        //Debug.Log("Creating an Attack Button");
    }

    public void SetupBuildButton(Tile tile) {
        GameObject buildButton = GameObject.Find("Build Button");
        GameObject destroyButton = GameObject.Find("Destroy Button");
        if (buildButton.GetComponent<Image>().enabled)
        {
            if (tile.getBuilding().getName() == "Nothing")
            {
                buildButton.GetComponent<Image>().enabled = true;
                buildButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

                //cancelButton.GetComponent<Image>().enabled = true;
                //cancelButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

                buildButton.transform.position = new Vector3(tile.GetTilePosition().x, 2.5f, tile.GetTilePosition().y + 0.45f);
                buildButton.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                buildButton.transform.eulerAngles = new Vector3(90, 0, 0);

                cancelButton.SetActive(true);
                //Debug.Log("Creating a Build Button");
            }
            else
            {
                destroyButton.GetComponent<Image>().enabled = true;
                destroyButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

                cancelButton.GetComponent<Image>().enabled = true;
                cancelButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

                destroyButton.transform.position = new Vector3(tile.GetTilePosition().x, 2.5f, tile.GetTilePosition().y + 0.45f);
                destroyButton.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                destroyButton.transform.eulerAngles = new Vector3(90, 0, 0);

                cancelButton.SetActive(true);
                Debug.Log("Creating a Destroy Button");
            }
        }
    }
}
