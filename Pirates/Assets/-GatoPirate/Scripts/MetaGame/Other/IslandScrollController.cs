using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IslandScrollController : MonoBehaviour
{
    // TODO: For testing only, make private when release
    public Vector3 offset;
    public float objectDepth;

    void OnMouseDown()
    {
        objectDepth = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = objectDepth;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorldPoint = GetMouseAsWorldPoint();
        transform.position = new Vector3(mouseWorldPoint.x + offset.x, transform.position.y, transform.position.z);
    }
}
