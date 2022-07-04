using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
    public ShipHealthController EnemyShipHealthController { get; set; }
    public float WeakSpotPlayerDamageMultiplier { get; set; }

    private float ballDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            ballDamage = other.GetComponent<Projectile>().BallDamage * WeakSpotPlayerDamageMultiplier;
            CauseDamage();
        }
        else if (other.CompareTag("SpecialAttack"))
        {
            // TODO: Update this with correct script
            ballDamage = other.GetComponent<Projectile>().BallDamage * WeakSpotPlayerDamageMultiplier;
            CauseDamage();
        }
    }

    private void CauseDamage()
    {
        if ((EnemyShipHealthController.CurrentHealth - ballDamage) <= 0)
        {
            EnemyShipHealthController.CurrentHealth = 0;
            EnemyShipHealthController.CombatOver();
        }
        else
            EnemyShipHealthController.CurrentHealth -= (int)ballDamage;

        // Calculate percentage and send it to UI
        EnemyShipHealthController.CurrentHealthUIEvent
            .Raise(EnemyShipHealthController.CurrentHealth / EnemyShipHealthController.ShipHealth);
    }
}
