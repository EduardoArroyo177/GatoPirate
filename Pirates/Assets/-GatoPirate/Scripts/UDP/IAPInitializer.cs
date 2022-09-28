using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UDP;

public class IAPInitializer : MonoBehaviour
{
    public string productID = "no_ads";

    InitListener listener;
    PurchaseListener purchaseListener;
    // Start is called before the first frame update
    void Start()
    {
        listener = new InitListener();
        StoreService.Initialize(listener);

        purchaseListener = new PurchaseListener();
    }

    public void PurchaseProduct()
    {
        PurchaseInfo purchaseInfo = new PurchaseInfo();
        purchaseInfo.ProductId = productID;
        //StoreService.ConsumePurchase(purchaseInfo, purchaseListener);
        StoreService.Purchase(productID, "", purchaseListener);
    }

}

//public class InitListener : IInitListener
//{
//    public void OnInitialized(UserInfo userInfo)
//    {
//        Debug.Log("Initialization succeeded");
//        // You can call the QueryInventory method here
//        // to check whether there are purchases that haven’t be consumed.
//        StoreService.QueryInventory(new PurchaseListener());
//    }

//    public void OnInitializeFailed(string message)
//    {
//        Debug.Log("Initialization failed: " + message);
//    }
//}

//public class PurchaseListener : IPurchaseListener
//{
//    public void OnPurchase(PurchaseInfo purchaseInfo)
//    {
//        // The purchase has succeeded.
//        // If the purchased product is consumable, you should consume it here.
//        // Otherwise, deliver the product.
//        Debug.Log("PURCHASE SUCCEDED");
//    }

//    public void OnPurchaseFailed(string message, PurchaseInfo purchaseInfo)
//    {
//        Debug.Log("Purchase Failed: " + message);
//    }

//    public void OnPurchaseRepeated(string productCode)
//    {
//        // Some stores don't support queryInventory.

//    }

//    public void OnPurchaseConsume(PurchaseInfo purchaseInfo)
//    {
//        // The consumption succeeded.
//        // You should deliver the product here.
//        Debug.Log("PURCHASE CONSUMED");
//    }

//    public void OnPurchaseConsumeFailed(string message, PurchaseInfo purchaseInfo)
//    {
//        // The consumption failed.
//    }

//    public void OnQueryInventory(Inventory inventory)
//    {
//        // Querying inventory succeeded.
//        IList<ProductInfo> ProductInfoList = inventory.GetProductList();
//        for (int index = 0; index < ProductInfoList.Count; index++)
//        {
//            Debug.Log($"PRODUCT NAME {ProductInfoList[index].Title} PRICE" +
//                $" {ProductInfoList[index].Price}" +
//                $" {ProductInfoList[index].ProductId}" +
//                $" {ProductInfoList[index].Currency}");
//        }
//    }

//    public void OnQueryInventoryFailed(string message)
//    {
//        // Querying inventory failed.
//    }

//    public void OnPurchasePending(string message, PurchaseInfo purchaseInfo)
//    {
//        //
//    }
//}


