using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonScript : MonoBehaviour
{
    // Unity serialized fields
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject cancelButton;
    [SerializeField]
    private Stash stashButton;
    
    void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
        stashButton.Activate(false);
    }

    //Moves the Attack Button out of frame
    public void outOfFrame()
    {
        transform.position = new Vector3(0,-375,0);
    }
    public void OnButtonClick()
    {
        stashButton.Activate(true);
        cancelButton.SetActive(false);
        Debug.Log("Stash activated for Attacker");
    }
}