using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CatRecruitmentController : MonoBehaviour
{
    [SerializeField]
    private CatRecruitmentModel catRecruitmentModel;

    [Header("View references")]
    [SerializeField]
    private CatRecruitmentView catRecruitmentView;
    [SerializeField]
    private CatalogueNavigationView catCatalogueNavigationView;
    [SerializeField]
    private CatalogueNavigationView skinCatalogueNavigationView;
    [SerializeField]
    private CatRecruitmentPopUpsView catRecruitmentPopUpsView;
    [SerializeField]
    private CatRecruitmentSelectedItemView catRecruitmentSelectedItemView;

    #region Events
    public IntCatalogueTypeEvent PurchaseCatalogueCatEvent { get; set; }
    public IntCatalogueTypeEvent PurchasecatalogueSkinEvent { get; set; }


    // Pop ups
    public VoidEvent OpenGoToStorePopUpEvent { get; set; }
    public VoidEvent OpenCrewManagementPopUpEvent { get; set; }
    
    // Close view
    public VoidEvent CloseRecruitmentViewEvent { get; set; }

    // Information view
    public IntCatalogueTypeEvent ShowSelectedItemEvent { get; set; }

    // Island update
    public CatTypeIDEvent NewCatPurchasedEvent { get; set; }
    #endregion

    private List<IAtomEventHandler> _eventHandlers = new();
    private bool inventoryChanged;
    private List<CatalogueItemView> catBasicItemList = new List<CatalogueItemView>();
    private List<CatalogueItemView> catSpecialItemList = new List<CatalogueItemView>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(PurchaseCatalogueCatEvent, PurchaseCatalogueCatEventCallback));
        //_eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(PurchasecatalogueSkinEvent, PurchasecatalogueSkinEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseRecruitmentViewEvent, CloseRecruitmentViewEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(ShowSelectedItemEvent, ShowSelectedItemEventCallback));


        catRecruitmentPopUpsView.CloseRecruitmentViewEvent = CloseRecruitmentViewEvent;
        catRecruitmentPopUpsView.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentPopUpsView.OpenCrewManagementPopUpEvent = OpenCrewManagementPopUpEvent;
        catRecruitmentPopUpsView.Initialize();

        catRecruitmentSelectedItemView.PurchaseCatalogueItemEvent = PurchaseCatalogueCatEvent;

        FillCatCatalogueData();
        FillSkinCatalogueData();
    }

    #region Data initialization
    private void FillCatCatalogueData()
    {
        GameObject catItemViewHelper;
        CatalogueItemView catalogueCatItemViewHelper;
        CatData catDataHelper;

        for (int index = 0; index < CatsModel.Instance.CatsDataList.Count; index++)
        {
            catDataHelper = CatsModel.Instance.CatsDataList[index];
            catItemViewHelper = Instantiate(catRecruitmentView.CatCatalogueItemView);

            catalogueCatItemViewHelper = catItemViewHelper.GetComponent<CatalogueItemView>();
            // Events
            catalogueCatItemViewHelper.PurchaseCatalogueItemEvent = PurchaseCatalogueCatEvent;
            catalogueCatItemViewHelper.ShowSelectedItemEvent = ShowSelectedItemEvent;
            catalogueCatItemViewHelper.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
            // Setting data
            catalogueCatItemViewHelper.SetIndexAndTypes(index, catDataHelper.CatTier, catDataHelper.CatType);
            catalogueCatItemViewHelper.SetName(catDataHelper.CatName);
            catalogueCatItemViewHelper.SetDescription(catDataHelper.CatDescription);
            catalogueCatItemViewHelper.SetSprite(catDataHelper.CatPreviewSprite);
            catalogueCatItemViewHelper.SetPurchasePrice(catDataHelper.CatPrice);
            // TODO: Check if currency is enough to buy
            //if (catDataHelper.IsUnlocked)
            //    catalogueCatItemViewHelper.SetItemUnlocked();
            //else
            //    catalogueCatItemViewHelper.SetItemLocked();
            catalogueCatItemViewHelper.SetItemUnlocked();

            if (catDataHelper.CatTier.Equals(ItemTier.BASIC))
            {
                catItemViewHelper.transform.SetParent(catRecruitmentView.CatBasicCatalogueContent);
                catBasicItemList.Add(catalogueCatItemViewHelper);
            }
            else if (catDataHelper.CatTier.Equals(ItemTier.SPECIAL))
            {
                catItemViewHelper.transform.SetParent(catRecruitmentView.CatSpecialCatalogueContent);
                catSpecialItemList.Add(catalogueCatItemViewHelper);
            }
        }

        catCatalogueNavigationView.Initialize();
    }

    private void FillSkinCatalogueData()
    {
        skinCatalogueNavigationView.Initialize();
    }
    #endregion

    #region Event callbacks
    private void PurchaseCatalogueCatEventCallback(int _itemIndex, ItemTier _itemType)
    {
        inventoryChanged = true;
        string itemName = "";
        CatType catType = CatType.GENERIC;
        int index;
        switch (_itemType)
        {
            case ItemTier.BASIC:
                index = catBasicItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = catBasicItemList[index].ItemName;
                catType = catBasicItemList[index].CatType;
                break;
            case ItemTier.SPECIAL:
                index = catSpecialItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = catSpecialItemList[index].ItemName;
                catType = catSpecialItemList[index].CatType;
                break;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("There was a problem with the item purchase");
            return;
        }
        // TODO: (if needed) Get island and its slot to save it, then call event to place it there
        // TODO: Reduce currency amount with item price
        // Save cat data
        string catID = IDGenerator.Instance.GetGeneratedID(itemName);
        CatsDataSaveManager.Instance.SaveNewCat(catType, catID, itemName);
        //  Update cat island event
        NewCatPurchasedEvent.Raise(catType, catID);
        // TODO: Show purchased animation
    }

    private void PurchasecatalogueSkinEventCallback(int _itemIndex, ItemTier _itemType)
    { 
    
    }

    private void ShowSelectedItemEventCallback(int _itemIndex, ItemTier _itemType)
    {
        catRecruitmentSelectedItemView.SetItemData(_itemIndex, _itemType);

        int index;
        string itemName = "";
        Sprite itemSprite = null;
        string itemDescription = "";
        int itemPrice = -1;

        switch (_itemType)
        {
            case ItemTier.BASIC:
                index = catBasicItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = catBasicItemList[index].ItemName;
                itemSprite = catBasicItemList[index].ItemSprite;
                itemDescription = catBasicItemList[index].ItemDescription;
                itemPrice = catBasicItemList[index].ItemPrice;
                break;
            case ItemTier.SPECIAL:
                index = catSpecialItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = catSpecialItemList[index].ItemName;
                itemSprite = catSpecialItemList[index].ItemSprite;
                itemDescription = catSpecialItemList[index].ItemDescription;
                itemPrice = catSpecialItemList[index].ItemPrice;
                break;
            case ItemTier.SKIN_BASIC:
                break;
            case ItemTier.SKIN_SPECIAL:
                break;
            case ItemTier.SKIN_PREMIUM:
                break;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("There was a problem with the item information display");
            return;
        }

        catRecruitmentSelectedItemView.ShowSelectedCatInfo(itemName, itemSprite, itemDescription, itemPrice);
    }

    private void CloseRecruitmentViewEventCallback(Void _item)
    {
        // Check if there was a change in inventory to show pop up
        if (inventoryChanged)
        {
            inventoryChanged = false;
            OpenCrewManagementPopUpEvent.Raise();
        }
        else
        {
            catRecruitmentView.gameObject.SetActive(false);
        }
    }
    #endregion

    #region OnDestroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
