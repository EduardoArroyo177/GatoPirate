using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerShipHealthController : MonoBehaviour
{
    // Properties
    public float ShipLevelHealthMultiplier { get; set; }
    public float ShipHealth { get; set; }
    // TODO: Make property after testing
    public int CurrentHealth;// { get; set; }

    // Events
    public FloatEvent CurrentHealthUIEvent { get; set; }

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
            if ((CurrentHealth - ballDamage) <= 0)
            {
                CurrentHealth = 0;
                // Trigger combat over
            }
            else
                CurrentHealth -= (int)ballDamage;

            // Calculate percentage and send it to UI
            CurrentHealthUIEvent.Raise(CurrentHealth / ShipHealth);
        }
    }
}
