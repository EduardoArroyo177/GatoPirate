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
    private CatRecruitmentNavigationView catRecruitmentNavigationView;
    [SerializeField]
    private CatalogueNavigationView catCatalogueNavigationView;
    [SerializeField]
    private CatalogueNavigationView skinCatalogueNavigationView;
    [SerializeField]
    private CatRecruitmentPopUpsView catRecruitmentPopUpsView;
    [SerializeField]
    private CatRecruitmentSelectedItemView catRecruitmentSelectedItemView;

    #region Events
    public VoidEvent OpenCatRecruitmentScreenEvent { get; set; }
    public IntCatalogueTypeEvent PurchaseCatalogueCatEvent { get; set; }
    public IntCatalogueTypeEvent PurchaseCatalogueSkinEvent { get; set; }


    // Pop ups
    public VoidEvent OpenGoToStorePopUpEvent { get; set; }
    public VoidEvent OpenStoreEvent { get; set; }
    public VoidEvent RemoveAdsPurchasedEvent { get; set; }

    // Information view
    public IntCatalogueTypeEvent ShowSelectedCatInfoEvent { get; set; }
    public IntCatalogueTypeEvent ShowSelectedSkinInfoEvent { get; set; }

    // Island update
    public CatTypeIDEvent NewCatPurchasedEvent { get; set; }

    // Skin update
    public StringEvent SkinPurchasedEvent { get; set; }

    // Currency update
    public VoidEvent CurrenciesUpdatedEvent { get; set; }
    public CurrencyTypeIntEvent ShowSpentCurrencyEvent { get; set; }

    // Sounds
    public UISoundsEvent TriggerUISoundEvent { get; set; }

    // Tutorial
    public VoidEvent TriggerMetaGameRecruitmentTutorialEvent { get; set; }
    public VoidEvent FreeRecruitmentTutorialEvent { get; set; }
    public VoidEvent TriggerMetaGameIslandTutorialEvent { get; set; }

    public VoidEvent UnloadEventsEvent { get; set; }



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
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenCatRecruitmentScreenEvent, OpenCatRecruitmentScreenEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(PurchaseCatalogueCatEvent, PurchaseCatalogueCatEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(PurchaseCatalogueSkinEvent, PurchaseCatalogueSkinEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(ShowSelectedCatInfoEvent, ShowSelectedCatEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int, ItemTier>.BuildEventHandler(ShowSelectedSkinInfoEvent, ShowSelectedSkinInfoEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CurrenciesUpdatedEvent, CurrenciesUpdatedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));

        catRecruitmentView.CatRecruitmentController = this;
       
        catRecruitmentPopUpsView.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentPopUpsView.OpenStoreEvent = OpenStoreEvent;
        catRecruitmentPopUpsView.RemoveAdsPurchasedEvent = RemoveAdsPurchasedEvent;
        catRecruitmentPopUpsView.Initialize();

        catRecruitmentSelectedItemView.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        
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

        // Check if free recruitment has been made
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.FREE_FIRST_RECRUITMENT))
            catBasicItemList[0].SetPurchasePrice(0);
    }

    private void FillSkinCatalogueData()
    {
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

            if (CatsDataSaveManager.Instance.IsSkinPurchased(skinDataHelper.SkinType.ToString()))
                catalogueSkinItemViewHelper.SetAsPurchased();

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
    private void OpenCatRecruitmentScreenEventCallback(Void _item)
    {
        catRecruitmentView.gameObject.SetActive(true);
        catRecruitmentNavigationView.SelectCatsMenu();

        // Check if tutorial hasn't been completed
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.META_GAME_RECRUITMENT))
            TriggerMetaGameRecruitmentTutorialEvent.Raise();
    }

    private void PurchaseCatalogueCatEventCallback(int _itemIndex, ItemTier _itemTier)
    {
        CatalogueItemView selectedItem = null;
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
                selectedItem = catBasicItemList[index];
                break;
            case ItemTier.SPECIAL:
                index = catSpecialItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                selectedItem = catSpecialItemList[index];
                break;
        }

        if (!selectedItem)
        {
            Debug.LogError("There was a problem with the item purchase");
            return;
        }

        itemName = selectedItem.ItemName;
        catType = selectedItem.CatType;
        currencyType = selectedItem.CurrencyType;
        itemPrice = selectedItem.ItemPrice;

        // TODO: (if needed) Get island and its slot to save it, then call event to place it there
        // Reduce currency Amount with item price
        CurrencyDataSaveManager.Instance.UpdateCurrency(currencyType, -itemPrice);
        // Play currency spend animation
        ShowSpentCurrencyEvent.Raise(currencyType, itemPrice);
        // Save cat data
        string catID = IDGenerator.Instance.GetGeneratedID(itemName);
        CatsDataSaveManager.Instance.SaveNewCat(catType, catID, itemName);
        //  Update cat island event
        NewCatPurchasedEvent.Raise(catType, catID);
        // Show purchased animation
        selectedItem.PlayPurchasedAnimation();
        // Play item purchased sound
        TriggerUISoundEvent.Raise(UISounds.STORE_ITEM_PURCHASED);
        // TODO: Play celebration sound

        // Tutorial
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.FREE_FIRST_RECRUITMENT))
        {
            if (itemPrice == 0) // Purchased the free cat
            {
                catBasicItemList[0].SetPurchasePrice(CatsModel.Instance.CatsDataList[0].CatPrice);
                FreeRecruitmentTutorialEvent.Raise();
            }
        }

        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.META_GAME_ISLAND))
            TutorialDataSaveManager.Instance.PurchasedCat = true;
    }

    private void PurchaseCatalogueSkinEventCallback(int _itemIndex, ItemTier _itemTier)
    {
        CatalogueItemView selectedItem = null;
        string itemName = "";
        SkinType skinType = SkinType.NONE;
        CurrencyType currencyType = CurrencyType.GOLDEN_COINS;
        int index;
        int itemPrice = 0;

        switch (_itemTier)
        {
            case ItemTier.BASIC:
                index = skinBasicItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                selectedItem = skinBasicItemList[index];
                break;
            case ItemTier.SPECIAL:
                index = skinSpecialItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                selectedItem = skinSpecialItemList[index];
                break;
            case ItemTier.PREMIUM:
                index = skinPremiumItemList.FindIndex(x => x.ItemIndex == _itemIndex);
                if (index < 0)
                    return;
                selectedItem = skinPremiumItemList[index];
                break;
        }

        if (!selectedItem)
        {
            Debug.LogError("There was a problem with the item purchase");
            return;
        }

        itemName = selectedItem.ItemName;
        skinType = selectedItem.SkinType;
        currencyType = selectedItem.CurrencyType;
        itemPrice = selectedItem.ItemPrice;
        // TODO: (if needed) Get island and its slot to save it, then call event to place it there
        selectedItem.SetAsPurchased();
        // Reduce currency Amount with item price
        CurrencyDataSaveManager.Instance.UpdateCurrency(currencyType, -itemPrice);
        ShowSpentCurrencyEvent.Raise(currencyType, itemPrice);
        // Save skin data
        CatsDataSaveManager.Instance.UnlockSkin(skinType);
        // Update skin management
        SkinPurchasedEvent.Raise(skinType.ToString());
        // Show purchased animation
        selectedItem.PlayPurchasedAnimation();
        // TODO: Play item purchased sound
        TriggerUISoundEvent.Raise(UISounds.STORE_ITEM_PURCHASED);
        // TODO: Play celebration sound
    }

    private void ShowSelectedCatEventCallback(int _itemIndex, ItemTier _itemType)
    {
        catRecruitmentSelectedItemView.SetItemData(_itemIndex, _itemType);
        catRecruitmentSelectedItemView.PurchaseCatalogueItemEvent = PurchaseCatalogueCatEvent;

        int index;
        string itemName = "";
        Sprite itemSprite = null;
        string itemDescription = "";
        int itemPrice = -1;
        Sprite currencySprite = null;
        bool isUnlocked = false;

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
                isUnlocked = catBasicItemList[index].ItemUnlocked;
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
                isUnlocked = catSpecialItemList[index].ItemUnlocked;
                break;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("There was a problem with the item information display");
            return;
        }

        catRecruitmentSelectedItemView.ShowSelectedCatInfo(itemName, itemSprite, itemDescription, itemPrice, currencySprite);
        if (isUnlocked)
            catRecruitmentSelectedItemView.SetItemUnlocked();
        else
            catRecruitmentSelectedItemView.SetItemLocked();
    }

    private void ShowSelectedSkinInfoEventCallback(int _itemIndex, ItemTier _itemType)
    {
        catRecruitmentSelectedItemView.SetItemData(_itemIndex, _itemType);
        catRecruitmentSelectedItemView.PurchaseCatalogueItemEvent = PurchaseCatalogueSkinEvent; ;

        int index;
        string itemName = "";
        Sprite itemSprite = null;
        string itemDescription = "";
        int itemPrice = -1;
        Sprite currencySprite = null;
        bool isUnlocked = false;

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
                isUnlocked = skinBasicItemList[index].ItemUnlocked;
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
                isUnlocked = skinSpecialItemList[index].ItemUnlocked;
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
                isUnlocked = skinPremiumItemList[index].ItemUnlocked;
                break;
        }

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("There was a problem with the item information display");
            return;
        }

        catRecruitmentSelectedItemView.ShowSelectedCatInfo(itemName, itemSprite, itemDescription, itemPrice, currencySprite);
        if (isUnlocked)
            catRecruitmentSelectedItemView.SetItemUnlocked();
        else
            catRecruitmentSelectedItemView.SetItemLocked();
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

    #region Pulbic methods
    public void ClosedRecruitmentView()
    {
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.META_GAME_ISLAND))
        {
            if(TutorialDataSaveManager.Instance.PurchasedCat)
                TriggerMetaGameIslandTutorialEvent.Raise();
        }
    }
    #endregion

    #region OnDestroy
    private void UnloadEventsEventCallback(Void _item)
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
        catRecruitmentPopUpsView.CleanListeners();
    }
    #endregion
}
