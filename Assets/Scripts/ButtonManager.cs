using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private JointManager jointManager;

    [SerializeField]
    private Transform offsetTransform;

    [SerializeField]
    private MotionManager motionManager;
    
    private float currentAngle = 90.0f;
    private float currentYPosition = 0.7656894f;
    private readonly float fingerRadius = 0.3f;
    private readonly float stopThreshold = 0.5f;
    private readonly float valueThreshold = 0.00001f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float updatedYPosition = offsetTransform.transform.position.y;
        var (updatedAngle, deltaAngle) = jointManager.CalculateAngle(
            currentAngle, fingerRadius, stopThreshold
        );

        motionManager.IsMoving = Mathf.Abs(deltaAngle) > stopThreshold 
            && Mathf.Abs(updatedYPosition - currentYPosition) > valueThreshold;

        currentAngle = updatedAngle;
        currentYPosition = updatedYPosition;
    }
}
