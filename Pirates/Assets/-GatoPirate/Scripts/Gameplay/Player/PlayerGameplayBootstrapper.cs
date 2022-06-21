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
    private PlayerShipAttackController playerShipController;

    [Header("UI")]
    [SerializeField]
    private PlayerShipUIController playerShipUIController;

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
        playerShipUIController.ShootCannonEvent = ShootCannonEvent;
        playerShipUIController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipUIController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        playerShipUIController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
        playerShipUIController.Initialize();

        // Controller Properties
        playerShipController.ShipLevelAttackMultiplier = playerShipData.ShipLevelAttackMultiplier;
        playerShipController.ShipLevelBallSpeedMultiplier = playerShipData.ShipLevelBallSpeedMultiplier;
        playerShipController.ShipLevelCoolDownMultiplier = playerShipData.ShipLevelCoolDownMultiplier;
        playerShipController.ShipLevelSpecialAttackMultiplier = playerShipData.ShipLevelSpecialAttackMultiplier;

        playerShipController.CannonBallSpeed = playerShipData.CannonBallSpeed;
        playerShipController.CannonBallDamage = playerShipData.CannonBallDamage;
        playerShipController.CannonCoolDownTime = playerShipData.CannonCoolDownTime;
        playerShipController.BasicAttackDamage = playerShipData.BasicAttackDamage;
        playerShipController.BasicAttackCoolDownTime = playerShipData.BasicAttackCoolDownTime;
        playerShipController.SpecialAttackDamage = playerShipData.SpecialAttackDamage;
        playerShipController.SpecialAttackChargeTime = playerShipData.SpecialAttackChargeTime;

        // TODO: Initialize ship with data from main screen 

        // Events
        playerShipController.ShootCannonEvent = ShootCannonEvent;
        playerShipController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        playerShipController.Initialize();
    }
}
