using System.Collections;
using System.Collections.Generic;
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

    private float currentCountDown;

    public void Initialize()
    {

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

        // TODO: Move this to a StartCombatEvent
        StartCoroutine("AutomaticAttack");

    }

    
    private IEnumerator AutomaticAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(CannonAttackRateMin, CannonAttackRateMax));
            // Shoot
            ShootRandomCannon();
        }
    }

    private void ShootRandomCannon()
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
                    ShootRandomCannon();
                break;
            case 1:
                if (!middleCannonShooting.IsShooting)
                {
                    middleCannon.ShootCannonBall(true);
                    middleCannonShooting.StartCoolDownTimer(CannonCoolDownTime);
                }
                else
                    ShootRandomCannon();
                break;
            case 2:
                if (!rightCannonShooting.IsShooting)
                {
                    rightCannon.ShootCannonBall(true);
                    rightCannonShooting.StartCoolDownTimer(CannonCoolDownTime);
                }
                else
                    ShootRandomCannon();
                break;
        }
    }
}
