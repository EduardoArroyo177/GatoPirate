using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PanelCurrenciesController : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField]
    private TextMeshProUGUI lbl_goldenCoinsAmnt;
    [SerializeField]
    private TextMeshProUGUI lbl_spentGoldenCoins;

    [Header("Wood")]
    [SerializeField]
    private TextMeshProUGUI lbl_woodAmnt;
    [SerializeField]
    private TextMeshProUGUI lbl_spentWood;

    [Header("Gems")]
    [SerializeField]
    private TextMeshProUGUI lbl_gemAmnt;
    [SerializeField]
    private TextMeshProUGUI lbl_spentGems;

    // Events
    public VoidEvent CurrenciesUpdatedEvent { get; set; }
    public CurrencyTypeIntEvent ShowSpentCurrencyEvent { get; set; }

    // Properties
    public TextMeshProUGUI Lbl_goldenCoinsAmnt { get => lbl_goldenCoinsAmnt; set => lbl_goldenCoinsAmnt = value; }
    public TextMeshProUGUI Lbl_woodAmnt { get => lbl_woodAmnt; set => lbl_woodAmnt = value; }
    public TextMeshProUGUI Lbl_gemAmnt { get => lbl_gemAmnt; set => lbl_gemAmnt = value; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {        
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CurrenciesUpdatedEvent, CurrenciesUpdatedEventCallback));
        _eventHandlers.Add(EventHandlerFactory<CurrencyType, int>.BuildEventHandler(ShowSpentCurrencyEvent, ShowSpentCurrencyEventCallback));
    }

    #region Event callbacks
    private void CurrenciesUpdatedEventCallback(Void _item)
    {
        Lbl_goldenCoinsAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.GOLDEN_COINS)}";
        Lbl_woodAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.WOOD)}";
        Lbl_gemAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.PREMIUM_GEM)}";
    }

    private void ShowSpentCurrencyEventCallback(CurrencyType _currency, int _spentAmount)
    {
        switch (_currency)
        {
            case CurrencyType.GOLDEN_COINS:
                lbl_spentGoldenCoins.text = $"-{_spentAmount}";
                lbl_spentGoldenCoins.gameObject.SetActive(true);
                break;
            case CurrencyType.WOOD:
                lbl_spentWood.text = $"-{_spentAmount}";
                lbl_spentWood.gameObject.SetActive(true);
                break;
            case CurrencyType.PREMIUM_GEM:
                lbl_spentGems.text = $"-{_spentAmount}";
                lbl_spentGems.gameObject.SetActive(true);
                break;
        }
    }
    #endregion

    #region Public methods
    #endregion

    #region On Destroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
