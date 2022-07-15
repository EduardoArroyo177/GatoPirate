using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IslandScrollController : MonoBehaviour
{
    public float movementSpeed;
    public float dragDelay;

    public Vector3 lastPosition;
    public Vector3 newPosition;

    public Vector3 mousePosition;
    public Vector3 lastMousePosition;
    private bool isDragging;

    private void Start()
    {
        lastPosition = transform.position;
    }

    void OnMouseDown()
    {
        lastMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        //StartCoroutine("CheckIfDragging");

        Debug.Log("MOUSE DOWN");
    }

    private IEnumerator CheckIfDragging()
    {
        yield return new WaitForSeconds(dragDelay);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        //if(isDragging)
            ScrollIsland();
    }

    private void ScrollIsland()
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        if (lastMousePosition != mousePosition)
        {
            lastMousePosition = mousePosition;
            newPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            //Debug.Log("MOUSE DRAGGING");
        }
    }

    private void OnMouseUp()
    {
        //StopCoroutine("CheckIfDragging");
        //isDragging = false;
        //lastPosition = transform.position;
        Debug.Log("MOUSE UP");
    }

    private void Update()
    {
        //if (isDragging)
        //{
        //    transform.position = new Vector3(lastPosition.x + newPosition.x,
        //        transform.position.y,
        //        transform.position.z);
        //}
    }

}
