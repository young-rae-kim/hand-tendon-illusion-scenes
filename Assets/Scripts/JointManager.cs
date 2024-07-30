using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointManager : MonoBehaviour
{
    [SerializeField]
    private MotionManager motionManager;
    
    [SerializeField]
    private Transform leftIndexTransform;
    
    [SerializeField]
    private Transform leftThumbTransform;

    [SerializeField]
    private Transform leftPalmTransform;

    [SerializeField]
    private Transform leftIndexProximalTransform;

    private float currentAngle = 0.0f;
    private readonly float angleThreshold = 2f;

    void Update()
    {
        UpdateMotionType();
    }

    public (float updatedAngle, float deltaAngle) CalculateAngle (float currentAngle, float objectRadius)
    {
        // Calculate the euler angle of gadget based on the distance between index and thumb
        float dist = Vector3.Distance(leftIndexTransform.position, leftThumbTransform.position);
        float updatedAngle = Mathf.Asin(dist / 2 / objectRadius) * (180 / Mathf.PI) * 2;
        float deltaAngle = updatedAngle - currentAngle;

        return (updatedAngle, deltaAngle);
    }

    public void UpdateMotionType()
    {
        float updatedAngle = Quaternion.Angle(leftPalmTransform.rotation, leftIndexProximalTransform.rotation);
        float deltaAngle = updatedAngle - currentAngle;

        // Update motion type (flexion or extension)
        if (Mathf.Abs(deltaAngle) > angleThreshold)
            motionManager.MotionType = (deltaAngle > 0) 
                ? MotionManager.Motion.Flexion 
                : MotionManager.Motion.Extension;
        
        currentAngle = updatedAngle;
    }
}
