using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class ReviveScreenView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_reviveCoinsPrice;
    [SerializeField]
    private Button btn_payCoins;
    [SerializeField]
    private TextMeshProUGUI lbl_reviveCoinsPriceFree;
    [SerializeField]
    private Button btn_payCoinsFree;

    [Header("Currencies")]
    [SerializeField]
    private PanelCurrenciesController panelCurrenciesController;

    [Header("IAP")]
    [SerializeField]
    private GameObject pnl_watchAds;
    [SerializeField]
    private GameObject pnl_freeRevive;

    public ResultScreenController ResultScreenController { get; set; }
    public PanelCurrenciesController PanelCurrenciesController { get => panelCurrenciesController; set => panelCurrenciesController = value; }

    public void SetRevivePrice(int _currencyAmnt)
    {
        // TODO: Update this using localization
        lbl_reviveCoinsPrice.text = $"Pay {_currencyAmnt} coins";
        lbl_reviveCoinsPriceFree.text = $"Pay {_currencyAmnt} coins";
    }

    public void SetCoinsButtonAsLocked()
    {
        lbl_reviveCoinsPrice.color = Color.red;
        lbl_reviveCoinsPriceFree.color = Color.red;
        btn_payCoins.interactable = false;
        btn_payCoinsFree.interactable = false;
    }

    public void RemoveAdsPurchased()
    {
        pnl_watchAds.SetActive(false);
        pnl_freeRevive.SetActive(true);
    }

    #region Button methods
    public void PayCoins()
    {
        ResultScreenController.ReviveWithCoins();
    }

    public void WatchAd()
    {
        ResultScreenController.ReviveWithAd();
    }

    public void Quit()
    {
        ResultScreenController.LoadMainMenuScene();
    }
    #endregion
}
