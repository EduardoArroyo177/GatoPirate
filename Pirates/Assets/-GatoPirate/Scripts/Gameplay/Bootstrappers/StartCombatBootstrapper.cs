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

    [Header("Music manager")]
    [SerializeField]
    private MusicManagerCombat musicManagerCombat;
    [SerializeField]
    private VoidEvent TriggerCombatMusicEvent;
    [SerializeField]
    private FloatEvent SetMusicVolumeEvent;

    [Header("Scene loader")]
    [SerializeField]
    private SceneLoaderManager sceneLoaderManager;
    [SerializeField]
    private VoidEvent LoadMainMenuSceneEvent;

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
    [SerializeField]
    private VoidEvent SkipInitialAnimationsEvent;

    [Header("Combat flow events")]
    [SerializeField]
    private VoidEvent StartCombatEvent;
    [SerializeField]
    private VoidEvent StopCombatEvent;
    [SerializeField]
    private CharacterTypeEvent ShowResultScreenEvent;
    [SerializeField]
    private BoolEvent WinChestEvent;
    [SerializeField]
    private VoidEvent TriggerEnemyLostAnimationEvent;
    [SerializeField]
    private VoidEvent TriggerPlayerLostAnimationEvent;

    [Header("Other events")]
    [SerializeField]
    private FloatEvent TriggerShakingCameraEvent;

    private int playerActiveCannons;

    private void Awake()
    {
        // Scene loader
        sceneLoaderManager.LoadMainMenuSceneEvent = LoadMainMenuSceneEvent;
        sceneLoaderManager.Initialize();

        // Combat data
        playerActiveCannons = combatData.CatCrewDataList.Length;

        // Music manager
        musicManagerCombat.TriggerCombatMusicEvent = TriggerCombatMusicEvent;
        musicManagerCombat.SetMusicVolumeEvent = SetMusicVolumeEvent;
        musicManagerCombat.Initialize();

        // Start combat controller
        startCombatController.StartCombatEvent = StartCombatEvent;
        startCombatController.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;
        startCombatController.Initialize();

        // Virtual cameras
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
        playerGameplayBootstrapper.SkipInitialAnimationsEvent = SkipInitialAnimationsEvent;

        playerGameplayBootstrapper.TriggerPlayerStartingAnimationEvent = TriggerPlayerStartingAnimationEvent;
        playerGameplayBootstrapper.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        playerGameplayBootstrapper.TriggerPlayerLostAnimationEvent = TriggerPlayerLostAnimationEvent;
        playerGameplayBootstrapper.InitializeBootstrapper();

        // Enemy
        // Properties
        enemyGameplayBootstrapper.EnemyShipData = combatData.EnemyShipData;
        // Events
        enemyGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        enemyGameplayBootstrapper.StopCombatEvent = StopCombatEvent;
        enemyGameplayBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        enemyGameplayBootstrapper.WinChestEvent = WinChestEvent;
        enemyGameplayBootstrapper.TriggerEnemyLostAnimationEvent = TriggerEnemyLostAnimationEvent;
        enemyGameplayBootstrapper.SkipInitialAnimationsEvent = SkipInitialAnimationsEvent;

        enemyGameplayBootstrapper.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        enemyGameplayBootstrapper.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;
        enemyGameplayBootstrapper.InitializeBootstrapper();

        Invoke("StartingAnimation", 0.5f);
    }

    private void StartingAnimation()
    {
        TriggerPlayerStartingAnimationEvent.Raise();
        CombatInitializationCompleted();
    }

    private void CombatInitializationCompleted()
    {
        TriggerCombatMusicEvent.Raise();
    }
}
