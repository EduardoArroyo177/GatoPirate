using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using Void = UnityAtoms.Void;

public class HuaweiAdsControllerMenu : MonoBehaviour
{
    [Header("Testing IDs")]
    [SerializeField]
    private string imageAdsID = "teste9ih9j0rc3";
    [SerializeField]
    private string videoAdsID = "testb4znbuh3n2";
    [SerializeField]
    private string rewardedAdsID = "testx9dtjwj8hp";

    [Header("Game IDs")]
    [SerializeField]
    private string freeCoinsRecruitmentID;
    [SerializeField]
    private string freeCoinsStoreID;

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
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(FreeCoinsRewardSuccessEvent, FreeCoinsRewardSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadFreeCoinsAdRecruitmentEvent, LoadFreeCoinsAdRecruitmentEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadFreeCoinsAdStoreEvent, LoadFreeCoinsAdStoreEventCallback));
    }

    #region Event callbacks
    private void LoadFreeCoinsAdRecruitmentEventCallback(Void _item)
    {
#if UNITY_EDITOR
        if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
            Debug.Log("No Ads Purchased, giving reward for free");
        FreeCoinsRewardSuccessEventCallback(new Void());
#else
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

#region Other ad methods
    //public void SetConsentStatus(bool personal)
    //{
    //    Consent consentInfo = Consent.getInstance(new Context());
    //    var consentStatus = personal ? ConsentStatus.PERSONALIZED : ConsentStatus.NON_PERSONALIZED;
    //    consentInfo.setConsentStatus(consentStatus);
    //    Debug.Log($"set consent status as {consentStatus}");
    //}

    //public void CheckConsentStatus()
    //{
    //    Consent consentInfo = Consent.getInstance(new Context());
    //    consentInfo.requestConsentUpdate(new MConsentUpdateListener());
    //}

    //public void SetRequestOptionsNonPersonalizedAd()
    //{
    //    RequestOptions reqOptions = HwAds.getRequestOptions()
    //        .toBuilder()
    //        .setNonPersonalizedAd(new Integer(NonPersonalizedAd.ALLOW_ALL))
    //        .build();
    //    HwAds.setRequestOptions(reqOptions);

    //    Debug.Log("RequestOptions NonPersonalizedAd:" + HwAds.getRequestOptions().getNonPersonalizedAd());
    //}
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
