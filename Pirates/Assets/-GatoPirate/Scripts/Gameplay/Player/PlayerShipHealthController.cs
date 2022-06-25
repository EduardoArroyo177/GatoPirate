using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipHealthController : MonoBehaviour
{
    public float ShipLevelHealthMultiplier { get; set; }
    public float ShipHealth { get; set; }
    // TODO: Make property after testing
    public int CurrentHealth;// { get; set; }

    private float ballDamage;

    public void Initialize()
    {
        ShipHealth *= ShipLevelHealthMultiplier;
        CurrentHealth = (int)ShipHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CannonBall"))
        {
            ballDamage = other.GetComponent<CannonBall>().BallDamage;
            if ((CurrentHealth - ballDamage) < 0)
            {
                CurrentHealth = 0;
                // Trigger combat over
            }
            else
                CurrentHealth -= (int)ballDamage;
        }
    }
}
