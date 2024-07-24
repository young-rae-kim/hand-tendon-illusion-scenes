using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMotionFlags : MonoBehaviour
{
    public TextMeshProUGUI isMovingText;
    public TextMeshProUGUI motionTypeText;
    public MotionManager motionManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isMovingText.text = motionManager.IsMoving.ToString();
        motionTypeText.text = motionManager.MotionType.ToString();
    }
}
