using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public void VibrateTowardUp () {
        Vibrate(false);
    }

    public void VibrateTowardDown () {
        Vibrate(true);
    }

    public void StopVibration () {
        Debug.Log("Vibration stopped");
    }

    private void Vibrate(bool up) {
        if (up) {
            Debug.Log("Activate upper proximal vibration");
        } else {
            Debug.Log("Activate downward proximal vibration");
        }
    }
}
