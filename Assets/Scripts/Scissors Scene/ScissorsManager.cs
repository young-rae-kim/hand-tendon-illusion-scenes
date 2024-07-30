using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands;

public class ScissorsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject leftScissorA;

    [SerializeField]
    private JointManager jointManager;

    [SerializeField]
    private TextMeshProUGUI closedText;

    private List<XRHandSubsystem> handSubsystems;
    private XRHandSubsystem m_HandSubsystem;
    private float currentHingeAngle = 20f;
    private readonly float rotationSpeed = 1.5f;
    private readonly float stabilizeSpeed = 0.35f;
    private readonly float scissorRadius = 0.19469448883f;

    // For collision & motion flag
    [SerializeField]
    private MotionManager motionManager;

    private bool closed = false;
    private bool inContact = false;
    private readonly float stopThreshold = 0.75f;
    private readonly float closedThreshold = 15f;
    
    public bool Closed
    {
        get { return closed; }
        set { closed = value; }
    }
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
            var (updatedAngle, deltaAngle) = jointManager.CalculateAngle(currentHingeAngle, scissorRadius, stopThreshold);

            // If the scissors are in contact with the interactable, update whether the fingers are moving 
            motionManager.IsMoving = inContact && Mathf.Abs(deltaAngle) > stopThreshold;

            // Rotate the left scissor and stabilize the whole scissors
            gameObject.transform.Rotate(0, 0, deltaAngle * stabilizeSpeed);
            leftScissorA.transform.Rotate(0, deltaAngle * rotationSpeed, 0);
            closed = updatedAngle < closedThreshold;

            currentHingeAngle = updatedAngle;
        }

        closedText.text = "Closed: " + closed.ToString();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Interactable"))
        {
            inContact = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Interactable"))
        {
            inContact = false;
        }   
    }
}
