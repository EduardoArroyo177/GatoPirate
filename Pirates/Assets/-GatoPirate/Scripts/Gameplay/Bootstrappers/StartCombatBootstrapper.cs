using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StartCombatBootstrapper : MonoBehaviour
{
    [Header("Combat data")]
    [SerializeField]
    private CombatData combatData;

    [Header("Gameplay Script references")]
    [SerializeField]
    private StartCombatController startCombatController;
    [SerializeField]
    private PlayerGameplayBootstrapper playerGameplayBootstrapper;
    [SerializeField]
    private EnemyGameplayBootstrapper enemyGameplayBootstrapper;
    [SerializeField]
    private VirtualCameraController virtualCameraController;

    [Header("UI script references")]
    [SerializeField]
    private UICanvasBootstrapper uiCanvasBootstrapper;

    [Header("Events")]
    [SerializeField]
    private VoidEvent StartCombatEvent;
    [SerializeField]
    private VoidEvent StopCombatEvent;
    [SerializeField]
    private CharacterTypeEvent ShowResultScreenEvent;
    [SerializeField]
    private FloatEvent TriggerShakingCameraEvent;
    [SerializeField]
    private BoolEvent WinChestEvent;

    private int activeCannons;

    private void Awake()
    {
        activeCannons = combatData.CatCrewControllerList.Length;

        startCombatController.StartCombatEvent = StartCombatEvent;

        virtualCameraController.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        virtualCameraController.Initialize();

        uiCanvasBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        uiCanvasBootstrapper.WinChestEvent = WinChestEvent;
        uiCanvasBootstrapper.Initialize();

        // Player
        // Properties
        playerGameplayBootstrapper.NumberOfActiveCannons = activeCannons;
        playerGameplayBootstrapper.PlayerShipData = combatData.PlayerShipData;
        playerGameplayBootstrapper.CatCrewControllerObjectsList = combatData.CatCrewControllerList;
        // Events
        playerGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        playerGameplayBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        playerGameplayBootstrapper.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        playerGameplayBootstrapper.StopCombatEvent = StopCombatEvent;

        playerGameplayBootstrapper.InitializeBootstrapper();

        // Enemy
        // Properties
        enemyGameplayBootstrapper.NumberOfActiveCannons = activeCannons;
        enemyGameplayBootstrapper.EnemyShipData = combatData.EnemyShipData;
        // Events
        enemyGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        enemyGameplayBootstrapper.StopCombatEvent = StopCombatEvent;
        enemyGameplayBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        enemyGameplayBootstrapper.WinChestEvent = WinChestEvent;
        enemyGameplayBootstrapper.InitializeBootstrapper();
    }
}
