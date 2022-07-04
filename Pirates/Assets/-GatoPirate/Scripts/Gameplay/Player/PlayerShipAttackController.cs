using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerShipAttackController : MonoBehaviour
{
    [Header("Cannons")]
    [SerializeField]
    private CannonShotController leftCannon;
    [SerializeField]
    private CannonShotController middleCannon;
    [SerializeField]
    private CannonShotController rightCannon;

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
    public int SpecialAttackDamage { get; set; }
    public float SpecialAttackChargeTime { get; set; }

    // Events
    public CannonSideEvent ShootCannonEvent { get; set; }
    public CannonSideFloatEvent StartCoolDownTimerAnimationEvent { get; set; }
    public FloatEvent InitializeSpecialAttackEvent { get; set; }
    public VoidEvent ShootSpecialAttackEvent { get; set; }
    public VoidEvent StartCombatEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private float currentCountDown;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide>.BuildEventHandler(ShootCannonEvent, ShootCannonEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(ShootSpecialAttackEvent, ShootSpecialAttackEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartCombatEvent, StartCombatEventCallback));

        // Basic attack
        middleCannon.SetDamageValue(BasicAttackDamage * ShipLevelAttackMultiplier);
        middleCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        // Normal attack
        leftCannon.SetDamageValue(NormalAttackDamage * ShipLevelAttackMultiplier);
        leftCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        rightCannon.SetDamageValue(NormalAttackDamage * ShipLevelAttackMultiplier);
        rightCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        currentCountDown = NormalAttackCoolDownTime * ShipLevelCoolDownMultiplier;

        // TODO: Automatic attack

        // Special attack
        specialCannon.SetSpecialDamageValue(SpecialAttackDamage * ShipLevelSpecialAttackMultiplier);
        // TODO: Create special attack movement speed if needed
        specialCannon.SetSpecialMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

    }

    private void StartCombatEventCallback(Void _item)
    {
        // Special attack
        InitializeSpecialAttackEvent.Raise(SpecialAttackChargeTime);
    }

    private void ShootCannonEventCallback(CannonSide _side)
    {
        switch (_side)
        {
            case CannonSide.LEFT:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.LEFT, currentCountDown);
                leftCannon.ShootCannonBall();
                break;
            case CannonSide.MIDDLE: // This is basic attack cannon
                middleCannon.ShootCannonBall();
                break;
            case CannonSide.RIGHT:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.RIGHT, currentCountDown);
                rightCannon.ShootCannonBall();
                break;
        }
        HapticController.fallbackPreset = HapticPatterns.PresetType.LightImpact;
        HapticPatterns.PlayEmphasis(0.05f, 0.05f);
    }

    private void ShootSpecialAttackEventCallback(Void _item)
    {
        specialCannon.ShootSpecialAttack();
        HapticController.fallbackPreset = HapticPatterns.PresetType.HeavyImpact;
        HapticPatterns.PlayEmphasis(0.05f, 0.05f);
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
