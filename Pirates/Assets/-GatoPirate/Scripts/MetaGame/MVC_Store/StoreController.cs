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
    // IAP Events
    public ProductInfoListEvent StoreProductsListEvent { get; set; }
    public PurchaseInfoListEvent StorePurchasesListEvent { get; set; }
    public PurchaseInfoEvent PurchaseItemSuccesfulEvent { get; set; }
    public StringPurchaseInfoEvent PurchaseResultEvent { get; set; }
    public PurchaseInfoEvent ConsumedItemSuccesfulEvent { get; set; }

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

        iapController.StoreProductsListEvent = StoreProductsListEvent;
        iapController.StorePurchasesListEvent = StorePurchasesListEvent;
        iapController.PurchaseItemSuccesfulEvent = PurchaseItemSuccesfulEvent;
        iapController.PurchaseResultEvent = PurchaseResultEvent;
        iapController.ConsumedItemSuccesfulEvent = ConsumedItemSuccesfulEvent;
        iapController.Initialize();
    }

    #region Event callbacks
    private void StoreProductsListEventCallback(IList<ProductInfo> _productsList)
    {
        Debug.Log($"Products list received! {_productsList.Count}");
        int productIndex = -1;
        for (int index = 0; index < _productsList.Count; index++)
        {
            productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals(_productsList[index].ProductId));
            if (productIndex < 0)
                continue;

            storeItemViewList[productIndex].PurchaseStoreItemEvent = PurchaseStoreItemEvent;
            storeItemViewList[productIndex].SetStoreItemData(_productsList[index].Title,
                _productsList[index].Description, 
                $"{_productsList[index].Currency} {_productsList[index].Price}");
        }
    }

    private void StorePurchasesListEventCallback(IList<PurchaseInfo> _purchasesList)
    {
        Debug.Log($"Purchases list received! {_purchasesList.Count}");

        int purchaseIndex = -1;
        for (int index = 0; index < _purchasesList.Count; index++)
        {
            purchaseIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals(_purchasesList[index].ProductId));
            
            if (purchaseIndex < 0)
                continue;

            int consumableIndex = nonConsumableIDList.FindIndex(x => x.Equals(_purchasesList[index].ProductId));

            if (consumableIndex >= 0)
                storeItemViewList[purchaseIndex].SetAsPurchased();

            // Trigger event with purchased product id, to enable feature
        }
    }

    private void OpenStoreEventCallback(UnityAtoms.Void _item)
    {
        storeView.gameObject.SetActive(true);
    }

    private void PurchaseStoreItemEventCallback(string _storeItemID)
    {
        Debug.Log($"Purchasing {_storeItemID}");
        // Trigger purchase animation with IAP callback
        // Update the button with "Already owned" text
        iapController.PurchaseItem(_storeItemID);
    }

    private void PurchaseItemSuccesfulEventCallback(PurchaseInfo _purchaseInfo)
    {
        int nonConsumableIndex = nonConsumableIDList.FindIndex(x => x.Equals(_purchaseInfo.ProductId));

        if (nonConsumableIndex >= 0) 
        {
            int productIndex;

            switch (_purchaseInfo.ProductId)
            {
                case "remove_ads":
                    // TODO: Trigger event to not show ads anymore
                    productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("remove_ads"));
                    if (productIndex >= 0)
                    {
                        storeItemViewList[productIndex].PlayPurchasedAnimation();
                    }
                    break;
                case "the_treasure":
                    // TODO: Trigger event to not show ads anymore
                    // Give 15,000 coins (play animation)
                    productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("the_treasure"));
                    if (productIndex >= 0)
                    {
                        storeItemViewList[productIndex].PlayPurchasedAnimation();
                    }
                    break;
            }

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
                productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("coins_2500"));
                if (productIndex >= 0)
                {
                    storeItemViewList[productIndex].PlayPurchasedAnimation();
                }
                break;
            case "coins_10000":
                // Give 10,000 coins (play animation)
                productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("coins_10000"));
                if (productIndex >= 0)
                {
                    storeItemViewList[productIndex].PlayPurchasedAnimation();
                }
                break;
            case "coins_30000":
                // Give 30,000 coins (play animation)
                productIndex = storeItemViewList.FindIndex(x => x.StoreItemID.Equals("coins_30000"));
                if (productIndex >= 0)
                {
                    storeItemViewList[productIndex].PlayPurchasedAnimation();
                }
                break;
            case "coins_80000":
                // Give 80,000 coins (play animation)
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
