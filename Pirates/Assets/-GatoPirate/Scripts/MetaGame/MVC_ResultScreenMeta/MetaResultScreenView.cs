using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetaResultScreenView : MonoBehaviour
{
    [SerializeField]
    private float screenShownDelay;
    [SerializeField]
    private TextMeshProUGUI lbl_currentCoins;
    [SerializeField]
    private TextMeshProUGUI lbl_earnedCoins;
    [SerializeField]
    private DOTweenAnimation tweenAnimation;
    [SerializeField]
    private float animationDuration;

    public MetaResultScreenController ResultScreenController { get; set; }
    public TextMeshProUGUI Lbl_currentCoins { get => lbl_currentCoins; set => lbl_currentCoins = value; }
    public TextMeshProUGUI Lbl_earnedCoins { get => lbl_earnedCoins; set => lbl_earnedCoins = value; }
    public float AnimationDuration { get => animationDuration; set => animationDuration = value; }
    public DOTweenAnimation TweenAnimation { get => tweenAnimation; set => tweenAnimation = value; }
    public float ScreenShownDelay { get => screenShownDelay; set => screenShownDelay = value; }

    public void SetResultsData(int _currentCoins, int _earnedCoins)
    { 
        lbl_currentCoins.text = _currentCoins.ToString();
        lbl_earnedCoins.text = _earnedCoins.ToString();
    }

    #region Button methods
    public void CurrencyAnimation()
    {
        ResultScreenController.TriggerCurrencyAnimation();
    }

    public void SaveDataAndClose()
    {
        ResultScreenController.TriggerAddedCoinsSound();
        ResultScreenController.SaveCurrencyData();
        gameObject.SetActive(false);
    }
    #endregion
}
