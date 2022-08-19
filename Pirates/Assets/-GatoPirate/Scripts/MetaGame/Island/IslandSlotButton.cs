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
    public BoolEvent OpenScreenEvent { get; set; }

    private string catID;// { get; set; }
    private bool isInitialized;
    private bool isZoomedIn;
    private bool isScreenOpen;

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize(string _catID)
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseSelectedCatCameraEvent, CloseSelectedCatCameraEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CatSelectedEvent, CatSelectedEventCallback));
        _eventHandlers.Add(EventHandlerFactory<bool>.BuildEventHandler(OpenScreenEvent, OpenScreenEventCallback));

        isInitialized = true;
        catID = _catID;
    }

    private void OnMouseUpAsButton()
    {
        if (isInitialized && !isZoomedIn && !isScreenOpen)
        {
            // Camera zoom
            TriggerSelectedCatCameraEvent.Raise(gameObject);
            // Cat options pop up
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

    private void OpenScreenEventCallback(bool _isScreenOpen)
    {
        isScreenOpen = _isScreenOpen;
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
