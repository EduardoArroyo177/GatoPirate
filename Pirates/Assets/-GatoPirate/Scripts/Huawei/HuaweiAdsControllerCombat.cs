using HmsPlugin;
using HuaweiMobileServices.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using static HmsPlugin.HMSAdsKitManager;

public class HuaweiAdsControllerCombat : AdsControllerCombat
{
    [Header("Game IDs")]
    [SerializeField]
    private string doubleRewardPlacementID;
    [SerializeField]
    private string revivePlacementID;
    [SerializeField]
    private string combatCompletedPlacementID;

    private AdLoadMethod RewardedAdLoadMethod = AdLoadMethod.WithAdId;
    private RewardVerifyConfig rewardVerifyConfig;

    private List<IAtomEventHandler> _eventHandlers = new();
    private CombatAdType combatAdType = CombatAdType.NONE;
    private bool rewardCompleted;
    private bool loadAndPlayAd;

    public override void Initialize()
    {
        HMSAdsKitManager.Instance = new Builder()
        .SetHasPurchasedNoAds(false)
        .SetRewardedAdLoadMethod(RewardedAdLoadMethod, rewardVerifyConfig)
        .Build();

        HMSAdsKitManager.Instance.OnRewardedAdLoaded = OnRewardedAdLoaded;
        HMSAdsKitManager.Instance.OnRewardedAdFailedToLoad = OnRewardedAdFailedToLoad;
        HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        HMSAdsKitManager.Instance.OnRewardAdClosed = OnRewardedAdClosed;

        HMSAdsKitManager.Instance.OnInterstitialAdClosed = OnInterstitialAdClosed;
        HMSAdsKitManager.Instance.ConsentOnSuccess = OnConsentSuccess;
        HMSAdsKitManager.Instance.ConsentOnFail = OnConsentFail;

        HMSAdsKitManager.Instance.RequestConsentUpdate();

        HMSAdsKitManager.Instance.LoadRewardedAd(doubleRewardPlacementID);
        HMSAdsKitManager.Instance.LoadRewardedAd(revivePlacementID);

        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CombatRewardAdSuccessEvent, CombatRewardAdSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadReviveAdEvent, PlayAdRevive));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadDoubleRewardAdEvent, PlayAdDoubleReward));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadCombatFinishedAdEvent, LoadCombatFinishedAdEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));
    }

    public override void PlayAdDoubleReward(Void _item)
    {
        combatAdType = CombatAdType.DOUBLE;
        if (HMSAdsKitManager.Instance.IsRewardedAdLoaded)
            HMSAdsKitManager.Instance.ShowRewardedAd();
        else
        {
            HMSAdsKitManager.Instance.LoadRewardedAd(doubleRewardPlacementID);
            loadAndPlayAd = true;
        }
    }

    public override void PlayAdRevive(Void _item)
    {
        combatAdType = CombatAdType.REVIVE;
        if (HMSAdsKitManager.Instance.IsRewardedAdLoaded)
            HMSAdsKitManager.Instance.ShowRewardedAd();
        else
        {
            HMSAdsKitManager.Instance.LoadRewardedAd(revivePlacementID);
            loadAndPlayAd = true;
        }
    }

    private void LoadCombatFinishedAdEventCallback(Void _item)
    {
        // TODO: Implement interstitial call
    }

    private void CombatRewardAdSuccessEventCallback(Void _item)
    {
        switch (combatAdType)
        {
            case CombatAdType.REVIVE:
                // TODO: Trigger reviveID event
                ReviveSuccessEvent.Raise();
                break;
            case CombatAdType.DOUBLE:
                // TODO: Trigger give double reward event
                DoubleRewardSuccessEvent.Raise();
                break;
        }

        combatAdType = CombatAdType.NONE;
    }

    #region Consent
    private void OnConsentSuccess(ConsentStatus consentStatus, bool isNeedConsent, IList<AdProvider> adProviders)
    {
        Debug.Log($"HMS ADS: OnConsentSuccess consentStatus:{consentStatus} isNeedConsent:{isNeedConsent}");
        foreach (var AdProvider in adProviders)
        {
            Debug.Log($"HMS ADS: OnConsentSuccess adproviders: Id:{AdProvider.Id} Name:{AdProvider.Name} PrivacyPolicyUrl:{AdProvider.PrivacyPolicyUrl} ServiceArea:{AdProvider.ServiceArea}");
        }
    }

    private void OnConsentFail(string desc)
    {
        Debug.LogError($"HMS ADS: OnConsentFail:{desc}");
    }
    #endregion

    #region Rewarded ads
    private void OnRewardedAdLoaded()
    {
        Debug.Log($"HMS ADS: OnRewardedAdLoaded");
        if (loadAndPlayAd)
        {
            loadAndPlayAd = false;
            HMSAdsKitManager.Instance.ShowRewardedAd();
        }
        #region RewardVerifyConfig
        /*RewardVerifyConfig verifyConfig = new RewardVerifyConfig.Builder().SetData("CUSTOM_DATA").SetUserId("123456").Build();
        SetRewardVerifyConfig(verifyConfig);
        Debug.Log($"{TAG}OnRewardedAdLoaded:{verifyConfig.UserId} - {verifyConfig.Data}");*/
        #endregion
    }

    private void OnRewardedAdFailedToLoad(int _error)
    {
        Debug.Log($"HMS ADS: OnRewardedAdFailedToLoad error code: {_error}");
        loadAndPlayAd = false;
    }

    private void OnRewarded(Reward reward)
    {
        Debug.Log($"HMS ADS: rewarded!");
        rewardCompleted = true;
        HMSAdsKitManager.Instance.LoadRewardedAd(revivePlacementID);
    }

    private void OnRewardedAdClosed()
    {
        // Give reward
        if(rewardCompleted)
            CombatRewardAdSuccessEventCallback(new Void());
        rewardCompleted = false;
    }

    #endregion

    #region Interstitial ads
    public void ShowInterstitialAd()
    {
        Debug.Log($"HMS ADS: ShowInterstitialAd");
        HMSAdsKitManager.Instance.ShowInterstitialAd();
    }

    public void OnInterstitialAdClosed()
    {
        Debug.Log($"HMS ADS: interstitial ad closed");
    }
    #endregion

    #region OnDestroy
    private void UnloadEventsEventCallback(Void _item)
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
