using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckGesture : MonoBehaviour
{
    public TextMeshProUGUI gestureCheck;
    private bool isGrabbing = false;
    public bool IsGrabbing
    {
        get { return isGrabbing; }
        set { isGrabbing = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gestureCheck.text = isGrabbing.ToString();
    }
}
