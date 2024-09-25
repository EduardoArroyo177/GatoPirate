using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VungleAdsTest : MonoBehaviour
{
    [Header("Game IDs")]
    [SerializeField]
    private string androidAppID;

    [Header("Game IDs")]
    [SerializeField]
    private string freeCoinsRecruitmentPlacementID;

    private void Start()
    {
        
    }

    public void Initialize()
    {
        Debug.Log("INITIALIZING!");
        if (!Vungle.isInitialized())
            Vungle.init(androidAppID);
        else
        {
            Vungle.loadAd(freeCoinsRecruitmentPlacementID);
        }
        Vungle.onInitializeEvent += VungleInitialized;
        Vungle.adPlayableEvent += AdPlayable;
        Vungle.onAdStartedEvent += AdStarted;
        Vungle.onAdEndEvent += AdFinished;
        Debug.Log("COMPLETED INITIALIZE");
    }

    public void LoadAd()
    {
        if (Vungle.isAdvertAvailable(freeCoinsRecruitmentPlacementID))
        {
            Vungle.playAd(freeCoinsRecruitmentPlacementID);
        }
        else
        {
            Debug.Log($"Vungle: Ad not available");
        }
    }

    public void SetLog()
    {
        Vungle.setLogEnable(true);
    }

    #region Vungle init
    private void VungleInitialized()
    {
        Debug.Log("VUNGLE INITIALIZED°!!!!!!!!");
        Debug.Log("Vungle: Vungle initialized succesfully");
        Vungle.loadAd(freeCoinsRecruitmentPlacementID);
    }

    private void AdPlayable(string _placementID, bool _playable)
    {
        Debug.Log($"Vungle: Placement id? {_placementID} playable? {_playable}");
    }

    private void AdStarted(string _placementID)
    {
        Debug.Log($"Vungle: Ad started with placement id {_placementID}");
    }

    private void AdFinished(string _placementID)
    {
        Debug.Log($"Add finished placement {_placementID}");
    }
    #endregion

}
