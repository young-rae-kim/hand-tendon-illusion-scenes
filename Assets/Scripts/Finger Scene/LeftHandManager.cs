using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;

public class LeftHandManager : MonoBehaviour
{
    [SerializeField]
    private HandVisualizer handVisualizer;

    private HandManager handManager;

    // Start is called before the first frame update
    void Start()
    {
        handManager = handVisualizer.GetComponent<HandManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OnTriggerEnter(Collider other) {
        yield return StartCoroutine(handManager.JointTransform());
    }
}
