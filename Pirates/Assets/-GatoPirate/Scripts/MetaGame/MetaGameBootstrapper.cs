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

    private void Awake()
    {
        // First loading data
        CatsDataSaveManager.Instance.LoadCatsSavedData();

        // Vritual cameras
        virtualCameraControllerMainMenu.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
        virtualCameraControllerMainMenu.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
        virtualCameraControllerMainMenu.Initialize();

        // Island
        islandCatsController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        islandCatsController.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
        islandCatsController.Initialize();

        // Cat crew management
        catCrewManagementController.SelectCatEvent = SelectCatEvent;
        catCrewManagementController.SelectShipSlotEvent = SelectShipSlotEvent;
        catCrewManagementController.NewCatPurchasedEvent = NewCatPurchasedEvent;
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
    }
}
