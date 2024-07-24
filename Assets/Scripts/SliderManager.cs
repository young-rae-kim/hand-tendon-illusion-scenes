using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField]
    VibrationManager vibrationManager;
    Transform targetWrist;
    Slider slider;

    private float currentValue;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        currentValue = slider.value;
        // Debug.Log("Start value: " + currentValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateVibration () {
        float wristRotationAngle = targetWrist.rotation.eulerAngles.z;
        float sliderVelocity = CalculateVelocity();

        // Debug.Log("Wrist angle: " + wristRotationAngle);
        // Debug.Log("Velocity: " + sliderVelocity);

        if (wristRotationAngle > -90 && wristRotationAngle < 90) {
            if (sliderVelocity > 0) {
                vibrationManager.VibrateTowardDown();
            } else {
                vibrationManager.VibrateTowardUp();
            }
        } else {
            if (sliderVelocity > 0) {
                vibrationManager.VibrateTowardUp();
            } else {
                vibrationManager.VibrateTowardDown();
            }
        }
    }

    private float CalculateVelocity () {
        float updatedValue = slider.value;
        float velocity = (updatedValue - currentValue) / Time.deltaTime;
        currentValue = updatedValue;
        return velocity;
    }
}
