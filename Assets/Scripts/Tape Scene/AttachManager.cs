using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachManager : MonoBehaviour
{
    [SerializeField]
    private TapeManager tapeManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Holder"))
            tapeManager.Attached = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Holder"))
            tapeManager.Attached = false;
    }
}
