using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuaweiAdsManager : MonoBehaviour
{
    public static HuaweiAdsManager instance;
    [Header("Testing IDs")]
    [SerializeField]
    private string imageAdsID = "teste9ih9j0rc3";
    [SerializeField]
    private string videoAdsID = "testb4znbuh3n2";
    [SerializeField]
    private string rewardedAdsID = "testx9dtjwj8hp";

    [Header("Game IDs")]
    [SerializeField]
    private string freeCoinsRecruitment;
    [SerializeField]
    private string freeCoinsStore;
    [SerializeField]
    private string doubleReward;
    [SerializeField]
    private string revive;
    [SerializeField]
    private string combatsPlayed;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoadImageAds()
    {
        //InterstitialAd ad = new InterstitialAd(new Context());
        //ad.setAdId(imageAdsID);
        //ad.setAdListener(new InterstitialAdListener(ad));
        //AdParam.Builder builder = new AdParam.Builder();
        //AdParam adParam = builder.build();
        //ad.loadAd(adParam);
    }
    public void LoadVideoAds()
    {
        //InterstitialAd ad = new InterstitialAd(new Context());
        //ad.setAdId(videoAdsID);
        //ad.setAdListener(new InterstitialAdListener(ad));
        //AdParam.Builder builder = new AdParam.Builder();
        //ad.loadAd(builder.build());
    }
    public void LoadRewardAds()
    {
        //RewardAd ad = new RewardAd(new Context(), rewardedAdsID);
        //AdParam adParam = new AdParam.Builder().build();
        //MRewardLoadListener rewardAdLoadListener = new MRewardLoadListener(ad);
        //ad.loadAd(adParam, rewardAdLoadListener);
    }

    public void SetConsentStatus(bool personal)
    {
        //Consent consentInfo = Consent.getInstance(new Context());
        //var consentStatus = personal ? ConsentStatus.PERSONALIZED : ConsentStatus.NON_PERSONALIZED;
        //consentInfo.setConsentStatus(consentStatus);
        //Debug.Log($"set consent status as {consentStatus}");
    }

    public void CheckConsentStatus()
    {
        //Consent consentInfo = Consent.getInstance(new Context());
        //consentInfo.requestConsentUpdate(new MConsentUpdateListener());
    }

    public void SetRequestOptionsNonPersonalizedAd()
    {
        //RequestOptions reqOptions = HwAds.getRequestOptions()
        //    .toBuilder()
        //    .setNonPersonalizedAd(new Integer(NonPersonalizedAd.ALLOW_ALL))
        //    .build();
        //HwAds.setRequestOptions(reqOptions);

       // Debug.Log("RequestOptions NonPersonalizedAd:" + HwAds.getRequestOptions().getNonPersonalizedAd());
    }
}
