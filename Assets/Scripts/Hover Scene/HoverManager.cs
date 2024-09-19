using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    [SerializeField]
    private ArduinoController arduinoController;

    [SerializeField]
    private MotionManager motionManager;

    [SerializeField]
    private Transform buttonTransform;

    public TextMeshProUGUI hoveredText;
    public TextMeshProUGUI pushedText;

    public bool Hovered
    {
        get { return hovered; }
        set 
        { 
            if (hovered != value)
            {
                if (value)
                    arduinoController.Vibrate(false);
                else
                    arduinoController.Stop(false);
            }

            hovered = value;
        }
    }

    public bool Pushed
    {
        get { return pushed; }
        set
        {
            if (!pushed && value)
                arduinoController.Stop(false);
            
            pushed = value;
        }
    }

    private bool hovered = false;
    private bool pushed = false;

    // Update is called once per frame
    void Update()
    {
        hoveredText.text = "Hovered: " + Hovered.ToString();
        pushedText.text = "Pushed: " + Pushed.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!Hovered && other.gameObject.CompareTag("Indicator"))
        {
            Debug.Log("Hover detected");
            Hovered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (Hovered && other.gameObject.CompareTag("Indicator"))
        {
            Debug.Log("Hover out-of-range");
            Hovered = false;
        }
    }
}
