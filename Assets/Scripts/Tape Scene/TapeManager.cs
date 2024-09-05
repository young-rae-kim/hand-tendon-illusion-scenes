using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHand;
    
    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private GameObject extendedTape;

    [SerializeField]
    private GameObject leftHandPosition;

    [SerializeField]
    private Transform leftIndexTip;

    [SerializeField]
    private ArduinoController arduinoController;

    public TextMeshProUGUI attachedText;
    public TextMeshProUGUI holdingText;
    public TextMeshProUGUI pinchingText;

    private bool firstAttached = false;

    public bool Attached
    {
        get { return attached; }
        set 
        {
            if (value != attached)
            {
                if (value)
                {
                    rightHand.SetActive(false);
                    firstAttached = true;
                }    
                else
                    rightHand.SetActive(true);
            }

            attached = value;
        }
    }

    public bool Pinching
    {
        get { return pinching; }
        set 
        {
            if (!value)
                Holding = false;
            pinching = value;
        }
    }

    public bool Holding
    {
        get { return holding; }
        set 
        {
            if (holding != value)
            {
                if (value && Pinching)
                    arduinoController.Vibrate(false);
                else
                    arduinoController.Stop(false);
            }

            holding = value;
        }
    }
    
    
    private bool attached = false;
    private bool holding = false;
    private bool pinching = false;

    private readonly float offset = 0.705f;
    private readonly float lowerbound = 0.225f;
    private readonly float upperbound = 0.825f;

    // Update is called once per frame
    void Update()
    {
        float updatedPosition = leftIndexTip.position.x + offset;

        if (extendedTape.transform.position.x > upperbound)
        {
            MoveTape(upperbound);
        }
        else if (extendedTape.transform.position.x < lowerbound)
        {
            Debug.Log("Tape maximum length reached");
            RevertTape();
        } 
        else if (Holding)
        {
            MoveTape(updatedPosition);
            leftHand.SetActive(false);
            leftHandPosition.SetActive(true);
        }

        attachedText.text = "Attached (RH): " + attached.ToString();
        holdingText.text = "Holding (LH): " + holding.ToString();
        pinchingText.text = "Pinching: " + pinching.ToString();
    }

    private void MoveTape(float xPosition)
    {
        extendedTape.transform.position = new Vector3 (
            xPosition, extendedTape.transform.position.y, extendedTape.transform.position.z
        );
    }

    private void MoveTapeWithAnimation(float xPosition)
    {
        extendedTape.transform.position = new Vector3 (
            xPosition, extendedTape.transform.position.y, extendedTape.transform.position.z
        );
    }

    private void RevertTape()
    {
        MoveTapeWithAnimation(upperbound);
        leftHand.SetActive(true);
        leftHandPosition.SetActive(true);
        Holding = false;
    }

    public void Revert()
    {
        firstAttached = false;
        RevertTape();
    }

    public void ToggleHolding()
    {
        if (!Holding && firstAttached && Pinching)
        {
            Debug.Log("Tape trigger detected");
            leftHand.SetActive(false);
            Holding = true;
        }
    }
}
