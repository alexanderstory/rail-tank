using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AimInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private Vector2 position;
    private bool touched;
    private int pointerID;
    private bool doubleTouch;
    private float lastTouch;

    void Awake()
    {
        position = new Vector2(Screen.width / 2, Screen.height / 2);
        touched = false;
        doubleTouch = false;
        lastTouch = 0;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            position = data.position;
            if(Time.time - lastTouch < 0.5)
            {
                doubleTouch = true;
            }
            else
            {
                doubleTouch = false;
            }
            lastTouch = Time.time;
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            position = data.position;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            touched = false;
        }
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public bool Firing()
    {
        return touched;
    }

    public bool GetDoubleTouch()
    {
        if(doubleTouch == true)
        {
            doubleTouch = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
