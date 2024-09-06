using System;
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
    private Transform extendedTape;

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
                RevertTape();
                
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
    private readonly float lowerbound = 0.255f;
    private readonly float upperbound = 0.825f;

    private float currentSpeed = 0f;
    private readonly float threshold = 0.01f;
    private readonly float maxSpeed = 1f;
    private readonly float acceleration = 1f;

    // Update is called once per frame
    void Update()
    {
        float updatedPosition = leftIndexTip.position.x + offset;

        if (extendedTape.position.x > upperbound)
        {
            MoveTape(upperbound);
        }
        else if (extendedTape.position.x < lowerbound)
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
        pinchingText.text = "Downward (LH): " + pinching.ToString();
    }

    private void MoveTape(float xPosition)
    {
        extendedTape.position = new Vector3 (
            xPosition, extendedTape.position.y, extendedTape.position.z
        );
    }

    private IEnumerator MoveTapeWithAnimation(float xPosition)
    {
        while (Math.Abs(extendedTape.position.x - xPosition) > threshold)
        {
            Debug.Log(extendedTape.position.x);
            currentSpeed += Mathf.Min(acceleration * Time.deltaTime, 1);
            extendedTape.position = Vector3.MoveTowards(extendedTape.position,
                new Vector3(xPosition, extendedTape.position.y, extendedTape.position.z), 
                maxSpeed * currentSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        currentSpeed = 0f;
        Debug.Log("Returned");
    }

    private void RevertTape()
    {
        StartCoroutine(MoveTapeWithAnimation(upperbound));
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
