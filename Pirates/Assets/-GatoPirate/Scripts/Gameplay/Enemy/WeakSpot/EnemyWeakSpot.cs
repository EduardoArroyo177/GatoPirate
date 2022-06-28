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
        if (other.CompareTag("CannonBall"))
        {
            ballDamage = other.GetComponent<CannonBall>().BallDamage * WeakSpotPlayerDamageMultiplier;
            if ((EnemyShipHealthController.CurrentHealth - ballDamage) <= 0)
            {
                EnemyShipHealthController.CurrentHealth = 0;
                // Trigger combat over
            }
            else
                EnemyShipHealthController.CurrentHealth -= (int)ballDamage;

            // Calculate percentage and send it to UI
            EnemyShipHealthController.CurrentHealthUIEvent
                .Raise(EnemyShipHealthController.CurrentHealth / EnemyShipHealthController.ShipHealth);
        }
    }
}
