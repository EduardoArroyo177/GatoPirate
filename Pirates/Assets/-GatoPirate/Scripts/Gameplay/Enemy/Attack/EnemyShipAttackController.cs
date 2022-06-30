using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyShipAttackController : MonoBehaviour
{
    [Header("Cannons")]
    [SerializeField]
    private CannonShotController leftCannon;
    [SerializeField]
    private EnemyCannonShooting leftCannonShooting;
    [SerializeField]
    private CannonShotController middleCannon;
    [SerializeField]
    private EnemyCannonShooting middleCannonShooting;
    [SerializeField]
    private CannonShotController rightCannon;
    [SerializeField]
    private EnemyCannonShooting rightCannonShooting;

    [Header ("Special cannon")]
    [SerializeField]
    private CannonShotController specialCannon;
    [SerializeField]
    private EnemySpecialAttackShooting specialCannonShooting;

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
    public float CannonAttackRateMin { get; set; }
    public float CannonAttackRateMax { get; set; }

    // Events
    public VoidEvent StartCombatEvent { get; set; }

    // Other properties
    public int NumberOfActiveCannons { get; set; }

    private float currentCountDown;

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartCombatEvent, StartCombatEventCallback));

        // Cannon ball
        leftCannon.SetDamageValue(CannonBallDamage * ShipLevelAttackMultiplier);
        leftCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        middleCannon.SetDamageValue(CannonBallDamage * ShipLevelAttackMultiplier);
        middleCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        rightCannon.SetDamageValue(CannonBallDamage * ShipLevelAttackMultiplier);
        rightCannon.SetMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        // Special attack
        specialCannon.SetSpecialDamageValue(SpecialAttackDamage * ShipLevelSpecialAttackMultiplier);
        // TODO: Create special attack movement speed if needed
        specialCannon.SetSpecialMovementSpeedValue(CannonBallSpeed * ShipLevelBallSpeedMultiplier);

        // Ship
        currentCountDown = CannonCoolDownTime * ShipLevelCoolDownMultiplier;
    }

    public void StartCombatEventCallback(Void _item)
    {
        StartCoroutine("AutomaticAttack");
        specialCannonShooting.EnemyShipAtkController = this;

        if(NumberOfActiveCannons == 4)
            specialCannonShooting.StartCoolDownTimer(SpecialAttackChargeTime);
    }

    
    private IEnumerator AutomaticAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(CannonAttackRateMin, CannonAttackRateMax));
            // Shoot
            ShootCannon();
        }
    }

    private void ShootCannon()
    {
        switch (NumberOfActiveCannons)
        {
            case 1:
                ShootWithOneCannon();
                break;
            case 2:
                ShootWithTwoCannons();
                break;
            case 3:
                ShootWithThreeCannons();
                break;
            case 4:
                ShootWithThreeCannons();
                break;
        }
    }

    private void ShootWithThreeCannons()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                if (!leftCannonShooting.IsShooting)
                {
                    leftCannon.ShootCannonBall(true);
                    leftCannonShooting.StartCoolDownTimer(CannonCoolDownTime);
                }
                else
                    ShootWithThreeCannons();
                break;
            case 1:
                if (!middleCannonShooting.IsShooting)
                {
                    middleCannon.ShootCannonBall(true);
                    middleCannonShooting.StartCoolDownTimer(CannonCoolDownTime);
                }
                else
                    ShootWithThreeCannons();
                break;
            case 2:
                if (!rightCannonShooting.IsShooting)
                {
                    rightCannon.ShootCannonBall(true);
                    rightCannonShooting.StartCoolDownTimer(CannonCoolDownTime);
                }
                else
                    ShootWithThreeCannons();
                break;
        }
    }

    private void ShootWithTwoCannons()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                if (!leftCannonShooting.IsShooting)
                {
                    leftCannon.ShootCannonBall(true);
                    leftCannonShooting.StartCoolDownTimer(CannonCoolDownTime);
                }
                else
                    ShootWithTwoCannons();
                break;
            case 1:
                if (!rightCannonShooting.IsShooting)
                {
                    rightCannon.ShootCannonBall(true);
                    rightCannonShooting.StartCoolDownTimer(CannonCoolDownTime);
                }
                else
                    ShootWithThreeCannons();
                break;
        }
    }

    private void ShootWithOneCannon()
    {
        if (!middleCannonShooting.IsShooting)
        {
            middleCannon.ShootCannonBall(true);
            middleCannonShooting.StartCoolDownTimer(CannonCoolDownTime);
        }
    }

    public void ShootSpecialAttack()
    {
        specialCannon.ShootSpecialAttack(true);
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
