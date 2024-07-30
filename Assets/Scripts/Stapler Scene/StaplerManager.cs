using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;

public class StaplerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject staplerBracket;
    
    [SerializeField]
    private JointManager jointManager;

    private float currentHingeAngle = 0f;
    private readonly float rotationSpeed = 1.2f;
    private readonly float staplerRadius = 0.51631709605f;

    // For Grab & motion flag
    [SerializeField]
    private MotionManager motionManager;

    private bool grabbing = false;
    private bool holding = false;
    private readonly float stopThreshold = 0.5f;
    public bool Grabbing 
    {
        get { return grabbing; }
        set { grabbing = value; }
    }
    public bool Holding 
    {
        get { return holding; }
        set { holding = value; }
    }
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Fetching hands, if there exist and left one is holding stapler then transform the stapler
        if (holding) 
        {
            // Calculate the euler angle of stapler based on the distance between index distal and thumb tip
            var (updatedAngle, _) = jointManager.CalculateAngle(currentHingeAngle, staplerRadius);
            float updatedHingeAngle = 9 - updatedAngle;
            float deltaAngle = updatedHingeAngle - currentHingeAngle;

            // If the hand are in contact with the interactable, 
            // update whether the fingers are moving
            motionManager.IsMoving = Mathf.Abs(deltaAngle) > stopThreshold;
            
            // Rotate the bracket and stabilize the whole stapler
            staplerBracket.transform.Rotate(0, 0, deltaAngle * rotationSpeed);
            currentHingeAngle = updatedHingeAngle;
        }
    }

    public void ResetStapler()
    {
        float deltaAngle = 0 - currentHingeAngle;
        staplerBracket.transform.Rotate(0, 0, deltaAngle * rotationSpeed);
    }
}
