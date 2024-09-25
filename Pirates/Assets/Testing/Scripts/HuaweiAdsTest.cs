using HmsPlugin;
using HuaweiMobileServices.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HmsPlugin.HMSAdsKitManager;

public class HuaweiAdsTest : MonoBehaviour
{
    //private Toggle testAdStatusToggle;
    private AdLoadMethod RewardedAdLoadMethod = AdLoadMethod.WithAdId;
    private AdLoadMethod InterstitialAdLoadMethod = AdLoadMethod.WithAdId;
    private RewardVerifyConfig rewardVerifyConfig;

    private const string TAG = "HUAWEI!!!  ";

    public void Initialize()
    {
        HMSAdsKitManager.Instance = new Builder()
        .SetHasPurchasedNoAds(false)
                                        .SetRewardedAdLoadMethod(RewardedAdLoadMethod, rewardVerifyConfig)
                                        .SetInterstitialAdLoadMethod(InterstitialAdLoadMethod)
                                        .Build();

        HMSAdsKitManager.Instance.OnRewardedAdLoaded = OnRewardedAdLoaded;
        HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        HMSAdsKitManager.Instance.OnRewardedAdFailedToLoad = OnRewardedAdFailedToLoad;

        HMSAdsKitManager.Instance.OnInterstitialAdClosed = OnInterstitialAdClosed;
        HMSAdsKitManager.Instance.ConsentOnSuccess = OnConsentSuccess;
        HMSAdsKitManager.Instance.ConsentOnFail = OnConsentFail;

        HMSAdsKitManager.Instance.RequestConsentUpdate();
    }

    #region Rewarded
    public void LoadRewardedAd()
    {
        HMSAdsKitManager.Instance.LoadRewardedAd();
    }

    public void ShowRewardedAd()
    {
        Debug.Log($"{TAG}ShowRewardedAd");
        HMSAdsKitManager.Instance.ShowRewardedAd();
    }

    private void OnRewardedAdLoaded()
    {
        Debug.Log($"{TAG}OnRewardedAdLoaded");
        #region RewardVerifyConfig
        /*RewardVerifyConfig verifyConfig = new RewardVerifyConfig.Builder().SetData("CUSTOM_DATA").SetUserId("123456").Build();
        SetRewardVerifyConfig(verifyConfig);
        Debug.Log($"{TAG}OnRewardedAdLoaded:{verifyConfig.UserId} - {verifyConfig.Data}");*/
        #endregion
    }

    public void OnRewarded(Reward reward)
    {
        Debug.Log($"{TAG}rewarded!");
    }


    private void OnRewardedAdFailedToLoad(int _error)
    {
        Debug.Log($"{TAG} FAILED WITH int {_error}");
    }
    #endregion

    #region Interstitial
    public void LoadInterstitialAd()
    {
        HMSAdsKitManager.Instance.LoadInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        Debug.Log($"{TAG}ShowInterstitialAd");
        HMSAdsKitManager.Instance.ShowInterstitialAd();
    }

    public void OnInterstitialAdClosed()
    {
        Debug.Log($"{TAG}interstitial ad closed");
    }
    #endregion

    #region Consent
    private void OnConsentSuccess(ConsentStatus consentStatus, bool isNeedConsent, IList<AdProvider> adProviders)
    {
        Debug.Log($"{TAG}OnConsentSuccess consentStatus:{consentStatus} isNeedConsent:{isNeedConsent}");
        foreach (var AdProvider in adProviders)
        {
            Debug.Log($"{TAG}OnConsentSuccess adproviders: Id:{AdProvider.Id} Name:{AdProvider.Name} PrivacyPolicyUrl:{AdProvider.PrivacyPolicyUrl} ServiceArea:{AdProvider.ServiceArea}");
        }
    }

    private void OnConsentFail(string desc)
    {
        Debug.LogError($"{TAG}OnConsentFail:{desc}");
    }
    #endregion


    

    

    

    

    
}
