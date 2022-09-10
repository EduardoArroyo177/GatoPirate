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
        if ((EnemyShipHealthController.CurrentHealth - projectileDamage) <= 0)
        {
            EnemyShipHealthController.CurrentHealth = 0;
            EnemyShipHealthController.CombatOver();
        }
        else
            EnemyShipHealthController.CurrentHealth -= (int)projectileDamage;

        // Calculate percentage and send it to UI
        EnemyShipHealthController.CurrentHealthUIEvent
            .Raise(EnemyShipHealthController.CurrentHealth / EnemyShipHealthController.ShipHealth);
    }
}
