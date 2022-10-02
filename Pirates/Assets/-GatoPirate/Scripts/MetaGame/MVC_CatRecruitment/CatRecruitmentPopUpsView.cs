using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CatRecruitmentPopUpsView : MonoBehaviour
{
    [SerializeField]
    private GameObject contentWatchAd;
    [SerializeField]
    private GameObject contentStoreOnly;
    [SerializeField]
    private GameObject btn_watchAd;

    public VoidEvent OpenGoToStorePopUpEvent { get; set; }
    public VoidEvent OpenStoreEvent { get; set; }
    public VoidEvent WatchAdForCoinsEvent { get; set; }
    public VoidEvent RemoveAdsPurchasedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenGoToStorePopUpEvent, OpenGoToStorePopUpEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(RemoveAdsPurchasedEvent, RemoveAdsPurchasedEventCallback));
    }

    #region Event callbacks
    private void OpenGoToStorePopUpEventCallback(Void _item)
    {
        gameObject.SetActive(true);
    }

    private void RemoveAdsPurchasedEventCallback(Void _item)
    {
        contentWatchAd.SetActive(false);
        contentStoreOnly.SetActive(true);
        btn_watchAd.SetActive(false);
    }
    #endregion

    #region Not enough resources pop up buttons
    public void OpenStore()
    {
        OpenStoreEvent.Raise();
    }

    public void WatchAnAd()
    {
        // TODO: Check if we need to create an enum for knowing what type of ad to show
        WatchAdForCoinsEvent.Raise(); 
    }
    #endregion buttons


    // On destroy
    private void OnDestroy()
    {
        CleanListeners();
    }

    public void CleanListeners()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
}
