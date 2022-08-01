using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CatRecruitmentPopUpsView : MonoBehaviour
{
    [SerializeField]
    private GameObject popup_notEnoughResources;
    [SerializeField]
    private GameObject popup_crewManagement;

    public VoidEvent OpenGoToStorePopUpEvent { get; set; }
    public VoidEvent OpenStoreEvent { get; set; }
    public VoidEvent WatchAdForCoinsEvent { get; set; }

    public VoidEvent OpenCrewManagementPopUpEvent { get; set; }

    public VoidEvent CloseRecruitmentViewEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenGoToStorePopUpEvent, OpenGoToStorePopUpEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenCrewManagementPopUpEvent, OpenCrewManagementPopUpEventCallback));
    }

    private void OpenGoToStorePopUpEventCallback(Void _item)
    {
        popup_notEnoughResources.SetActive(true);
        gameObject.SetActive(true);
    }

    private void OpenCrewManagementPopUpEventCallback(Void _item)
    {
        popup_crewManagement.SetActive(true);
        gameObject.SetActive(true);
    }

    #region Not enough resources pop up
    public void OpenStore()
    {
        OpenStoreEvent.Raise();
    }

    public void WatchAnAd()
    {
        // TODO: Check if we need to create an enum for knowing what type of ad to show
        WatchAdForCoinsEvent.Raise(); 
    }
    #endregion

    #region Crew management pop up
    public void GoToCrewManagement()
    {
        Debug.Log("GO TO CREW MANAGEMENT");
    }

    public void ExitCatRecruitment()
    {
        CloseRecruitmentViewEvent.Raise();
        popup_crewManagement.SetActive(false);
        gameObject.SetActive(false);
    }
    #endregion


    // On destroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
}
