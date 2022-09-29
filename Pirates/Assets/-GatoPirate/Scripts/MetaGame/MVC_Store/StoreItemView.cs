using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StoreItemView : MonoBehaviour
{
    [SerializeField]
    private string storeItemID;
    [SerializeField]
    private TextMeshProUGUI lbl_itemTitle;
    [SerializeField]
    private TextMeshProUGUI lbl_itemDescription;
    [SerializeField]
    private TextMeshProUGUI lbl_itemPrice;

    [Header("Purchased")]
    [SerializeField]
    private GameObject img_purchasedBg;
    [SerializeField]
    private GameObject btn_purchase;
    [SerializeField]
    private GameObject pnl_purchased;

    [Header("No ads")]
    [SerializeField]
    private GameObject pnl_watchAd;
    [SerializeField]
    private GameObject pnl_freeCoins;
    [SerializeField]
    private TextMeshProUGUI lbl_freeCoins;

    public StringEvent PurchaseStoreItemEvent { get; set; }
    public UISoundsEvent TriggerUISoundEvent { get; set; }
    public string StoreItemID { get => storeItemID;}

    #region Data set
    public void SetStoreItemData(string _title, string _price)
    {
        lbl_itemTitle.text = _title;
        lbl_itemPrice.text = _price;
    }

    public void SetStoreItemData(string _title, string _description, string _price)
    {
        lbl_itemTitle.text = _title;
        if(lbl_itemDescription)
            lbl_itemDescription.text = _description;
        lbl_itemPrice.text = _price;
    }

    public void SetAsPurchased()
    { 
        // Hide purchase button
        btn_purchase.SetActive(false);
        // Show lbl "Purchased"
        pnl_purchased.SetActive(true);
    }

    public void PlayPurchasedAnimation()
    {
        // Show animation
        img_purchasedBg.SetActive(true);
        // Trigger sound
        TriggerUISoundEvent.Raise(UISounds.STORE_ITEM_PURCHASED);
    }

    public void RemoveAdsPurchased()
    {
        pnl_watchAd.SetActive(false);
        pnl_freeCoins.SetActive(true);
    }

    public void UpdateRemainingFreeCalls(int _remainingCalls)
    {
        lbl_freeCoins.text = $"Free coins! x{_remainingCalls}";
    }
    #endregion

    #region Button methods
    public void PurchaseItem()
    {
        PurchaseStoreItemEvent.Raise(StoreItemID);
    }
    #endregion

}
