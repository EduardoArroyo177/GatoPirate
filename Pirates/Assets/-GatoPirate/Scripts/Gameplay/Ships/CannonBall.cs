using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float ballDamage;
    [SerializeField]
    private float destroyTime;

    public Vector3 Direction { get; set; }

    private void OnEnable()
    {
        Invoke("DestroyCannonBall", destroyTime);
    }

    void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyCannonBall();
    }

    private void DestroyCannonBall()
    {
        CancelInvoke("DestroyCannonBall");
        gameObject.SetActive(false);
    }

    public void SetDamageAndSpeed(float _damage, float _speed)
    {
        ballDamage = _damage;
        movementSpeed = _speed;
    }
}
