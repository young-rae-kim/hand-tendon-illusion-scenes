using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCloth;
using Unity.Mathematics;
using UnityEngine;

public class ClothManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonOffset;

    [SerializeField]
    private SliceObject sliceObject;

    [SerializeField]
    private GameObject smallClothPrefab;

    private float currentYPosition = 0.888f;
    private readonly float threshold = 0.855f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float updatedYPosition = buttonOffset.transform.position.y;

        if (sliceObject.UpperHull && currentYPosition < threshold) 
        { 
            DestroyHulls();
            Instantiate(smallClothPrefab);

        } 

        currentYPosition = updatedYPosition;
    }

    void DestroyHulls() 
    {
        Destroy(sliceObject.LowerHull);
        Destroy(sliceObject.UpperHull);
        sliceObject.LowerHull = null;
        sliceObject.UpperHull = null;
    }
}
