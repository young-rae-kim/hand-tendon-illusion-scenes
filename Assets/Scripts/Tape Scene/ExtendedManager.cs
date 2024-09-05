using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedManager : MonoBehaviour
{
    [SerializeField]
    private TapeManager tapeManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Indicator"))
            tapeManager.ToggleHolding();
    }
}
