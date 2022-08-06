using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityAtoms;

public class CatRecruitmentSelectedItemView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_itemName;
    [SerializeField]
    private Image img_selectedItem;
    [SerializeField]
    private TextMeshProUGUI lbl_itemDescription;
    [SerializeField]
    private TextMeshProUGUI lbl_itemPrice;

    public IntCatalogueTypeEvent PurchaseCatalogueItemEvent { get; set; }

    public int ItemIndex { get; set; }
    public ItemTier ItemType { get; set; }

    #region Data set
    public void SetItemData(int _itemIndex, ItemTier _itemType)
    {
        ItemIndex = _itemIndex;
        ItemType = _itemType;
    }

    public void ShowSelectedCatInfo(string _itemName, Sprite _itemSprite, string _itemDescription, int _itemPrice)
    {
        lbl_itemName.text = _itemName;
        img_selectedItem.sprite = _itemSprite;
        lbl_itemDescription.text = _itemDescription;
        lbl_itemPrice.text = _itemPrice.ToString();
        gameObject.SetActive(true);
        // TODO: Fill data for not enough currency button
        // TODO: Ask for currency to update sprite
    }
    #endregion

    #region Buttons
    public void PurchaseItem()
    {
        PurchaseCatalogueItemEvent.Raise(ItemIndex, ItemType);
        gameObject.SetActive(false);
    }

    public void GoToStore()
    { 
        
    }
    #endregion
}
