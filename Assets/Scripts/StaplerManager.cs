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

    private List<XRHandSubsystem> handSubsystems;
    private XRHandSubsystem m_HandSubsystem;
    private float currentHingeAngle = 0f;
    private readonly float rotationSpeed = 1.2f;
    private readonly float stabilizeSpeed = 0.35f;
    private readonly float staplerRadius = 0.51631709605f;

    // For Grab & motion flag
    [SerializeField]
    private MotionManager motionManager;

    private bool grabbing = false;
    private bool holding = false;
    private readonly float stopThreshold = 0.05f;
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
        handSubsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(handSubsystems);

        for (int i = 0; i < handSubsystems.Count; i++)
        {
            XRHandSubsystem handSubsystem = handSubsystems[i];
            if (handSubsystem.running)
            {
                m_HandSubsystem = handSubsystem;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // // Fetching hands, if there exist and left one is grabbing then transform the stapler
        if (m_HandSubsystem != null) 
        {
            // Calculate the euler angle of stapler based on the distance between index distal and thumb tip
            float dist = Vector3.Distance(leftIndexDistalTransform.position, leftThumbTipTransform.position);
            float updatedHingeAngle = 9 - (Mathf.Asin(dist / 2 / staplerRadius) * (180 / Mathf.PI) * 2);

            // Calculate angular difference 
            float deltaAngle = updatedHingeAngle - currentHingeAngle;

            // If the stapler are in contact with the interactable, 
            // update whether the fingers are moving and their motion type
            if (deltaAngle > stopThreshold)
            {
                motionManager.IsMoving = true;
                motionManager.MotionType = MotionManager.Motion.Flexion;
            } 

            // Else, moving state is always false
            else
            {
                motionManager.IsMoving = false;
                motionManager.MotionType = MotionManager.Motion.Extension;
            }

            // Rotate the bracket and stabilize the whole stapler
            // gameObject.transform.Rotate(0, 0, deltaAngle * stabilizeSpeed);
            staplerBracket.transform.Rotate(0, 0, deltaAngle * rotationSpeed);
            currentHingeAngle = updatedHingeAngle;
        }
    }
}
