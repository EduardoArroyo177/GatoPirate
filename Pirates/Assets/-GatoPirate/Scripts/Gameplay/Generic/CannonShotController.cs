using Lofelt.NiceVibrations;
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

    public void ShootCannonBall(bool _isEnemy = false)
    {
        GameObject newCannonBall = ObjectPooling.Instance.GetCannonBall();
        if (!newCannonBall)
            return;
        newCannonBall.transform.rotation = transform.rotation;
        newCannonBall.transform.position = transform.position;
        CannonBall cannonBallHelper = newCannonBall.GetComponent<CannonBall>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsEnemy = _isEnemy;
        newCannonBall.SetActive(true);
        GameObject cannonBallShotParticle = ObjectPooling.Instance.GetCannonBallShotParticle();
        if (cannonBallShotParticle)
        {
            cannonBallShotParticle.transform.position = transform.position;
            cannonBallShotParticle.SetActive(true);
        }
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

    public void ShootSpecialAttack(bool _isEnemy = false)
    {
        GameObject newSpecialAttackProjectile = ObjectPooling.Instance.GetSpecialProjectile();
        newSpecialAttackProjectile.transform.rotation = transform.rotation;
        newSpecialAttackProjectile.transform.position = transform.position;
        // TODO: Update movement speed if needed
        CannonBall cannonBallHelper = newSpecialAttackProjectile.GetComponent<CannonBall>();
        cannonBallHelper.SetDamageAndSpeed(specialDamage, specialMovementSpeed);
        cannonBallHelper.IsEnemy = _isEnemy;
        newSpecialAttackProjectile.SetActive(true);
    }
}
