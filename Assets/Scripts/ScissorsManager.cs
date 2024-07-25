using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;

public class ScissorsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject leftScissorA;
    
    [SerializeField]
    private Transform leftIndexTipTransform;
    
    [SerializeField]
    private Transform leftThumbTipTransform;

    private List<XRHandSubsystem> handSubsystems;
    private XRHandSubsystem m_HandSubsystem;
    private float currentHingeAngle = 20f;
    private readonly float rotationSpeed = 1.5f;
    private readonly float stabilizeSpeed = 0.35f;
    private readonly float scissorRadius = 0.19469448883f;

    // For collision & motion flag
    [SerializeField]
    private MotionManager motionManager;

    private bool inContact = false;
    private readonly float stopThreshold = 0.75f;
    public bool InContact
    {
        get { return inContact; }
        set { inContact = value; }
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
        // Fetching hands, if there exist then transform the scissors
        if (m_HandSubsystem != null) 
        {
            // Calculate the euler angle of scissors based on the distance between index tip and thumb tip
            float dist = Vector3.Distance(leftIndexTipTransform.position, leftThumbTipTransform.position);
            float updatedHingeAngle = Mathf.Asin(dist / 2 / scissorRadius) * (180 / Mathf.PI) * 2;

            // Calculate angular difference 
            float deltaAngle = updatedHingeAngle - currentHingeAngle;

            // If the scissors are in contact with the interactable, 
            // update whether the fingers are moving and their motion type
            if (inContact && Mathf.Abs(deltaAngle) > stopThreshold)
            {
                motionManager.IsMoving = true;
                if (deltaAngle < 0) 
                {
                    motionManager.MotionType = MotionManager.Motion.Flexion;
                }
                else
                {
                    motionManager.MotionType = MotionManager.Motion.Extension;
                }
            } 

            // Else, moving state is always false
            else
            {
                motionManager.IsMoving = false;
            }

            // Rotate the left scissor and stabilize the whole scissors
            gameObject.transform.Rotate(0, 0, deltaAngle * stabilizeSpeed);
            leftScissorA.transform.Rotate(0, deltaAngle * rotationSpeed, 0);
            currentHingeAngle = updatedHingeAngle;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Interactable"))
        {
            // Debug.Log("Entered");
            inContact = true;
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Interactable"))
        {
            // Debug.Log("Staying");
        }   
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Interactable"))
        {
            // Debug.Log("Exited");
            inContact = false;
        }   
    }
}
