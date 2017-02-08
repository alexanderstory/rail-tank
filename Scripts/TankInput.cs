using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TankInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public float smoothing;

    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private Vector2 currentPosition;
    private bool touched;
    private int pointerID;

    public GameObject player;

    void Awake()
    {
        direction = Vector2.zero;
        currentPosition = new Vector2(Screen.width / 2, Screen.height / 14);
        touched = false;
        origin = new Vector2(Screen.width / 2, Screen.height / 14);
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            //origin = data.position;
            origin = Camera.main.WorldToScreenPoint(player.transform.position);
            currentPosition = data.position;
            Debug.Log(origin);
        }

    }

    public void OnDrag(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            currentPosition = data.position;
            CalcDirection();
            
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            direction = Vector2.zero;
            touched = false;
        }
    }

    void CalcDirection()
    {
        origin = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector2 directionRaw = currentPosition - origin;
        direction = directionRaw.normalized;
    }

    public Vector2 GetDirection()
    {
        CalcDirection();
        smoothDirection = Vector2.Lerp(smoothDirection, direction, Time.deltaTime * smoothing);
        return smoothDirection;
    }
}
