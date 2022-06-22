using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{
    // TODO: Make this private after testing
    public float damage;
    public float movementSpeed;
    public float specialDamage;
    public float specialMovementSpeed;
    public bool isShooting;

    // Normal cannons
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
        if (!newCannonBall)
            return;
        newCannonBall.transform.rotation = transform.rotation;
        newCannonBall.transform.position = transform.position;
        newCannonBall.GetComponent<CannonBall>().SetDamageAndSpeed(damage, movementSpeed);
        newCannonBall.SetActive(true);
    }

    // Special attack
    public void SetSpecialDamageValue(float _damage)
    {
        specialDamage = _damage;
    }

    public void SetSpecialMovementSpeedValue(float _speed)
    {
        specialMovementSpeed = _speed;
    }

    public void ShootSpecialAttack()
    {
        GameObject newSpecialAttackProjectile = ObjectPooling.Instance.GetSpecialProjectile();
        newSpecialAttackProjectile.transform.rotation = transform.rotation;
        newSpecialAttackProjectile.transform.position = transform.position;
        // TODO: Update movement speed if needed
        newSpecialAttackProjectile.GetComponent<CannonBall>().SetDamageAndSpeed(specialDamage, specialMovementSpeed);
        newSpecialAttackProjectile.SetActive(true);
    }
}
