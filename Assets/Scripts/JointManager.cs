using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;

public class JointManager : MonoBehaviour
{
    [SerializeField]
    private MotionManager motionManager;
    
    [SerializeField]
    private Transform leftIndexTransform;
    
    [SerializeField]
    private Transform leftThumbTransform;

    public (float updatedAngle, float deltaAngle) CalculateAngle (float currentAngle, float objectRadius, float angleThreshold, bool reverse = false)
    {
        // Calculate the euler angle of gadget based on the distance between index and thumb
        float dist = Vector3.Distance(leftIndexTransform.position, leftThumbTransform.position);
        float updatedAngle = Mathf.Asin(dist / 2 / objectRadius) * (180 / Mathf.PI) * 2;
        float deltaAngle = updatedAngle - currentAngle;

        // Update motion type (flexion or extension)
        if (Mathf.Abs(deltaAngle) > angleThreshold)
            motionManager.MotionType = (reverse ^ deltaAngle < 0) 
                ? MotionManager.Motion.Flexion 
                : MotionManager.Motion.Extension;

        return (updatedAngle, deltaAngle);
    }
}
