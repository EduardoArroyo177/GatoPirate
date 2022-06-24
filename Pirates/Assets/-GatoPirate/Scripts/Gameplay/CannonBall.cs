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
    [SerializeField]
    private Transform cannonBallSprite;

    public Vector3 Direction { get; set; }
    public bool IsEnemy;// { get; set; }

    private Vector3 currentRotationEuler;

    private void OnEnable()
    {
        if (IsEnemy)
            cannonBallSprite.localEulerAngles = new Vector3(-30, 0, currentRotationEuler.z);
        else
            cannonBallSprite.localEulerAngles = new Vector3(30, currentRotationEuler.y, currentRotationEuler.z);
       
        Invoke("DestroyCannonBall", destroyTime);
    }

    private void Awake()
    {
        currentRotationEuler = cannonBallSprite.eulerAngles;
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

    public void SetDamage(float _damage)
    {
        ballDamage = _damage;
    }
}
