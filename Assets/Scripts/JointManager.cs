using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    private TextMeshProUGUI velocityText;

    private ushort index = 0;
    private float currentAngle = 0.0f;
    private readonly float angleThreshold = 20f;
    public float flexionThreshold;
    public float extensionThreshold;

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
        float angleVelocity = (updatedAngle - currentAngle) / Time.deltaTime;

        if (index > 10) 
        {
            velocityText.text = "Angular velocity: " + angleVelocity.ToString("N2");
            index = 0;
        }

        index++;
        
        // Update motion type (flexion or extension)
        if (!motionManager.IsMoving)
        {
            switch (motionManager.MotionType)
            {
                case MotionManager.Motion.Flexion:
                    if (angleVelocity < -extensionThreshold)
                    {
                        motionManager.MotionType = MotionManager.Motion.Extension;
                        motionManager.IsMoving = true;
                    }
                    else
                    {
                        if (updatedAngle < angleThreshold)
                            motionManager.MotionType = MotionManager.Motion.Extension;
                    }
                    break;

                case MotionManager.Motion.Extension:
                    if (angleVelocity > flexionThreshold)
                    {
                        motionManager.MotionType = MotionManager.Motion.Flexion;
                        motionManager.IsMoving = true;
                    }
                    else
                    {
                        if (updatedAngle > angleThreshold)
                            motionManager.MotionType = MotionManager.Motion.Flexion;
                    }
                    break;
            }
        } 
        
        else
        {
            switch (motionManager.MotionType)
            {
                case MotionManager.Motion.Flexion:
                    motionManager.IsMoving = angleVelocity > flexionThreshold;
                    break;

                case MotionManager.Motion.Extension:
                    motionManager.IsMoving = angleVelocity < -extensionThreshold;
                    break;
            }
        }
        
        currentAngle = updatedAngle;
    }
}
