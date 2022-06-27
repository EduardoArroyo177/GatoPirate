using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyGameplayBootstrapper : MonoBehaviour
{
    [Header("Ships")]
    [SerializeField]
    private EnemyShipData enemyShipData; // TODO: Someone needs to give this from another place
    [SerializeField]
    private EnemyShipAttackController enemyShipAttackController;
    [SerializeField]
    private ShipHealthController enemyShipHealthController;
    [SerializeField]
    private EnemyShipWeakSpotController enemyShipWeakSpotController;

    [Header("UI")]
    [SerializeField]
    private ShipHealthUIController enemyShipHealthUIController;

    [Header("Health events")]
    [SerializeField]
    private FloatEvent CurrentEnemyHealthUIEvent;

    public VoidEvent StartCombatEvent { get; set; }

    public void InitializeBootstrapper()
    {
        InitializeEnemy();
    }

    //private void Awake()
    //{
    //    InitializeEnemy();
    //}

    private void InitializeEnemy()
    {
        // UI Init 
        enemyShipHealthUIController.CurrentHealthUIEvent = CurrentEnemyHealthUIEvent;
        enemyShipHealthUIController.Initialize();

        // Enemy ship attack controller
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

        // Events
        enemyShipAttackController.StartCombatEvent = StartCombatEvent;

        // TODO: Initialize this after x amount of time or from a button or whatever
        enemyShipAttackController.Initialize();

        // Enemy ship health controller
        enemyShipHealthController.ShipHealth = enemyShipData.ShipHealth;
        enemyShipHealthController.ShipLevelHealthMultiplier = enemyShipData.ShipLevelHealthMultiplier;

        // Events
        enemyShipHealthController.CurrentHealthUIEvent = CurrentEnemyHealthUIEvent;
        enemyShipHealthController.Initialize();

        // Enemy ship weak spot controller
        enemyShipWeakSpotController.WeakSpotAppearanceRateMin = enemyShipData.WeakSpotAppearanceRateMin;
        enemyShipWeakSpotController.WeakSpotAppearanceRateMax = enemyShipData.WeakSpotAppearanceRateMax;
        enemyShipWeakSpotController.WeakSpotCoolDownTime = enemyShipData.WeakSpotCoolDownTime;
        enemyShipWeakSpotController.WeakSpotPlayerDamageMultiplier = enemyShipData.WeakSpotPlayerDamageMultiplier;
        enemyShipWeakSpotController.EnemyShipHealthController = enemyShipHealthController;
        // TODO: Update this with value that comes from main menu
        enemyShipWeakSpotController.PlayerNumberOfCannons = 3;
        // Events
        enemyShipWeakSpotController.StartCombatEvent = StartCombatEvent;
        // TODO: Initialize this after x amount of time or from a button or whatever
        enemyShipWeakSpotController.Initialize();
    }
}


