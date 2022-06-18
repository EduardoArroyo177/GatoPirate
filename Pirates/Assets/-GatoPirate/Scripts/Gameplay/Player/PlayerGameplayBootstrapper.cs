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
    private CannonSideEvent EnableCannonEvent;


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
        playerShipController.EnableCannonEvent = EnableCannonEvent;
        playerShipController.Initialize();

        playerShipUIController.ShootCannonEvent = ShootCannonEvent;
        playerShipUIController.EnableCannonEvent = EnableCannonEvent;
        playerShipUIController.Initialize();
    }
}
