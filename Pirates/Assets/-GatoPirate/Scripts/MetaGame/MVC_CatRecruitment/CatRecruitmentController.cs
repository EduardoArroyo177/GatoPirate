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
    public IntCatalogueTypeEvent PurchaseCatalogueSkinEvent { get; set; }


    // Pop ups
    public VoidEvent OpenGoToStorePopUpEvent { get; set; }

    // Information view
    public IntCatalogueTypeEvent ShowSelectedCatInfoEvent { get; set; }
    public IntCatalogueTypeEvent ShowSelectedSkinInfoEvent { get; set; }

    // Island update
    public CatTypeIDEvent NewCatPurchasedEvent { get; set; }

    // Skin update
    public StringEvent SkinPurchasedEvent { get; set; }

    // Currency update
    public VoidEvent CurrenciesUpdatedEvent { get; set; }
    #endregion

    private List<IAtomEventHandler> _eventHandlers = new();
    // Cat catalogues
    private List<CatalogueItemView> catBasicItemList = new List<CatalogueItemView>();
    private List<CatalogueItemView> catSpecialItemList = new List<CatalogueItemView>();
    // Skin catalogues
    private List<CatalogueItemView> skinBasicItemList = new List<CatalogueItemView>();
    private List<CatalogueItemView> skinSpecialItemList = new List<CatalogueItemView>(); 
    private List<CatalogueItemView> skinPremiumItemList = new List<CatalogueItemView>();
    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(PurchaseCatalogueCatEvent, PurchaseCatalogueCatEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(PurchaseCatalogueSkinEvent, PurchaseCatalogueSkinEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(ShowSelectedCatInfoEvent, ShowSelectedItemEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(ShowSelectedSkinInfoEvent, ShowSelectedSkinInfoEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CurrenciesUpdatedEvent, CurrenciesUpdatedEventCallback));

        catRecruitmentPopUpsView.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
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
        int currencyAmount = 0;

        for (int index = 0; index < CatsModel.Instance.CatsDataList.Count; index++)
        {
            catDataHelper = CatsModel.Instance.CatsDataList[index];
            catItemViewHelper = Instantiate(catRecruitmentView.CatalogueItemView);

            catalogueCatItemViewHelper = catItemViewHelper.GetComponent<CatalogueItemView>();
            // Events
            catalogueCatItemViewHelper.PurchaseCatalogueItemEvent = PurchaseCatalogueCatEvent;
            catalogueCatItemViewHelper.ShowSelectedItemEvent = ShowSelectedCatInfoEvent;
            catalogueCatItemViewHelper.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
            // Setting data
            catalogueCatItemViewHelper.SetIndexAndTypes(index, catDataHelper.CatTier, catDataHelper.CatType);
            catalogueCatItemViewHelper.SetName(catDataHelper.CatName);
            catalogueCatItemViewHelper.SetDescription(catDataHelper.CatDescription);
            catalogueCatItemViewHelper.SetSprites(catDataHelper.CatPreviewSprite,
                 GeneralDataModel.Instance.GetCurrencySprite(catDataHelper.CurrencyType));
            catalogueCatItemViewHelper.SetPurchasePrice(catDataHelper.CatPrice);
            // Check if currency is enough to buy
            catalogueCatItemViewHelper.CurrencyType = catDataHelper.CurrencyType;
            currencyAmount = CurrencyDataSaveManager.Instance.GetCurrencyAmount(catDataHelper.CurrencyType);
            if (currencyAmount < catDataHelper.CatPrice)
            {
                catalogueCatItemViewHelper.SetItemLocked();
            }
            else
            {
                catalogueCatItemViewHelper.SetItemUnlocked();
            }

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
        // TODO: Check for skins that are already purchased
        GameObject catItemViewHelper;
        CatalogueItemView catalogueSkinItemViewHelper;
        CatSkinData skinDataHelper;
        int currencyAmount = 0;

        for (int index = 0; index < CatsModel.Instance.CatsSkinDataList.Count; index++)
        {
            skinDataHelper = CatsModel.Instance.CatsSkinDataList[index];
            catItemViewHelper = Instantiate(catRecruitmentView.CatalogueItemView);

            catalogueSkinItemViewHelper = catItemViewHelper.GetComponent<CatalogueItemView>();
            // Events
            catalogueSkinItemViewHelper.PurchaseCatalogueItemEvent = PurchaseCatalogueSkinEvent;
            catalogueSkinItemViewHelper.ShowSelectedItemEvent = ShowSelectedSkinInfoEvent;
            catalogueSkinItemViewHelper.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
            // Setting data
            catalogueSkinItemViewHelper.SetIndexAndTypes(index, skinDataHelper.SkinTier, skinDataHelper.SkinType);
            catalogueSkinItemViewHelper.SetName(skinDataHelper.SkinName);
            catalogueSkinItemViewHelper.SetDescription(skinDataHelper.SkinDescription);
            catalogueSkinItemViewHelper.SetSprites(skinDataHelper.SkinPreviewSprite,
                GeneralDataModel.Instance.GetCurrencySprite(skinDataHelper.CurrencyType));
            catalogueSkinItemViewHelper.SetPurchasePrice(skinDataHelper.SkinPrice);

            // Check if currency is enough to buy
            catalogueSkinItemViewHelper.CurrencyType = skinDataHelper.CurrencyType;
            currencyAmount = CurrencyDataSaveManager.Instance.GetCurrencyAmount(skinDataHelper.CurrencyType);
            if (currencyAmount < skinDataHelper.SkinPrice)
            {
                catalogueSkinItemViewHelper.SetItemLocked();
            }
            else
            {
                catalogueSkinItemViewHelper.SetItemUnlocked();
            }

            if (CatsDataSaveManager.Instance.IsSkinPurchased(skinDataHelper.SkinType.ToString()))
                catalogueSkinItemViewHelper.SetAsPurchased();

            if (skinDataHelper.SkinTier.Equals(ItemTier.BASIC))
            {
                catItemViewHelper.transform.SetParent(catRecruitmentView.SkinBasicCatalogueContent);
                skinBasicItemList.Add(catalogueSkinItemViewHelper);
            }
            else if (skinDataHelper.SkinTier.Equals(ItemTier.SPECIAL))
            {
                catItemViewHelper.transform.SetParent(catRecruitmentView.SkinSpecialCatalogueContent);
                skinSpecialItemList.Add(catalogueSkinItemViewHelper);
            }
            else if (skinDataHelper.SkinTier.Equals(ItemTier.PREMIUM))
            {
                catItemViewHelper.transform.SetParent(catRecruitmentView.SkinPremiumCatalogueContent);
                skinPremiumItemList.Add(catalogueSkinItemViewHelper);
            }
        }

        catCatalogueNavigationView.Initialize();
        skinCatalogueNavigationView.Initialize();
    }
    #endregion

    #region Event callbacks
    private void PurchaseCatalogueCatEventCallback(int _itemIndex, ItemTier _itemTier)
    {
        string itemName = "";
        CatType catType = CatType.GENERIC;
        int index;
        CurrencyType currencyType = CurrencyType.GOLDEN_COINS;
        int itemPrice = 0;
        switch (_itemTier)
        {
            case ItemTier.BASIC:
                index = catBasicItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = catBasicItemList[index].ItemName;
                catType = catBasicItemList[index].CatType;
                currencyType = catBasicItemList[index].CurrencyType;
                itemPrice = catBasicItemList[index].ItemPrice;
                break;
            case ItemTier.SPECIAL:
                index = catSpecialItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = catSpecialItemList[index].ItemName;
                catType = catSpecialItemList[index].CatType;
                currencyType = catSpecialItemList[index].CurrencyType;
                itemPrice = catSpecialItemList[index].ItemPrice;
                break;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("There was a problem with the item purchase");
            return;
        }
        // TODO: (if needed) Get island and its slot to save it, then call event to place it there
        // TODO: Reduce currency Amount with item price
        CurrencyDataSaveManager.Instance.UpdateCurrency(currencyType, -itemPrice);
        // Save cat data
        string catID = IDGenerator.Instance.GetGeneratedID(itemName);
        CatsDataSaveManager.Instance.SaveNewCat(catType, catID, itemName);
        //  Update cat island event
        NewCatPurchasedEvent.Raise(catType, catID);
        // TODO: Show purchased animation

    }

    private void PurchaseCatalogueSkinEventCallback(int _itemIndex, ItemTier _itemTier)
    {
        string itemName = "";
        SkinType skinType = SkinType.NONE;
        int index;
        switch (_itemTier)
        {
            case ItemTier.BASIC:
                index = skinBasicItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = skinBasicItemList[index].ItemName;
                skinType = skinBasicItemList[index].SkinType;
                skinBasicItemList[index].SetAsPurchased();
                break;
            case ItemTier.SPECIAL:
                index = skinSpecialItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = skinSpecialItemList[index].ItemName;
                skinType = skinSpecialItemList[index].SkinType;
                skinSpecialItemList[index].SetAsPurchased();
                break;
            case ItemTier.PREMIUM:
                index = skinPremiumItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = skinPremiumItemList[index].ItemName;
                skinType = skinPremiumItemList[index].SkinType;
                skinPremiumItemList[index].SetAsPurchased();
                break;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("There was a problem with the item purchase");
            return;
        }
        // TODO: (if needed) Get island and its slot to save it, then call event to place it there
        // TODO: Reduce currency Amount with item price
        // Save skin data
        CatsDataSaveManager.Instance.UnlockSkin(skinType);
        // Update skin management
        SkinPurchasedEvent.Raise(skinType.ToString());
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
        Sprite currencySprite = null;

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
                currencySprite = catBasicItemList[index].CurrencySprite;
                break;
            case ItemTier.SPECIAL:
                index = catSpecialItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = catSpecialItemList[index].ItemName;
                itemSprite = catSpecialItemList[index].ItemSprite;
                itemDescription = catSpecialItemList[index].ItemDescription;
                itemPrice = catSpecialItemList[index].ItemPrice;
                currencySprite = catSpecialItemList[index].CurrencySprite;
                break;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("There was a problem with the item information display");
            return;
        }

        catRecruitmentSelectedItemView.ShowSelectedCatInfo(itemName, itemSprite, itemDescription, itemPrice, currencySprite);
    }

    private void ShowSelectedSkinInfoEventCallback(int _itemIndex, ItemTier _itemType)
    {
        catRecruitmentSelectedItemView.SetItemData(_itemIndex, _itemType);

        int index;
        string itemName = "";
        Sprite itemSprite = null;
        string itemDescription = "";
        int itemPrice = -1;
        Sprite currencySprite = null;

        switch (_itemType)
        {
            case ItemTier.BASIC:
                index = skinBasicItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = skinBasicItemList[index].ItemName;
                itemSprite = skinBasicItemList[index].ItemSprite;
                itemDescription = skinBasicItemList[index].ItemDescription;
                itemPrice = skinBasicItemList[index].ItemPrice;
                currencySprite = skinBasicItemList[index].CurrencySprite;
                break;
            case ItemTier.SPECIAL:
                index = skinSpecialItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = skinSpecialItemList[index].ItemName;
                itemSprite = skinSpecialItemList[index].ItemSprite;
                itemDescription = skinSpecialItemList[index].ItemDescription;
                itemPrice = skinSpecialItemList[index].ItemPrice;
                currencySprite = skinSpecialItemList[index].CurrencySprite;
                break;
            case ItemTier.PREMIUM:
                index = skinPremiumItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                itemName = skinPremiumItemList[index].ItemName;
                itemSprite = skinPremiumItemList[index].ItemSprite;
                itemDescription = skinPremiumItemList[index].ItemDescription;
                itemPrice = skinPremiumItemList[index].ItemPrice;
                currencySprite = skinPremiumItemList[index].CurrencySprite;
                break;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("There was a problem with the item information display");
            return;
        }

        catRecruitmentSelectedItemView.ShowSelectedCatInfo(itemName, itemSprite, itemDescription, itemPrice, currencySprite);
    }

    private void CurrenciesUpdatedEventCallback(Void _item)
    {
        int currencyAmount = 0;
        // Cat basic items
        for (int index = 0; index < catBasicItemList.Count; index++)
        {
            currencyAmount = CurrencyDataSaveManager.Instance.GetCurrencyAmount(catBasicItemList[index].CurrencyType);
            if (currencyAmount < catBasicItemList[index].ItemPrice)
            {
                catBasicItemList[index].SetItemLocked();
            }
            else
            {
                catBasicItemList[index].SetItemUnlocked();
            }
        }

        // Cat special items
        for (int index = 0; index < catSpecialItemList.Count; index++)
        {
            currencyAmount = CurrencyDataSaveManager.Instance.GetCurrencyAmount(catSpecialItemList[index].CurrencyType);
            if (currencyAmount < catSpecialItemList[index].ItemPrice)
            {
                catSpecialItemList[index].SetItemLocked();
            }
            else
            {
                catSpecialItemList[index].SetItemUnlocked();
            }
        }

        // Skin basic items
        for (int index = 0; index < skinBasicItemList.Count; index++)
        {
            currencyAmount = CurrencyDataSaveManager.Instance.GetCurrencyAmount(skinBasicItemList[index].CurrencyType);
            if (currencyAmount < skinBasicItemList[index].ItemPrice)
            {
                skinBasicItemList[index].SetItemLocked();
            }
            else
            {
                skinBasicItemList[index].SetItemUnlocked();
            }
        }

        // Skin special items
        for (int index = 0; index < skinSpecialItemList.Count; index++)
        {
            currencyAmount = CurrencyDataSaveManager.Instance.GetCurrencyAmount(skinSpecialItemList[index].CurrencyType);
            if (currencyAmount < skinSpecialItemList[index].ItemPrice)
            {
                skinSpecialItemList[index].SetItemLocked();
            }
            else
            {
                skinSpecialItemList[index].SetItemUnlocked();
            }
        }

        // Skin premium items
        for (int index = 0; index < skinPremiumItemList.Count; index++)
        {
            currencyAmount = CurrencyDataSaveManager.Instance.GetCurrencyAmount(skinPremiumItemList[index].CurrencyType);
            if (currencyAmount < skinPremiumItemList[index].ItemPrice)
            {
                skinPremiumItemList[index].SetItemLocked();
            }
            else
            {
                skinPremiumItemList[index].SetItemUnlocked();
            }
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
