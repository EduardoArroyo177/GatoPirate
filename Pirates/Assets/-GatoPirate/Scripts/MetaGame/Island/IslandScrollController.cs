using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IslandScrollController : MonoBehaviour
{
    [SerializeField]
    private float scrollLimit;

    private Vector3 offset;
    private float objectDepth;

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
        float valueClamp = Mathf.Clamp(mouseWorldPoint.x + offset.x, -scrollLimit, scrollLimit);
        transform.position = new Vector3(valueClamp, transform.position.y, transform.position.z);
    }
}
