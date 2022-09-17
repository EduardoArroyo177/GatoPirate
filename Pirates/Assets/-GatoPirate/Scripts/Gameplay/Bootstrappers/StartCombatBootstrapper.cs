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

    [Header("Settings")]
    [SerializeField]
    private SettingsController settingsController;
    [SerializeField]
    private FloatEvent SetMusicVolumeEvent;
    [SerializeField]
    private FloatEvent SetSoundsVolumeEvent;

    [Header("Audio")]
    [Header("Music manager")]
    [SerializeField]
    private MusicManagerCombat musicManagerCombat;
    [SerializeField]
    private VoidEvent TriggerCombatMusicEvent;
    [SerializeField]
    private VoidEvent SetPreviousMusicVolumeEvent;

    [Header("Sound manager")]
    [SerializeField]
    private SoundsEffectsController soundsEffectsController;
    [SerializeField]
    private CombatSoundEvent TriggerCombatSoundEvent;

    [Header("Ambience sound manager")]
    [SerializeField]
    private AmbienceAudioManager ambienceAudioManager;

    [Header("UI Sound manager")]
    [SerializeField]
    private UISoundManagerCombat uiSoundManagerCombat;
    [SerializeField]
    private VoidEvent UISoundScreenOpenEvent;
    [SerializeField]
    private VoidEvent UISoundScreenClosedEvent;
    [SerializeField]
    private VoidEvent UISoundButtonPressedEvent;
    [SerializeField]
    private VoidEvent UISoundButtonCancelEvent;
    [SerializeField]
    private VoidEvent UISoundTapEvent;

    [Header("Scene loader")]
    [SerializeField]
    private SceneLoaderManager sceneLoaderManager;
    [SerializeField]
    private VoidEvent LoadMainMenuSceneEvent;
    [SerializeField]
    private VoidEvent LoadCombatSceneEvent;

    [Header("Gameplay Script references")]
    [SerializeField]
    private StartCombatController startCombatController;
    [SerializeField]
    private PlayerGameplayBootstrapper playerGameplayBootstrapper;
    [SerializeField]
    private EnemyGameplayBootstrapper enemyGameplayBootstrapper;
    [SerializeField]
    private VirtualCameraController virtualCameraController;

    [Header("UI references")]
    [SerializeField]
    private UICanvasBootstrapper uiCanvasBootstrapper;
    [SerializeField]
    private VoidEvent PauseGameEvent;

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
        // Load currency
        CurrencyDataSaveManager.Instance.LoadCurrencySavedData();
        // Load settings
        SettingsDataSaveManager.Instance.LoadSettingsSavedData();

        // Scene loader
        sceneLoaderManager.LoadMainMenuSceneEvent = LoadMainMenuSceneEvent;
        sceneLoaderManager.LoadCombatSceneEvent = LoadCombatSceneEvent;
        sceneLoaderManager.Initialize();

        // Combat data
        playerActiveCannons = combatData.CatCrewDataList.Length;

        // Sounds
        // Music
        musicManagerCombat.TriggerCombatMusicEvent = TriggerCombatMusicEvent;
        musicManagerCombat.SetMusicVolumeEvent = SetMusicVolumeEvent;
        musicManagerCombat.SetPreviousMusicVolumeEvent = SetPreviousMusicVolumeEvent;
        musicManagerCombat.Initialize();

        // Sfx 
        soundsEffectsController.TriggerCombatSoundEvent = TriggerCombatSoundEvent;
        soundsEffectsController.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        soundsEffectsController.Initialize();

        // Ambience
        ambienceAudioManager.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        ambienceAudioManager.Initialize();

        // UI
        uiSoundManagerCombat.UISoundScreenOpenEvent = UISoundScreenOpenEvent;
        uiSoundManagerCombat.UISoundScreenClosedEvent = UISoundScreenClosedEvent;
        uiSoundManagerCombat.UISoundButtonPressedEvent = UISoundButtonPressedEvent;
        uiSoundManagerCombat.UISoundButtonCancelEvent = UISoundButtonCancelEvent;
        uiSoundManagerCombat.UISoundTapEvent = UISoundTapEvent;
        uiSoundManagerCombat.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        uiSoundManagerCombat.Initialize();

        // Settings
        settingsController.SetMusicVolumeEvent = SetMusicVolumeEvent;
        settingsController.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        settingsController.Initialize();

        // Vibration 
        VibrationController.Instance.Initialize();

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

        uiCanvasBootstrapper.PauseGameEvent = PauseGameEvent;
        uiCanvasBootstrapper.LoadCombatSceneEvent = LoadCombatSceneEvent;
        uiCanvasBootstrapper.LoadMainMenuSceneEvent = LoadMainMenuSceneEvent;
        uiCanvasBootstrapper.SetMusicVolumeEvent = SetMusicVolumeEvent;
        uiCanvasBootstrapper.SetPreviousMusicVolumeEvent = SetPreviousMusicVolumeEvent;
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
        playerGameplayBootstrapper.SetSoundsVolumeEvent = SetSoundsVolumeEvent;

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
        enemyGameplayBootstrapper.TriggerCombatSoundEvent = TriggerCombatSoundEvent;
        enemyGameplayBootstrapper.SetSoundsVolumeEvent = SetSoundsVolumeEvent;

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
