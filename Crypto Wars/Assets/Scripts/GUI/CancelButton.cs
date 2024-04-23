using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    [SerializeField]
    private GameObject cancelButton;
    // Start is called before the first frame update
    void Awake()
    {
        Deactivate();
    }

    //Moves the cancel button out of frame
    public void Deactivate()
    {
        cancelButton.SetActive(false);
    }
}
