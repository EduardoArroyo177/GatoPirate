using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MetaGameBootstrapper : MonoBehaviour
{
    [Header("Virtual cameras")]
    [SerializeField]
    private VirtualCameraControllerMainMenu virtualCameraControllerMainMenu;
    [SerializeField]
    private GameObjectEvent TriggerSelectedCatCameraEvent;
    [SerializeField]
    private VoidEvent CloseSelectedCatCameraEvent;

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
    private VoidEvent CloseRecruitmentViewEvent;
    [SerializeField]
    private VoidEvent OpenGoToStorePopUpEvent;
    [SerializeField]
    private VoidEvent OpenCrewManagementPopUpEvent;

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


    private void Awake()
    {
        // First loading data
        CatsDataSaveManager.Instance.LoadCatsSavedData();

        // Virtual cameras
        virtualCameraControllerMainMenu.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
        virtualCameraControllerMainMenu.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
        virtualCameraControllerMainMenu.Initialize();

        // Island
        islandCatsController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        islandCatsController.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
        islandCatsController.OpenSelectedCatOptionsEvent = OpenSelectedCatOptionsEvent;
        islandCatsController.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
        islandCatsController.CatSelectedEvent = CatSelectedEvent;
        islandCatsController.CatUpdatedEvent = CatUpdatedEvent;
        islandCatsController.Initialize();

        // Cat crew management
        catCrewManagementController.SelectCatEvent = SelectCatEvent;
        catCrewManagementController.SelectShipSlotEvent = SelectShipSlotEvent;
        catCrewManagementController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        catCrewManagementController.OpenCatCrewManagementEvent = OpenCatCrewManagementEvent;
        catCrewManagementController.CatUpdatedEvent = CatUpdatedEvent;
        catCrewManagementController.Initialize();

        // Cat recruitment
        catRecruitmentController.PurchaseCatalogueCatEvent = PurchaseCatalogueCatEvent;
        catRecruitmentController.PurchaseCatalogueSkinEvent = PurchaseCatalogueSkinEvent;
        catRecruitmentController.ShowSelectedCatInfoEvent = ShowSelectedItemEvent;
        catRecruitmentController.ShowSelectedSkinInfoEvent = ShowSelectedSkinInfoEvent;
        catRecruitmentController.CloseRecruitmentViewEvent = CloseRecruitmentViewEvent;
        catRecruitmentController.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentController.OpenCrewManagementPopUpEvent = OpenCrewManagementPopUpEvent;
        catRecruitmentController.NewCatPurchasedEvent = NewCatPurchasedEvent;
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
        catSkinManagementController.Initialize();
    }
}
