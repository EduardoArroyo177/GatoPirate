using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;


public class InterstitialAdListener 
{
    //private InterstitialAd ad;
    //public InterstitialAdListener(InterstitialAd _ad) : base()
    //{
    //    ad = _ad;
    //}
    public void onAdLoaded()
    {
        Debug.Log("AdListener onAdLoaded");
        //ad.show();
    }

    public void onAdFailed(int arg0)
    {
        Debug.Log($"Ad failed to load with error code {arg0}.");
    }

    public void onAdOpened()
    {
        Debug.Log("Ad Opened");
    }

    public void onAdClicked()
    {
        Debug.Log("Ad Clicked");
    }

    public void onAdLeave()
    {
        Debug.Log("Ad Leave");
    }

    public void onAdClosed()
    {
        Debug.Log("Ad Closed");
    }
}

public class MRewardLoadListener 
{
    //private RewardAd ad;
    private VoidEvent RewardSuccessEvent;
    //public MRewardLoadListener(RewardAd _ad, VoidEvent _rewardSuccessEvent = null)
    //{
    //    ad = _ad;
    //    RewardSuccessEvent = _rewardSuccessEvent;
    //}
    public void onRewardAdFailedToLoad(int errorCode)
    {
        Debug.Log("RewardAdLoadListener onRewardAdFailedToLoad " + errorCode);
    }

    public void onRewardedLoaded()
    {
        Debug.Log("RewardAdLoadListener onRewardedLoaded");
        //ad.show(new Context(), new MRewardAdStatusListener(RewardSuccessEvent));
    }
}
public class MRewardAdStatusListener 
{
    private VoidEvent RewardSuccessEvent;
    private bool rewardSuccess;

    public MRewardAdStatusListener(VoidEvent _rewardSuccessEvent = null)
    {
        RewardSuccessEvent = _rewardSuccessEvent;
        rewardSuccess = false;
    }

    public  void onRewardAdOpened()
    {
        Debug.Log("RewardAdStatusListener onRewardAdOpened");
    }
    public void onRewardAdClosed()
    {
        Debug.Log("RewardAdStatusListener onRewardAdClosed");
        if (rewardSuccess)
        {
            RewardSuccessEvent?.Raise();
        }
    }
    public void onRewarded()
    {
        //Debug.Log($"RewardAdStatusListener onRewarded {arg0.getName()}");
        rewardSuccess = true;
    }
    public void onRewardAdFailedToShow(int arg0)
    {
        Debug.Log("RewardAdStatusListener onRewarded");
    }
}

public class MConsentUpdateListener 
{
    public void onSuccess(bool isNeedConsent)
    {
        Debug.Log("ConsentUpdateListener onSuccess");
        //if (arg0 == ConsentStatus.UNKNOWN)
        //{
        //    Debug.Log("UNKNOWN");
        //}
        //else if (arg0 == ConsentStatus.PERSONALIZED)
        //{
        //    Debug.Log("PERSONALIZED");
        //}
        //else if (arg0 == ConsentStatus.NON_PERSONALIZED)
        //{
        //    Debug.Log("NON_PERSONALIZED");
        //}
        //else
        //{
        //    Debug.Log("NON");
        //}
    }
    public void onFail(string arg0)
    {
        Debug.Log("ConsentUpdateListener UNKNOWN");
        //base.onFail(arg0);
    }
}

