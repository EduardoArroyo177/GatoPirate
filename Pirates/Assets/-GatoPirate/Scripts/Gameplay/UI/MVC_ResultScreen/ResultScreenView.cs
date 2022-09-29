using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreenView : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField]
    private GameObject pnl_earnedCoins;
    [SerializeField]
    private TextMeshProUGUI lbl_earnedCoins;

    [Header("Wood")]
    [SerializeField]
    private GameObject pnl_earnedWood;
    [SerializeField]
    private TextMeshProUGUI lbl_earnedWood;

    [Header("Gems")]
    [SerializeField]
    private GameObject pnl_earnedGems;
    [SerializeField]
    private TextMeshProUGUI lbl_earnedGems;

    [Header("Resources animation")]
    [SerializeField]
    private float resourcesAnimationDuration;

    [Header("Ads")]
    [SerializeField]
    private GameObject btn_watchAd;
    [SerializeField]
    private GameObject btn_freeReward;

    [Header("IAP")]
    [SerializeField]
    private GameObject pnl_watchAds;
    [SerializeField]
    private GameObject pnl_freeReward;
    [SerializeField]
    private TextMeshProUGUI lbl_freeReward;

    public ResultScreenController ResultScreenController { get; set; }

    public TextMeshProUGUI Lbl_earnedCoins { get => lbl_earnedCoins; set => lbl_earnedCoins = value; }
    public TextMeshProUGUI Lbl_earnedWood { get => lbl_earnedWood; set => lbl_earnedWood = value; }
    public TextMeshProUGUI Lbl_earnedGems { get => lbl_earnedGems; set => lbl_earnedGems = value; }
    public float ResourcesAnimationDuration { get => resourcesAnimationDuration; set => resourcesAnimationDuration = value; }
    public GameObject Pnl_earnedCoins { get => pnl_earnedCoins; set => pnl_earnedCoins = value; }
    public GameObject Pnl_earnedWood { get => pnl_earnedWood; set => pnl_earnedWood = value; }
    public GameObject Pnl_earnedGems { get => pnl_earnedGems; set => pnl_earnedGems = value; }
    public GameObject Btn_watchAd { get => btn_watchAd; set => btn_watchAd = value; }
    public GameObject Btn_freeReward { get => btn_freeReward; set => btn_freeReward = value; }

    public void StartResultScreenAnimations()
    {
        ResultScreenController.TriggerResultScreenAnimations();
    }

    public void RemoveAdsPurchased()
    {
        pnl_watchAds.SetActive(false);
        pnl_freeReward.SetActive(true);
    }

    public void UpdateRemainingFreeCalls(int _remainingCalls)
    {
        lbl_freeReward.text = $"Free reward! x{_remainingCalls}";
    }

    #region Button methods
    public void DoubleReward()
    {
        ResultScreenController.WatchAdForDoubleReward();
    }

    public void LoadMainMenu()
    {
        ResultScreenController.LoadMainMenuScene();
    }
    #endregion
}
