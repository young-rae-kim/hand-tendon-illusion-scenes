using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PushHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private HoverManager hoverManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        hoverManager.Pushed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        hoverManager.Pushed = false;
    }
}
