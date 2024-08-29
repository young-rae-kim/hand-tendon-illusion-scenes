using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField]
    private PushManager pushManager;
    
    [SerializeField]
    private GameObject train;

    [SerializeField]
    private GameObject leftHand;

    [SerializeField]
    private GameObject leftHandPosition;

    [SerializeField]
    private Transform leftIndexTip;

    private bool firstPushed = false;
    private float currentXPosition = 0.4f;
    private readonly float offset = 1.04f;
    private readonly float threshold = 0.1f;
    private readonly float lowerbound = 0.4f;
    private readonly float upperbound = 1.9f;

    // Update is called once per frame
    void Update()
    {
        float updatedPosition = leftIndexTip.position.x + offset;
        pushManager.Pushing = firstPushed && updatedPosition >= currentXPosition - threshold;

        if (train.transform.position.x < lowerbound)
        {
            train.transform.position = new Vector3 (
                lowerbound, train.transform.position.y, train.transform.position.z
            );
        }
        else if (train.transform.position.x > upperbound)
        {
            Debug.Log("Train reached");
            train.transform.position = new Vector3 (
                lowerbound, train.transform.position.y, train.transform.position.z
            );
            firstPushed = false;
            leftHand.SetActive(true);
            leftHandPosition.SetActive(true);
            pushManager.Pushing = false;
        } 
        else if (firstPushed)
        {
            if (updatedPosition >= currentXPosition)
            {
                train.transform.position = new Vector3 (
                    updatedPosition, train.transform.position.y, train.transform.position.z
                );
                leftHand.SetActive(false);
                leftHandPosition.SetActive(true);
            }
            else if (updatedPosition < currentXPosition - threshold)
            {
                pushManager.Pushing = false;
                leftHand.SetActive(true);
                if (firstPushed)
                    leftHandPosition.SetActive(false);
            }
        }

        currentXPosition = train.transform.position.x;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!firstPushed && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Train collision detected");
            firstPushed = true;
            leftHand.SetActive(false);
            pushManager.Pushing = true;
        }
    }
}
