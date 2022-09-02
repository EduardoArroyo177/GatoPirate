using HuaweiService;
using HuaweiService.ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuaweiAdsManager : MonoBehaviour
{
    public static HuaweiAdsManager instance;
    [SerializeField]
    private string imageAdsID = "teste9ih9j0rc3";
    [SerializeField]
    private string videoAdsID = "testb4znbuh3n2";
    [SerializeField]
    private string rewardedAdsID = "testx9dtjwj8hp";

    [SerializeField]
    private int resultScreensBeforeInterstitial;

    private int resultScreenCounter;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            resultScreenCounter = 0;
        }
        else
            Destroy(gameObject);
    }

    public void LoadImageAds()
    {
        InterstitialAd ad = new InterstitialAd(new Context());
        ad.setAdId(imageAdsID);
        ad.setAdListener(new InterstitialAdListener(ad));
        AdParam.Builder builder = new AdParam.Builder();
        AdParam adParam = builder.build();
        ad.loadAd(adParam);
    }
    public void LoadVideoAds()
    {
        resultScreenCounter++;
        if (resultScreenCounter < resultScreensBeforeInterstitial)
            return;

        resultScreenCounter = 0;
        InterstitialAd ad = new InterstitialAd(new Context());
        ad.setAdId(videoAdsID);
        ad.setAdListener(new InterstitialAdListener(ad));
        AdParam.Builder builder = new AdParam.Builder();
        ad.loadAd(builder.build());
    }
    public void LoadRewardAds()
    {
        RewardAd ad = new RewardAd(new Context(), rewardedAdsID);
        AdParam adParam = new AdParam.Builder().build();
        MRewardLoadListener rewardAdLoadListener = new MRewardLoadListener(ad);
        ad.loadAd(adParam, rewardAdLoadListener);
    }

    public void SetConsentStatus(bool personal)
    {
        Consent consentInfo = Consent.getInstance(new Context());
        var consentStatus = personal ? ConsentStatus.PERSONALIZED : ConsentStatus.NON_PERSONALIZED;
        consentInfo.setConsentStatus(consentStatus);
        Debug.Log($"set consent status as {consentStatus}");
    }

    public void checkConsentStatus()
    {
        Consent consentInfo = Consent.getInstance(new Context());
        consentInfo.requestConsentUpdate(new MConsentUpdateListener());
    }

    public void setRequestOptionsNonPersonalizedAd()
    {
        RequestOptions reqOptions = HwAds.getRequestOptions()
            .toBuilder()
            .setNonPersonalizedAd(new Integer(NonPersonalizedAd.ALLOW_ALL))
            .build();
        HwAds.setRequestOptions(reqOptions);

        Debug.Log("RequestOptions NonPersonalizedAd:" + HwAds.getRequestOptions().getNonPersonalizedAd());
    }
}
