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
    private TextMeshProUGUI lbl_updatedGoldenCoins;

    [Header("Wood")]
    [SerializeField]
    private TextMeshProUGUI lbl_woodAmnt;
    [SerializeField]
    private TextMeshProUGUI lbl_updatedWood;

    [Header("Gems")]
    [SerializeField]
    private TextMeshProUGUI lbl_gemAmnt;
    [SerializeField]
    private TextMeshProUGUI lbl_updatedGems;

    [Header("Colors")]
    [SerializeField]
    private Color spentColor;
    [SerializeField]
    private Color rewardedColor;

    // Events
    public VoidEvent CurrenciesUpdatedEvent { get; set; }
    public CurrencyTypeIntEvent ShowSpentCurrencyEvent { get; set; }
    public CurrencyTypeIntEvent ShowRewardedCurrencyEvent { get; set; }
    public VoidEvent OpenStoreEvent { get; set; }
    public VoidEvent CurrencyUpdateAnimationFinishedEvent { get; set; }

    // Properties
    public TextMeshProUGUI Lbl_goldenCoinsAmnt { get => lbl_goldenCoinsAmnt; set => lbl_goldenCoinsAmnt = value; }
    public TextMeshProUGUI Lbl_woodAmnt { get => lbl_woodAmnt; set => lbl_woodAmnt = value; }
    public TextMeshProUGUI Lbl_gemAmnt { get => lbl_gemAmnt; set => lbl_gemAmnt = value; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {        
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CurrenciesUpdatedEvent, CurrenciesUpdatedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CurrencyUpdateAnimationFinishedEvent, CurrencyUpdateAnimationFinishedEventCallback));

        if (ShowSpentCurrencyEvent)
            _eventHandlers.Add(EventHandlerFactory<CurrencyType, int>.BuildEventHandler(ShowSpentCurrencyEvent, ShowSpentCurrencyEventCallback));
        if(ShowRewardedCurrencyEvent)
            _eventHandlers.Add(EventHandlerFactory<CurrencyType, int>.BuildEventHandler(ShowRewardedCurrencyEvent, ShowRewardedCurrencyEventCallback));
    }

    #region Event callbacks
    private void CurrenciesUpdatedEventCallback(Void _item)
    {
        Lbl_goldenCoinsAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.GOLDEN_COINS)}";
        Lbl_woodAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.WOOD)}";
        Lbl_gemAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.PREMIUM_GEM)}";
    }

    private void CurrencyUpdateAnimationFinishedEventCallback(Void _item)
    {
        lbl_updatedGoldenCoins.gameObject.SetActive(false);
        lbl_updatedWood.gameObject.SetActive(false);
        lbl_updatedGems.gameObject.SetActive(false);
    }

    private void ShowSpentCurrencyEventCallback(CurrencyType _currency, int _spentAmount)
    {
        switch (_currency)
        {
            case CurrencyType.GOLDEN_COINS:
                lbl_updatedGoldenCoins.color = spentColor;
                lbl_updatedGoldenCoins.text = $"-{_spentAmount}";
                lbl_updatedGoldenCoins.gameObject.SetActive(true);
                break;
            case CurrencyType.WOOD:
                lbl_updatedWood.color = spentColor;
                lbl_updatedWood.text = $"-{_spentAmount}";
                lbl_updatedWood.gameObject.SetActive(true);
                break;
            case CurrencyType.PREMIUM_GEM:
                lbl_updatedGems.color = spentColor;
                lbl_updatedGems.text = $"-{_spentAmount}";
                lbl_updatedGems.gameObject.SetActive(true);
                break;
        }
        // TODO: Trigger cancel animation
    }

    private void ShowRewardedCurrencyEventCallback(CurrencyType _currency, int _rewardedAmount)
    {
        switch (_currency)
        {
            case CurrencyType.GOLDEN_COINS:
                lbl_updatedGoldenCoins.color = rewardedColor;
                lbl_updatedGoldenCoins.text = $"+{_rewardedAmount}";
                lbl_updatedGoldenCoins.gameObject.SetActive(true);
                break;
            case CurrencyType.WOOD:
                lbl_updatedWood.color = rewardedColor;
                lbl_updatedWood.text = $"+{_rewardedAmount}";
                lbl_updatedWood.gameObject.SetActive(true);
                break;
            case CurrencyType.PREMIUM_GEM:
                lbl_updatedGems.color = rewardedColor;
                lbl_updatedGems.text = $"+{_rewardedAmount}";
                lbl_updatedGems.gameObject.SetActive(true);
                break;
        }
    }
    #endregion

    #region Public methods
    public void OpenStore()
    {
        OpenStoreEvent?.Raise();
    }
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
