using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Transform offsetTransform;

    [SerializeField]
    private ArduinoController arduinoController;

    [SerializeField]
    private MotionManager motionManager;

    public TextMeshProUGUI pushedText;
    public bool Pushed
    {
        get { return pushed; }
        set 
        { 
            bool previousValue = pushed; 
             
            if (previousValue != value)
            {
                if (value)
                    arduinoController.Vibrate(true);
                else
                    arduinoController.Stop(true);
            }

            pushed = value;
        }
    }

    private bool pushed = false;
    private readonly float positionThreshold = 0.76f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float updatedYPosition = offsetTransform.transform.position.y;
        Pushed = updatedYPosition < positionThreshold;
        pushedText.text = "Pushed: " + pushed.ToString();
    }
}
