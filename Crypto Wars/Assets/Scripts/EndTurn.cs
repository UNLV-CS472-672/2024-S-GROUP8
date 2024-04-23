using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndTurn : MonoBehaviour
{
    // Game screen displays
    GameObject turnObject;
    GameObject phaseObject;
    public TextMeshProUGUI turnOutput;
    public TextMeshProUGUI phaseOutput;
    public TextMeshProUGUI turnPhaseName;

    // EndTurn Variables
    public int turnNum;

    [SerializeField]
    PlayerController controller;
    Player pl;

    // Start is called before the first frame update
    // Attaches the counter to a Text (TMP) gameObject
    void Start()
    {
        turnNum = 0; // starting turn #

        // On-screen text for displaying the current phase
        phaseObject = GameObject.Find("PhaseDisplay");
        if (phaseObject == null)
        {
            phaseObject = new GameObject("PhaseDisplay");
            phaseObject.AddComponent<TextMeshProUGUI>();
            phaseObject.SetActive(true);
        }
        phaseOutput = phaseObject.GetComponent<TextMeshProUGUI>();
        phaseOutput.text = "Phase: " + "Defense";

        // On-screen text for turn counter
        turnObject = GameObject.Find("TurnCounter");
        if (turnObject == null)
        {
            turnObject = new GameObject("TurnCounter");
            turnObject.AddComponent<TextMeshProUGUI>();
            turnObject.SetActive(true);
        }
        turnOutput = turnObject.GetComponent<TextMeshProUGUI>();
        turnOutput.text = "Turn " + turnNum.ToString();

        //GameObject playerCtrlGameObject = new GameObject("PlayerCtrller");
        //playerList = playerCtrlGameObject.AddComponent<PlayerController>();

        turnPhaseName = GameObject.Find("Misc Bar").transform.Find("EndTurn").transform.Find("TurnName Bar").transform.Find("EndTurnName").gameObject.GetComponent<TextMeshProUGUI>();
        turnPhaseName.text = "End Phase";

    }

    /*
      * Advance - moves onto a player's next phase
      *      or once a player reaches their "Build" phase,
      *      their turn will be ended
      */
    public void Advance()
    {
        controller.DisableButtonCanvas();

        TurnMaster.AdvancePlayerPhase(PlayerController.CurrentPlayer);
        phaseOutput.text = "Phase: " + PlayerController.CurrentPlayer.GetCurrentPhase();

        turnNum = TurnMaster.GetCurrentTurn();
        turnOutput.text = "Turn " + turnNum.ToString();

        if (PlayerController.CurrentPlayer.GetCurrentPhase() == Player.Phase.Build){
            turnPhaseName.text = "End Turn";
        }
        else {
            turnPhaseName.text = "End Phase";
        }

    }

}
