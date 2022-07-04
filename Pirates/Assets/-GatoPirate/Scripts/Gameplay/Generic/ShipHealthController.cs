using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ShipHealthController : MonoBehaviour
{
    [SerializeField]
    private CharacterType shipType;
    [SerializeField]
    private float _cameraShakeDuration;

    // Properties
    public float ShipLevelHealthMultiplier { get; set; }
    public float ShipHealth { get; set; }
    // TODO: Make property after testing
    public int CurrentHealth;// { get; set; }

    // Events
    public FloatEvent CurrentHealthUIEvent { get; set; }
    public FloatEvent TriggerShakingCameraEvent { get; set; }
    public VoidEvent StopCombatEvent { get; set; }
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }

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
        if (other.CompareTag("Projectile"))
        {
            ballDamage = other.GetComponent<Projectile>().BallDamage;
            CauseDamage();
        }
        else if (other.CompareTag("SpecialAttack"))
        {
            // TODO: Get correct component based on whatever projectile special attack is
            ballDamage = other.GetComponent<Projectile>().BallDamage;
            CauseDamage();
            if (!enemyResourcesDrop)
            {
                TriggerShakingCameraEvent.Raise(_cameraShakeDuration);
                VibrationController.Instance.TriggerReceiveSpecialAttackVibration(_cameraShakeDuration);
            }
        }
    }

    private void CauseDamage()
    {
        if ((CurrentHealth - ballDamage) <= 0)
        {
            CurrentHealth = 0;
            CombatOver();
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

    public void CombatOver()
    {
        Debug.Log("Combat over");
        StopCombatEvent.Raise();
        // TODO: Trigger destroy animation
        // DestroyAnim();
        // TODO: This should be called after previous anim is completed
        if (shipType.Equals(CharacterType.ENEMY))
            ShowResultScreenEvent.Raise(CharacterType.PLAYER);
        else
            ShowResultScreenEvent.Raise(CharacterType.ENEMY);
    }
}
