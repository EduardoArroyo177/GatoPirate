using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MetaResultScreenController : MonoBehaviour
{
    [SerializeField]
    private MetaResultScreenView resultScreenView;

    private List<IAtomEventHandler> _eventHandlers = new();
    private int earnedCoins;
    private int currentCoins;

    public void Initialize()
    {
        resultScreenView.ResultScreenController = this;
        LoadCurrencyData();
    }

    private void LoadCurrencyData()
    {
        // Get earned coins
        earnedCoins = CurrencyDataSaveManager.Instance.GetEarnedCurrencyAmount(CurrencyType.GOLDEN_COINS);
        // Check if player comes from combat
        if (earnedCoins > 0)
        {
            // Get current coins
            currentCoins = CurrencyDataSaveManager.Instance.GetCurrencyAmount(CurrencyType.GOLDEN_COINS);
            // Update UI
            resultScreenView.SetResultsData(currentCoins, earnedCoins);
            // TODO: Update this with all resources the player has earned
            StartCoroutine("CurrencyCounterAnimation");
        }
        
    }

    private IEnumerator CurrencyCounterAnimation()
    {
        resultScreenView.gameObject.SetActive(true);
        int start = currentCoins;
        float timer = 0;
        int score;
        int reducedScore;
        int totalAmount = currentCoins + earnedCoins;        

        while (timer < resultScreenView.AnimationDuration)
        {
            float progress = timer / resultScreenView.AnimationDuration;
            score = (int)Mathf.Lerp(start, totalAmount, progress);
            resultScreenView.Lbl_currentCoins.text = $"{score}";

            reducedScore = (int)Mathf.Lerp(earnedCoins, 0, progress);
            resultScreenView.Lbl_earnedCoins.text = $"{reducedScore}";
            yield return null;
            timer += Time.deltaTime;
        }

        score = totalAmount;
        resultScreenView.Lbl_currentCoins.text = $"x {score}";
        resultScreenView.Lbl_earnedCoins.text = 0.ToString();

        // Trigger tween animation
        resultScreenView.TweenAnimation.DOPlayAllById("CoinsTween");
    }

    public void SaveCurrencyData()
    {
        StopCoroutine("CurrencyCounterAnimation");
        // Save new resource amount
        CurrencyDataSaveManager.Instance.UpdateCurrency(CurrencyType.GOLDEN_COINS, earnedCoins);
        // Remove earned resources
        CurrencyDataSaveManager.Instance.UpdateEarnedCurrency(CurrencyType.GOLDEN_COINS, -earnedCoins);
    }

    #region OnDestroy
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
