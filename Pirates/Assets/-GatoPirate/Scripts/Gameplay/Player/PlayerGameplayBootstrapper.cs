using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class PlayerGameplayBootstrapper : MonoBehaviour
{
    [Header("Ships")]
    [SerializeField]
    private PlayerShipController playerShipController;

    [Header("Cannon Events")]
    [SerializeField]
    private CannonSideEvent ShootCannonEvent;


    private void Awake()
    {
        InitializePoolingSystem();
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        playerShipController.ShootCannonEvent = ShootCannonEvent;
        playerShipController.Initialize();
    }

    private void InitializePoolingSystem()
    {
        ObjectPooling.Instance.Initialize();
    }
}
