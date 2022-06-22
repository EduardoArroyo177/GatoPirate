using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameplayBootstrapper : MonoBehaviour
{
    [Header("Ships")]
    [SerializeField]
    private EnemyShipData enemyShipData; // TODO: Someone needs to give this from another place
    [SerializeField]
    private EnemyShipAttackController enemyShipAttackController;

    private void Awake()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        // Controller Properties
        enemyShipAttackController.ShipLevelAttackMultiplier = enemyShipData.ShipLevelAttackMultiplier;
        enemyShipAttackController.ShipLevelBallSpeedMultiplier = enemyShipData.ShipLevelBallSpeedMultiplier;
        enemyShipAttackController.ShipLevelCoolDownMultiplier = enemyShipData.ShipLevelCoolDownMultiplier;
        enemyShipAttackController.ShipLevelSpecialAttackMultiplier = enemyShipData.ShipLevelSpecialAttackMultiplier;

        enemyShipAttackController.CannonBallSpeed = enemyShipData.CannonBallSpeed;
        enemyShipAttackController.CannonBallDamage = enemyShipData.CannonBallDamage;
        enemyShipAttackController.CannonCoolDownTime = enemyShipData.CannonCoolDownTime;
        enemyShipAttackController.BasicAttackDamage = enemyShipData.BasicAttackDamage;
        enemyShipAttackController.BasicAttackCoolDownTime = enemyShipData.BasicAttackCoolDownTime;
        enemyShipAttackController.SpecialAttackDamage = enemyShipData.SpecialAttackDamage;
        enemyShipAttackController.SpecialAttackChargeTime = enemyShipData.SpecialAttackChargeTime;
        enemyShipAttackController.CannonAttackRateMin = enemyShipData.CannonAttackRateMin;
        enemyShipAttackController.CannonAttackRateMax = enemyShipData.CannonAttackRateMax;

        enemyShipAttackController.Initialize();
    }

}
