using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverToggle : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool Toggled = false;

    public void OnPointerDown(PointerEventData data)
    {
            Toggled = true;           
    }

    public void OnPointerUp(PointerEventData data)
    {
        Toggled = false;
    }
}
