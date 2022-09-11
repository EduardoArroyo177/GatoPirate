using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StoreItemView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_itemTitle;
    [SerializeField]
    private TextMeshProUGUI lbl_itemDescription;
    [SerializeField]
    private TextMeshProUGUI lbl_itemPrice;

    public StringEvent PurchaseStoreItemEvent { get; set; }
    public string StoreItemID { get; set; }

    #region Data set
    public void SetStoreItemData(string _title, string _price)
    {
        lbl_itemTitle.text = _title;
        lbl_itemPrice.text = _price;
    }

    public void SetStoreItemData(string _title, string _description, string _price)
    {
        lbl_itemTitle.text = _title;
        lbl_itemDescription.text = _description;
        lbl_itemPrice.text = _price;
    }
    #endregion

    #region Button methods
    public void PurchaseItem()
    {
        PurchaseStoreItemEvent.Raise(StoreItemID);
    }
    #endregion

}
