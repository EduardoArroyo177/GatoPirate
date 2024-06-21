using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms;
using UnityEngine;
using static HuaweiAdsControllerCombat;
using Mosframe;

public class VungleAdsControllerCombat : MonoBehaviour
{
    [Header("Game IDs")]
    [SerializeField]
    private string windowsAppID;
    [SerializeField]
    private string doubleRewardPlacementID;
    [SerializeField]
    private string revivePlacementID;
    [SerializeField]
    private string combatCompletedPlacementID;

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
        // Vungle Init
        if (!Vungle.isInitialized())
            Vungle.init(windowsAppID);
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

        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CombatRewardAdSuccessEvent, CombatRewardAdSuccessEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadReviveAdEvent, LoadReviveAdEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadDoubleRewardAdEvent, LoadDoubleRewardAdEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(LoadCombatFinishedAdEvent, LoadCombatFinishedAdEventCallback));
        //RealtimeConsole.Instance.open();
    }

    public void TestAds()
    {
        if (Vungle.isInitialized())
        {
            Debug.Log("Vungle: Initialized!");
        }
        else
        {
            Debug.Log("Vungle: Not initialized, so initializing");
            Vungle.init(windowsAppID);

        }

        if (Vungle.isAdvertAvailable(revivePlacementID))
        {
            Vungle.playAd(revivePlacementID);
        }
        else
        {
            Debug.Log($"Vungle: Ad not available");
        }
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
    #endregion

    #region Event callbacks
    private void LoadReviveAdEventCallback(Void _item)
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

    private void LoadDoubleRewardAdEventCallback(Void _item)
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
    private void OnDestroy()
    {
        Vungle.onInitializeEvent -= VungleInitialized;
        Vungle.adPlayableEvent -= AdPlayable;
        Vungle.onAdStartedEvent -= AdStarted;
        Vungle.onAdFinishedEvent -= AdFinished;

        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}

