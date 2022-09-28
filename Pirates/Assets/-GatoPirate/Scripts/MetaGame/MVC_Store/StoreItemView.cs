using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private GameObject btn_purchase;
    [SerializeField]
    private GameObject pnl_purchased;

    public StringEvent PurchaseStoreItemEvent { get; set; }
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
        // Trigger sound
    }
    #endregion

    #region Button methods
    public void PurchaseItem()
    {
        PurchaseStoreItemEvent.Raise(StoreItemID);
    }
    #endregion

}
