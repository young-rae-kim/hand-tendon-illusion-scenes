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
            float radius = 0.19469448883f;
            float updatedHingeAngle = Mathf.Asin(dist / 2 / radius) * (180 / Mathf.PI) * 2;

            // Rotate the left scissor and stabilize the whole scissors
            gameObject.transform.Rotate(0, 0, (updatedHingeAngle - currentHingeAngle) * stabilizeSpeed);
            leftScissorA.transform.Rotate(0, (updatedHingeAngle - currentHingeAngle) * rotationSpeed, 0);
            currentHingeAngle = updatedHingeAngle;
        }
    }
}
