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
    private bool buildBarOver;
    private static Stash stash;
    private static GameObject cancelButton;
    private bool notAdj = false;    

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
        stash = GameObject.Find("Stash Bar").transform.Find("Stash").GetComponent<Stash>();
        Debug.Log(stash != null);
        cancelButton = GameObject.Find("Misc Bar").transform.Find("Cancel Button").gameObject;
        CurrentPlayer.SetPhase(Player.Phase.Defense);
        buildBarOver = false;
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
                        // Grabs a new tile to check and makes sure it's not being checked multiple times
                        if (selectedTile == null || !selectedTile.GetTilePosition().Equals(tile.GetTilePosition())) {
                            SetSelectedTile(tile); // Sets the controller's tile that was last clicked
                            if (Tile.IsAdjacent(CurrentPlayer, tile)) {
                                notAdj = false;
                            }
                            else {
                                notAdj = true;

                        // NEEDS TO MOVED AROUND
                        GameObject destroyButton = GameObject.Find("Destroy Button");
                        GameObject attackButton = GameObject.Find("Attack Button");
                        GameObject buildButton = GameObject.Find("Build Button");
                        //GameObject cancelButton = GameObject.Find("Cancel Button");
                        selectedTile = tile;
                        if(tile.GetPlayer() > -1)
                        {
                            // If the tile clicked on is not controlled by the current player
                            if(tile.GetPlayer() != CurrentPlayerIndex){
                              moveAttack(tile);
                              moveCancel(tile);
                              Debug.Log("Creating an Attack Button");
                            }
                            // If the tile is owned by the current player
                            if(tile.GetPlayer() == CurrentPlayerIndex)
                            { 
                                //If there is no building yet on that tile
                                if(tile.getBuilding().getName() == "Nothing" && !buildBarOver)
                                {
                                    //Move the build and cancel buttons into place, and put the building bar into place as well
                                    moveBuild(tile);   
                                    moveCancel(tile);
                                    Button bldButton = buildButton.GetComponent<Button>();
                                    if(tile.getBuilding().getName() == "Nothing")
                                    {
                                        bldButton.onClick.AddListener(() => buildButton.GetComponent<BuildButtonScript>().outOfFrame());
                                        bldButton.onClick.AddListener(() => moveBuildBar(tile));
                                        Debug.Log(tile.getBuilding().getName());
                                    }
                                    Debug.Log("Creating a Build Button");
                                }
                                //If there is a building on that tile 
                                else
                                {
                                    moveDestroy(tile);
                                    moveCancel(tile);
                                    Button desButton = destroyButton.GetComponent<Button>();
                                    desButton.onClick.AddListener(() => buildButton.GetComponent<BuildButtonScript>().deleteObject(tile));
                                    Debug.Log("Creating a Destroy Button");
                                }
                            }
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
                        }
                        // If the tile clicked on is not controlled by the current player
                        if(tile.GetPlayer() != CurrentPlayerIndex && CurrentPlayer.GetCurrentPhase() == Player.Phase.Attack){
                            if(!notAdj)
                                SetupAttackButton(tile);
                        }
                        // If the tile clicked on and the player owns it to build
                        if (tile.GetPlayer() == CurrentPlayerIndex && CurrentPlayer.GetCurrentPhase() == Player.Phase.Build){
                            SetupBuildButton(tile);
                        }
                        // If the tile clicked on and the player wants to defend it from an attack
                        // (If an attack exists)
                        if (tile.GetPlayer() == CurrentPlayerIndex && CurrentPlayer.GetCurrentPhase() == Player.Phase.Defense)
                        {
                            // Grabs the tile to check if it can be defended
                            if (CreateDefenseSystem.IsDefendable(tile.GetTilePosition())){
                                stash.Activate(true);
                            }
                            else {
                                stash.Activate(false);
                            }
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

    public void moveBuildBar(Tile tile)
    {
        GameObject buildBar = GameObject.Find("Building Bar");
        buildBarOver = true;

        buildBar.transform.position = new Vector3(tile.transform.position.x+0.25f, tile.transform.position.y+1f, tile.transform.position.z+.2f);
        buildBar.transform.localScale = new Vector3(0.005f,0.005f,0.005f);
        buildBar.transform.eulerAngles = new Vector3(90,0,0);
    }

    public void moveCancel(Tile tile)
    {
        GameObject cancelButton = GameObject.Find("Cancel Button");
        
        cancelButton.transform.position = new Vector3(tile.transform.position.x-0.05f, tile.transform.position.y+1f, tile.transform.position.z-.1f);
        cancelButton.transform.localScale = new Vector3(0.005f,0.005f,0.005f);
        cancelButton.transform.eulerAngles = new Vector3(90,0,0);
    }

    public void moveBuild(Tile tile)
    {
        GameObject buildButton = GameObject.Find("Build Button");
        GameObject destroyButton = GameObject.Find("Destroy Button");
        GameObject attackButton = GameObject.Find("Attack Button");

        attackButton.GetComponent<AttackButtonScript>().outOfFrame();
        //destroyButton.GetComponent<BuildButtonScript>().outOfFrame();

        buildButton.transform.position = new Vector3(tile.transform.position.x-0.05f, tile.transform.position.y+1f, tile.transform.position.z+.2f);
        buildButton.transform.localScale = new Vector3(0.005f,0.005f,0.005f);
        buildButton.transform.eulerAngles = new Vector3(90,0,0);
    }

    public void moveAttack(Tile tile)
    {
        GameObject buildButton = GameObject.Find("Build Button");
        GameObject attackButton = GameObject.Find("Attack Button");

        buildButton.GetComponent<BuildButtonScript>().outOfFrame();
        //destroyButton.GetComponent<BuildButtonScript>().outOfFrame();

        attackButton.transform.position = new Vector3(tile.transform.position.x-0.05f, tile.transform.position.y+1f, tile.transform.position.z+.2f);
        attackButton.transform.localScale = new Vector3(0.005f,0.015f,0.005f);
        attackButton.transform.eulerAngles = new Vector3(90,0,0);
    }

    public void moveDestroy(Tile tile)
    {
        GameObject destroyButton = GameObject.Find("Destroy Button");
        GameObject attackButton = GameObject.Find("Attack Button");
        GameObject buildButton = GameObject.Find("Build Button");

        attackButton.GetComponent<AttackButtonScript>().outOfFrame();
        buildButton.GetComponent<BuildButtonScript>().outOfFrame();

        destroyButton.transform.position = new Vector3(tile.transform.position.x-0.05f, tile.transform.position.y+1f, tile.transform.position.z+.2f);
        destroyButton.transform.localScale = new Vector3(0.005f,0.005f,0.005f);
        destroyButton.transform.eulerAngles = new Vector3(90,0,0);
    }
    public int GetCurrentPlayerIndex()
    {
        return CurrentPlayerIndex;
    }

    public List<Player> GetPlayerList()
    {
        return players;
    }
    
    public Player GetCurrentPlayer()
    {
        int index = this.GetCurrentPlayerIndex();
        return players[index];
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
        GameObject cancelButton = GameObject.Find("Cancel Button");
        
        
        attackButton.transform.position = new Vector3(tile.GetTilePosition().x, 2.5f, tile.GetTilePosition().y);
        attackButton.transform.localScale = new Vector3(0.055f, 0.055f, 0.055f);
        attackButton.transform.eulerAngles = new Vector3(90, 0, 0);
        //cancelButton.SetActive(true);
        //Debug.Log("Creating an Attack Button");
    }

    public void SetupBuildButton(Tile tile) {
        GameObject buildButton = GameObject.Find("Build Button");
        GameObject destroyButton = GameObject.Find("Destroy Button");
        GameObject cancelButton = GameObject.Find("Cancel Button");
        if (buildButton.GetComponent<Image>().enabled)
        {
            if (tile.GetBuilding().getName() == "Nothing")
            {
                buildButton.GetComponent<Image>().enabled = true;
                buildButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

                //cancelButton.GetComponent<Image>().enabled = true;
                //cancelButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

                buildButton.transform.position = new Vector3(tile.GetTilePosition().x, 2.5f, tile.GetTilePosition().y + 0.45f);
                buildButton.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                buildButton.transform.eulerAngles = new Vector3(90, 0, 0);

                //cancelButton.SetActive(true);
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

                //cancelButton.SetActive(true);
                Debug.Log("Creating a Destroy Button");
            }
        }
    }
}
