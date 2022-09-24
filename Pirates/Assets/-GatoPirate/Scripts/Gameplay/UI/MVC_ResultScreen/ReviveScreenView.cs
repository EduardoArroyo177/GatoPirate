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

    [Header("Currencies")]
    [SerializeField]
    private PanelCurrenciesController panelCurrenciesController;

    public ResultScreenController ResultScreenController { get; set; }
    public PanelCurrenciesController PanelCurrenciesController { get => panelCurrenciesController; set => panelCurrenciesController = value; }

    public void SetRevivePrice(int _currencyAmnt)
    {
        // TODO: Update this using localization
        lbl_reviveCoinsPrice.text = $"Pay {_currencyAmnt} coins";
    }

    public void SetCoinsButtonAsLocked()
    {
        lbl_reviveCoinsPrice.color = Color.red;
        btn_payCoins.interactable = false;
    }

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
}
