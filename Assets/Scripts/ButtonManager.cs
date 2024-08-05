using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private JointManager jointManager;

    [SerializeField]
    private Transform offsetTransform;

    public TextMeshProUGUI pushedText;
    public bool Pushed
    {
        get { return pushed; }
        set { pushed = value; }
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
        pushed = updatedYPosition < positionThreshold;
        pushedText.text = "Pushed: " + pushed.ToString();
    }
}
