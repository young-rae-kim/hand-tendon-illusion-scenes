using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class LiftManager : MonoBehaviour, IXRSelectFilter
{
    [SerializeField]
    private ArduinoController arduinoController;

    [SerializeField]
    private XRInteractionManager xrInteractionManager;

    [SerializeField]
    private GameObject leftHandPosition;

    [SerializeField]
    private HoldManager holdManager;

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

    public bool canProcess => isActiveAndEnabled;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;

        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetupAttachLantern);
        grabInteractable.selectExited.AddListener(CancelAttachLantern);
    }

    // Update is called once per frame
    void Update()
    {
        pointingText.text = "Pointing: " + Pointing.ToString();
        holdingText.text = "Holding (Lantern): " + Holding.ToString();
    }

    private void SetupAttachLantern(BaseInteractionEventArgs arg)
    {      
        holdManager.gameObject.SetActive(false);
        leftHandPosition.SetActive(false);
        Holding = true;
    }

    private void CancelAttachLantern(BaseInteractionEventArgs arg)
    {
        gameObject.transform.SetPositionAndRotation(originalPosition, originalRotation);
        holdManager.gameObject.SetActive(true);
        leftHandPosition.SetActive(true);
        Holding = false;
    }

    public void Revert()
    {
        gameObject.transform.SetPositionAndRotation(originalPosition, originalRotation);
        holdManager.gameObject.SetActive(true);
        leftHandPosition.SetActive(true);
        Holding = false;
    }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return Pointing;
    }
}
