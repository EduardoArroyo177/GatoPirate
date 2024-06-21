using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UDP;

public class StoreController : MonoBehaviour
{
    [SerializeField]
    private StoreView storeView;
    [SerializeField]
    private List<StoreItemView> storeItemViewList;

    [Header("IAP")]
    [SerializeField]
    private IAPController iapController;
    [SerializeField]
    private List<string> nonConsumableIDList;

    public VoidEvent OpenStoreEvent { get; set; }
    public StringEvent PurchaseStoreItemEvent { get; set; }
    public UISoundsEvent TriggerUISoundEvent { get; set; }
    public VoidEvent LoadFreeCoinsAdStoreEvent { get; set; }
    public CurrencyTypeIntEvent ShowRewardedCurrencyEvent { get; set; }
    // IAP Events
    public ProductInfoListEvent StoreProductsListEvent { get; set; }
    public PurchaseInfoListEvent StorePurchasesListEvent { get; set; }
    public PurchaseInfoEvent PurchaseItemSuccesfulEvent { get; set; }
    public StringPurchaseInfoEvent PurchaseResultEvent { get; set; }
    public PurchaseInfoEvent ConsumedItemSuccesfulEvent { get; set; }
    public VoidEvent RemoveAdsPurchasedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenStoreEvent, OpenStoreEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(PurchaseStoreItemEvent, PurchaseStoreItemEventCallback));
        _eventHandlers.Add(EventHandlerFactory<IList<ProductInfo>>.BuildEventHandler(StoreProductsListEvent, StoreProductsListEventCallback));
        _eventHandlers.Add(EventHandlerFactory<IList<PurchaseInfo>>.BuildEventHandler(StorePurchasesListEvent, StorePurchasesListEventCallback));
        _eventHandlers.Add(EventHandlerFactory<PurchaseInfo>.BuildEventHandler(PurchaseItemSuccesfulEvent, PurchaseItemSuccesfulEventCallback));
        _eventHandlers.Add(EventHandlerFactory<PurchaseInfo>.BuildEventHandler(ConsumedItemSuccesfulEvent, ConsumedItemSuccesfulEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string,PurchaseInfo>.BuildEventHandler(PurchaseResultEvent, PurchaseResultEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(RemoveAdsPurchasedEvent, RemoveAdsPurchasedEventCallback));
        for (int i = 0; i < storeItemViewList.Count; i++)
        {
            storeItemViewList[i].PurchaseStoreItemEvent = PurchaseStoreItemEvent;
            storeItemViewList[i].TriggerUISoundEvent = TriggerUISoundEvent;
        }
        //iapController.StoreProductsListEvent = StoreProductsListEvent;
        //iapController.StorePurchasesListEvent = StorePurchasesListEvent;
        //iapController.PurchaseItemSuccesfulEvent = PurchaseItemSuccesfulEvent;
        //iapController.PurchaseResultEvent = PurchaseResultEvent;
        //iapController.ConsumedItemSuccesfulEvent = ConsumedItemSuccesfulEvent;
        //iapController.Initialize();
    }

    #region Event callbacks
    private void StoreProductsListEventCallback(IList<ProductInfo> _productsList)
    {
        int productIndex = -1;
        for (int index = 0; index < _productsList.Count; index++)
        {
            productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals(_productsList[index].ProductId));
            if (productIndex < 0)
                continue;

            storeItemViewList[productIndex].PurchaseStoreItemEvent = PurchaseStoreItemEvent;
            storeItemViewList[productIndex].TriggerUISoundEvent = TriggerUISoundEvent;
            storeItemViewList[productIndex].SetStoreItemData(_productsList[index].Title,
                _productsList[index].Description, 
                $"{_productsList[index].Currency} {_productsList[index].Price}");
        }

        // Free coins item
        productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("free_coins"));
        if (productIndex < 0)
            return;
        storeItemViewList[productIndex].PurchaseStoreItemEvent = PurchaseStoreItemEvent;
        storeItemViewList[productIndex].TriggerUISoundEvent = TriggerUISoundEvent;
    }

    private void StorePurchasesListEventCallback(IList<PurchaseInfo> _purchasesList)
    {
        int purchaseIndex = -1;
        for (int index = 0; index < _purchasesList.Count; index++)
        {
            // Remove Ads
            if (_purchasesList[index].ProductId.Equals("the_treasure")
                || _purchasesList[index].ProductId.Equals("remove_ads"))
            {
                purchaseIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("the_treasure"));
                if (purchaseIndex >= 0)
                    storeItemViewList[purchaseIndex].SetAsPurchased();

                purchaseIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("remove_ads"));
                if (purchaseIndex >= 0)
                    storeItemViewList[purchaseIndex].SetAsPurchased();

                // Trigger Save No Ads
                PurchasesDataSaveManager.Instance.UpdatePurchasedNonConsumable(NonConsumableIAP.REMOVE_ADS);
            }
            else
            {
                iapController.ConsumePurchasedItem(_purchasesList[index]);
            }
        }
    }

    private void OpenStoreEventCallback(UnityAtoms.Void _item)
    {
        storeView.gameObject.SetActive(true);
    }

    private void PurchaseStoreItemEventCallback(string _storeItemID)
    {
        //Debug.Log($"Purchasing {_storeItemID}");
        if (_storeItemID.Equals("free_coins"))
        {
            LoadFreeCoinsAdStoreEvent.Raise();
        }
        else
            iapController.PurchaseItem(_storeItemID);
    }

    private void PurchaseItemSuccesfulEventCallback(PurchaseInfo _purchaseInfo)
    {
        int nonConsumableIndex = nonConsumableIDList.FindIndex(x => x.Equals(_purchaseInfo.ProductId));

        if (nonConsumableIndex >= 0) 
        {
            // If remove ads or treasure is purchased, both items become unavailable
            int productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("remove_ads"));

            // Remove ads
            if (productIndex >= 0)
            {
                storeItemViewList[productIndex].PlayPurchasedAnimation();
                storeItemViewList[productIndex].SetAsPurchased();
            }
            // Treasure
            productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("the_treasure"));
            if (productIndex >= 0)
            {
                storeItemViewList[productIndex].PlayPurchasedAnimation();
                storeItemViewList[productIndex].SetAsPurchased();
            }

            if(_purchaseInfo.ProductId.Equals("the_treasure"))
                GiveCoins(15000);

            // Trigger Save No Ads
            PurchasesDataSaveManager.Instance.UpdatePurchasedNonConsumable(NonConsumableIAP.REMOVE_ADS);
        }
        else // Needs to be consumed
        {
            iapController.ConsumePurchasedItem(_purchaseInfo);
        }
    }

    private void ConsumedItemSuccesfulEventCallback(PurchaseInfo _purchaseInfo)
    {
        int productIndex;
        switch (_purchaseInfo.ProductId)
        {
            case "coins_2500":
                // Give 2500 coins (play animation)
                GiveCoins(2500);
                productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("coins_2500"));
                if (productIndex >= 0)
                {
                    storeItemViewList[productIndex].PlayPurchasedAnimation();
                }
                break;
            case "coins_10000":
                // Give 10,000 coins (play animation)
                GiveCoins(10000);
                productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("coins_10000"));
                if (productIndex >= 0)
                {
                    storeItemViewList[productIndex].PlayPurchasedAnimation();
                }
                break;
            case "coins_30000":
                // Give 30,000 coins (play animation)
                GiveCoins(30000);
                productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("coins_30000"));
                if (productIndex >= 0)
                {
                    storeItemViewList[productIndex].PlayPurchasedAnimation();
                }
                break;
            case "coins_80000":
                // Give 80,000 coins (play animation)
                GiveCoins(80000);
                productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("coins_80000"));
                if (productIndex >= 0)
                {
                    storeItemViewList[productIndex].PlayPurchasedAnimation();
                }
                break;
        }

    }

    private void PurchaseResultEventCallback(string _message, PurchaseInfo _purchaseInfo)
    {
        if (_purchaseInfo != null)
        {
            // Show pop up with message and id
        }
        else
        { 
            // Show pop up with message
        }
    }

    private void RemoveAdsPurchasedEventCallback(UnityAtoms.Void _item)
    {
        int index = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("free_coins"));
        if (index < 0)
            return;

        storeItemViewList[index].RemoveAdsPurchased();
    }
    #endregion

    private void GiveCoins(int _coinsAmnt)
    {
        CurrencyDataSaveManager.Instance.UpdateCurrency(CurrencyType.GOLDEN_COINS, _coinsAmnt);
        ShowRewardedCurrencyEvent.Raise(CurrencyType.GOLDEN_COINS, _coinsAmnt);
    }


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
