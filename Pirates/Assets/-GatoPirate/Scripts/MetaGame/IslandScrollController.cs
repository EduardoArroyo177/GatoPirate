using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IslandScrollController : MonoBehaviour
{
    public float mousePositionDistance;

    //public Vector3 lastPosition;
    //public Vector3 mouseWorldPointPosition;

    //public Vector3 mousePosition;
    //public Vector3 lastMousePosition;

    //public Vector3 normalizedPosition;
    //private bool isDragging;

    public Vector3 screenPoint;
    public Vector3 offset;
    // private Vector3 originalPosition;
    public Vector3 cursorPoint;
    public Vector3 cursorPosition;

    private void Start()
    {
        //originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        //lastMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mousePositionDistance);
        // posicion init = posición actual
        // parent = parent actual
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

    void OnMouseDrag()
    {
        ScrollIsland();
    }

    private void ScrollIsland()
    {
        cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition; //new Vector3(cursorPosition.x, cursorPosition.y, transform.position.z);

        //mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mousePositionDistance);

        //if (lastMousePosition != mousePosition)
        //{
        //    isDragging = true;
        //    mouseWorldPointPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //    transform.position = mouseWorldPointPosition;
            
            //normalizedPosition = mouseWorldPointPosition.normalized;
            //transform.position = new Vector3(lastPosition.x + mouseWorldPointPosition.x,
            //transform.position.y,
            //transform.position.z);
        //}
    }

    private void OnMouseUp()
    {
        //lastMousePosition = mousePosition;
        //isDragging = false;
        //lastPosition = transform.position;
    }

    private void Update()
    {
        //if (isDragging)
        //{
        //    //transform.position = new Vector3(mouseWorldPointPosition.x,
        //    //transform.position.y,
        //    //transform.position.z);
        //}
    }

}
