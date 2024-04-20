using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonScript : MonoBehaviour
{
    private Button button;
    private Stash stash;
    // Start is called before the first frame update
    private PlayerController players;
    private GameObject cancelButton;
    void Awake()
    {
        //outOfFrame();
        stash = FindObjectOfType<Stash>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        stash.Activate(false);
        cancelButton = GameObject.Find("Cancel Button");
    }

    // Update is called once per frame
    void Update()
    {   

    }

    //Moves the Attack Button out of frame
    public void outOfFrame()
    {
        this.transform.position = new Vector3(0,-375,0);
    }
    public void OnButtonClick()
    {
        stash.Activate(true);
        cancelButton.SetActive(false);
        Debug.Log("Stash activated for Attacker");
    }
}