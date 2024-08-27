using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GripperManager : MonoBehaviour
{   
    [SerializeField]
    private JointManager jointManager;

    [SerializeField]
    private ArduinoController arduinoController;

    [SerializeField]
    private GameObject gripperJoint;
    
    [SerializeField]
    private GameObject leftHandle;

    public TextMeshProUGUI grabbingText;
    public TextMeshProUGUI holdingText;

    private float currentAngle = 10f;
    private readonly float rotationSpeed = 1f;
    private readonly float gripperRadius = 0.51631709605f;

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
        // Fetching hands, if there exist and left one is holding stapler then transform the stapler
        if (holding) 
        {
            // Calculate the euler angle of stapler based on the distance between index distal and thumb tip
            var (updatedAngle, deltaAngle) = jointManager.CalculateAngle(currentAngle, gripperRadius);

            // Rotate the bracket and stabilize the whole stapler
            leftHandle.transform.RotateAround(gripperJoint.transform.position, Vector3.back, deltaAngle * rotationSpeed);
            currentAngle = updatedAngle;
        }

        grabbingText.text = "Grabbing: " + Grabbing.ToString();
        holdingText.text = "Holding: " + Holding.ToString();
    }
}
