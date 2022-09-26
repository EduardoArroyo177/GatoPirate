using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MetaResultScreenController : MonoBehaviour
{
    [SerializeField]
    private MetaResultScreenView resultScreenView;

    public UISoundsEvent TriggerUISoundEvent { get; set; }
    public VoidEvent TriggerMetaGameTutorialEvent { get; set; }

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

            StartCoroutine("ShowScreen");
            
        }
    }

    private IEnumerator ShowScreen()
    {
        // Small delay before showing the screen
        yield return new WaitForSeconds(resultScreenView.ScreenShownDelay);
        resultScreenView.gameObject.SetActive(true);
        // Trigger result screen music 
        TriggerUISoundEvent.Raise(UISounds.MENU_RESULT_SCREEN_MUSIC);
    }

    private IEnumerator CurrencyCounterAnimation()
    {
        
        int start = currentCoins;
        float timer = 0;
        int score;
        int reducedScore;
        int totalAmount = currentCoins + earnedCoins;
        
        // Trigger coins count sound
        TriggerUISoundEvent.Raise(UISounds.MENU_RESULT_SCREEN_EARNED_COINS);

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

        // TODO: Update this with all resources the player has earned
    }

    public void SaveCurrencyData()
    {
        StopCoroutine("CurrencyCounterAnimation");
        // Save new resource amount
        CurrencyDataSaveManager.Instance.UpdateCurrency(CurrencyType.GOLDEN_COINS, earnedCoins);
        // Remove earned resources
        CurrencyDataSaveManager.Instance.UpdateEarnedCurrency(CurrencyType.GOLDEN_COINS, -earnedCoins);

        // Trigger tutorial if available
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.META_GAME))
            TriggerMetaGameTutorialEvent.Raise();
    }

    public void TriggerCurrencyAnimation()
    {
        StartCoroutine("CurrencyCounterAnimation");
    }

    public void TriggerAddedCoinsSound()
    {
        TriggerUISoundEvent.Raise(UISounds.MENU_RESULT_SCREEN_ADDED_COINS);
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
