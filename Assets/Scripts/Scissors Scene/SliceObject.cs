using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using EzySlice;
using UnityEngine;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public ScissorsManager scissorsManager;

    private GameObject upperHull;
    private GameObject lowerHull;
    public GameObject UpperHull 
    { 
        get { return upperHull; } 
        set { upperHull = value; }
    }
    public GameObject LowerHull 
    { 
        get { return lowerHull; } 
        set { lowerHull = value; }
    }


    public Material crossSectionMaterial;
    public float cutForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit && scissorsManager.Closed)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        UnityEngine.Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        UnityEngine.Vector3 planeNormal = UnityEngine.Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);

            lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject) 
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
