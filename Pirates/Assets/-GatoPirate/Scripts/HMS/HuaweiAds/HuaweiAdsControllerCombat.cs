using HuaweiService.ads;
using HuaweiService;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;
using Void = UnityAtoms.Void;

public class HuaweiAdsControllerCombat : MonoBehaviour
{
    public enum CombatAdType
    { 
        NONE,
        REVIVE,
        DOUBLE
    }
    [Header("Testing IDs")]
    [SerializeField]
    private string imageAdsID = "teste9ih9j0rc3";
    [SerializeField]
    private string videoAdsID = "testb4znbuh3n2";
    [SerializeField]
    private string rewardedAdsID = "testx9dtjwj8hp";

    [Header("Game IDs")]
    [SerializeField]
    private string doubleRewardID;
    [SerializeField]
    private string reviveID;
    [SerializeField]
    private string combatsPlayedID;

    // Events
    public VoidEvent LoadReviveAdEvent { get; set; } 
    public VoidEvent LoadDoubleRewardAdEvent { get; set; }
    public VoidEvent LoadCombatFinishedAdEvent { get; set; }
    public VoidEvent CombatRewardAdSuccessEvent { get; set; }
    public VoidEvent ReviveSuccessEvent { get; set; }
    public VoidEvent DoubleRewardSuccessEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private CombatAdType combatAdType = CombatAdType.NONE;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CombatRewardAdSuccessEvent, CombatRewardAdSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadReviveAdEvent, LoadReviveAdEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadDoubleRewardAdEvent, LoadDoubleRewardAdEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadCombatFinishedAdEvent, LoadCombatFinishedAdEventCallback));

    }

    #region Event callbacks
    private void LoadReviveAdEventCallback(Void _item)
    {
        combatAdType = CombatAdType.REVIVE;
#if UNITY_EDITOR
        CombatRewardAdSuccessEventCallback(new Void());
#else
        //RewardAd ad = new RewardAd(new Context(), rewardedAdsID);
        // TODO: Uncomment this for publishing
        RewardAd ad = new RewardAd(new Context(), reviveID);
        AdParam adParam = new AdParam.Builder().build();
        MRewardLoadListener rewardAdLoadListener = new MRewardLoadListener(ad, CombatRewardAdSuccessEvent);
        ad.loadAd(adParam, rewardAdLoadListener);
#endif
    }

    private void LoadDoubleRewardAdEventCallback(Void _item)
    {
        combatAdType = CombatAdType.DOUBLE;
#if UNITY_EDITOR
        CombatRewardAdSuccessEventCallback(new Void());
#else
        //RewardAd ad = new RewardAd(new Context(), rewardedAdsID);
        // TODO: Uncomment this for publishing
        RewardAd ad = new RewardAd(new Context(), doubleRewardID);
        AdParam adParam = new AdParam.Builder().build();
        MRewardLoadListener rewardAdLoadListener = new MRewardLoadListener(ad, CombatRewardAdSuccessEvent);
        ad.loadAd(adParam, rewardAdLoadListener);
#endif  
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
#endregion

#region Other ad methods
    public void SetConsentStatus(bool personal)
    {
        Consent consentInfo = Consent.getInstance(new Context());
        var consentStatus = personal ? ConsentStatus.PERSONALIZED : ConsentStatus.NON_PERSONALIZED;
        consentInfo.setConsentStatus(consentStatus);
        Debug.Log($"set consent status as {consentStatus}");
    }

    public void CheckConsentStatus()
    {
        Consent consentInfo = Consent.getInstance(new Context());
        consentInfo.requestConsentUpdate(new MConsentUpdateListener());
    }

    public void SetRequestOptionsNonPersonalizedAd()
    {
        RequestOptions reqOptions = HwAds.getRequestOptions()
            .toBuilder()
            .setNonPersonalizedAd(new Integer(NonPersonalizedAd.ALLOW_ALL))
            .build();
        HwAds.setRequestOptions(reqOptions);

        Debug.Log("RequestOptions NonPersonalizedAd:" + HwAds.getRequestOptions().getNonPersonalizedAd());
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
