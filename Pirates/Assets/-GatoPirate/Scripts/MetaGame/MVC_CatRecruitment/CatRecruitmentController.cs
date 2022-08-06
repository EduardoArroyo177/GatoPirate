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
    private CatRecruitmentPopUpsView catRecruitmentPopUpsView;
    [SerializeField]
    private CatRecruitmentSelectedItemView catRecruitmentSelectedItemView;

    #region Events
    public IntCatalogueTypeEvent PurchaseCatalogueItemEvent { get; set; }
    
    // Pop ups
    public VoidEvent OpenGoToStorePopUpEvent { get; set; }
    public VoidEvent OpenCrewManagementPopUpEvent { get; set; }
    
    // Close view
    public VoidEvent CloseRecruitmentViewEvent { get; set; }

    // Information view
    public IntCatalogueTypeEvent ShowSelectedItemEvent { get; set; }

    // Island update
    public VoidEvent UpdateIslandCatsEvent { get; set; }
    #endregion

    private List<IAtomEventHandler> _eventHandlers = new();
    private bool inventoryChanged;
    private List<CatalogueItemView> catBasicItemList = new List<CatalogueItemView>();
    private List<CatalogueItemView> catSpecialItemList = new List<CatalogueItemView>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(PurchaseCatalogueItemEvent, PurchaseCatalogueItemEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseRecruitmentViewEvent, CloseRecruitmentViewEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(ShowSelectedItemEvent, ShowSelectedItemEventCallback));


        catRecruitmentPopUpsView.CloseRecruitmentViewEvent = CloseRecruitmentViewEvent;
        catRecruitmentPopUpsView.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentPopUpsView.OpenCrewManagementPopUpEvent = OpenCrewManagementPopUpEvent;
        catRecruitmentPopUpsView.Initialize();

        catRecruitmentSelectedItemView.PurchaseCatalogueItemEvent = PurchaseCatalogueItemEvent;

        FillCatCatalogueData();
        // Fill Cat Skins catalogue data
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
            catalogueCatItemViewHelper.PurchaseCatalogueItemEvent = PurchaseCatalogueItemEvent;
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

        //for (int index = 0; index < catRecruitmentModel.CatBasicCatalogueList.Length; index++)
        //{
        //    catVisualizationHelper = catRecruitmentModel.CatBasicCatalogueList[index];
        //    catItemViewHelper = Instantiate(catRecruitmentView.CatCatalogueItemView);
        //    catItemViewHelper.transform.SetParent(catRecruitmentView.CatBasicCatalogueContent);

        //    catalogueCatItemViewHelper = catItemViewHelper.GetComponent<CatalogueItemView>();

        //    // Events
        //    catalogueCatItemViewHelper.PurchaseCatalogueItemEvent = PurchaseCatalogueItemEvent;
        //    catalogueCatItemViewHelper.ShowSelectedItemEvent = ShowSelectedItemEvent;
        //    catalogueCatItemViewHelper.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        //    // Setting data
        //    catalogueCatItemViewHelper.SetIndexAndTypes(index, ItemTier.CAT_BASIC, catVisualizationHelper.CatType);
        //    catalogueCatItemViewHelper.SetName(catVisualizationHelper.ItemName);
        //    catalogueCatItemViewHelper.SetDescription(catVisualizationHelper.ItemDescription);
        //    catalogueCatItemViewHelper.SetSprite(catVisualizationHelper.ItemSprite);
        //    catalogueCatItemViewHelper.SetPurchasePrice(catVisualizationHelper.ItemPrice);

        //    // TODO: Check if currency is enough to buy
        //    if (catVisualizationHelper.IsUnlocked)
        //        catalogueCatItemViewHelper.SetItemUnlocked();
        //    else
        //        catalogueCatItemViewHelper.SetItemLocked();

        //    catBasicItemList.Add(catalogueCatItemViewHelper);
        //}

        //// Premium cats
        //for (int index = 0; index < catRecruitmentModel.CatSpecialCatalogueList.Length; index++)
        //{
        //    catVisualizationHelper = catRecruitmentModel.CatSpecialCatalogueList[index];
        //    catItemViewHelper = Instantiate(catRecruitmentView.CatCatalogueItemView);
        //    catItemViewHelper.transform.SetParent(catRecruitmentView.CatSpecialCatalogueContent);

        //    catalogueCatItemViewHelper = catItemViewHelper.GetComponent<CatalogueItemView>();
        //    // Events
        //    catalogueCatItemViewHelper.PurchaseCatalogueItemEvent = PurchaseCatalogueItemEvent;
        //    catalogueCatItemViewHelper.ShowSelectedItemEvent = ShowSelectedItemEvent;
        //    catalogueCatItemViewHelper.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        //    // Setting data
        //    catalogueCatItemViewHelper.SetIndexAndTypes(index, ItemTier.CAT_SPECIAL, catVisualizationHelper.CatType);
        //    catalogueCatItemViewHelper.SetName(catVisualizationHelper.ItemName);
        //    catalogueCatItemViewHelper.SetDescription(catVisualizationHelper.ItemDescription);
        //    catalogueCatItemViewHelper.SetSprite(catVisualizationHelper.ItemSprite);
        //    catalogueCatItemViewHelper.SetPurchasePrice(catVisualizationHelper.ItemPrice);

        //    // TODO: Check if currency is enough to buy
        //    if (catVisualizationHelper.IsUnlocked)
        //        catalogueCatItemViewHelper.SetItemUnlocked();
        //    else
        //        catalogueCatItemViewHelper.SetItemLocked();

        //    catSpecialItemList.Add(catalogueCatItemViewHelper);
        //}

    }
    #endregion

    #region Event callbacks
    private void PurchaseCatalogueItemEventCallback(int _itemIndex, ItemTier _itemType)
    {
        inventoryChanged = true;
        string itemName = "";
        Cats catType = Cats.GENERIC;
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
            case ItemTier.SKIN_BASIC:
                break;
            case ItemTier.SKIN_SPECIAL:
                break;
            case ItemTier.SKIN_PREMIUM:
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
        if(!catType.Equals(Cats.GENERIC)) // This is a cat
            CatsDataSaveManager.Instance.SaveNewCat(catType, IDGenerator.Instance.GetGeneratedID(itemName), itemName);
        // else save skin purchased
            
        // TODO: Update cat island event
        // TODO: Show purchased animation
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
            UpdateIslandCatsEvent.Raise();
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
