using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The Stash acts a the main method of creating an attack and a defence
// Cards in the stash will go to either depending on player action
public class Stash : MonoBehaviour
{
    // Unity Serialized Fields
    [SerializeField]
    private Sprite stack_1;
    [SerializeField]
    private Sprite stack_2;
    [SerializeField]
    private Sprite stack_3;
    [SerializeField]
    private GameObject stackImage;

    // Buttons
    [SerializeField]
    private Button Confirm;
    [SerializeField]
    private Button Exit;
    [SerializeField]
    private Button Dropoff;

    // Default Fields
    private List<Card> stashedCards = new List<Card>();
    private Battles.AttackObject makeAttack;
    private Battles.DefendObject makeDefend;
    private Tile tileSelect;

    // Start is called before the first frame update
    void Start()
    {
        // Listener for the Button that interacts with the Stash
        Dropoff.onClick.AddListener(HandtoStash);
        // Accept and Cancel button listeners
        Confirm.onClick.AddListener(Accept);
        Exit.onClick.AddListener(Cancel);
        ModifyStashImage();
    }

    /// <summary>
    /// Shows an image of a 1-3 stack of cards to generally indicate to the player
    /// how many cards they may have in their stash
    /// </summary>
    public void ModifyStashImage() {
        if (stashedCards.Count < 1){
            stackImage.GetComponent<Image>().enabled = false;
        }
        else {
            stackImage.GetComponent<Image>().enabled = true;
        }
        if (stashedCards.Count > 0 && stashedCards.Count < 2) {
            stackImage.GetComponent<Image>().sprite = stack_1;
        }
        if (stashedCards.Count >= 2 && stashedCards.Count < 5){
            stackImage.GetComponent<Image>().sprite = stack_2;
        }
        if (stashedCards.Count >= 5){
            stackImage.GetComponent<Image>().sprite = stack_3;
        }
    }

    /// <summary>
    /// This method gets the player's hand of cards and transfers it to the stash
    /// Checking to make sure the hand actually has stuff to transfer
    /// </summary>
    public void HandtoStash() {
        Debug.Log("Getting hand's cards");
        Hand hand = PlayerController.CurrentPlayer.GetInventory().GetHand();
        if (hand != null && !hand.IsEmpty()){
            stashedCards.AddRange(hand.GetHandCards());
            hand.GetHandCards().Clear();
            ModifyStashImage();
        }
        
    }

    /// <summary>
    /// This method gets the player's inventory of cards and transfers the stash to the inventory
    /// Useful if you want to clear the stash if a player wishes to change
    /// </summary>
    public void StashtoInventory(){
        Debug.Log("Returning cards to inventory");
        Inventory inv = PlayerController.CurrentPlayer.GetInventory();
        foreach (Card card in stashedCards) { 
            inv.AddToCardToStack(card);
        }
        stashedCards.Clear();
        ModifyStashImage();
    }

    /// <summary>
    /// Cancels the stash decision
    /// </summary>
    public void Cancel() {

        StashtoInventory();
        if (stashedCards.Count < 1) { 
            // Cancel back to main menu
        }
    }

    /// <summary>
    /// Accepts the cards in the stash to create an Attack on a clicked tile or a defence on that tile
    /// </summary>
    public void Accept() {
        // Controls the actions the player can take on tiles
        Debug.Log(PlayerController.CurrentPlayer.GetCurrentPhase().ToString());
        if ((PlayerController.CurrentPlayer.GetCurrentPhase() == Player.Phase.Attack) && GetStashSize() > 0){
            Activate(false);
            Debug.Log("Collecting attacker data");
            tileSelect = PlayerController.GetSelectedTile();
            makeAttack = new Battles.AttackObject(stashedCards, new Vector2(0, 0) /* Null for now */, tileSelect.GetTilePosition());
            // Create a new incomplete battle with our attacker object
            GameManager.AddAttackerToBattle(PlayerController.CurrentPlayer, PlayerController.players[tileSelect.GetPlayer()], makeAttack);
            // Clear the stash since we have our attackObject storing it now
            Clear();
            // DEBUG
            // PlayerController.NextPlayer();
            // Debug.Log(PlayerController.CurrentPlayer.GetCurrentPhase().ToString());
            // PlayerController.CurrentPlayer.SetPhase(Player.Phase.Defense);
        }
        else if ((PlayerController.CurrentPlayer.GetCurrentPhase() == Player.Phase.Defense) && GetStashSize() > 0){
            Activate(false);
            Debug.Log("Collecting defender data");
            tileSelect = PlayerController.GetSelectedTile();
            makeDefend = new Battles.DefendObject(stashedCards, tileSelect.GetTilePosition());
            // Finish the incomplete battle with our attacker object
            GameManager.AddDefenderToBattle(PlayerController.CurrentPlayer, makeDefend);
            // Clear the stash since we have our defendObject storing it now
            CreateDefenseSystem.RemoveDefenceObject(tileSelect.GetTilePosition());
            Clear();
        }
        else {
            if(stashedCards.Count < 1)
                Debug.Log("No cards in stash");
            else
                Debug.Log("Wrong Phase");
        }
    }

    /// <summary>
    /// Enable / Disable the stash for attacks and defends
    /// </summary>
    public void Activate(bool isActive) {
        if (isActive) { 
            gameObject.SetActive(true);
        }
        else{
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Get size of the stashed cards list
    /// </summary>
    public int GetStashSize() { 
        return stashedCards.Count;
    }

    /// <summary>
    /// Clear the stashed cards and reset the image 
    /// </summary>
    public void Clear(){
        stashedCards.Clear();
        ModifyStashImage();
    }
}
