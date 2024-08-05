using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public TextMeshProUGUI inChangeText;
    public bool isVertical;
    public bool InChange 
    {
        get { return inChange; }
        set { inChange = value; }
    }

    private bool inChange = false;
    private float value;

    void Start()
    {
        value = gameObject.GetComponent<Slider>().value;
    }

    void Update()
    {
        float updatedValue = gameObject.GetComponent<Slider>().value;
        inChange = updatedValue != value;

        inChangeText.text = (isVertical ? "Vertical in change: " : "Horizontal in change: ") + inChange.ToString();
        value = updatedValue;
    }
}
