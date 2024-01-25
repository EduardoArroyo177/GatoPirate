using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MetaGameBootstrapper : MonoBehaviour
{
    #region Variables
    [Header("Combat data")]
    [SerializeField]
    private CombatData combatData;

    [Header("Music")]
    [SerializeField]
    private MusicManagerMainMenu musicManager;
    [SerializeField]
    private VoidEvent TriggerIslandMusicEvent;
    [SerializeField]
    private VoidEvent TriggerStoreMusicEvent;

    [Header("UI Sounds")]
    [SerializeField]
    private SoundsManagerMainMenu soundsManager;
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

    [Header("Other sounds")]
    [SerializeField]
    private AmbienceAudioManager ambienceAudioManager;
    [SerializeField]
    private CatSoundEvent TriggerCatSoundEvent;
    [SerializeField]
    private ShipSoundEvent TriggerShipSoundEvent;
    [SerializeField]
    private UISoundsEvent TriggerUISoundEvent;

    [Header("Scene loader")]
    [SerializeField]
    private SceneLoaderManager sceneLoaderManager;
    [SerializeField]
    private VoidEvent LoadCombatSceneEvent;

    [Header("Settings")]
    [SerializeField]
    private SettingsController settingsController;
    [SerializeField]
    private FloatEvent SetMusicVolumeEvent;
    [SerializeField]
    private FloatEvent SetSoundsVolumeEvent;

    [Header("Virtual cameras")]
    [SerializeField]
    private VirtualCameraControllerMainMenu virtualCameraControllerMainMenu;
    [SerializeField]
    private GameObjectEvent TriggerSelectedCatCameraEvent;
    [SerializeField]
    private VoidEvent CloseSelectedCatCameraEvent;
    [SerializeField]
    private VoidEvent TriggerShipCameraEvent;
    [SerializeField]
    private VoidEvent CloseShipCameraEvent;

    [Header("Main menu")]
    [SerializeField]
    private MainMenuController mainMenuController;

    [Header("Currency panels")]
    [SerializeField]
    private PanelCurrenciesController[] pnl_currenciesList;
    [SerializeField]
    private VoidEvent CurrenciesUpdatedEvent;
    [SerializeField]
    private CurrencyTypeIntEvent ShowSpentCurrencyEvent;
    [SerializeField]
    private CurrencyTypeIntEvent ShowRewardedCurrencyEvent;
    [SerializeField]
    private VoidEvent CurrencyUpdateAnimationFinishedEvent;

    [Header("Island")]
    [SerializeField]
    private IslandCatsController islandCatsController;
    [SerializeField]
    private CatTypeIDEvent NewCatPurchasedEvent;
    [SerializeField]
    private VoidEvent CatSelectedEvent;

    [Header("Cat Crew Management")]
    [SerializeField]
    private CatCrewManagementController catCrewManagementController;
    [SerializeField]
    private IntEvent SelectCatEvent;
    [SerializeField]
    private ShipSlotViewEvent SelectShipSlotEvent;

    [Header("Cat Recruitment")]
    [SerializeField]
    private CatRecruitmentController catRecruitmentController;
    [SerializeField]
    private VoidEvent OpenCatRecruitmentScreenEvent;
    [SerializeField]
    private IntCatalogueTypeEvent PurchaseCatalogueCatEvent;
    [SerializeField]
    private IntCatalogueTypeEvent PurchaseCatalogueSkinEvent;
    [SerializeField]
    private IntCatalogueTypeEvent ShowSelectedItemEvent;
    [SerializeField]
    private IntCatalogueTypeEvent ShowSelectedSkinInfoEvent;
    [SerializeField]
    private VoidEvent OpenGoToStorePopUpEvent;
    [SerializeField]
    private VoidEvent OpenCatCrewManagementNoIDEvent;

    [Header("Cat options")]
    [SerializeField]
    private CatOptionsController catOptionsController;
    [SerializeField]
    private StringEvent OpenSelectedCatOptionsEvent;
    [SerializeField]
    private StringEvent OpenCatCrewManagementEvent;
    [SerializeField]
    public StringEvent OpenSkinManagementEvent;

    [Header("Skin management")]
    [SerializeField]
    private CatSkinManagementController catSkinManagementController;
    [SerializeField]
    private IntEvent SelectSkinEvent;
    [SerializeField]
    private StringEvent CatUpdatedEvent;
    [SerializeField]
    private StringEvent SkinPurchasedEvent;
    [SerializeField]
    private BoolEvent OpenScreenEvent;

    [Header("Ship options")]
    [SerializeField]
    private MetaGameShipController shipOptionsController;
    [SerializeField]
    private VoidEvent OpenShipOptionsEvent;
    [SerializeField]
    private VoidEvent StartCombatEvent;

    [Header("Game store")]
    [SerializeField]
    private StoreController storeController;
    [SerializeField]
    private VoidEvent OpenStoreEvent;
    [SerializeField]
    private StringEvent PurchaseStoreItemEvent;

    [Header("IAP")]
    [SerializeField]
    private ProductInfoListEvent StoreProductsListEvent;
    [SerializeField]
    private PurchaseInfoListEvent StorePurchasesListEvent;
    [SerializeField]
    private PurchaseInfoEvent PurchaseItemSuccesfulEvent;
    [SerializeField] 
    private StringPurchaseInfoEvent PurchaseResultEvent;
    [SerializeField]
    private PurchaseInfoEvent ConsumedItemSuccesfulEvent;
    [SerializeField]
    private VoidEvent RemoveAdsPurchasedEvent;

    [Header("Result screen")]
    [SerializeField]
    private MetaResultScreenController resultScreenController;

    [Header("Ads")]
    [SerializeField]
    private HuaweiAdsControllerMenu adsControllerMenu;
    [SerializeField]
    private VoidEvent FreeCoinsRewardSuccessEvent;
    [SerializeField]
    private VoidEvent LoadFreeCoinsAdRecruitmentEvent;
    [SerializeField]
    private VoidEvent LoadFreeCoinsAdStoreEvent;

    [Header("Tutorial")]
    [SerializeField]
    private TutorialMetaGameController tutorialMetaGameController;
    [SerializeField]
    private VoidEvent TriggerMetaGameTutorialEvent;
    [SerializeField]
    private VoidEvent TriggerMetaGameRecruitmentTutorialEvent;
    [SerializeField] 
    private VoidEvent TriggerMetaGameIslandTutorialEvent;
    [SerializeField] 
    private VoidEvent TriggerMetaGameCrewTutorialEvent;
    [SerializeField] 
    private VoidEvent FreeRecruitmentTutorialEvent;

    [Header("Game services")]
    [SerializeField]
    private HuaweiGameServicesController servicesController;
    [SerializeField]
    private VoidEvent PlayerLoginEvent;
    [SerializeField]
    private BoolEvent LoginSuccessfulEvent;

    [Header("Leaderboards")]
    [SerializeField]
    private LeaderboardsController leaderboardsController;
    [SerializeField] 
    private VoidEvent OpenLeaderboardsEvent;
    [SerializeField] 
    private StringEvent RequestLeaderboardsDataEvent;
    [SerializeField]
    private BoolEvent LeaderboardsDataRetrievedEvent;
    [SerializeField] 
    private LeaderboardDataEvent PlayerInitialRankDataEvent;
    [SerializeField]
    private LeaderboardDataEvent PlayerRankDataEvent;
    [SerializeField] 
    private LeaderboardDataListEvent LeaderboardRankDataListEvent;
    [SerializeField]
    private StringEvent RequestPlayerScoreEvent;
    [SerializeField]
    private StringIntEvent SubmitHighScoreEvent;
    [SerializeField] 
    private VoidEvent ScoreSubmittedEvent;

    #endregion

    private void Awake()
    {
        // First loading data
        CatsDataSaveManager.Instance.LoadCatsSavedData();
        CurrencyDataSaveManager.Instance.LoadCurrencySavedData();
        CurrencyDataSaveManager.Instance.CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
        SettingsDataSaveManager.Instance.LoadSettingsSavedData();
        TutorialDataSaveManager.Instance.LoadTutorialSavedData();
        PurchasesDataSaveManager.Instance.RemoveAdsPurchasedEvent = RemoveAdsPurchasedEvent;
        PurchasesDataSaveManager.Instance.LoadPurchaseIAPSavedData();
        LeaderboardsDataSaveManager.Instance.LoadLeaderboardsSavedData();

        // Music and audio
        musicManager.TriggerIslandMusicEvent = TriggerIslandMusicEvent;
        musicManager.TriggerStoreMusicEvent = TriggerStoreMusicEvent;
        musicManager.SetMusicVolumeEvent = SetMusicVolumeEvent;
        musicManager.Initialize();

        soundsManager.UISoundScreenOpenEvent = UISoundScreenOpenEvent;
        soundsManager.UISoundScreenClosedEvent = UISoundScreenClosedEvent;
        soundsManager.UISoundButtonPressedEvent = UISoundButtonPressedEvent;
        soundsManager.UISoundButtonCancelEvent = UISoundButtonCancelEvent;
        soundsManager.UISoundTapEvent = UISoundTapEvent;
        soundsManager.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        soundsManager.TriggerCatSoundEvent = TriggerCatSoundEvent;
        soundsManager.TriggerShipSoundEvent = TriggerShipSoundEvent;
        soundsManager.TriggerUISoundEvent = TriggerUISoundEvent;
        soundsManager.Initialize();

        ambienceAudioManager.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        ambienceAudioManager.Initialize();

        // Vibration 
        VibrationController.Instance.Initialize();

        // Scene loader
        sceneLoaderManager.LoadCombatSceneEvent = LoadCombatSceneEvent;
        sceneLoaderManager.Initialize();

        // Settings
        settingsController.SetMusicVolumeEvent = SetMusicVolumeEvent;
        settingsController.SetSoundsVolumeEvent = SetSoundsVolumeEvent;
        settingsController.Initialize();

        // Virtual cameras
        virtualCameraControllerMainMenu.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
        virtualCameraControllerMainMenu.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
        virtualCameraControllerMainMenu.TriggerShipCameraEvent = TriggerShipCameraEvent;
        virtualCameraControllerMainMenu.CloseShipCameraEvent = CloseShipCameraEvent;
        virtualCameraControllerMainMenu.Initialize();

        // Main menu
        mainMenuController.CatSelectedEvent = CatSelectedEvent;
        mainMenuController.Initialize();

        // Currency panels
        for (int index = 0; index < pnl_currenciesList.Length; index++)
        {
            pnl_currenciesList[index].CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
            pnl_currenciesList[index].ShowSpentCurrencyEvent = ShowSpentCurrencyEvent;
            pnl_currenciesList[index].ShowRewardedCurrencyEvent = ShowRewardedCurrencyEvent;
            pnl_currenciesList[index].OpenStoreEvent = OpenStoreEvent;
            pnl_currenciesList[index].CurrencyUpdateAnimationFinishedEvent = CurrencyUpdateAnimationFinishedEvent;
            pnl_currenciesList[index].Initialize();
        }

        // Island
        islandCatsController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        islandCatsController.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
        islandCatsController.OpenSelectedCatOptionsEvent = OpenSelectedCatOptionsEvent;
        islandCatsController.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
        islandCatsController.CatSelectedEvent = CatSelectedEvent;
        islandCatsController.CatUpdatedEvent = CatUpdatedEvent;
        islandCatsController.OpenScreenEvent = OpenScreenEvent;
        islandCatsController.CloseShipCameraEvent = CloseShipCameraEvent;
        islandCatsController.TriggerShipCameraEvent = TriggerShipCameraEvent;
        islandCatsController.OpenShipOptionsEvent = OpenShipOptionsEvent;
        islandCatsController.OpenShipOptionsEvent = OpenShipOptionsEvent;
        islandCatsController.TriggerShipSoundEvent = TriggerShipSoundEvent;
        islandCatsController.TriggerCatSoundEvent = TriggerCatSoundEvent;
        islandCatsController.Initialize();

        // Cat crew management
        catCrewManagementController.SelectCatEvent = SelectCatEvent;
        catCrewManagementController.SelectShipSlotEvent = SelectShipSlotEvent;
        catCrewManagementController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        catCrewManagementController.OpenCatCrewManagementEvent = OpenCatCrewManagementEvent;
        catCrewManagementController.CatUpdatedEvent = CatUpdatedEvent;
        catCrewManagementController.OpenCatCrewManagementNoIDEvent = OpenCatCrewManagementNoIDEvent;
        catCrewManagementController.StartCombatEvent = StartCombatEvent;
        catCrewManagementController.TriggerCatSoundEvent = TriggerCatSoundEvent;
        catCrewManagementController.TriggerMetaGameCrewTutorialEvent = TriggerMetaGameCrewTutorialEvent;
        catCrewManagementController.Initialize();

        // Cat recruitment
        catRecruitmentController.OpenCatRecruitmentScreenEvent = OpenCatRecruitmentScreenEvent;
        catRecruitmentController.PurchaseCatalogueCatEvent = PurchaseCatalogueCatEvent;
        catRecruitmentController.PurchaseCatalogueSkinEvent = PurchaseCatalogueSkinEvent;
        catRecruitmentController.ShowSelectedCatInfoEvent = ShowSelectedItemEvent;
        catRecruitmentController.ShowSelectedSkinInfoEvent = ShowSelectedSkinInfoEvent;
        catRecruitmentController.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentController.OpenStoreEvent = OpenStoreEvent;
        catRecruitmentController.RemoveAdsPurchasedEvent = RemoveAdsPurchasedEvent;
        catRecruitmentController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        catRecruitmentController.SkinPurchasedEvent = SkinPurchasedEvent;
        catRecruitmentController.CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
        catRecruitmentController.ShowSpentCurrencyEvent = ShowSpentCurrencyEvent;
        catRecruitmentController.TriggerUISoundEvent = TriggerUISoundEvent;
        catRecruitmentController.TriggerMetaGameRecruitmentTutorialEvent = TriggerMetaGameRecruitmentTutorialEvent;
        catRecruitmentController.FreeRecruitmentTutorialEvent = FreeRecruitmentTutorialEvent;
        catRecruitmentController.TriggerMetaGameIslandTutorialEvent = TriggerMetaGameIslandTutorialEvent;
        catRecruitmentController.Initialize();

        // Cat options
        catOptionsController.OpenSelectedCatOptionsEvent = OpenSelectedCatOptionsEvent;
        catOptionsController.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
        catOptionsController.OpenCatCrewManagementEvent = OpenCatCrewManagementEvent;
        catOptionsController.OpenSkinManagementEvent = OpenSkinManagementEvent;
        catOptionsController.Initialize();

        // Skin management
        catSkinManagementController.OpenSkinManagementEvent = OpenSkinManagementEvent;
        catSkinManagementController.SelectSkinEvent = SelectSkinEvent;
        catSkinManagementController.CatUpdatedEvent = CatUpdatedEvent;
        catSkinManagementController.SkinPurchasedEvent = SkinPurchasedEvent;
        catSkinManagementController.TriggerCatSoundEvent = TriggerCatSoundEvent;
        catSkinManagementController.Initialize();

        // Ship options
        shipOptionsController.CombatData = combatData;
        shipOptionsController.OpenShipOptionsEvent = OpenShipOptionsEvent;
        shipOptionsController.CloseShipCameraEvent = CloseShipCameraEvent;
        shipOptionsController.LoadCombatSceneEvent = LoadCombatSceneEvent;
        shipOptionsController.OpenCatCrewManagementNoIDEvent = OpenCatCrewManagementNoIDEvent;
        shipOptionsController.StartCombatEvent = StartCombatEvent;
        shipOptionsController.Initialize();

        // Store
        storeController.StoreProductsListEvent = StoreProductsListEvent;
        storeController.StorePurchasesListEvent = StorePurchasesListEvent;
        storeController.OpenStoreEvent = OpenStoreEvent;
        storeController.PurchaseStoreItemEvent = PurchaseStoreItemEvent;
        storeController.TriggerUISoundEvent = TriggerUISoundEvent;
        storeController.LoadFreeCoinsAdStoreEvent = LoadFreeCoinsAdStoreEvent;
        storeController.ShowRewardedCurrencyEvent = ShowRewardedCurrencyEvent;
        storeController.PurchaseItemSuccesfulEvent = PurchaseItemSuccesfulEvent;
        storeController.PurchaseResultEvent = PurchaseResultEvent;
        storeController.ConsumedItemSuccesfulEvent = ConsumedItemSuccesfulEvent;
        storeController.RemoveAdsPurchasedEvent = RemoveAdsPurchasedEvent;
        storeController.Initialize();

        // Ads
        adsControllerMenu.FreeCoinsRewardSuccessEvent = FreeCoinsRewardSuccessEvent;
        adsControllerMenu.LoadFreeCoinsAdRecruitmentEvent = LoadFreeCoinsAdRecruitmentEvent;
        adsControllerMenu.LoadFreeCoinsAdStoreEvent = LoadFreeCoinsAdStoreEvent;
        adsControllerMenu.ShowRewardedCurrencyEvent = ShowRewardedCurrencyEvent;
        adsControllerMenu.Initialize();

        // Tutorial
        tutorialMetaGameController.TriggerMetaGameTutorialEvent = TriggerMetaGameTutorialEvent;
        tutorialMetaGameController.TriggerMetaGameRecruitmentTutorialEvent = TriggerMetaGameRecruitmentTutorialEvent;
        tutorialMetaGameController.TriggerMetaGameIslandTutorialEvent = TriggerMetaGameIslandTutorialEvent;
        tutorialMetaGameController.TriggerMetaGameCrewTutorialEvent = TriggerMetaGameCrewTutorialEvent;
        tutorialMetaGameController.FreeRecruitmentTutorialEvent = FreeRecruitmentTutorialEvent;
        tutorialMetaGameController.Initialize();

        // Init completed
        GameInitializationCompleted();

        // Result screen
        resultScreenController.TriggerUISoundEvent = TriggerUISoundEvent;
        resultScreenController.TriggerMetaGameTutorialEvent = TriggerMetaGameTutorialEvent;
        resultScreenController.Initialize();

        // Load purchased items
        PurchasesDataSaveManager.Instance.CallForPurchasedIAP();

        // Huawei login
        HuaweiAccountLoginManager.Instance.LoginSuccessfulEvent = LoginSuccessfulEvent;
        HuaweiAccountLoginManager.Instance.Initialize();

        // Game services
        servicesController.PlayerLoginEvent = PlayerLoginEvent;
        servicesController.RequestLeaderboardsDataEvent = RequestLeaderboardsDataEvent;
        servicesController.LeaderboardsDataRetrievedEvent = LeaderboardsDataRetrievedEvent;
        servicesController.PlayerInitialRankDataEvent = PlayerInitialRankDataEvent;
        servicesController.PlayerRankDataEvent = PlayerRankDataEvent;
        servicesController.LeaderboardRankDataListEvent = LeaderboardRankDataListEvent;
        servicesController.RequestPlayerScoreEvent = RequestPlayerScoreEvent;
        servicesController.SubmitHighScoreEvent = SubmitHighScoreEvent;
        servicesController.ScoreSubmittedEvent = ScoreSubmittedEvent;
        servicesController.Initialize();

        // Leaderboards
        leaderboardsController.PlayerLoginEvent = PlayerLoginEvent;
        leaderboardsController.LoginSuccessfulEvent = LoginSuccessfulEvent;
        leaderboardsController.OpenLeaderboardsEvent = OpenLeaderboardsEvent;
        leaderboardsController.RequestLeaderboardsDataEvent = RequestLeaderboardsDataEvent;
        leaderboardsController.LeaderboardsDataRetrievedEvent = LeaderboardsDataRetrievedEvent;
        leaderboardsController.PlayerInitialRankDataEvent = PlayerInitialRankDataEvent;
        leaderboardsController.PlayerRankDataEvent = PlayerRankDataEvent;
        leaderboardsController.LeaderboardRankDataListEvent = LeaderboardRankDataListEvent;
        leaderboardsController.RequestPlayerScoreEvent = RequestPlayerScoreEvent;
        leaderboardsController.SubmitHighScoreEvent = SubmitHighScoreEvent;
        leaderboardsController.ScoreSubmittedEvent = ScoreSubmittedEvent;
        leaderboardsController.Initialize();

        //PlayerLogin();

        GameAnalyticsController.Instance.GameFlowEvent("SceneLoaded:MainMenu");
    }

    private void GameInitializationCompleted()
    {
        TriggerIslandMusicEvent.Raise();
        CurrenciesUpdatedEvent.Raise();
    }
}
