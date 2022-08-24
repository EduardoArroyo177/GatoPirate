using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class CatalogueItemView : MonoBehaviour
{
    [Header("Item references")]
    [SerializeField]
    private Image img_item;
    [SerializeField]
    private TextMeshProUGUI lbl_itemName;
    [SerializeField]
    private Button btn_purchaseItem;
    [SerializeField]
    private TextMeshProUGUI lbl_purchasePrice;
    [SerializeField]
    private GameObject pnl_purchasedItem;
    [SerializeField]
    private Image img_currencyIcon;

    [Header("Item locked")]
    [SerializeField]
    private GameObject img_lockedOverlay;
    [SerializeField]
    private GameObject btn_goToStore;
    [SerializeField]
    private TextMeshProUGUI lbl_purchasePrice2;

    // Events
    public IntCatalogueTypeEvent PurchaseCatalogueItemEvent { get; set; }
    public VoidEvent OpenGoToStorePopUpEvent { get; set; }
    public IntCatalogueTypeEvent ShowSelectedItemEvent { get; set; }

    // Set item catalogue type
    public int ItemIndex { get; set; }
    public ItemTier ItemType { get; set; }
    public CatType CatType { get; set; }
    public SkinType SkinType { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public int ItemPrice { get; set; }
    public Sprite ItemSprite { get; set; }
    public Sprite CurrencySprite { get; set; }
    public CurrencyType CurrencyType { get; set; }

    #region Data set
    // For cats
    public void SetIndexAndTypes(int _itemIndex, ItemTier _itemType, CatType _catType = CatType.GENERIC)
    {
        ItemIndex = _itemIndex;
        ItemType = _itemType;
        CatType = _catType;
    }
    // For skins
    public void SetIndexAndTypes(int _itemIndex, ItemTier _itemType, SkinType _skinType = SkinType.NONE)
    {
        ItemIndex = _itemIndex;
        ItemType = _itemType;
        SkinType = _skinType;
    }

    public void SetName(string _itemName)
    {
        ItemName = _itemName; 
        lbl_itemName.text = _itemName;
    }

    public void SetDescription(string _itemDescription)
    {
        ItemDescription = _itemDescription;
    }

    public void SetSprites(Sprite _itemSprite, Sprite _currencySprite)
    {
        ItemSprite = _itemSprite;
        CurrencySprite = _currencySprite;
        img_item.sprite = _itemSprite;
        img_currencyIcon.sprite = _currencySprite;
    }

    public void SetPurchasePrice(int _price)
    {
        ItemPrice = _price;
        lbl_purchasePrice.text = _price.ToString();
        lbl_purchasePrice2.text = _price.ToString();
    }

    public void SetItemLocked()
    {
        btn_purchaseItem.gameObject.SetActive(false);
        btn_goToStore.SetActive(true);
        img_lockedOverlay.SetActive(true);
    }

    public void SetItemUnlocked()
    {
        btn_purchaseItem.gameObject.SetActive(true);
        btn_goToStore.SetActive(false);
        img_lockedOverlay.SetActive(false);
    }

    public void SetAsPurchased()
    {
        btn_purchaseItem.gameObject.SetActive(false);
        pnl_purchasedItem.SetActive(true);
    }
    #endregion

    #region Buttons
    public void PurchaseItem()
    {
        PurchaseCatalogueItemEvent.Raise(ItemIndex, ItemType);
    }

    public void GoToStore()
    {
        OpenGoToStorePopUpEvent.Raise();
    }

    public void ShowItemInfo()
    {
        ShowSelectedItemEvent.Raise(ItemIndex, ItemType);
    }
    #endregion
}
