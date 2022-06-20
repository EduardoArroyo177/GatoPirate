using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerShipAttackController : MonoBehaviour
{
    [SerializeField]
    private CannonShotController leftCannon;
    [SerializeField]
    private CannonShotController middleCannon;
    [SerializeField]
    private CannonShotController rightCannon;

    // Properties
    public float ShipLevelAttackMultiplier { get; set; } //
    public float ShipLevelBallSpeedMultiplier { get; set; } //
    public float ShipLevelCoolDownMultiplier { get; set; } //
    public float ShipLevelSpecialAttackMultiplier { get; set; }
    public int CannonBallSpeed { get; set; } //
    public int CannonBallDamage { get; set; } //
    public float CannonCoolDownTime { get; set; } //
    public int BasicAttackDamage { get; set; }
    public float BasicAttackCoolDownTime { get; set; }
    public int SpecialAttackDamage { get; set; }
    public float SpecialAttackChargeTime { get; set; }

    // Events
    public CannonSideEvent ShootCannonEvent { get; set; }
    public CannonSideFloatEvent StartCoolDownTimerAnimationEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private float currentCountDown;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide>.BuildEventHandler(ShootCannonEvent, ShootCannonEventCallback));

        // Cannon ball
        leftCannon.SetDamageValue(CannonBallDamage * ShipLevelAttackMultiplier);
        leftCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        middleCannon.SetDamageValue(CannonBallDamage * ShipLevelAttackMultiplier);
        middleCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        rightCannon.SetDamageValue(CannonBallDamage * ShipLevelAttackMultiplier);
        rightCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        // Ship
        currentCountDown = CannonCoolDownTime * ShipLevelCoolDownMultiplier;
    }

    private void ShootCannonEventCallback(CannonSide _side)
    {
        switch (_side)
        {
            case CannonSide.LEFT:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.LEFT, currentCountDown);
                leftCannon.ShootCannonBall();
                break;
            case CannonSide.MIDDLE:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.MIDDLE, currentCountDown);
                middleCannon.ShootCannonBall();
                break;
            case CannonSide.RIGHT:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.RIGHT, currentCountDown);
                rightCannon.ShootCannonBall();
                break;
        }
    }
}
