using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class IslandShipButton : MonoBehaviour
{
    public VoidEvent TriggerShipCameraEvent { get; set; }
    public VoidEvent OpenShipOptionsEvent { get; set; }
    public VoidEvent CatSelectedEvent { get; set; }

    public VoidEvent CloseSelectedCatCameraEvent { get; set; }
    public VoidEvent CloseShipCameraEvent { get; set; }
    public BoolEvent OpenScreenEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    private bool isZoomedIn;
    private bool isScreenOpen;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseSelectedCatCameraEvent, CloseSelectedCatCameraEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseShipCameraEvent, CloseSelectedCatCameraEventCallback));
        _eventHandlers.Add(EventHandlerFactory<bool>.BuildEventHandler(OpenScreenEvent, OpenScreenEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CatSelectedEvent, CatSelectedEventCallback));

    }

    private void OnMouseUpAsButton()
    {
        if (!isZoomedIn && !isScreenOpen)
        {
            // Zoom in to ship
            TriggerShipCameraEvent.Raise();
            // Open ship options
            OpenShipOptionsEvent.Raise();
            // Block cats so they cannot be pressed as buttons
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
