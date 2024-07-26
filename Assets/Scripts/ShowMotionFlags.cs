using System.Collections;
using TMPro;
using UnityEngine;

public class ShowMotionFlags : MonoBehaviour
{
    public TextMeshProUGUI isMovingText;
    public TextMeshProUGUI motionTypeText;
    public TextMeshProUGUI grabbingText;
    public TextMeshProUGUI holdingText;
    public MotionManager motionManager;
    public StaplerManager staplerManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isMovingText.text = "Is Moving: " + motionManager.IsMoving.ToString();
        motionTypeText.text = "Motion Type: " + motionManager.MotionType.ToString();
        grabbingText.text = "Grabbing: " + staplerManager.Grabbing.ToString();
        holdingText.text = "Holding: " + staplerManager.Holding.ToString();
    }
}
