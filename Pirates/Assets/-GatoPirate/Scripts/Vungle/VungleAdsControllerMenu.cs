using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;

public class VungleAdsControllerMenu : MonoBehaviour
{
    [Header("Game IDs")]
    [SerializeField]
    private string windowsAppID;

    [Header("Game IDs")]
    [SerializeField]
    private string freeCoinsRecruitmentPlacementID;
    [SerializeField]
    private string freeCoinsStorePlacementID;

    [Header("Reward")]
    [SerializeField]
    private int freeCoinsAmount;

    // Events
    public VoidEvent FreeCoinsRewardSuccessEvent { get; set; }
    public VoidEvent LoadFreeCoinsAdRecruitmentEvent { get; set; }
    public VoidEvent LoadFreeCoinsAdStoreEvent { get; set; }
    public CurrencyTypeIntEvent ShowRewardedCurrencyEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        // Vungle Init
        Vungle.init(windowsAppID);
        Vungle.onInitializeEvent += VungleInitialized;
        Vungle.adPlayableEvent += AdPlayable;
        Vungle.onAdStartedEvent += AdStarted;
        Vungle.onAdFinishedEvent += AdFinished;

        // Events
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(FreeCoinsRewardSuccessEvent, FreeCoinsRewardSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadFreeCoinsAdRecruitmentEvent, LoadFreeCoinsAdRecruitmentEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadFreeCoinsAdStoreEvent, LoadFreeCoinsAdStoreEventCallback));
    }

    #region Vungle init
    private void VungleInitialized()
    {
        Debug.Log("Vungle: Vungle initialized succesfully");
        Vungle.loadAd(freeCoinsRecruitmentPlacementID);
        Vungle.loadAd(freeCoinsStorePlacementID);
    }

    private void AdPlayable(string _placementID, bool _playable)
    {
        Debug.Log($"Vungle: Placement id? {_placementID} playable? {_playable}");
    }

    private void AdStarted(string _placementID)
    {
        Debug.Log($"Vungle: Ad started with placement id {_placementID}");
    }

    private void AdFinished(string _placementID, AdFinishedEventArgs _args)
    {
        Debug.Log($"Add finished placement {_placementID} is completed? {_args.IsCompletedView}");
        if(_args.IsCompletedView)
            FreeCoinsRewardSuccessEventCallback(new Void());
    }
    #endregion

    // TESTING 
    public void TestAds()
    {
        if (Vungle.isAdvertAvailable(freeCoinsRecruitmentPlacementID))
        {
            Vungle.playAd(freeCoinsRecruitmentPlacementID);
        }
        else
        {
            Debug.Log($"Vungle: Ad not available");
        }
    }

    #region Event callbacks
    private void LoadFreeCoinsAdRecruitmentEventCallback(Void _item)
    {
#if UNITY_EDITOR
        if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
            Debug.Log("No Ads Purchased, giving reward for free");
        FreeCoinsRewardSuccessEventCallback(new Void());


#else
if (Vungle.isAdvertAvailable(freeCoinsRecruitmentPlacementID))
        {
            Vungle.playAd(freeCoinsRecruitmentPlacementID);
        }
        else
        {
            Debug.Log($"Vungle: Ad not available");
        }
        //if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
        //    FreeCoinsRewardSuccessEventCallback(new Void());
        //else
        //{
        ////RewardAd ad = new RewardAd(new Context(), rewardedAdsID);
        //// TODO: Uncomment this for publishing
        //RewardAd ad = new RewardAd(new Context(), freeCoinsRecruitmentID);
        //AdParam adParam = new AdParam.Builder().build();
        //MRewardLoadListener rewardAdLoadListener = new MRewardLoadListener(ad, FreeCoinsRewardSuccessEvent);
        //ad.loadAd(adParam, rewardAdLoadListener);
        //}
#endif
    }

    private void LoadFreeCoinsAdStoreEventCallback(Void _item)
    {
#if UNITY_EDITOR
        if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
            Debug.Log("No Ads Purchased, giving reward for free");

        FreeCoinsRewardSuccessEventCallback(new Void());


#else
 if (Vungle.isAdvertAvailable(freeCoinsStorePlacementID))
        {
            Vungle.playAd(freeCoinsStorePlacementID);
        }
        else
        {
            Debug.Log($"Vungle: Ad not available");
        }

        //if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
        //    FreeCoinsRewardSuccessEventCallback(new Void());
        //else
        //{
        ////RewardAd ad = new RewardAd(new Context(), rewardedAdsID);
        //// TODO: Uncomment this for publishing
        //RewardAd ad = new RewardAd(new Context(), freeCoinsStoreID);
        //AdParam adParam = new AdParam.Builder().build();
        //MRewardLoadListener rewardAdLoadListener = new MRewardLoadListener(ad, FreeCoinsRewardSuccessEvent);
        //ad.loadAd(adParam, rewardAdLoadListener);
        //}
#endif
    }

    private void FreeCoinsRewardSuccessEventCallback(Void _item)
    {
        // Add coins
        CurrencyDataSaveManager.Instance.UpdateCurrency(CurrencyType.GOLDEN_COINS, freeCoinsAmount);
        GameAnalyticsController.Instance.EarnedCurrencyEvent(CurrencyType.GOLDEN_COINS, freeCoinsAmount, CurrencyOrigin.FREE_GOLDEN_COINS);
        ShowRewardedCurrencyEvent.Raise(CurrencyType.GOLDEN_COINS, freeCoinsAmount);
        // TODO: Trigger sound
    }
    #endregion

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
