using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerShipAttackController : MonoBehaviour
{
    [Header("Basic cannon")]
    [SerializeField]
    private CannonShotController middleCannon;

    [Header("Normal cannons")]
    [SerializeField]
    private CannonShotController leftCannon;
    [SerializeField]
    private CannonShotController rightCannon;

    [Header("Automatic cannons")]
    [SerializeField]
    private CannonShotController automaticCannonLeft;
    [SerializeField]
    private CannonShotController automaticCannonRight;

    [Header("Special cannon")]
    [SerializeField]
    private CannonShotController specialCannon;

    // Properties
    public float ShipLevelAttackMultiplier { get; set; } //
    public float ShipLevelBallSpeedMultiplier { get; set; } //
    public float ShipLevelCoolDownMultiplier { get; set; } //
    public float ShipLevelSpecialAttackMultiplier { get; set; }
    public int CannonBallSpeed { get; set; } //
    public int BasicAttackDamage { get; set; }
    public int NormalAttackDamage { get; set; } //
    public float NormalAttackCoolDownTime { get; set; } //
    public int AutomaticAttackDamage { get; set; }
    public float AutomaticAttackCoolDownTime { get; set; }
    public float AutomaticAttackFireRate { get; set; }
    public int SpecialAttackDamage { get; set; }
    public float SpecialAttackChargeTime { get; set; }

    // Events
    public CannonSideEvent ShootCannonEvent { get; set; }
    public CannonSideFloatEvent StartCoolDownTimerAnimationEvent { get; set; }
    public FloatEvent InitializeSpecialAttackEvent { get; set; }
    public VoidEvent ShootSpecialAttackEvent { get; set; }
    public VoidEvent StartCombatEvent { get; set; }
    public VoidEvent StopCombatEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();
    private float currentCountDown;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide>.BuildEventHandler(ShootCannonEvent, ShootCannonEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(ShootSpecialAttackEvent, ShootSpecialAttackEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartCombatEvent, StartCombatEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StopCombatEvent, StopCombatEventCallback));

        // Basic attack
        middleCannon.SetDamageValue(BasicAttackDamage * ShipLevelAttackMultiplier);
        middleCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        // Normal attack
        leftCannon.SetDamageValue(NormalAttackDamage * ShipLevelAttackMultiplier);
        leftCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        rightCannon.SetDamageValue(NormalAttackDamage * ShipLevelAttackMultiplier);
        rightCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        currentCountDown = NormalAttackCoolDownTime * ShipLevelCoolDownMultiplier;

        // Automatic attack
        automaticCannonLeft.SetDamageValue(AutomaticAttackDamage * ShipLevelAttackMultiplier);
        // TODO: Create special attack movement speed if needed
        automaticCannonLeft.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        automaticCannonRight.SetDamageValue(AutomaticAttackDamage * ShipLevelAttackMultiplier);
        // TODO: Create special attack movement speed if needed
        automaticCannonRight.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        // Special attack
        specialCannon.SetDamageValue(SpecialAttackDamage * ShipLevelSpecialAttackMultiplier);
        // TODO: Create special attack movement speed if needed
        specialCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

    }

    private void StartCombatEventCallback(Void _item)
    {
        // Special attack
        InitializeSpecialAttackEvent.Raise(SpecialAttackChargeTime);
        StartCoroutine(AutomaticAttack());
    }

    private void StopCombatEventCallback(Void _item)
    {
        Debug.Log("STOPPING COMBAT");
        StopAllCoroutines();
    }

    private void ShootCannonEventCallback(CannonSide _side)
    {
        switch (_side)
        {
            case CannonSide.LEFT:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.LEFT, currentCountDown);
                leftCannon.ShootNormalProjectile();
                VibrationController.Instance.TriggerNormalAttackVibration();
                break;
            case CannonSide.MIDDLE: // This is basic attack cannon
                middleCannon.ShootBasicProjectile();
                VibrationController.Instance.TriggerBasicAttackVibration();
                break;
            case CannonSide.RIGHT:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.RIGHT, currentCountDown);
                rightCannon.ShootNormalProjectile();
                VibrationController.Instance.TriggerNormalAttackVibration();
                break;
        }
        
    }

    private void ShootSpecialAttackEventCallback(Void _item)
    {
        specialCannon.ShootSpecialAttack();
        VibrationController.Instance.TriggerSpecialAttackVibration();
    }

    private IEnumerator AutomaticAttack()
    {
        // Automatic attack starts after 3 seconds of play

        yield return new WaitForSeconds(3.0f);
        while (true)
        {
            yield return new WaitForSeconds(AutomaticAttackCoolDownTime);
            automaticCannonLeft.ShootAutomaticProjectile();
            yield return new WaitForSeconds(AutomaticAttackFireRate);
            automaticCannonRight.ShootAutomaticProjectile();
        }
    }

    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
}
