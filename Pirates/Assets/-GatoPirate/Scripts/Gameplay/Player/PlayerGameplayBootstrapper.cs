using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class PlayerGameplayBootstrapper : MonoBehaviour
{
    [Header("Ships")]
    [SerializeField]
    private GameObject playerShip;
    [SerializeField]
    private GameObject enemyShip;

    [Header("Cannons")]
    [SerializeField]
    private CannonShotController[] cannonShotControllerList;
    [SerializeField]
    private CannonSideEvent ShootCannonEvent;

    private EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = enemyShip.GetComponent<EnemyManager>();
        InitializeCannonShotControllers();
    }

    private void InitializeCannonShotControllers()
    {
        
        foreach (var item in cannonShotControllerList)
        {
            item.PlayerShipTransform = playerShip.transform;
            item.ShootCannonEvent = ShootCannonEvent;
            item.SetCannonTarget(CannonSide.LEFT, enemyManager.cannonOriginLeft.transform);
            item.SetCannonTarget(CannonSide.MIDDLE, enemyManager.cannonOriginMiddle.transform);
            item.SetCannonTarget(CannonSide.RIGHT, enemyManager.cannonOriginRight.transform);
            item.Initialize();
        }
    }
}
