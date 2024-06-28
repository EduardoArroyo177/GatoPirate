using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class VirtualCameraControllerMainMenu : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera mainCamera;
    [SerializeField]
    private CinemachineVirtualCamera catSelectedCamera;
    [SerializeField]
    private CinemachineVirtualCamera shipSelectedCamera;

    // Cat camera
    public GameObjectEvent TriggerSelectedCatCameraEvent { get; set; }
    public VoidEvent CloseSelectedCatCameraEvent { get; set; }

    // Ship camera
    public VoidEvent TriggerShipCameraEvent { get; set; }
    public VoidEvent CloseShipCameraEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        // Cats
        _eventHandlers.Add(EventHandlerFactory<GameObject>.BuildEventHandler(TriggerSelectedCatCameraEvent, TriggerSelectedCatCameraEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseSelectedCatCameraEvent, CloseSelectedCatCameraEventCallback));
        // Ships
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerShipCameraEvent, TriggerShipCameraEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseShipCameraEvent, CloseShipCameraEventCallback));

        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));
    }

    #region Event callbacks
    // Cats
    private void TriggerSelectedCatCameraEventCallback(GameObject _target)
    {
        catSelectedCamera.Follow = _target.transform;
        catSelectedCamera.gameObject.SetActive(true);
    }

    private void CloseSelectedCatCameraEventCallback(Void _item)
    {
        catSelectedCamera.gameObject.SetActive(false);
    }

    // Ship
    private void TriggerShipCameraEventCallback(Void _item)
    {
        shipSelectedCamera.gameObject.SetActive(true);
    }

    private void CloseShipCameraEventCallback(Void _item)
    {
        shipSelectedCamera.gameObject.SetActive(false);
    }
    #endregion


    #region OnDestroy
    private void UnloadEventsEventCallback(Void _item)
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
