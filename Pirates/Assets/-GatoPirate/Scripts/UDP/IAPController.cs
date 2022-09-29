using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UDP;

public class IAPController : MonoBehaviour
{
    public ProductInfoListEvent StoreProductsListEvent { get; set; }
    public PurchaseInfoListEvent StorePurchasesListEvent { get; set; }
    public PurchaseInfoEvent PurchaseItemSuccesfulEvent { get; set; }
    public PurchaseInfoEvent ConsumedItemSuccesfulEvent { get; set; }
    public StringPurchaseInfoEvent PurchaseResultEvent { get; set; }

    private InitListener listener;
    private PurchaseListener purchaseListener;
    public void Initialize()
    {
        listener = new InitListener(StoreProductsListEvent, StorePurchasesListEvent);
        StoreService.Initialize(listener);
        purchaseListener = new PurchaseListener(null, null, PurchaseItemSuccesfulEvent,
            PurchaseResultEvent, ConsumedItemSuccesfulEvent);
    }

    public void PurchaseItem(string _productID)
    {
        StoreService.Purchase(_productID, "", purchaseListener);
    }

    public void ConsumePurchasedItem(PurchaseInfo _purchaseInfo)
    {
        StoreService.ConsumePurchase(_purchaseInfo, purchaseListener);
    }
}

public class InitListener : IInitListener
{
    private ProductInfoListEvent StoreProductsListEvent;
    private PurchaseInfoListEvent StorePurchasesListEvent;

    public InitListener(ProductInfoListEvent _productListEvent = null,
        PurchaseInfoListEvent _purchaseListEvent = null)
    {
        StoreProductsListEvent = _productListEvent;
        StorePurchasesListEvent = _purchaseListEvent;
    }

    public void OnInitialized(UserInfo userInfo)
    {
        Debug.Log("Initialization succeeded");
        // You can call the QueryInventory method here
        // to check whether there are purchases that haven’t be consumed.
        StoreService.QueryInventory(new PurchaseListener(StoreProductsListEvent,
            StorePurchasesListEvent));
    }

    public void OnInitializeFailed(string message)
    {
        Debug.Log("Initialization failed: " + message);
    }
}

public class PurchaseListener : IPurchaseListener
{
    private ProductInfoListEvent StoreProductsListEvent;
    private PurchaseInfoListEvent StorePurchasesListEvent;
    private PurchaseInfoEvent PurchaseItemSuccesfulEvent;
    private StringPurchaseInfoEvent PurchaseResultEvent;
    private PurchaseInfoEvent ConsumedItemSuccesfulEvent;

    public PurchaseListener(ProductInfoListEvent _productListEvent = null,
        PurchaseInfoListEvent _purchaseListEvent = null,
        PurchaseInfoEvent _purchaseItemEvent = null,
        StringPurchaseInfoEvent _purchaseResultEvent = null,
        PurchaseInfoEvent _consumedItemSuccesfulEvent = null)
    {
        StoreProductsListEvent = _productListEvent;
        StorePurchasesListEvent = _purchaseListEvent;
        PurchaseItemSuccesfulEvent = _purchaseItemEvent;
        PurchaseResultEvent = _purchaseResultEvent;
        ConsumedItemSuccesfulEvent = _consumedItemSuccesfulEvent;
    }

    public void OnPurchase(PurchaseInfo purchaseInfo)
    {
        // The purchase has succeeded.
        // If the purchased product is consumable, you should consume it here.
        // Otherwise, deliver the product.
        Debug.Log($"Purchase succeded");
        // Check if purchase is consumable
        PurchaseItemSuccesfulEvent?.Raise(purchaseInfo);

    }

    public void OnPurchaseFailed(string message, PurchaseInfo purchaseInfo)
    {
        Debug.Log("Purchase Failed: " + message);
        PurchaseResultEvent?.Raise(message, purchaseInfo);
    }

    public void OnPurchaseRepeated(string productCode)
    {
        // Some stores don't support queryInventory.

    }

    public void OnPurchaseConsume(PurchaseInfo purchaseInfo)
    {
        // The consumption succeeded.
        // You should deliver the product here.
        Debug.Log("Purchase consume success");
        // Trigger event to give reward
        ConsumedItemSuccesfulEvent?.Raise(purchaseInfo);
    }

    public void OnPurchaseConsumeFailed(string message, PurchaseInfo purchaseInfo)
    {
        // The consumption failed.
        PurchaseResultEvent?.Raise(message, purchaseInfo);

    }

    public void OnQueryInventory(Inventory inventory)
    {
        // Querying inventory succeeded.
        IList<ProductInfo> ProductInfoList = inventory.GetProductList();
        StoreProductsListEvent?.Raise(ProductInfoList);

        IList<PurchaseInfo> PurchaseInfoList = inventory.GetPurchaseList();
        StorePurchasesListEvent?.Raise(PurchaseInfoList);

       // IDictionary<string, PurchaseInfo> purchaseDicc = inventory.GetPurchaseDictionary();

    }

    public void OnQueryInventoryFailed(string message)
    {
        // Querying inventory failed.
        PurchaseResultEvent?.Raise(message, null);

    }

    public void OnPurchasePending(string message, PurchaseInfo purchaseInfo)
    {
        PurchaseResultEvent?.Raise(message, purchaseInfo);

    }
}
