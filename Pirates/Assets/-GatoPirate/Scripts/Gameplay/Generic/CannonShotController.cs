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

    public CombatShipSoundEvent TriggerShipSoundEvent { get; set; }

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

        Projectile cannonBallHelper = newCannonBall.GetComponent<Projectile>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsShotByEnemy = _isEnemy;
        newCannonBall.SetActive(true);
        TriggerShipSoundEvent.Raise(CombatShipSounds.BASIC_CANNON_SHOT);
        // TODO: Update call with correct particles
        GameObject cannonBallShotParticle = ObjectPooling.Instance.GetBasicProjectileShotParticle();
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
        Projectile cannonBallHelper = newCannonBall.GetComponent<Projectile>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsShotByEnemy = _isEnemy;
        newCannonBall.SetActive(true);
        TriggerShipSoundEvent.Raise(CombatShipSounds.NORMAL_CANNON_SHOT);

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
        Projectile cannonBallHelper = newCannonBall.GetComponent<Projectile>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsShotByEnemy = _isEnemy;
        newCannonBall.SetActive(true);
        TriggerShipSoundEvent.Raise(CombatShipSounds.AUTOMATIC_CANNON_SHOT);

        // TODO: Update call with correct particles
        GameObject cannonBallShotParticle = ObjectPooling.Instance.GetAutomaticProjectileShotParticle();
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
        Projectile cannonBallHelper = newSpecialAttackProjectile.GetComponent<Projectile>();
        cannonBallHelper.SetDamageAndSpeed(damage, movementSpeed);
        cannonBallHelper.IsShotByEnemy = _isEnemy;
        newSpecialAttackProjectile.SetActive(true);
        TriggerShipSoundEvent.Raise(CombatShipSounds.SPECIAL_CANNON_SHOT);

        // TODO: Update call with correct particles
        GameObject cannonBallShotParticle = ObjectPooling.Instance.GetSpecialProjectileShotParticle();
        if (cannonBallShotParticle)
        {
            cannonBallShotParticle.transform.position = transform.position;
            cannonBallShotParticle.SetActive(true);
        }
    }
    #endregion
}
