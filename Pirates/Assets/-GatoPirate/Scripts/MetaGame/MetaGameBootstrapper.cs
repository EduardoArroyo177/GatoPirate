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
    private CatSoundEvent TriggerCatSoundEvent;
    [SerializeField]
    private ShipSoundEvent TriggerShipSoundEvent;

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
    private ShipOptionsController shipOptionsController;
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

    [Header("Result screen")]
    [SerializeField]
    private MetaResultScreenController resultScreenController;
    #endregion

    private void Awake()
    {
        // First loading data
        CatsDataSaveManager.Instance.LoadCatsSavedData();
        CurrencyDataSaveManager.Instance.LoadCurrencySavedData();
        CurrencyDataSaveManager.Instance.CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
        SettingsDataSaveManager.Instance.LoadSettingsSavedData();

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
        soundsManager.Initialize();

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
            pnl_currenciesList[index].OpenStoreEvent = OpenStoreEvent;
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
        catCrewManagementController.Initialize();

        // Cat recruitment
        catRecruitmentController.PurchaseCatalogueCatEvent = PurchaseCatalogueCatEvent;
        catRecruitmentController.PurchaseCatalogueSkinEvent = PurchaseCatalogueSkinEvent;
        catRecruitmentController.ShowSelectedCatInfoEvent = ShowSelectedItemEvent;
        catRecruitmentController.ShowSelectedSkinInfoEvent = ShowSelectedSkinInfoEvent;
        catRecruitmentController.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        catRecruitmentController.SkinPurchasedEvent = SkinPurchasedEvent;
        catRecruitmentController.CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
        catRecruitmentController.ShowSpentCurrencyEvent = ShowSpentCurrencyEvent;
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
        storeController.OpenStoreEvent = OpenStoreEvent;
        storeController.PurchaseStoreItemEvent = PurchaseStoreItemEvent;
        storeController.Initialize();

        // Init completed
        GameInitializationCompleted();

        // Result screen
        resultScreenController.Initialize();
    }

    private void GameInitializationCompleted()
    {
        TriggerIslandMusicEvent.Raise();
        CurrenciesUpdatedEvent.Raise();
    }
}
