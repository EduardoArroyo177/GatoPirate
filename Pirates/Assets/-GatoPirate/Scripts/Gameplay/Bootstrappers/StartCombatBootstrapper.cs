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

    [Header("Starting animations events")]
    [SerializeField]
    private VoidEvent TriggerPlayerStartingAnimationEvent;
    [SerializeField]
    private VoidEvent TriggerEnemyStartingAnimationEvent;
    [SerializeField]
    private VoidEvent StartingAnimationsFinishedEvent;

    [Header("Combat flow events")]
    [SerializeField]
    private VoidEvent StartCombatEvent;
    [SerializeField]
    private VoidEvent StopCombatEvent;
    [SerializeField]
    private CharacterTypeEvent ShowResultScreenEvent;
    [SerializeField]
    private BoolEvent WinChestEvent;

    [Header("Other events")]
    [SerializeField]
    private FloatEvent TriggerShakingCameraEvent;

    private int playerActiveCannons;

    private void Awake()
    {
        playerActiveCannons = combatData.CatCrewDataList.Length;

        startCombatController.StartCombatEvent = StartCombatEvent;
        startCombatController.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;
        startCombatController.Initialize();

        virtualCameraController.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        virtualCameraController.TriggerPlayerStartingAnimationEvent = TriggerPlayerStartingAnimationEvent;
        virtualCameraController.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        virtualCameraController.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;
        virtualCameraController.Initialize();

        uiCanvasBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        uiCanvasBootstrapper.WinChestEvent = WinChestEvent;
        uiCanvasBootstrapper.Initialize();

        // Player
        // Properties
        playerGameplayBootstrapper.NumberOfActiveCannons = playerActiveCannons;
        playerGameplayBootstrapper.PlayerShipData = combatData.PlayerShipData;
        playerGameplayBootstrapper.CatCrewDataList = combatData.CatCrewDataList;
        // Events
        playerGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        playerGameplayBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        playerGameplayBootstrapper.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        playerGameplayBootstrapper.StopCombatEvent = StopCombatEvent;

        playerGameplayBootstrapper.TriggerPlayerStartingAnimationEvent = TriggerPlayerStartingAnimationEvent;
        playerGameplayBootstrapper.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        playerGameplayBootstrapper.InitializeBootstrapper();

        // Enemy
        // Properties
        enemyGameplayBootstrapper.EnemyShipData = combatData.EnemyShipData;
        // Events
        enemyGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        enemyGameplayBootstrapper.StopCombatEvent = StopCombatEvent;
        enemyGameplayBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        enemyGameplayBootstrapper.WinChestEvent = WinChestEvent;

        enemyGameplayBootstrapper.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        enemyGameplayBootstrapper.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;
        enemyGameplayBootstrapper.InitializeBootstrapper();
    }
}
