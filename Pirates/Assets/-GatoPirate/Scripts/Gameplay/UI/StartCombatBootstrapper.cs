using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StartCombatBootstrapper : MonoBehaviour
{
    [Header("Combat data")]
    [SerializeField]
    private CombatData combatData;

    [Header("Script references")]
    [SerializeField]
    private StartCombatController startCombatController;
    [SerializeField]
    private PlayerGameplayBootstrapper playerGameplayBootstrapper;
    [SerializeField]
    private EnemyGameplayBootstrapper enemyGameplayBootstrapper;
    [SerializeField]
    private VirtualCameraController virtualCameraController;

    [Header("Events")]
    [SerializeField]
    private VoidEvent StartCombatEvent;
    [SerializeField]
    private FloatEvent TriggerShakingCameraEvent;

    private int activeCannons;

    private void Awake()
    {
        activeCannons = combatData.CatCrewControllerList.Length;

        startCombatController.StartCombatEvent = StartCombatEvent;

        virtualCameraController.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        virtualCameraController.Initialize();

        // Player
        // Properties
        playerGameplayBootstrapper.NumberOfActiveCannons = activeCannons;
        playerGameplayBootstrapper.PlayerShipData = combatData.PlayerShipData;
        playerGameplayBootstrapper.CatCrewControllerObjectsList = combatData.CatCrewControllerList;
        // Events
        playerGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        playerGameplayBootstrapper.TriggerShakingCameraEvent = TriggerShakingCameraEvent;

        playerGameplayBootstrapper.InitializeBootstrapper();

        // Enemy
        // Properties
        enemyGameplayBootstrapper.NumberOfActiveCannons = activeCannons;
        enemyGameplayBootstrapper.EnemyShipData = combatData.EnemyShipData;
        // Events
        enemyGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        enemyGameplayBootstrapper.InitializeBootstrapper();
    }
}
