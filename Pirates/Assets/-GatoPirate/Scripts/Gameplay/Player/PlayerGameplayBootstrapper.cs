using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class PlayerGameplayBootstrapper : MonoBehaviour
{
    [Header("Ships")]
    [SerializeField]
    private PlayerShipController playerShipController;

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
        playerShipController.ShootCannonEvent = ShootCannonEvent;
        playerShipController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipController.Initialize();

        playerShipUIController.ShootCannonEvent = ShootCannonEvent;
        playerShipUIController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipUIController.Initialize();
    }
}
