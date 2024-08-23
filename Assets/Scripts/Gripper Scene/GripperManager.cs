using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GripperManager : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHandle;
    
    [SerializeField]
    private JointManager jointManager;

    public TextMeshProUGUI grabbingText;
    public TextMeshProUGUI holdingText;

    private float currentAngle = 10f;
    private readonly float rotationSpeed = 1.2f;
    private readonly float gripperRadius = 0.51631709605f;
    private Vector3 originalEulerAngles;

    private bool grabbing = false;
    private bool holding = false;
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
        originalEulerAngles = leftHandle.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
                // Fetching hands, if there exist and left one is holding stapler then transform the stapler
        if (holding) 
        {
            // Calculate the euler angle of stapler based on the distance between index distal and thumb tip
            var (updatedAngle, deltaAngle) = jointManager.CalculateAngle(currentAngle, gripperRadius);

            // Rotate the bracket and stabilize the whole stapler
            leftHandle.transform.RotateAround(leftHandle.transform.position, Vector3.forward, deltaAngle * rotationSpeed);
            currentAngle = updatedAngle;
        }

        grabbingText.text = "Grabbing: " + Grabbing.ToString();
        holdingText.text = "Holding: " + Holding.ToString();
    }

    public void ResetGripper()
    {
        leftHandle.transform.localEulerAngles = originalEulerAngles;
        currentAngle = 10f;
    }
}
