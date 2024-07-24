using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionManager : MonoBehaviour
{
    private bool isMoving = false;
    public bool IsMoving 
    {
        get { return isMoving; }
        set { isMoving = value; }
    }
    
    private Motion motionType;
    public enum Motion
    {
        Flexion,
        Extension
    }
    public Motion MotionType
    {
        get { return motionType; }
        set { motionType = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
