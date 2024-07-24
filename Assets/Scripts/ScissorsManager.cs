using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Hands;

public class ScissorsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject rightScissorB;
    
    [SerializeField]
    private Transform rightIndexTipTransform;
    
    [SerializeField]
    private Transform rightThumbTipTransform;

    private List<XRHandSubsystem> handSubsystems;
    private XRHandSubsystem m_HandSubsystem;
    private float currentHingeAngle = -17.5f;

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
        Debug.Log("Ref rotation: " + rightScissorB.transform.eulerAngles);

        if (m_HandSubsystem != null) 
            {

            float dist = Vector3.Distance(rightIndexTipTransform.position, rightThumbTipTransform.position);
            float radius = 0.19469448883f;
            float updatedHingeAngle = - Mathf.Asin(dist / 2 / radius) * (180 / Mathf.PI) * 2;

            rightScissorB.transform.Rotate(0, (updatedHingeAngle - currentHingeAngle) * 1.5f, 0);
            currentHingeAngle = updatedHingeAngle;

            // float dist = Vector3.Distance(rightIndexTipTransform.position, rightThumbTipTransform.position);
            // float radius = 0.19469448883f;
            // float updatedRotation = Mathf.Asin(dist / 2 / radius) * (180 / Mathf.PI) * 2;
            // Debug.Log(updatedRotation);
            
            // rightScissorA.transform.position = Vector3.MoveTowards(
            //     rightScissorA.transform.position, 
            //     rightThumbTipTransform.position + new Vector3 (-0.09f, 0f, 0.08f), .9f);
        }
    }
}
