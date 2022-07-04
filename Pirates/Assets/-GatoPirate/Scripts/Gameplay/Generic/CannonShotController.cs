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

    public void SetDamageValue(float _damage)
    {
        damage = _damage;
    }

    public void SetMovementSpeedValue(float _speed)
    {
        movementSpeed = _speed;
    }

    #region Basic attack
    public void ShootBasicProjectile(bool _isEnemy = false)
    {
        GameObject newCannonBall = ObjectPooling.Instance.GetBasicProjectile();
        if (!newCannonBall)
            return;
        newCannonBall.transform.rotation = transform.rotation;
        newCannonBall.transform.position = transform.position;

        CannonBall cannonBallHelper = newCannonBall.GetComponent<CannonBall>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsShotByEnemy = _isEnemy;
        newCannonBall.SetActive(true);

        // TODO: Update call with correct particles
        GameObject cannonBallShotParticle = ObjectPooling.Instance.GetNormalProjectileShotParticle();
        if (cannonBallShotParticle)
        {
            cannonBallShotParticle.transform.position = transform.position;
            cannonBallShotParticle.SetActive(true);
        }
    }
    #endregion

    #region Normal attack
    public void ShootNormalProjectile(bool _isEnemy = false)
    {
        GameObject newCannonBall = ObjectPooling.Instance.GetNormalProjectile();
        if (!newCannonBall)
            return;
        newCannonBall.transform.rotation = transform.rotation;
        newCannonBall.transform.position = transform.position;
        CannonBall cannonBallHelper = newCannonBall.GetComponent<CannonBall>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsShotByEnemy = _isEnemy;
        newCannonBall.SetActive(true);
        GameObject cannonBallShotParticle = ObjectPooling.Instance.GetNormalProjectileShotParticle();
        if (cannonBallShotParticle)
        {
            cannonBallShotParticle.transform.position = transform.position;
            cannonBallShotParticle.SetActive(true);
        }
    }
    #endregion

    #region Automatic attack
    public void ShootAutomaticProjectile(bool _isEnemy = false)
    {
        GameObject newCannonBall = ObjectPooling.Instance.GetAutomaticProjectile();
        if (!newCannonBall)
            return;
        newCannonBall.transform.rotation = transform.rotation;
        newCannonBall.transform.position = transform.position;
        CannonBall cannonBallHelper = newCannonBall.GetComponent<CannonBall>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsShotByEnemy = _isEnemy;
        newCannonBall.SetActive(true);
        // TODO: Update call with correct particles
        GameObject cannonBallShotParticle = ObjectPooling.Instance.GetNormalProjectileShotParticle();
        if (cannonBallShotParticle)
        {
            cannonBallShotParticle.transform.position = transform.position;
            cannonBallShotParticle.SetActive(true);
        }
    }
    #endregion

    #region Special attack
    public void ShootSpecialAttack(bool _isEnemy = false)
    {
        GameObject newSpecialAttackProjectile = ObjectPooling.Instance.GetSpecialProjectile();
        if (!newSpecialAttackProjectile)
            return;
        newSpecialAttackProjectile.transform.rotation = transform.rotation;
        newSpecialAttackProjectile.transform.position = transform.position;
        // TODO: Update movement speed if needed
        CannonBall cannonBallHelper = newSpecialAttackProjectile.GetComponent<CannonBall>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsShotByEnemy = _isEnemy;
        newSpecialAttackProjectile.SetActive(true);
        // TODO: Update call with correct particles
        GameObject cannonBallShotParticle = ObjectPooling.Instance.GetNormalProjectileShotParticle();
        if (cannonBallShotParticle)
        {
            cannonBallShotParticle.transform.position = transform.position;
            cannonBallShotParticle.SetActive(true);
        }
    }
    #endregion
}
