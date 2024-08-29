using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PushManager : MonoBehaviour
{
    [SerializeField]
    private JointManager jointManager;

    [SerializeField]
    private ArduinoController arduinoController;

    public TextMeshProUGUI pointingText;
    public TextMeshProUGUI pushingText;

    private bool pointing = false;
    private bool pushing = false;

    public bool Pointing 
    {
        get { return pointing; }
        set { pointing = value; }
    }
    public bool Pushing 
    {
        get { return pushing; }
        set 
        { 
            bool previousValue = pushing; 
             
            if (Pointing && previousValue != value)
            {
                if (value)
                    arduinoController.Vibrate(true);
                else
                    arduinoController.Stop(true);
            }
                
            pushing = value; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        pointingText.text = "Pointing: " + pointing.ToString();
        pushingText.text = "Pushing: " + pushing.ToString();
    }
}
