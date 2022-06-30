using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyGameplayBootstrapper : MonoBehaviour
{
    [Header("Ship data")]
    [SerializeField]
    private EnemyShipData enemyShipData; // TODO: Someone needs to give this from another place
    [SerializeField]
    private EnemyShipAttackController enemyShipAttackController;
    [SerializeField]
    private ShipHealthController enemyShipHealthController;
    [SerializeField]
    private EnemyShipWeakSpotController enemyShipWeakSpotController;
    [SerializeField]
    private EnemyResourcesDrop enemyResourcesDrop;

    [Header("UI")]
    [SerializeField]
    private ShipHealthUIController enemyShipHealthUIController;
    [SerializeField]
    private EnemyResourcesDropUIController enemyResourcesDropUIController;

    [Header("Health events")]
    [SerializeField]
    private FloatEvent CurrentEnemyHealthUIEvent;

    [Header("Resources drop events")]
    [SerializeField]
    private IntEvent GoldResourcesDroppedEvent;
    [SerializeField]
    private IntEvent WoodResourcesDroppedEvent;

    // Events
    public VoidEvent StartCombatEvent { get; set; }
    // Properties
    public EnemyShipData EnemyShipData { get => enemyShipData; set => enemyShipData = value; }
    public int NumberOfActiveCannons { get; set; }

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

        enemyResourcesDropUIController.GoldResourcesDroppedEvent = GoldResourcesDroppedEvent;
        enemyResourcesDropUIController.WoodResourcesDroppedEvent = WoodResourcesDroppedEvent;
        enemyResourcesDropUIController.Initialize();

        // Enemy ship attack controller
        // Controller Properties
        enemyShipAttackController.ShipLevelAttackMultiplier = EnemyShipData.ShipLevelAttackMultiplier;
        enemyShipAttackController.ShipLevelBallSpeedMultiplier = EnemyShipData.ShipLevelBallSpeedMultiplier;
        enemyShipAttackController.ShipLevelCoolDownMultiplier = EnemyShipData.ShipLevelCoolDownMultiplier;
        enemyShipAttackController.ShipLevelSpecialAttackMultiplier = EnemyShipData.ShipLevelSpecialAttackMultiplier;

        enemyShipAttackController.CannonBallSpeed = EnemyShipData.CannonBallSpeed;
        enemyShipAttackController.CannonBallDamage = EnemyShipData.CannonBallDamage;
        enemyShipAttackController.CannonCoolDownTime = EnemyShipData.CannonCoolDownTime;
        enemyShipAttackController.BasicAttackDamage = EnemyShipData.BasicAttackDamage;
        enemyShipAttackController.BasicAttackCoolDownTime = EnemyShipData.BasicAttackCoolDownTime;
        enemyShipAttackController.SpecialAttackDamage = EnemyShipData.SpecialAttackDamage;
        enemyShipAttackController.SpecialAttackChargeTime = EnemyShipData.SpecialAttackChargeTime;
        enemyShipAttackController.CannonAttackRateMin = EnemyShipData.CannonAttackRateMin;
        enemyShipAttackController.CannonAttackRateMax = EnemyShipData.CannonAttackRateMax;

        // Events
        enemyShipAttackController.StartCombatEvent = StartCombatEvent;

        // TODO: Initialize this after x amount of time or from a button or whatever
        enemyShipAttackController.Initialize();

        // Enemy ship health controller
        enemyShipHealthController.ShipHealth = EnemyShipData.ShipHealth;
        enemyShipHealthController.ShipLevelHealthMultiplier = EnemyShipData.ShipLevelHealthMultiplier;

        // Events
        enemyShipHealthController.CurrentHealthUIEvent = CurrentEnemyHealthUIEvent;
        enemyShipHealthController.Initialize();

        // Enemy ship weak spot controller
        enemyShipWeakSpotController.WeakSpotAppearanceRateMin = EnemyShipData.WeakSpotAppearanceRateMin;
        enemyShipWeakSpotController.WeakSpotAppearanceRateMax = EnemyShipData.WeakSpotAppearanceRateMax;
        enemyShipWeakSpotController.WeakSpotCoolDownTime = EnemyShipData.WeakSpotCoolDownTime;
        enemyShipWeakSpotController.WeakSpotPlayerDamageMultiplier = EnemyShipData.WeakSpotPlayerDamageMultiplier;
        enemyShipWeakSpotController.EnemyShipHealthController = enemyShipHealthController;
        // TODO: Update this with value that comes from main menu
        enemyShipWeakSpotController.PlayerNumberOfCannons = 3;
        // Events
        enemyShipWeakSpotController.StartCombatEvent = StartCombatEvent;
        enemyShipWeakSpotController.Initialize();

        // Enemy resources drop 
        // Properties
        enemyResourcesDrop.ChanceToDropResources = EnemyShipData.ChanceToDropResources;
        enemyResourcesDrop.ResourcesDroppedAmntMin = EnemyShipData.ResourcesDroppedAmntMin;
        enemyResourcesDrop.ResourcesDroppedAmntMax = EnemyShipData.ResourcesDroppedAmntMax;

        // Events
        enemyResourcesDrop.GoldResourcesDroppedEvent = GoldResourcesDroppedEvent;
        enemyResourcesDrop.WoodResourcesDroppedEvent = WoodResourcesDroppedEvent;
    }
}


