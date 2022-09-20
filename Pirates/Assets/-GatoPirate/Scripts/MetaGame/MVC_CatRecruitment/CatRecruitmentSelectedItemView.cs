using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

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
    [SerializeField]
    private Image img_currency;
    [SerializeField]
    private GameObject btn_purchaseItem;

    [Header("Item locked")]
    [SerializeField]
    private GameObject img_lockedOverlay;
    [SerializeField]
    private GameObject btn_goToStore;
    [SerializeField]
    private TextMeshProUGUI lbl_itemPrice2;

    public IntCatalogueTypeEvent PurchaseCatalogueItemEvent { get; set; }
    public IntCatalogueTypeEvent PurchaseCatalogueSkinEvent { get; set; }
    public VoidEvent OpenGoToStorePopUpEvent { get; set; }

    public int ItemIndex { get; set; }
    public ItemTier ItemTierType { get; set; }

    #region Data set
    public void SetItemData(int _itemIndex, ItemTier _itemTier)
    {
        ItemIndex = _itemIndex;
        ItemTierType = _itemTier;
    }

    public void SetItemLocked()
    {
        btn_purchaseItem.SetActive(false);
        btn_goToStore.SetActive(true);
        img_lockedOverlay.SetActive(true);
    }

    public void SetItemUnlocked()
    {
        btn_purchaseItem.SetActive(true);
        btn_goToStore.SetActive(false);
        img_lockedOverlay.SetActive(false);
    }

    public void ShowSelectedCatInfo(string _itemName, Sprite _itemSprite, string _itemDescription, int _itemPrice, Sprite _currencySprite)
    {
        lbl_itemName.text = _itemName;
        img_selectedItem.sprite = _itemSprite;
        lbl_itemDescription.text = _itemDescription;
        lbl_itemPrice.text = _itemPrice.ToString();
        img_currency.sprite = _currencySprite;

        gameObject.SetActive(true);
        // TODO: Fill data for not enough currency button
    }
    #endregion

    #region Buttons
    public void PurchaseItem()
    {
        PurchaseCatalogueItemEvent.Raise(ItemIndex, ItemTierType);
        gameObject.SetActive(false);
    }

    public void GoToStore()
    {
        OpenGoToStorePopUpEvent.Raise();
    }
    #endregion
}
