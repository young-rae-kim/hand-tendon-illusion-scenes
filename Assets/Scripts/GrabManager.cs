using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    [SerializeField]
    private Transform leftIndexTipTransform;
    
    [SerializeField]
    private Transform leftThumbTipTransform;

    private float currentDistance;

    // For Grab & motion flag
    [SerializeField]
    private MotionManager motionManager;

    private bool grabbing = false;
    private bool holding = false;
    private readonly float stopThreshold = 0.001f;
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
        currentDistance = Vector3.Distance(
            leftIndexTipTransform.position, leftThumbTipTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (holding) 
        {
            float updatedDistance = Vector3.Distance(
                leftIndexTipTransform.position, leftThumbTipTransform.position);
            float delta = updatedDistance - currentDistance;

            if (Mathf.Abs(delta) > stopThreshold) 
            { 
                motionManager.IsMoving = true;
                motionManager.MotionType = (delta < 0) 
                    ? MotionManager.Motion.Flexion : MotionManager.Motion.Extension;
            } 
            else 
            {
                motionManager.IsMoving = false;
            }
            currentDistance = updatedDistance;
        }
    }
}
