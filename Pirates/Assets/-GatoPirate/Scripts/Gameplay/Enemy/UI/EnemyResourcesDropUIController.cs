using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using TMPro;
using UnityAtoms;

public class EnemyResourcesDropUIController : MonoBehaviour
{
    [SerializeField]
    private PanelCurrenciesController currenciesController;
    [SerializeField]
    private float animationDuration;

    public IntEvent GoldResourcesDroppedEvent { get; set; }
    public IntEvent WoodResourcesDroppedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private int currentEarnedCoins;
    private int currentEarnedWood;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<int>.BuildEventHandler(GoldResourcesDroppedEvent, GoldResourcesDroppedEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int>.BuildEventHandler(WoodResourcesDroppedEvent, WoodResourcesDroppedEventCallback));
    }

    private void GoldResourcesDroppedEventCallback(int _droppedGold)
    {
        CurrencyManager.Instance.EarnGoldenCoins(_droppedGold);
        StartCoroutine(CurrencyAmountAnimation(_droppedGold, CurrencyType.GOLDEN_COINS));
    }

    private void WoodResourcesDroppedEventCallback(int _droppedWood)
    {
        CurrencyManager.Instance.EarnWood(_droppedWood);
        StartCoroutine(CurrencyAmountAnimation(_droppedWood, CurrencyType.WOOD));
    }

    private IEnumerator CurrencyAmountAnimation(int _currencyToIncrease, CurrencyType _currencyType)
    {
        int start = 0;
        float timer = 0;
        int score;
        int totalAmount = 0;
        TextMeshProUGUI currencyText = null;

        if (_currencyType.Equals(CurrencyType.GOLDEN_COINS))
        {
            start = currentEarnedCoins;
            totalAmount = currentEarnedCoins + _currencyToIncrease;

            currencyText = currenciesController.Lbl_goldenCoinsAmnt;
            currentEarnedCoins = totalAmount;
        }
        else if (_currencyType.Equals(CurrencyType.WOOD))
        {
            start = currentEarnedWood;
            totalAmount = currentEarnedWood + _currencyToIncrease;
            currencyText = currenciesController.Lbl_woodAmnt;
            currentEarnedWood = totalAmount;
        }

        while (timer < animationDuration)
        {
            float progress = timer / animationDuration;
            score = (int)Mathf.Lerp(start, totalAmount, progress);
            currencyText.text = $"x {score}";
            yield return null;
            timer += Time.deltaTime;
        }

        score = totalAmount;
        currencyText.text = $"x {score}";
    }

    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }

        _eventHandlers.Clear();
    }
}
