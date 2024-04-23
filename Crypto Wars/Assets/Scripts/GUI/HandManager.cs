using System;
using System.Collections;
using System.Collections.Generic;
// using System.Drawing;
// using System.Linq;
// using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Card;

public class HandManager : MonoBehaviour
{
    private static List<GameObject> Slots = new List<GameObject>();
    private static List<string> cardNamesInHand;
    private static Hand currentPlayerHand = null;
    private Card newSlot1Card, newSlot2Card, newSlot3Card, newSlot4Card, newSlot5Card;
    private int cardsInHandLast, newSlot1Count, newSlot2Count, newSlot3Count, newSlot4Count, newSlot5Count;
    public GameObject panel, handGrid, canvas;
    private TextMeshProUGUI handSlot1Text, handSlot2Text, handSlot3Text, handSlot4Text, handSlot5Text;
    public Boolean visible, cardsUpdated;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        TMP_FontAsset font = Resources.Load<TMP_FontAsset>("LiberationSans.ttf");
        canvas = GameObject.Find("Canvas");

        currentPlayerHand = PlayerController.CurrentPlayer.GetInventory().GetHand();

        panel = new GameObject("Handheld Cards");
        panel.transform.parent = canvas.transform;
        panel.AddComponent<CanvasRenderer>();
        image = panel.AddComponent<Image>();
        image.color = new Color(0, 0, 0, 0.5f);

        EventTrigger et = panel.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        et.triggers.Add(entry);

        panel.GetComponent<RectTransform>().localPosition = Vector3.zero;
        panel.GetComponent<RectTransform>().pivot = new Vector2(-0.2f, 0.6f);

        handGrid = new GameObject("Hand Grid");
        handGrid.transform.parent = panel.transform;
        handGrid.AddComponent<VerticalLayoutGroup>();
        GameObject temp;

        GameObject slot1 = new GameObject("Hand_Slot_1");
        slot1.AddComponent<RectTransform>();
        slot1.transform.parent = handGrid.transform;
        slot1.GetComponent<RectTransform>().localPosition = handGrid.GetComponent<RectTransform>().localPosition;
        GameObject slot1Text = new GameObject("HandCardName");
        handSlot1Text = slot1Text.AddComponent<TextMeshProUGUI>();
        handSlot1Text.GetComponent<RectTransform>().pivot = new Vector2(-0.46f, 1.2f);
        handSlot1Text.transform.parent = slot1.transform;
        handSlot1Text.fontSize = 14;
        handSlot1Text.text = "Slot 1 Text";
        
        GameObject slot2 = new GameObject("Hand_Slot_2");
        slot2.AddComponent<RectTransform>();
        slot2.transform.parent = handGrid.transform;
        slot2.GetComponent<RectTransform>().localPosition = handGrid.GetComponent<RectTransform>().localPosition;
        GameObject slot2Text = new GameObject("HandCardName");
        handSlot2Text = slot2Text.AddComponent<TextMeshProUGUI>();
        handSlot2Text.GetComponent<RectTransform>().pivot = new Vector2(-0.46f, 1.2f);
        handSlot2Text.transform.parent = slot2.transform;
        handSlot2Text.fontSize = 14;
        handSlot2Text.text = "Slot 2 Text";

        GameObject slot3 = new GameObject("Hand_Slot_3");
        slot3.AddComponent<RectTransform>();
        slot3.transform.parent = handGrid.transform;
        slot3.GetComponent<RectTransform>().localPosition = handGrid.GetComponent<RectTransform>().localPosition;
        GameObject slot3Text = new GameObject("HandCardName");
        handSlot3Text = slot3Text.AddComponent<TextMeshProUGUI>();
        handSlot3Text.GetComponent<RectTransform>().pivot = new Vector2(-0.46f, 1.2f);
        handSlot3Text.transform.parent = slot3.transform;
        handSlot3Text.fontSize = 14;
        handSlot3Text.text = "Slot 3 Text";

        GameObject slot4 = new GameObject("Hand_Slot_4");
        slot4.AddComponent<RectTransform>();
        slot4.transform.parent = handGrid.transform;
        slot4.GetComponent<RectTransform>().localPosition = handGrid.GetComponent<RectTransform>().localPosition;
        GameObject slot4Text = new GameObject("HandCardName");
        handSlot4Text = slot4Text.AddComponent<TextMeshProUGUI>();
        handSlot4Text.GetComponent<RectTransform>().pivot = new Vector2(-0.46f, 1.2f);
        handSlot4Text.transform.parent = slot4.transform;
        handSlot4Text.fontSize = 14;
        handSlot4Text.text = "Slot 4 Text";

        GameObject slot5 = new GameObject("Hand_Slot_5");
        slot5.AddComponent<RectTransform>();
        slot5.transform.parent = handGrid.transform;
        slot5.GetComponent<RectTransform>().localPosition = handGrid.GetComponent<RectTransform>().localPosition;
        GameObject slot5Text = new GameObject("HandCardName");
        handSlot5Text = slot5Text.AddComponent<TextMeshProUGUI>();
        handSlot5Text.GetComponent<RectTransform>().pivot = new Vector2(-0.46f, 1.2f);
        handSlot5Text.transform.parent = slot5.transform;
        handSlot5Text.fontSize = 14;
        handSlot5Text.text = "Slot 5 Text";

        for (int i = 0; i < 5; i++) {
            SetupSlot(i); // Create each slot in the inventory
            Debug.Log("Hand Slot " + Slots.Count);
        }

        visible = true;
        cardsUpdated = false;
        cardsInHandLast = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Code to keep the GUI elements next to the mouse at all times.
        Vector3 offsetVector = Vector3.zero;
        Vector3 offsetPosition = Input.mousePosition + offsetVector;
        panel.transform.position = offsetPosition;

        if(currentPlayerHand.GetHandCards().Count != 0)
        {
            if(currentPlayerHand.GetHandCards().Count != cardsInHandLast)
            {
                cardsUpdated = true;
            }
        }

        if(cardsUpdated)
        {
            ChangeDisplayedCards();
        }

        // DEBUG: Remove before shipping?
        if (Input.GetKeyDown(KeyCode.H)) {
            visible = !visible;
            ToggleHideAll();
            Debug.Log("Toggling Hand Visibility Status: " + visible);
        }
        
        // DEBUG: Remove before shipping?
        if (Input.GetKeyDown(KeyCode.P)) {
            int count = 4;
            PartialReveal(count);
            Debug.Log("Partially Revealing: " + count);
        }
        
        // DEBUG: Remove before shipping?
        if (Input.GetKeyDown(KeyCode.C)) {
            visible = false;
            ForceHideAll();
            Debug.Log("Forcing Hand Visibility Status: " + visible);
        }

        // DEBUG: Remove before shipping?
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("Testing Cards In Hand: " + currentPlayerHand.GetHandCards().Count);
        }
    }

    // Reveals the requested number of card names
    public void PartialReveal(int count)
    {
        image.enabled = true;

        switch(count)
        {
            case 1:
                handSlot1Text.enabled = true;
                break;
            case 2:
                handSlot1Text.enabled = true;
                handSlot2Text.enabled = true;
                break;
            case 3:
                handSlot1Text.enabled = true;
                handSlot2Text.enabled = true;
                handSlot3Text.enabled = true;
                break;
            case 4:
                handSlot1Text.enabled = true;
                handSlot2Text.enabled = true;
                handSlot3Text.enabled = true;
                handSlot4Text.enabled = true;
                break;
            case 5:
                handSlot1Text.enabled = true;
                handSlot2Text.enabled = true;
                handSlot3Text.enabled = true;
                handSlot4Text.enabled = true;
                handSlot5Text.enabled = true;
                break;
            default:
                break;
        }
    }

    // DEBUG: Hides/Reveals all info about the hand slots, regardless of status
    public void ToggleHideAll()
    {
        image.enabled = visible;
        handSlot1Text.enabled = visible;
        handSlot2Text.enabled = visible;
        handSlot3Text.enabled = visible;
        handSlot4Text.enabled = visible;
        handSlot5Text.enabled = visible;
    }

    // DEBUG: Forcibly hides all info about hand slots
    public void ForceHideAll()
    {
        image.enabled = false;
        handSlot1Text.enabled = false;
        handSlot2Text.enabled = false;
        handSlot3Text.enabled = false;
        handSlot4Text.enabled = false;
        handSlot5Text.enabled = false;
    }

    public void ChangeDisplayedCards()
    {
        newSlot1Card = null;
        newSlot2Card = null;
        newSlot3Card = null;
        newSlot4Card = null;
        newSlot5Card = null;
        newSlot1Count = 0;
        newSlot2Count = 0;
        newSlot3Count = 0;
        newSlot4Count = 0;
        newSlot5Count = 0;
        List<string> cardNamesInHand = new List<string>();
        // for(int i = 0; i < currentPlayerHand.GetHandCards().Count; i++)
        // {
            
        // }
        
        // newSlot1Card = currentPlayerHand.GetHandCards()[0];
        if(currentPlayerHand.GetHandCards().Count != 0)
        {
            foreach (Card oldCard in currentPlayerHand.GetHandCards())
            {
                if(newSlot1Card == null)
                {
                    if(!cardNamesInHand.Contains(oldCard.GetName()))
                    {
                        newSlot1Card = oldCard;
                        cardNamesInHand.Add(newSlot1Card.GetName());
                    }
                }
                if(newSlot1Card != null)
                {
                    if (cardNamesInHand.IndexOf(oldCard.GetName()) == 0)
                    {
                        newSlot1Count++;
                    }
                }
                if(newSlot2Card == null)
                {
                    if(!cardNamesInHand.Contains(oldCard.GetName()))
                    {
                        newSlot2Card = oldCard;
                        cardNamesInHand.Add(newSlot2Card.GetName());
                    }
                }
                if(newSlot2Card != null)
                {
                    if(cardNamesInHand.IndexOf(oldCard.GetName()) == 1)
                    {
                        newSlot2Count++;
                    }
                }
                if(newSlot3Card == null)
                {
                    if(!cardNamesInHand.Contains(oldCard.GetName()))
                    {
                        newSlot3Card = oldCard;
                        cardNamesInHand.Add(newSlot3Card.GetName());
                    }
                }
                if(newSlot3Card != null)
                {
                    if(cardNamesInHand.IndexOf(oldCard.GetName()) == 2)
                    {
                        newSlot3Count++;
                    }
                }
                if(newSlot4Card == null)
                {
                    if(!cardNamesInHand.Contains(oldCard.GetName()))
                    {
                        newSlot4Card = oldCard;
                        cardNamesInHand.Add(newSlot4Card.GetName());
                    }
                }
                if(newSlot4Card != null)
                {
                    if(cardNamesInHand.IndexOf(oldCard.GetName()) == 3)
                    {
                        newSlot4Count++;
                    }
                }
                if(newSlot3Card == null)
                {
                    if(!cardNamesInHand.Contains(oldCard.GetName()))
                    {
                        newSlot5Card = oldCard;
                        cardNamesInHand.Add(newSlot5Card.GetName());
                    }
                }
                if(newSlot5Card != null)
                {
                    if(cardNamesInHand.IndexOf(oldCard.GetName()) == 4)
                    {
                        newSlot5Count++;
                    }
                }
            }
        
            if(newSlot1Card != null)
            {
                handSlot1Text.text = newSlot1Count + "x " + newSlot1Card.GetName();
            }
            if(newSlot2Card != null)
            {
                handSlot2Text.text = newSlot2Count + "x " + newSlot2Card.GetName();
            }
            if(newSlot3Card != null)
            {
                handSlot3Text.text = newSlot3Count + "x " + newSlot3Card.GetName();
            }
            if(newSlot4Card != null)
            {
                handSlot4Text.text = newSlot4Count + "x " + newSlot4Card.GetName();
            }
            if(newSlot5Card != null)
            {
                handSlot5Text.text = newSlot5Count + "x " + newSlot5Card.GetName();
            }
        }
        else
        {
            ForceHideAll();
        }
        
        cardsInHandLast = currentPlayerHand.GetHandCards().Count;
        cardsUpdated = false;
    }
    
    // Sets the text of a given TextMeshProUGUI object
    public void SetText(string textObject, int index, string newText) {
        if(Slots[index] != null)
            Slots[index].transform.Find(textObject).GetComponent<TextMeshProUGUI>().text = newText;
    }


    // Sets up a hand slot and sets the text values to an empty string
    public void SetupSlot(int index)
    {
        Slots.Add(gameObject.transform.Find("Hand Grid").Find("Hand_Slot_" + (index + 1)).gameObject);
        SetText("HandCardName", index, "");
        SetText("HandAmount", index, "");
    }
}
