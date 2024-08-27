using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    [SerializeField]
    private JointManager jointManager;

    [SerializeField]
    private ArduinoController arduinoController;

    public TextMeshProUGUI grabbingText;
    public TextMeshProUGUI holdingText;

    private bool grabbing = false;
    private bool holding = false;

    public bool Grabbing 
    {
        get { return grabbing; }
        set 
        { 
            bool previousValue = grabbing; 
             
            if (Holding && previousValue != value)
            {
                if (value)
                    arduinoController.Vibrate(true);
                else
                    arduinoController.Stop(true);
            }

            grabbing = value;
        }
    }
    public bool Holding 
    {
        get { return holding; }
        set 
        { 
            if (!value && arduinoController.Vibration)
                arduinoController.Stop(true);
                
            holding = value; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        grabbingText.text = "Grabbing: " + grabbing.ToString();
        holdingText.text = "Holding: " + holding.ToString();
    }
}
