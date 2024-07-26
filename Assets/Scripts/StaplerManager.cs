using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;

public class StaplerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject staplerBracket;
    
    [SerializeField]
    private Transform leftIndexDistalTransform;
    
    [SerializeField]
    private Transform leftThumbTipTransform;

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
            float dist = Vector3.Distance(leftIndexDistalTransform.position, leftThumbTipTransform.position);
            float updatedHingeAngle = 9 - (Mathf.Asin(dist / 2 / staplerRadius) * (180 / Mathf.PI) * 2);

            // Calculate angular difference 
            float deltaAngle = updatedHingeAngle - currentHingeAngle;

            // If the hand are in contact with the interactable, 
            // update whether the fingers are moving and their motion type
            if (Mathf.Abs(deltaAngle) > stopThreshold) 
            { 
                motionManager.IsMoving = true;
                motionManager.MotionType = (deltaAngle > 0) ? MotionManager.Motion.Flexion : MotionManager.Motion.Extension;
            } 

            // Else, moving state is always false
            else 
            {
                motionManager.IsMoving = false;
            }
            
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
