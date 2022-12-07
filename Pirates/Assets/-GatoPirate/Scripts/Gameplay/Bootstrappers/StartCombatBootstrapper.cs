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
    [SerializeField]
    private VoidEvent PauseWihtoutScreenEvent;
    [SerializeField]
    private VoidEvent ResumeGameEvent;

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
    private VoidEvent ResumeCombatEvent;
    [SerializeField]
    private CharacterTypeEvent ShowResultScreenEvent;
    [SerializeField]
    private BoolEvent WinChestEvent;
    [SerializeField]
    private VoidEvent TriggerEnemyLostAnimationEvent;
    [SerializeField]
    private VoidEvent TriggerPlayerLostAnimationEvent;
    [SerializeField]
    private VoidEvent CurrenciesUpdatedEvent;

    [Header("Other events")]
    [SerializeField]
    private FloatEvent TriggerShakingCameraEvent;

    [Header("Ads")]
    [SerializeField]
    private HuaweiAdsControllerCombat huaweiAdsController;
    [SerializeField]
    private VoidEvent LoadReviveAdEvent;
    [SerializeField]
    private VoidEvent LoadDoubleRewardAdEvent;
    [SerializeField]
    private VoidEvent LoadCombatFinishedAdEvent;
    [SerializeField]
    private VoidEvent CombatRewardAdSuccessEvent;
    [SerializeField]
    private VoidEvent ReviveSuccessEvent;
    [SerializeField]
    private VoidEvent DoubleRewardSuccessEvent;

    [Header("Tutorial events")]
    [SerializeField]
    private VoidEvent TriggerCombatTutorialEvent;
    [SerializeField]
    private VoidEvent TriggerCombatWeakSpotTutorialEvent;
    [SerializeField]
    private VoidEvent TriggerCombatResourcesBoxTutorialEvent;

    [Header("IAP")]
    [SerializeField]
    private VoidEvent RemoveAdsPurchasedEvent;

    [Header("Face controller player")]
    [SerializeField]
    private CombatCatFacesController playerCatFacesController;
    [SerializeField]
    private VoidEvent UpdateToSurprisedFacePlayerEvent;
    [SerializeField] 
    private VoidEvent UpdateToBraveFacePlayerEvent;
    [SerializeField] 
    private VoidEvent UpdateToHappyFacePlayerEvent;
    [SerializeField] 
    private VoidEvent UpdateToWorriedFacePlayerEvent;
    [SerializeField] 
    private VoidEvent UpdateToSadFacePlayerEvent;
    

    [Header("Face controller enemy")]
    [SerializeField]
    private CombatCatFacesController enemyCatFacesController;
    [SerializeField]
    private VoidEvent UpdateToWorriedFaceEnemyEvent;
    [SerializeField]
    private VoidEvent UpdateToSadFaceEnemyEvent;
    [SerializeField]
    private VoidEvent UpdateToDeadFaceEnemyEvent;

    private int playerActiveCannons;

    private void Awake()
    {
        // Load currency
        CurrencyDataSaveManager.Instance.CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
        CurrencyDataSaveManager.Instance.LoadCurrencySavedData();
        // Load settings
        SettingsDataSaveManager.Instance.LoadSettingsSavedData();
        // Load tutorial data
        TutorialDataSaveManager.Instance.LoadTutorialSavedData();
        // Load IAP data
        PurchasesDataSaveManager.Instance.RemoveAdsPurchasedEvent = RemoveAdsPurchasedEvent;
        PurchasesDataSaveManager.Instance.LoadPurchaseIAPSavedData();
        // Load leaderboard data
        LeaderboardsDataSaveManager.Instance.LoadLeaderboardsSavedData();

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
        startCombatController.TriggerCombatTutorialEvent = TriggerCombatTutorialEvent;
        startCombatController.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;
        startCombatController.Initialize();

        // Virtual cameras
        virtualCameraController.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        virtualCameraController.TriggerPlayerStartingAnimationEvent = TriggerPlayerStartingAnimationEvent;
        virtualCameraController.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        virtualCameraController.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;
        virtualCameraController.Initialize();

        uiCanvasBootstrapper.PauseGameEvent = PauseGameEvent;
        uiCanvasBootstrapper.PauseWihtoutScreenEvent = PauseWihtoutScreenEvent;
        uiCanvasBootstrapper.ResumeGameEvent = ResumeGameEvent;
        uiCanvasBootstrapper.LoadCombatSceneEvent = LoadCombatSceneEvent;
        uiCanvasBootstrapper.LoadMainMenuSceneEvent = LoadMainMenuSceneEvent;
        uiCanvasBootstrapper.SetMusicVolumeEvent = SetMusicVolumeEvent;
        uiCanvasBootstrapper.SetPreviousMusicVolumeEvent = SetPreviousMusicVolumeEvent;
        uiCanvasBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        uiCanvasBootstrapper.WinChestEvent = WinChestEvent;
        uiCanvasBootstrapper.LoadReviveAdEvent = LoadReviveAdEvent;
        uiCanvasBootstrapper.LoadDoubleRewardAdEvent = LoadDoubleRewardAdEvent;
        uiCanvasBootstrapper.LoadCombatFinishedAdEvent = LoadCombatFinishedAdEvent;
        uiCanvasBootstrapper.ReviveSuccessEvent = ReviveSuccessEvent;
        uiCanvasBootstrapper.DoubleRewardSuccessEvent = DoubleRewardSuccessEvent;
        uiCanvasBootstrapper.ReviveCurrencyPrice = combatData.PlayerShipData.ReviveCurrencyPrice;
        uiCanvasBootstrapper.CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
        uiCanvasBootstrapper.StartCombatEvent = StartCombatEvent;
        uiCanvasBootstrapper.TriggerCombatSoundEvent = TriggerCombatSoundEvent;
        uiCanvasBootstrapper.RemoveAdsPurchasedEvent = RemoveAdsPurchasedEvent;
        uiCanvasBootstrapper.TriggerCombatTutorialEvent = TriggerCombatTutorialEvent;
        uiCanvasBootstrapper.TriggerCombatWeakSpotTutorialEvent = TriggerCombatWeakSpotTutorialEvent;
        uiCanvasBootstrapper.TriggerCombatResourcesBoxTutorialEvent = TriggerCombatResourcesBoxTutorialEvent;
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
        playerGameplayBootstrapper.ResumeCombatEvent = ResumeCombatEvent;
        playerGameplayBootstrapper.SkipInitialAnimationsEvent = SkipInitialAnimationsEvent;
        playerGameplayBootstrapper.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        playerGameplayBootstrapper.ReviveSuccessEvent = ReviveSuccessEvent;

        playerGameplayBootstrapper.TriggerPlayerStartingAnimationEvent = TriggerPlayerStartingAnimationEvent;
        playerGameplayBootstrapper.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        playerGameplayBootstrapper.TriggerPlayerLostAnimationEvent = TriggerPlayerLostAnimationEvent;

        playerGameplayBootstrapper.UpdateToWorriedFacePlayerEvent = UpdateToWorriedFacePlayerEvent;
        playerGameplayBootstrapper.UpdateToSadFacePlayerEvent = UpdateToSadFacePlayerEvent;
        playerGameplayBootstrapper.InitializeBootstrapper();

        // Enemy
        // Properties
        enemyGameplayBootstrapper.EnemyShipData = combatData.EnemyShipData;
        // Events
        enemyGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        enemyGameplayBootstrapper.StopCombatEvent = StopCombatEvent;
        enemyGameplayBootstrapper.ResumeCombatEvent = ResumeCombatEvent;
        enemyGameplayBootstrapper.ShowResultScreenEvent = ShowResultScreenEvent;
        enemyGameplayBootstrapper.WinChestEvent = WinChestEvent;
        enemyGameplayBootstrapper.TriggerEnemyLostAnimationEvent = TriggerEnemyLostAnimationEvent;
        enemyGameplayBootstrapper.SkipInitialAnimationsEvent = SkipInitialAnimationsEvent;
        enemyGameplayBootstrapper.TriggerCombatSoundEvent = TriggerCombatSoundEvent;
        enemyGameplayBootstrapper.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        enemyGameplayBootstrapper.TriggerCombatWeakSpotTutorialEvent = TriggerCombatWeakSpotTutorialEvent;
        enemyGameplayBootstrapper.TriggerCombatResourcesBoxTutorialEvent = TriggerCombatResourcesBoxTutorialEvent;

        enemyGameplayBootstrapper.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        enemyGameplayBootstrapper.StartingAnimationsFinishedEvent = StartingAnimationsFinishedEvent;

        enemyGameplayBootstrapper.UpdateToWorriedFaceEnemyEvent = UpdateToWorriedFaceEnemyEvent;
        enemyGameplayBootstrapper.UpdateToSadFaceEnemyEvent = UpdateToSadFaceEnemyEvent;
        enemyGameplayBootstrapper.UpdateToDeadFaceEnemyEvent = UpdateToDeadFaceEnemyEvent;
        enemyGameplayBootstrapper.InitializeBootstrapper();

        // Ads
        huaweiAdsController.LoadReviveAdEvent = LoadReviveAdEvent;
        huaweiAdsController.LoadDoubleRewardAdEvent = LoadDoubleRewardAdEvent;
        huaweiAdsController.LoadCombatFinishedAdEvent = LoadCombatFinishedAdEvent;
        huaweiAdsController.CombatRewardAdSuccessEvent = CombatRewardAdSuccessEvent;
        huaweiAdsController.ReviveSuccessEvent = ReviveSuccessEvent;
        huaweiAdsController.DoubleRewardSuccessEvent = DoubleRewardSuccessEvent;
        huaweiAdsController.Initialize();

        // IAP
        PurchasesDataSaveManager.Instance.CallForPurchasedIAP();

        // Cat faces
        playerCatFacesController.UpdateToSurprisedFaceEvent = UpdateToSurprisedFacePlayerEvent;
        playerCatFacesController.UpdateToBraveFaceEvent = UpdateToBraveFacePlayerEvent;
        playerCatFacesController.UpdateToHappyFaceEvent = UpdateToHappyFacePlayerEvent;
        playerCatFacesController.UpdateToWorriedFaceEvent = UpdateToWorriedFacePlayerEvent;
        playerCatFacesController.UpdateToSadFaceEvent = UpdateToSadFacePlayerEvent;
        playerCatFacesController.Initialize();

        enemyCatFacesController.UpdateToWorriedFaceEvent = UpdateToWorriedFaceEnemyEvent;
        enemyCatFacesController.UpdateToSadFaceEvent = UpdateToSadFaceEnemyEvent;
        enemyCatFacesController.UpdateToDeadFaceEvent = UpdateToDeadFaceEnemyEvent;
        enemyCatFacesController.Initialize();

        UpdateToHappyFacePlayerEvent.Raise();
        Invoke("StartingAnimation", 0.5f);

        GameAnalyticsController.Instance.GameFlowEvent("SceneLoaded:Combat");
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
