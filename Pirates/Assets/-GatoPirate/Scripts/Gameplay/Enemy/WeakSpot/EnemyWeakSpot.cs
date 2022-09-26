using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
    public ShipHealthController EnemyShipHealthController { get; set; }
    public float WeakSpotPlayerDamageMultiplier { get; set; }
    public CombatSoundEvent TriggerCombatSoundEvent { get; set; }


    private float projectileDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            projectileDamage = other.GetComponent<Projectile>().ProjectileDamage * WeakSpotPlayerDamageMultiplier;
            CauseDamage();
        }
        else if (other.CompareTag("SpecialAttack"))
        {
            // TODO: Update this with correct script
            projectileDamage = other.GetComponent<Projectile>().ProjectileDamage * WeakSpotPlayerDamageMultiplier;
            CauseDamage();
        }
    }

    private void CauseDamage()
    {
        // Trigger weak spot hit sound
        TriggerCombatSoundEvent.Raise(CombatSounds.WEAK_SPOT_HIT);
        // Trigger weak spot hit particles
        GameObject particles = ObjectPooling.Instance.GetWeakSpotHitParticles();
        if (particles)
        {
            particles.transform.position = transform.position;
            particles.SetActive(true);
        }

        if ((EnemyShipHealthController.CurrentHealth - projectileDamage) <= 0)
        {
            EnemyShipHealthController.CurrentHealth = 0;
            EnemyShipHealthController.CombatOver();
        }
        else
            EnemyShipHealthController.CurrentHealth -= (int)projectileDamage;

        GameObject damageTextHelper = ObjectPooling.Instance.GetEnemyDamageTextParticle();
        
        damageTextHelper.transform.position = transform.position;
        damageTextHelper.GetComponent<DamageTextParticleController>().ShowTextParticle(ProjectileType.SPECIAL, (int)projectileDamage, true);


        // Calculate percentage and send it to UI
        EnemyShipHealthController.CurrentHealthUIEvent
            .Raise(EnemyShipHealthController.CurrentHealth / EnemyShipHealthController.ShipHealth);
    }
}
