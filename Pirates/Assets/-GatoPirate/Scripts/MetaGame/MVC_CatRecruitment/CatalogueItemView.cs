using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityAtoms;

public class CatalogueItemView : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [Header("Item references")]
    [SerializeField]
    private Image img_item;
    [SerializeField]
    private TextMeshProUGUI lbl_itemName;
    [SerializeField]
    private Button btn_purchaseItem;
    [SerializeField]
    private TextMeshProUGUI lbl_purchasePrice;

    [Header("Item locked")]
    [SerializeField]
    private GameObject img_lockedOverlay;
    [SerializeField]
    private GameObject btn_goToStore;
    [SerializeField]
    private TextMeshProUGUI lbl_purchasePrice2;

    // Events
    public StringIntEvent PurchaseItemEvent { get; set; }

    private int itemPrice;

    #region Data set
    public void SetName(string _itemName)
    {
        itemName = _itemName; 
        lbl_itemName.text = _itemName;
    }

    public void SetSprite(Sprite _itemSprite)
    {
        img_item.sprite = _itemSprite;
    }

    public void SetPurchasePrice(int _price)
    {
        itemPrice = _price;
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
    #endregion

    #region Buttons
    public void PurchaseItem()
    {
        PurchaseItemEvent.Raise(itemName, itemPrice);
    }

    public void GoToStore()
    {
        Debug.Log("NOT ENOUGH MONEY, GO TO STORE");
    }

    public void ShowItemInfo()
    {
        Debug.Log("OPEN INFO SCREEN");
    }
    #endregion
}
