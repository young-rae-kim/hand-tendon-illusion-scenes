using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LiftManager : MonoBehaviour
{
    [SerializeField]
    private ArduinoController arduinoController;

    [SerializeField]
    private XRInteractionManager xrInteractionManager;

    [SerializeField]
    private GameObject leftHandPosition;

    public TextMeshProUGUI pointingText;
    public TextMeshProUGUI holdingText;

    private bool pointing = false;
    private bool holding = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public bool Pointing 
    {
        get { return pointing; }
        set { pointing = value; }
    }
    public bool Holding 
    {
        get { return holding; }
        set 
        { 
            bool previousValue = holding; 
             
            if (previousValue != value)
            {
                if (value)
                    arduinoController.Vibrate(true);
                else
                    arduinoController.Stop(true);
            }
                
            holding = value; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;

        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.hoverEntered.AddListener(SetupAttach);
        grabInteractable.selectExited.AddListener(CancelAttach);
    }

    // Update is called once per frame
    void Update()
    {
        pointingText.text = "Pointing: " + Pointing.ToString();
        holdingText.text = "Holding: " + Holding.ToString();
    }

    private void SetupAttach(BaseInteractionEventArgs arg)
    {      
        if (Pointing) 
        {
            xrInteractionManager.SelectEnter(
                (IXRSelectInteractor) arg.interactorObject,
                (IXRSelectInteractable) arg.interactableObject);
            leftHandPosition.SetActive(false);
            Holding = true;
        }
    }

    private void CancelAttach(BaseInteractionEventArgs arg)
    {
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
        leftHandPosition.SetActive(true);
        Holding = false;
    }

    public void Revert()
    {
        gameObject.transform.SetPositionAndRotation(originalPosition, originalRotation);
        leftHandPosition.SetActive(true);
        Holding = false;
    }
}
