using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField]
    private JointManager jointManager;

    [SerializeField]
    private MotionManager motionManager;

    [SerializeField]
    private Slider slider;

    private float currentValue;
    private float currentAngle = 90.0f;
    private readonly float fingerRadius = 0.3f;
    private readonly float stopThreshold = 0.5f;
    private readonly float valueThreshold = 0.001f;

    void Start()
    {
        currentValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        float updatedValue = slider.value;
        var (updatedAngle, deltaAngle) = jointManager.CalculateAngle(currentAngle, fingerRadius);

        motionManager.IsMoving = Mathf.Abs(deltaAngle) > stopThreshold 
            && Mathf.Abs(updatedValue - currentValue) > valueThreshold;

        currentAngle = updatedAngle;
        currentValue = updatedValue;
    }
}
