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
    private PlayerShipHealthController playerShipHealthController;

    [Header("UI")]
    [SerializeField]
    private PlayerShipAttackUIController playerShipAttackUIController;
    [SerializeField]
    private PlayerShipHealthUIController playerShipHealthUIController;

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
    private FloatEvent CurrentHealthUIEvent;


    private void Awake()
    {
        InitializePoolingSystem();
        InitializePlayer();
    }

    private void InitializePoolingSystem()
    {
        ObjectPooling.Instance.Initialize();
    }

    private void InitializePlayer()
    {
        // UI Init goes first
        playerShipAttackUIController.ShootCannonEvent = ShootCannonEvent;
        playerShipAttackUIController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipAttackUIController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        playerShipAttackUIController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
        playerShipAttackUIController.Initialize();

        playerShipHealthUIController.CurrentHealthUIEvent = CurrentHealthUIEvent;
        playerShipHealthUIController.Initialize();

        // Player ship attack controller
        // Controller Properties
        playerShipAttackController.ShipLevelAttackMultiplier = playerShipData.ShipLevelAttackMultiplier;
        playerShipAttackController.ShipLevelBallSpeedMultiplier = playerShipData.ShipLevelBallSpeedMultiplier;
        playerShipAttackController.ShipLevelCoolDownMultiplier = playerShipData.ShipLevelCoolDownMultiplier;
        playerShipAttackController.ShipLevelSpecialAttackMultiplier = playerShipData.ShipLevelSpecialAttackMultiplier;

        playerShipAttackController.CannonBallSpeed = playerShipData.CannonBallSpeed;
        playerShipAttackController.CannonBallDamage = playerShipData.CannonBallDamage;
        playerShipAttackController.CannonCoolDownTime = playerShipData.CannonCoolDownTime;
        playerShipAttackController.BasicAttackDamage = playerShipData.BasicAttackDamage;
        playerShipAttackController.BasicAttackCoolDownTime = playerShipData.BasicAttackCoolDownTime;
        playerShipAttackController.SpecialAttackDamage = playerShipData.SpecialAttackDamage;
        playerShipAttackController.SpecialAttackChargeTime = playerShipData.SpecialAttackChargeTime;

        // TODO: Initialize ship with data from main screen 

        // Events
        playerShipAttackController.ShootCannonEvent = ShootCannonEvent;
        playerShipAttackController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipAttackController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        playerShipAttackController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
        playerShipAttackController.Initialize();

        // Player ship health controller
        // Properties
        playerShipHealthController.ShipHealth = playerShipData.ShipHealth;
        playerShipHealthController.ShipLevelHealthMultiplier = playerShipData.ShipLevelHealthMultiplier;

        // Events
        playerShipHealthController.CurrentHealthUIEvent = CurrentHealthUIEvent;
        playerShipHealthController.Initialize();
    }
}
