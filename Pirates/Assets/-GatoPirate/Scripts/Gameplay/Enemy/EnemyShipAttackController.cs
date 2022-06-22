using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAttackController : MonoBehaviour
{
    [Header("Cannons")]
    [SerializeField]
    private CannonShotController leftCannon;
    [SerializeField]
    private CannonShotController middleCannon;
    [SerializeField]
    private CannonShotController rightCannon;

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
        float randomCannon = Random.Range(0, 3);
        switch (Random.Range(0, 3))
        {
            case 0:
                leftCannon.ShootCannonBall();
                break;
            case 1:
                middleCannon.ShootCannonBall();
                break;
            case 2:
                rightCannon.ShootCannonBall();
                break;
        }
    }
}
