using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class IslandSlotButton : MonoBehaviour
{
    public GameObjectEvent TriggerSelectedCatCameraEvent { get; set; }
    public StringEvent OpenSelectedCatOptionsEvent { get; set; }
    public VoidEvent CloseSelectedCatCameraEvent { get; set; }
    public VoidEvent CatSelectedEvent { get; set; }

    private string catID;// { get; set; }
    private bool isInitialized;
    private bool isZoomedIn;

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize(string _catID)
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseSelectedCatCameraEvent, CloseSelectedCatCameraEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CatSelectedEvent, CatSelectedEventCallback));

        isInitialized = true;
        catID = _catID;
    }

    private void OnMouseUpAsButton()
    {
        if (isInitialized && !isZoomedIn)
        {
            TriggerSelectedCatCameraEvent.Raise(gameObject);
            // TODO: Raise another event to open pop up (with cat id)
            OpenSelectedCatOptionsEvent.Raise(catID);
            // TODO: Mark cat with outline or something to show this is the selected cat
            CatSelectedEvent.Raise();
        }
    }

    #region Event callbacks
    private void CatSelectedEventCallback(Void _item)
    {
        isZoomedIn = true;
    }

    private void CloseSelectedCatCameraEventCallback(Void _item)
    {
        isZoomedIn = false;
    }
    #endregion

    #region On Destroy
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
