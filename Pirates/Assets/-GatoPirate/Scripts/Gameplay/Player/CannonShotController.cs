using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{
    [SerializeField]
    private CannonSide cannonSide;
    [SerializeField]
    private GameObject cannonBall;
    // TODO: Make this private after testing
    public float damage;
    public float movementSpeed;


    public void SetDamageValue(float _damage)
    {
        damage = _damage;
    }

    public void SetMovementSpeedValue(float _speed)
    {
        movementSpeed = _speed;
    }

    public void ShootCannonBall()
    {
        GameObject newCannonBall = ObjectPooling.Instance.GetCannonBall();
        newCannonBall.transform.rotation = transform.rotation;
        newCannonBall.transform.position = transform.position;
        newCannonBall.GetComponent<CannonBall>().SetDamageAndSpeed(damage, movementSpeed);
        newCannonBall.SetActive(true);
    }
}
