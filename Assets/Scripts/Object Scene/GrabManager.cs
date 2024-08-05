using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    [SerializeField]
    private JointManager jointManager;

    public TextMeshProUGUI grabbingText;
    public TextMeshProUGUI holdingText;

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

    // Update is called once per frame
    void Update()
    {
        grabbingText.text = "Grabbing: " + grabbing.ToString();
        holdingText.text = "Holding: " + holding.ToString();
    }
}
