using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MetaGameBootstrapper : MonoBehaviour
{
    [Header("Combat data")]
    [SerializeField]
    private CombatData combatData;

    [Header("Music and Audio")]
    [SerializeField]
    private MusicManagerMainMenu musicManager;
    [SerializeField]
    private VoidEvent TriggerIslandMusicEvent;
    [SerializeField]
    private VoidEvent TriggerStoreMusicEvent;

    [Header("Scene loader")]
    [SerializeField]
    private SceneLoaderManager sceneLoaderManager;
    [SerializeField]
    private VoidEvent LoadCombatSceneEvent;

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
    

    private void Awake()
    {
        // First loading data
        CatsDataSaveManager.Instance.LoadCatsSavedData();

        // Music and audio
        musicManager.TriggerIslandMusicEvent = TriggerIslandMusicEvent;
        musicManager.TriggerStoreMusicEvent = TriggerStoreMusicEvent;
        musicManager.Initialize();

        // Scene loader
        sceneLoaderManager.LoadCombatSceneEvent = LoadCombatSceneEvent;
        sceneLoaderManager.Initialize();

        // Virtual cameras
        virtualCameraControllerMainMenu.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
        virtualCameraControllerMainMenu.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
        virtualCameraControllerMainMenu.TriggerShipCameraEvent = TriggerShipCameraEvent;
        virtualCameraControllerMainMenu.CloseShipCameraEvent = CloseShipCameraEvent;
        virtualCameraControllerMainMenu.Initialize();

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
        islandCatsController.Initialize();

        // Cat crew management
        catCrewManagementController.SelectCatEvent = SelectCatEvent;
        catCrewManagementController.SelectShipSlotEvent = SelectShipSlotEvent;
        catCrewManagementController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        catCrewManagementController.OpenCatCrewManagementEvent = OpenCatCrewManagementEvent;
        catCrewManagementController.CatUpdatedEvent = CatUpdatedEvent;
        catCrewManagementController.OpenCatCrewManagementNoIDEvent = OpenCatCrewManagementNoIDEvent;
        catCrewManagementController.Initialize();

        // Cat recruitment
        catRecruitmentController.PurchaseCatalogueCatEvent = PurchaseCatalogueCatEvent;
        catRecruitmentController.PurchaseCatalogueSkinEvent = PurchaseCatalogueSkinEvent;
        catRecruitmentController.ShowSelectedCatInfoEvent = ShowSelectedItemEvent;
        catRecruitmentController.ShowSelectedSkinInfoEvent = ShowSelectedSkinInfoEvent;
        catRecruitmentController.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        catRecruitmentController.SkinPurchasedEvent = SkinPurchasedEvent;
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
        shipOptionsController.Initialize();

        // Init completed
        GameInitializationCompleted();
    }

    private void GameInitializationCompleted()
    {
        TriggerIslandMusicEvent.Raise();
    }
}
