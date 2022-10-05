using HuaweiService;
using HuaweiService.ads;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;


public class InterstitialAdListener : AdListener
{
    private InterstitialAd ad;
    public InterstitialAdListener(InterstitialAd _ad) : base()
    {
        ad = _ad;
    }
    public override void onAdLoaded()
    {
        Debug.Log("AdListener onAdLoaded");
        ad.show();
    }

    public override void onAdFailed(int arg0)
    {
        Debug.Log($"Ad failed to load with error code {arg0}.");
    }

    public override void onAdOpened()
    {
        Debug.Log("Ad Opened");
    }

    public override void onAdClicked()
    {
        Debug.Log("Ad Clicked");
    }

    public override void onAdLeave()
    {
        Debug.Log("Ad Leave");
    }

    public override void onAdClosed()
    {
        Debug.Log("Ad Closed");
    }
}

public class MRewardLoadListener : RewardAdLoadListener
{
    private RewardAd ad;
    private VoidEvent RewardSuccessEvent;
    public MRewardLoadListener(RewardAd _ad, VoidEvent _rewardSuccessEvent = null)
    {
        ad = _ad;
        RewardSuccessEvent = _rewardSuccessEvent;
    }
    public override void onRewardAdFailedToLoad(int errorCode)
    {
        Debug.Log("RewardAdLoadListener onRewardAdFailedToLoad " + errorCode);
    }

    public override void onRewardedLoaded()
    {
        Debug.Log("RewardAdLoadListener onRewardedLoaded");
        ad.show(new Context(), new MRewardAdStatusListener(RewardSuccessEvent));
    }
}
public class MRewardAdStatusListener : RewardAdStatusListener
{
    private VoidEvent RewardSuccessEvent;
    private bool rewardSuccess;

    public MRewardAdStatusListener(VoidEvent _rewardSuccessEvent = null)
    {
        RewardSuccessEvent = _rewardSuccessEvent;
        rewardSuccess = false;
    }

    public override void onRewardAdOpened()
    {
        Debug.Log("RewardAdStatusListener onRewardAdOpened");
    }
    public override void onRewardAdClosed()
    {
        Debug.Log("RewardAdStatusListener onRewardAdClosed");
        if (rewardSuccess)
        {
            RewardSuccessEvent?.Raise();
        }
    }
    public override void onRewarded(Reward arg0)
    {
        Debug.Log($"RewardAdStatusListener onRewarded {arg0.getName()}");
        rewardSuccess = true;
    }
    public override void onRewardAdFailedToShow(int arg0)
    {
        Debug.Log("RewardAdStatusListener onRewarded");
    }
}

public class MConsentUpdateListener : ConsentUpdateListener
{
    public override void onSuccess(ConsentStatus arg0, bool isNeedConsent, List arg2)
    {
        Debug.Log("ConsentUpdateListener onSuccess");
        if (arg0 == ConsentStatus.UNKNOWN)
        {
            Debug.Log("UNKNOWN");
        }
        else if (arg0 == ConsentStatus.PERSONALIZED)
        {
            Debug.Log("PERSONALIZED");
        }
        else if (arg0 == ConsentStatus.NON_PERSONALIZED)
        {
            Debug.Log("NON_PERSONALIZED");
        }
        else
        {
            Debug.Log("NON");
        }
    }
    public override void onFail(string arg0)
    {
        Debug.Log("ConsentUpdateListener UNKNOWN");
        base.onFail(arg0);
    }
}

