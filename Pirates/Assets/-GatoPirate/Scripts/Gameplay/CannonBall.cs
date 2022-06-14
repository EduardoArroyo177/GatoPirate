using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    public Vector3 Direction { get; set; }

    void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime * Direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyCannonBall();
    }

    private void DestroyCannonBall()
    {
        Destroy(gameObject);
    }
}
