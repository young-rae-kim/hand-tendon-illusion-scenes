using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.Samples.VisualizerSample;

public class HandManager : MonoBehaviour
{
    [SerializeField]
    private Transform testJoint;
    [SerializeField]
    private GameObject leftDirectInteractor;
    
    // private XRHandSubsystem m_HandSubsystem;
    private bool triggerEntered;
    private bool jointClosed;
    private HandVisualizer handVisualizer;
    private SphereCollider l_Collider;
    

    [SerializeField]
    public float rotationSpeed = 10.0f;
    
    void Awake() {
        // List<XRHandSubsystem> handSubsystems = new List<XRHandSubsystem>();
        // SubsystemManager.GetSubsystems(handSubsystems);

        // for (int i = 0; i < handSubsystems.Count; i++) {
        //     XRHandSubsystem handSubsystem = handSubsystems[i];
        //     if (handSubsystem.running) {
        //         m_HandSubsystem = handSubsystem;
        //         break;
        //     }
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        handVisualizer = GetComponent<HandVisualizer>();
        l_Collider = leftDirectInteractor.GetComponent<SphereCollider>();
        l_Collider.enabled = true;
        l_Collider.center = new Vector3(-0.1f, 0.03f, 0.03f);
        l_Collider.radius = 0.05f;
        triggerEntered = false;
        jointClosed = false;

        Debug.Log("Left Collider set");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator JointTransform() {
        if (!triggerEntered) {

            if (!jointClosed) {

                Debug.Log("Joint Closing, Start angle: " + testJoint.rotation.x);
                triggerEntered = true;
                handVisualizer.drawMeshes = false;

                while (testJoint.rotation.x > 0.02) {
                    float step = rotationSpeed * Time.deltaTime;
                    RotateJoint(step);
                    yield return null;
                }

                triggerEntered = false;
                jointClosed = true;
                handVisualizer.drawMeshes = true;
                Debug.Log("Joint Closed");

            } else {

                Debug.Log("Joint Opening, Start angle: " + testJoint.rotation.x);
                triggerEntered = true;
                handVisualizer.drawMeshes = false;

                while (testJoint.rotation.x < 0.08) {
                    float step = - rotationSpeed * Time.deltaTime;
                    RotateJoint(step);
                    yield return null;
                }

                triggerEntered = false;
                jointClosed = false;
                handVisualizer.drawMeshes = true;
                Debug.Log("Joint Opened");

            }
        }
    }

    private void RotateJoint(float step)
    {
        testJoint.Rotate(step, 0, 0);
    }
}
