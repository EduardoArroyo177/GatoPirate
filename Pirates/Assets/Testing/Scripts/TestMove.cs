using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private Touch touch;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * movementSpeed,
                    transform.position.y,
                    transform.position.z);
            }
        }

        transform.position = new Vector3(
            transform.position.x + Input.mousePosition.x * movementSpeed,
            transform.position.y,
            transform.position.z);

        
    }

    private void OnMouseDown()
    {
        
    }
}
