using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


// Need some way of switching the InventoryManager system for each player
public class InventoryManager : MonoBehaviour
{ 
    private static List<GameObject> Slots = new List<GameObject>();
    private static Inventory currentPlayerInventory = null;
    private Card Temp;
    private Card Temp1;

    public static InventoryManager GetManager() { 
        return FindObjectOfType<InventoryManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Slots = new List<GameObject>();
        for (int i = 0; i < 5; i++) {
            SetupSlot(i); // Create each slot in the inventory
            Debug.Log("" + Slots.Count);
        }
        
        Temp = CardRegistry.GetCardByName("Python");
        Temp1 = CardRegistry.GetCardByName("Java");

        currentPlayerInventory = PlayerController.CurrentPlayer.GetInventory();
        int[] array = { 3, 2, 3 };
        // Starting inventory of 3 2 3
        Inventory.StartingInventory(array);

    }

    // Update is called once per frame
    void Update()
    {
        // Bunch of testing stuff
        if (Input.GetKeyDown(KeyCode.R)) {
            currentPlayerInventory.AddToCardToStack(Temp);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentPlayerInventory.AddToCardToStack(Temp1);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentPlayerInventory.RemoveCardFromStack(Temp);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentPlayerInventory.RemoveCardFromStack(Temp1);
        }

        if (PlayerController.Switching) {
            currentPlayerInventory = PlayerController.CurrentPlayer.GetInventory();

            Debug.Log(PlayerController.CurrentPlayer.GetName());

            // Empty slots
            for (int i = 0; i < 5; i++){
                SetupSlot(i); 
            }
            List<CardStack> stacks = currentPlayerInventory.GetStacks();
            for (int i = 0; i < currentPlayerInventory.GetStacksListSize(); i++){
                SetText("CardName", i, "" + stacks[i].GetCardinStack().GetName(), "CardName Bar");
                SetText("Amount", i, "" + stacks[i].GetSize());
            }
            PlayerController.Switching = false;
        }
    }

    // Easy reference to alter text inside the slot UI object
    public void SetText(string textObject, int index, string newText, string bar = "") {
        if (Slots[index] != null) { 
            if(bar.Equals(""))
                Slots[index].transform.Find(textObject).GetComponent<TextMeshProUGUI>().text = newText;
            else
                Slots[index].transform.Find(bar).Find(textObject).GetComponent<TextMeshProUGUI>().text = newText;
        }
    }

    // Setups a inventory slot and sets the text values to an empty string
    public void SetupSlot(int index) {
        Slots.Add(gameObject.transform.Find("Grid").Find("Slot_" + (index + 1)).gameObject);
        SetText("CardName", index, "", "CardName Bar");
        SetText("Amount", index, "");
    }

    // Reorders the inventory slot objects based on the list array once a item has been deleted
    public void ReorderSlots() {
        if (currentPlayerInventory != null){
            List<CardStack> stacks = currentPlayerInventory.GetStacks();
            for (int i = 0; i < stacks.Count(); i++) {
                SetText("CardName", i, "" + stacks[i].GetCardinStack().GetName(), "CardName Bar");
                SetText("Amount", i, "" + stacks[i].GetSize());
            }
        SetText("CardName", stacks.Count(), "", "CardName Bar");
        SetText("Amount", stacks.Count(), "");
        }
    }

    // Function for unit tests to reset slots
    public static void ClearSlots()
    {
        Slots.Clear();
    }

}
