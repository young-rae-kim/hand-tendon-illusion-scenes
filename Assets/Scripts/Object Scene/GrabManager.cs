using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    [SerializeField]
    private JointManager jointManager;

    private float currentAngle = 90.0f;

    // For Grab & motion flag
    [SerializeField]
    private MotionManager motionManager;

    private bool grabbing = false;
    private bool holding = false;
    private readonly float stopThreshold = 0.5f;
    private readonly float fingerRadius = 0.3f;

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

    // Update is called once per frame
    void Update()
    {
        if (holding) 
        {
            var (updatedAngle, deltaAngle) = jointManager.CalculateAngle(currentAngle, fingerRadius);
            motionManager.IsMoving = Mathf.Abs(deltaAngle) > stopThreshold;
            currentAngle = updatedAngle;
        }
    }
}
