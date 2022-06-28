using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ShipHealthController : MonoBehaviour
{
    [SerializeField]
    private CharacterType shipType;

    // Properties
    public float ShipLevelHealthMultiplier { get; set; }
    public float ShipHealth { get; set; }
    // TODO: Make property after testing
    public int CurrentHealth;// { get; set; }

    // Events
    public FloatEvent CurrentHealthUIEvent { get; set; }

    private float ballDamage;
    private EnemyResourcesDrop enemyResourcesDrop;

    public void Initialize()
    {
        ShipHealth *= ShipLevelHealthMultiplier;
        CurrentHealth = (int)ShipHealth;
        if (shipType.Equals(CharacterType.ENEMY))
            enemyResourcesDrop = GetComponent<EnemyResourcesDrop>();
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
                Debug.Log("Combat over");
            }
            else
                CurrentHealth -= (int)ballDamage;

            // Calculate percentage and send it to UI
            CurrentHealthUIEvent.Raise(CurrentHealth / ShipHealth);

            if (enemyResourcesDrop)
            {
                enemyResourcesDrop.DropResources();
            }
        }
    }
}
