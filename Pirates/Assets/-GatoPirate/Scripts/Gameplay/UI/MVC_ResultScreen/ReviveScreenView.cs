using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveScreenView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_reviveCoinsPrice;
    [SerializeField]
    private Button btn_payCoins;

    public ResultScreenController ResultScreenController { get; set; }

    public void SetRevivePrice(int _coinsAmnt)
    {
        // TODO: Update this using localization
        lbl_reviveCoinsPrice.text = $"Paga {_coinsAmnt} monedas";
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
