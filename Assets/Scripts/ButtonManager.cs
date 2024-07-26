using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Transform offsetTransform;

    [SerializeField]
    private MotionManager motionManager;
    
    private readonly float stopThreshold = 0.00001f;
    private float currentYPosition = 0.7656894f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float updatedYPosition = offsetTransform.transform.position.y;
        float delta = updatedYPosition - currentYPosition;

        if (Mathf.Abs(delta) > stopThreshold) 
        { 
            motionManager.IsMoving = true;
            motionManager.MotionType = (delta < 0) ? MotionManager.Motion.Flexion : MotionManager.Motion.Extension;
        } 

        // Else, moving state is always false
        else 
        {
            motionManager.IsMoving = false;
        }

        currentYPosition = updatedYPosition;
    }
}
