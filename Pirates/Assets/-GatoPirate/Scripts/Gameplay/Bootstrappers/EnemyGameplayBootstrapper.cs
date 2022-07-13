using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
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
    [SerializeField]
    private EnemyBuildShipController enemyBuildShipController;
    [SerializeField]
    private EnemyDefeatedController enemyDestroyedController;

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
    public VoidEvent StopCombatEvent { get; set; }
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }
    public VoidEvent TriggerEnemyStartingAnimationEvent { get; set; }
    public VoidEvent StartingAnimationsFinishedEvent { get; set; }
    public VoidEvent TriggerEnemyLostAnimationEvent { get; set; }
    public VoidEvent SkipInitialAnimationsEvent { get; set; }

    // Properties
    public EnemyShipData EnemyShipData { get => enemyShipData; set => enemyShipData = value; }
    public int NumberOfActiveCannons { get; set; }

    public void InitializeBootstrapper()
    {
        NumberOfActiveCannons = enemyShipData.NumberOfActiveCannons;
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        // UI Init 
        enemyShipHealthUIController.CurrentHealthUIEvent = CurrentEnemyHealthUIEvent;
        enemyShipHealthUIController.Initialize();

        enemyResourcesDropUIController.GoldResourcesDroppedEvent = GoldResourcesDroppedEvent;
        enemyResourcesDropUIController.WoodResourcesDroppedEvent = WoodResourcesDroppedEvent;
        enemyResourcesDropUIController.Initialize();

        // Enemy build ship controller
        enemyBuildShipController.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        enemyBuildShipController.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;
        enemyBuildShipController.SkipInitialAnimationsEvent = SkipInitialAnimationsEvent;
        enemyBuildShipController.Initialize();

        // Enemy ship attack controller
        // Controller Properties
        enemyShipAttackController.NumberOfActiveCannons = NumberOfActiveCannons;
        enemyShipAttackController.ShipLevelAttackMultiplier = EnemyShipData.ShipLevelAttackMultiplier;
        enemyShipAttackController.ShipLevelBallSpeedMultiplier = EnemyShipData.ShipLevelBallSpeedMultiplier;
        enemyShipAttackController.ShipLevelCoolDownMultiplier = EnemyShipData.ShipLevelCoolDownMultiplier;
        enemyShipAttackController.ShipLevelSpecialAttackMultiplier = EnemyShipData.ShipLevelSpecialAttackMultiplier;

        enemyShipAttackController.CannonBallSpeed = EnemyShipData.CannonBallSpeed;
        enemyShipAttackController.BasicAttackDamage = EnemyShipData.BasicAttackDamage;
        enemyShipAttackController.NormalAttackDamage = EnemyShipData.NormalAttackDamage;
        enemyShipAttackController.NormalAttackCoolDownTime = EnemyShipData.NormalAttackCoolDownTime;
        enemyShipAttackController.AutomaticAttackDamage = EnemyShipData.AutomaticAttackDamage;
        enemyShipAttackController.AutomaticAttackCoolDownTime = EnemyShipData.AutomaticAttackCoolDownTime;
        enemyShipAttackController.SpecialAttackDamage = EnemyShipData.SpecialAttackDamage;
        enemyShipAttackController.SpecialAttackChargeTime = EnemyShipData.SpecialAttackChargeTime;
        enemyShipAttackController.CannonAttackRateMin = EnemyShipData.CannonAttackRateMin;
        enemyShipAttackController.CannonAttackRateMax = EnemyShipData.CannonAttackRateMax;

        // Events
        enemyShipAttackController.StartCombatEvent = StartCombatEvent;
        enemyShipAttackController.StopCombatEvent = StopCombatEvent;

        // TODO: Initialize this after x amount of time or from a button or whatever
        enemyShipAttackController.Initialize();

        // Enemy ship health controller
        enemyShipHealthController.ShipHealth = EnemyShipData.ShipHealth;
        enemyShipHealthController.ShipLevelHealthMultiplier = EnemyShipData.ShipLevelHealthMultiplier;

        // Events
        enemyShipHealthController.CurrentHealthUIEvent = CurrentEnemyHealthUIEvent;
        enemyShipHealthController.StopCombatEvent = StopCombatEvent;
        enemyShipHealthController.ShowResultScreenEvent = ShowResultScreenEvent;
        enemyShipHealthController.TriggerEnemyLostAnimationEvent = TriggerEnemyLostAnimationEvent;
        enemyShipHealthController.Initialize();

        // Enemy ship weak spot controller
        enemyShipWeakSpotController.WeakSpotAppearanceRateMin = EnemyShipData.WeakSpotAppearanceRateMin;
        enemyShipWeakSpotController.WeakSpotAppearanceRateMax = EnemyShipData.WeakSpotAppearanceRateMax;
        enemyShipWeakSpotController.WeakSpotCoolDownTime = EnemyShipData.WeakSpotCoolDownTime;
        enemyShipWeakSpotController.WeakSpotPlayerDamageMultiplier = EnemyShipData.WeakSpotPlayerDamageMultiplier;
        enemyShipWeakSpotController.EnemyShipHealthController = enemyShipHealthController;
        enemyShipWeakSpotController.NumberOfActiveCannons = NumberOfActiveCannons;
        // Events
        enemyShipWeakSpotController.StartCombatEvent = StartCombatEvent;
        enemyShipWeakSpotController.StopCombatEvent = StopCombatEvent;
        enemyShipWeakSpotController.Initialize();

        // Enemy resources drop 
        // Properties
        enemyResourcesDrop.ChanceToDropResources = EnemyShipData.ChanceToDropResources;
        enemyResourcesDrop.BasicResourcesDroppedAmntMin = EnemyShipData.BasicResourcesDroppedAmntMin;
        enemyResourcesDrop.BasicResourcesDroppedAmntMax = EnemyShipData.BasicResourcesDroppedAmntMax;
        enemyResourcesDrop.NormalResourcesDroppedAmntMin = EnemyShipData.NormalResourcesDroppedAmntMin;
        enemyResourcesDrop.NormalResourcesDroppedAmntMax = EnemyShipData.NormalResourcesDroppedAmntMax;

        enemyResourcesDrop.ChanceToDropResourcesBox = EnemyShipData.ChanceToDropResourcesBox;
        enemyResourcesDrop.ResourcesBoxesPerCombat = EnemyShipData.ResourcesBoxesPerCombat;
        enemyResourcesDrop.ResourcesBoxAmntMin = EnemyShipData.ResourcesBoxAmntMin;
        enemyResourcesDrop.ResourcesBoxAmntMax = EnemyShipData.ResourcesBoxAmntMax;
        enemyResourcesDrop.ResourcesBoxTimeToDestroy = EnemyShipData.ResourcesBoxTimeToDestroy;

        enemyResourcesDrop.ChanceToGiveChest = EnemyShipData.ChanceToGiveChest;
        // Events
        enemyResourcesDrop.GoldResourcesDroppedEvent = GoldResourcesDroppedEvent;
        enemyResourcesDrop.WoodResourcesDroppedEvent = WoodResourcesDroppedEvent;
        enemyResourcesDrop.WinChestEvent = WinChestEvent;

        enemyResourcesDrop.Initialize();

        // Enemy destroyed
        enemyDestroyedController.ShowResultScreenEvent = ShowResultScreenEvent;
        enemyDestroyedController.TriggerEnemyLostAnimationEvent = TriggerEnemyLostAnimationEvent;
        enemyDestroyedController.Initialize();
    }
}


