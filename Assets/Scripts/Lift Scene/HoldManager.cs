using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class HoldManager : MonoBehaviour, IXRSelectFilter
{
    [SerializeField]
    private ArduinoController arduinoController;

    [SerializeField]
    private XRInteractionManager xrInteractionManager;

    [SerializeField]
    private LiftManager liftManager;

    [SerializeField]
    private GameObject leftHandPosition;

    public TextMeshProUGUI grabbingText;
    public TextMeshProUGUI holdingText;

    private bool grabbing = false;
    private bool holding = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public bool Grabbing 
    {
        get { return grabbing; }
        set { grabbing = value; }
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
                    arduinoController.Vibrate(false);
                else
                    arduinoController.Stop(false);
            }
                
            holding = value; 
        }
    }

    public bool canProcess => isActiveAndEnabled;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;

        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetupAttachKettlebell);
        grabInteractable.selectExited.AddListener(CancelAttachKettlebell);
    }

    // Update is called once per frame
    void Update()
    {
        grabbingText.text = "Grabbing: " + Grabbing.ToString();
        holdingText.text = "Holding (Kettlebell): " + Holding.ToString();
    }

    private void SetupAttachKettlebell(BaseInteractionEventArgs arg) 
    {
        liftManager.gameObject.SetActive(false);
        leftHandPosition.SetActive(false);
        Holding = true;
    }

    private void CancelAttachKettlebell(BaseInteractionEventArgs arg)
    {
        gameObject.transform.SetPositionAndRotation(originalPosition, originalRotation);
        liftManager.gameObject.SetActive(true);
        leftHandPosition.SetActive(true);
        Holding = false;
    }

    public void Revert()
    {
        gameObject.transform.SetPositionAndRotation(originalPosition, originalRotation);
        liftManager.gameObject.SetActive(true);
        leftHandPosition.SetActive(true);
        Holding = false;
    }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return Grabbing;
    }
}
