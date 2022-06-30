using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerGameplayBootstrapper : MonoBehaviour
{
    [Header("Ships")]
    [SerializeField]
    private ShipData playerShipData; // TODO: Someone needs to give this from another place
    [SerializeField]
    private PlayerShipAttackController playerShipAttackController;
    [SerializeField]
    private ShipHealthController playerShipHealthController;
    [SerializeField]
    private PlayerBuildShipController playerBuildShipController;

    [Header("UI")]
    [SerializeField]
    private PlayerShipAttackUIController playerShipAttackUIController;
    [SerializeField]
    private ShipHealthUIController playerShipHealthUIController;

    [Header("Cannon Events")]
    [SerializeField]
    private CannonSideEvent ShootCannonEvent;
    [SerializeField]
    private CannonSideFloatEvent StartCoolDownTimerAnimationEvent;

    [Header("Special attack events")]
    [SerializeField]
    private FloatEvent InitializeSpecialAttackEvent;
    [SerializeField]
    private VoidEvent ShootSpecialAttackEvent;

    [Header("Health events")]
    [SerializeField]
    private FloatEvent CurrentPlayerHealthUIEvent;

    // Events
    public FloatEvent TriggerShakingCameraEvent { get; set; }

    // Properties
    public CatCrewController[] CatCrewControllerObjectsList { get; set; }
    public ShipData PlayerShipData { get => playerShipData; set => playerShipData = value; }
    public int NumberOfActiveCannons { get; set; }

    public void InitializeBootstrapper()
    {
        InitializePoolingSystem();
        InitializePlayer();
    }

    //private void Awake()
    //{
    //    InitializePoolingSystem();
    //    InitializePlayer();
    //}

    private void InitializePoolingSystem()
    {
        ObjectPooling.Instance.Initialize();
    }

    private void InitializePlayer()
    {
        // UI Init goes first
        playerShipAttackUIController.NumberOfActiveCannons = NumberOfActiveCannons;
        playerShipAttackUIController.ShootCannonEvent = ShootCannonEvent;
        playerShipAttackUIController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipAttackUIController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        playerShipAttackUIController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
        playerShipAttackUIController.Initialize();

        playerShipHealthUIController.CurrentHealthUIEvent = CurrentPlayerHealthUIEvent;
        playerShipHealthUIController.Initialize();

        // Player build ship controller
        playerBuildShipController.CatCrewControllerObjectsList = CatCrewControllerObjectsList;
        playerBuildShipController.Initialize();

        // Player ship attack controller
        // Controller Properties
        playerShipAttackController.ShipLevelAttackMultiplier = PlayerShipData.ShipLevelAttackMultiplier;
        playerShipAttackController.ShipLevelBallSpeedMultiplier = PlayerShipData.ShipLevelBallSpeedMultiplier;
        playerShipAttackController.ShipLevelCoolDownMultiplier = PlayerShipData.ShipLevelCoolDownMultiplier;
        playerShipAttackController.ShipLevelSpecialAttackMultiplier = PlayerShipData.ShipLevelSpecialAttackMultiplier;

        playerShipAttackController.CannonBallSpeed = PlayerShipData.CannonBallSpeed;
        playerShipAttackController.CannonBallDamage = PlayerShipData.CannonBallDamage;
        playerShipAttackController.CannonCoolDownTime = PlayerShipData.CannonCoolDownTime;
        playerShipAttackController.BasicAttackDamage = PlayerShipData.BasicAttackDamage;
        playerShipAttackController.BasicAttackCoolDownTime = PlayerShipData.BasicAttackCoolDownTime;
        playerShipAttackController.SpecialAttackDamage = PlayerShipData.SpecialAttackDamage;
        playerShipAttackController.SpecialAttackChargeTime = PlayerShipData.SpecialAttackChargeTime;

        // TODO: Initialize ship with data from main screen 

        // Events
        playerShipAttackController.ShootCannonEvent = ShootCannonEvent;
        playerShipAttackController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipAttackController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        playerShipAttackController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
        playerShipAttackController.Initialize();

        // Player ship health controller
        // Properties
        playerShipHealthController.ShipHealth = PlayerShipData.ShipHealth;
        playerShipHealthController.ShipLevelHealthMultiplier = PlayerShipData.ShipLevelHealthMultiplier;

        // Events
        playerShipHealthController.CurrentHealthUIEvent = CurrentPlayerHealthUIEvent;
        playerShipHealthController.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        playerShipHealthController.Initialize();
    }
}
