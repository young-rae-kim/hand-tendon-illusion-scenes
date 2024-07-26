using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField]
    private MotionManager motionManager;

    [SerializeField]
    private Transform targetWrist;

    [SerializeField]
    private Slider slider;

    private float currentValue;
    private readonly float stopThreshold = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        currentValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        float updatedValue = slider.value;
        float delta = updatedValue - currentValue;

        if (Mathf.Abs(delta) > stopThreshold) 
        {
            float wristRotationAngle = targetWrist.rotation.eulerAngles.z;

            if (wristRotationAngle > -90 && wristRotationAngle < 90) 
            {
                motionManager.MotionType = delta < 0 
                    ? MotionManager.Motion.Extension : MotionManager.Motion.Flexion;
            } else 
            {
                motionManager.MotionType = delta < 0 
                    ? MotionManager.Motion.Flexion : MotionManager.Motion.Extension;
            }
        }
        else
        {
            motionManager.IsMoving = false;
        }

        currentValue = updatedValue;
    }
}
