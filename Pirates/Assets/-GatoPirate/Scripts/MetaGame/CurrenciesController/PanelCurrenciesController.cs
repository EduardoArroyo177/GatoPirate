using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PanelCurrenciesController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_goldenCoinsAmnt;
    [SerializeField]
    private TextMeshProUGUI lbl_woodAmnt;
    [SerializeField]
    private TextMeshProUGUI lbl_gemAmnt;

    public VoidEvent CurrenciesUpdatedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CurrenciesUpdatedEvent, CurrenciesUpdatedEventCallback));
    }

    #region Event callbacks
    private void CurrenciesUpdatedEventCallback(Void _item)
    {
        lbl_goldenCoinsAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.GOLDEN_COINS)}";
        lbl_woodAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.WOOD)}";
        lbl_gemAmnt.text = $"x{CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.PREMIUM_GEM)}";
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
