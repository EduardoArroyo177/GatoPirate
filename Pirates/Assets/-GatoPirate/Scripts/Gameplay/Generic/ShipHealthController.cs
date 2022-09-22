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
    public VoidEvent ResumeCombatEvent { get; set; }
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public VoidEvent TriggerEnemyLostAnimationEvent { get; set; }
    public VoidEvent TriggerPlayerLostAnimationEvent { get; set; }
    // Ad events
    public VoidEvent ReviveSuccessEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    private float projectileDamage;
    private ProjectileType projectileType;
    private EnemyResourcesDrop enemyResourcesDrop;

    public void Initialize()
    {
        ShipHealth *= ShipLevelHealthMultiplier;
        CurrentHealth = (int)ShipHealth;
        if (shipType.Equals(CharacterType.ENEMY))
            enemyResourcesDrop = GetComponent<EnemyResourcesDrop>();

        if(ReviveSuccessEvent)
            _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(ReviveSuccessEvent, ReviveSuccessEventCallback));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            projectileDamage = other.GetComponent<Projectile>().ProjectileDamage;
            projectileType = other.GetComponent<Projectile>().ProjectileType;
            CauseDamage();
        }
        else if (other.CompareTag("SpecialAttack"))
        {
            // TODO: Get correct component based on whatever projectile special attack is
            projectileDamage = other.GetComponent<Projectile>().ProjectileDamage;
            projectileType = other.GetComponent<Projectile>().ProjectileType;
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
        if ((CurrentHealth - projectileDamage) <= 0)
        {
            CurrentHealth = 0;
            CombatOver();
        }
        else
            CurrentHealth -= (int)projectileDamage;

        // Calculate percentage and send it to UI
        CurrentHealthUIEvent.Raise(CurrentHealth / ShipHealth);

        if (enemyResourcesDrop)
        {
            if (projectileType.Equals(ProjectileType.BASIC))
                enemyResourcesDrop.DropBasicResources();
            else if(!projectileType.Equals(ProjectileType.AUTOMATIC))
                enemyResourcesDrop.DropNormalResources();
        }
    }

    public void CombatOver()
    {
        StopCombatEvent.Raise();
        if (shipType.Equals(CharacterType.ENEMY))
        {
            TriggerEnemyLostAnimationEvent.Raise();
        }
        else
            TriggerPlayerLostAnimationEvent.Raise();
    }

    #region Event callbacks
    private void ReviveSuccessEventCallback(Void _item)
    {
        if (shipType.Equals(CharacterType.PLAYER))
        {
            CurrentHealth = (int)ShipHealth;
            CurrentHealthUIEvent.Raise(CurrentHealth / ShipHealth);
            // TODO: Trigger resume combat event
            ResumeCombatEvent.Raise();
        }
    }
    #endregion

    #region On Destroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }

        _eventHandlers.Clear();
    }
    #endregion
}
