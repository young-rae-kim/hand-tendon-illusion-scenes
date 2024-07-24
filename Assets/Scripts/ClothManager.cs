using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCloth;
using Unity.Mathematics;
using UnityEngine;

public class ClothManager : MonoBehaviour
{
    public UCCloth uCCloth;
    public Transform handTransform;

    private UCInternalSimData simData;
    private bool isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        simData = uCCloth.simData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Left Hand:" + other.transform.position);
        float3 currentPosition = other.transform.position;
        await Task.Run(() => GrabCloth(currentPosition));
    }

    private async void GrabCloth(float3 position) 
    { 
        if (!isGrabbing) 
        {
            isGrabbing = true;
            Debug.Log("Grab Start");
            UCPointQueryData currentPosition = new(position);
            Debug.Log(currentPosition.position);
            List<ushort> closestPoints = await uCCloth.QueryClosestPoints(currentPosition);
            Debug.Log(closestPoints[0]);
            Debug.Log("Grab End");
            isGrabbing = false;
        }
    }
}
