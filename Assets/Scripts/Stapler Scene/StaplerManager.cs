using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands;

public class StaplerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject staplerBracket;
    
    [SerializeField]
    private JointManager jointManager;

    [SerializeField]
    private ArduinoController arduinoController;

    public TextMeshProUGUI grabbingText;
    public TextMeshProUGUI holdingText;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private float currentHingeAngle = 0f;
    private readonly float rotationSpeed = 1.5f;
    private readonly float staplerRadius = 0.51631709605f;

    private bool grabbing = false;
    private bool holding = false;

    public bool Grabbing 
    {
        get { return grabbing; }
        set 
        { 
            bool previousValue = grabbing; 
             
            if (previousValue != value)
            {
                if (Holding && value)
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

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        Debug.Log(staplerBracket.transform.localEulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Angle(staplerBracket.transform.rotation.eulerAngles, transform.rotation.eulerAngles));

        // Fetching hands, if there exist and left one is holding stapler then transform the stapler
        if (holding) 
        {
            // Calculate the euler angle of stapler based on the distance between index distal and thumb tip
            var (updatedAngle, _) = jointManager.CalculateAngle(currentHingeAngle, staplerRadius);
            float updatedHingeAngle = 9 - updatedAngle;
            float deltaAngle = updatedHingeAngle - currentHingeAngle;
            
            // Rotate the bracket and stabilize the whole stapler
            staplerBracket.transform.Rotate(0, 0, deltaAngle * rotationSpeed);
            currentHingeAngle = updatedHingeAngle;
        }

        grabbingText.text = "Grabbing: " + Grabbing.ToString();
        holdingText.text = "Holding: " + Holding.ToString();
    }

    public void ResetStapler()
    {
        staplerBracket.transform.localEulerAngles = new Vector3(0, 0, 0);
        currentHingeAngle = 0f;
    }

    public void Revert()
    {
        transform.SetPositionAndRotation(originalPosition, originalRotation);
    }
}
