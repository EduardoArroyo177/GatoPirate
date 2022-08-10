using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MetaGameBootstrapper : MonoBehaviour
{
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
    private VoidEvent CloseRecruitmentViewEvent;
    [SerializeField]
    private VoidEvent OpenGoToStorePopUpEvent;
    [SerializeField]
    private VoidEvent OpenCrewManagementPopUpEvent;

    private void Awake()
    {
        // First loading data
        CatsDataSaveManager.Instance.LoadCatsSavedData();

        // Island
        islandCatsController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        islandCatsController.Initialize();

        // Cat crew management
        catCrewManagementController.SelectCatEvent = SelectCatEvent;
        catCrewManagementController.SelectShipSlotEvent = SelectShipSlotEvent;
        catCrewManagementController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        catCrewManagementController.Initialize();

        // Cat recruitment
        catRecruitmentController.PurchaseCatalogueCatEvent = PurchaseCatalogueCatEvent;
        catRecruitmentController.ShowSelectedItemEvent = ShowSelectedItemEvent;
        catRecruitmentController.CloseRecruitmentViewEvent = CloseRecruitmentViewEvent;
        catRecruitmentController.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentController.OpenCrewManagementPopUpEvent = OpenCrewManagementPopUpEvent;
        catRecruitmentController.NewCatPurchasedEvent = NewCatPurchasedEvent;
        catRecruitmentController.Initialize();
    }
}
