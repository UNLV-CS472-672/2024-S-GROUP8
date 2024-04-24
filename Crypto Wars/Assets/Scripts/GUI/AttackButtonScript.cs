using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonScript : MonoBehaviour
{
    // Unity serialized fields
    [SerializeField]
    private GameObject attackButton;
    [SerializeField]
    private GameObject cancelButton;
    [SerializeField]
    private Stash stashButton;

    private bool DoubleClicked = false;
    
    void Start()
    {
        Deactivate();
        attackButton.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        stashButton.Activate(false);
    }

    //Moves the Attack Button out of frame
    public void Deactivate()
    {
        attackButton.SetActive(false);
    }
    public void OnButtonClick()
    {
        if (DoubleClicked)
        {
            Deactivate();
            stashButton.Activate(true);
            cancelButton.SetActive(false);
            Debug.Log("Stash activated for Attacker");
            DoubleClicked = false;
        }
        else {
            DoubleClicked = true;
        }
    }

    public void ResetClicks() {
        DoubleClicked = false;
    }

    // For testing
    public void SetAttackButton(GameObject obj) {
        attackButton = obj;
    }

    // For testing
    public GameObject GetAttackButton()
    {
        return attackButton;
    }

    // For testing
    public void SetStash(Stash stash)
    {
        stashButton = stash;
    }

    // For testing
    public void SetCancelButton(GameObject obj)
    {
        cancelButton = obj;
    }
}