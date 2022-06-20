using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
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
        // Properties
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

        // Events
        playerShipController.ShootCannonEvent = ShootCannonEvent;
        playerShipController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipController.Initialize();

        playerShipUIController.ShootCannonEvent = ShootCannonEvent;
        playerShipUIController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipUIController.Initialize();
    }
}
