using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;

public class VungleAdsControllerCombatAndroid : AdsControllerCombat
{
    [Header("Game IDs")]
    [SerializeField]
    private string androidAppID;
    [SerializeField]
    private string doubleRewardPlacementID;
    [SerializeField]
    private string revivePlacementID;
    [SerializeField]
    private string combatCompletedPlacementID;

    private List<IAtomEventHandler> _eventHandlers = new();
    private CombatAdType combatAdType = CombatAdType.NONE;

    public override void Initialize()
    {
        // Vungle Init
        if (!Vungle.isInitialized())
            Vungle.init(androidAppID);
        else
        {
            Vungle.loadAd(doubleRewardPlacementID);
            Vungle.loadAd(revivePlacementID);
        }
        Vungle.setLogEnable(true);
        Vungle.onInitializeEvent += VungleInitialized;
        Vungle.adPlayableEvent += AdPlayable;
        Vungle.onAdStartedEvent += AdStarted;
        Vungle.onAdFinishedEvent += AdFinished;
        Vungle.onErrorEvent += VungleError;

        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CombatRewardAdSuccessEvent, CombatRewardAdSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadReviveAdEvent, PlayAdRevive));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadDoubleRewardAdEvent, PlayAdDoubleReward));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadCombatFinishedAdEvent, LoadCombatFinishedAdEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));
    }

    #region Vungle init
    private void VungleInitialized()
    {
        Debug.Log("Vungle: Vungle initialized succesfully");
        Vungle.loadAd(doubleRewardPlacementID);
        Vungle.loadAd(revivePlacementID);
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
        Debug.Log($"Vungle: Add finished placement {_placementID} is completed? {_args.IsCompletedView}");

        if(_args.IsCompletedView)
            CombatRewardAdSuccessEventCallback(new Void());
    }

    private void VungleError(string _error)
    {
        Debug.Log($"Vungle: An error occured {_error}");
    }
    #endregion

    #region Event callbacks
    public override void PlayAdRevive(Void _item)
    {
        combatAdType = CombatAdType.REVIVE;
#if UNITY_EDITOR
        if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
            Debug.Log("Remove ads purchased, giving reward for free");
        CombatRewardAdSuccessEventCallback(new Void());
#else

    if (Vungle.isAdvertAvailable(revivePlacementID))
        {
            Vungle.playAd(revivePlacementID);
        }
        else
        {
            Debug.Log($"Vungle: Ad not available");
        }

        //if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
        //    CombatRewardAdSuccessEventCallback(new Void());
        //else
        //{
        ////RewardAd ad = new RewardAd(new Context(), rewardedAdsID);
        //// TODO: Uncomment this for publishing
        //RewardAd ad = new RewardAd(new Context(), reviveID);
        //AdParam adParam = new AdParam.Builder().build();
        //MRewardLoadListener rewardAdLoadListener = new MRewardLoadListener(ad, CombatRewardAdSuccessEvent);
        //ad.loadAd(adParam, rewardAdLoadListener);
        //}
#endif
    }

    public override void PlayAdDoubleReward(Void _item)
    {
        combatAdType = CombatAdType.DOUBLE;
#if UNITY_EDITOR
        if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
            Debug.Log("Remove ads purchased, giving reward for free");
        CombatRewardAdSuccessEventCallback(new Void());
#else
if (Vungle.isAdvertAvailable(doubleRewardPlacementID))
        {
            Vungle.playAd(doubleRewardPlacementID);
        }
        else
        {
            Debug.Log($"Vungle: Ad not available");
        }

//if (PurchasesDataSaveManager.Instance.GetPurchasedNonConsumableStatus(NonConsumableIAP.REMOVE_ADS))
//            CombatRewardAdSuccessEventCallback(new Void());
//        else
//        {
//        //RewardAd ad = new RewardAd(new Context(), rewardedAdsID);
//        // TODO: Uncomment this for publishing
//        RewardAd ad = new RewardAd(new Context(), doubleRewardID);
//        AdParam adParam = new AdParam.Builder().build();
//        MRewardLoadListener rewardAdLoadListener = new MRewardLoadListener(ad, CombatRewardAdSuccessEvent);
//        ad.loadAd(adParam, rewardAdLoadListener);
//        }
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

    #region OnDestroy
    private void UnloadEventsEventCallback(Void _item)
    {
        Vungle.onInitializeEvent -= VungleInitialized;
        Vungle.adPlayableEvent -= AdPlayable;
        Vungle.onAdStartedEvent -= AdStarted;
        Vungle.onAdFinishedEvent -= AdFinished;
        Vungle.onErrorEvent -= VungleError;

        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion

    #region Testing

    public void TestAds()
    {
        if (Vungle.isInitialized())
        {
            Debug.Log("Vungle: Initialized!");
        }
        else
        {
            Debug.Log("Vungle: Not initialized, so initializing");
            Vungle.init(androidAppID);

        }

        if (Vungle.isAdvertAvailable(combatCompletedPlacementID))
        {
            Vungle.playAd(combatCompletedPlacementID);
        }
        else
        {
            Debug.Log($"Vungle: Ad not available");
        }
    }
    #endregion
}

