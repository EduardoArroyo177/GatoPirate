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

    public GameObjectEvent TriggerSelectedCatCameraEvent { get; set; }
    public VoidEvent CloseSelectedCatCameraEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<GameObject>.BuildEventHandler(TriggerSelectedCatCameraEvent, TriggerSelectedCatCameraEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CloseSelectedCatCameraEvent, CloseSelectedCatCameraEventCallback));
    }

    #region Event callbacks
    private void TriggerSelectedCatCameraEventCallback(GameObject _target)
    {
        catSelectedCamera.Follow = _target.transform;
        catSelectedCamera.gameObject.SetActive(true);
    }

    private void CloseSelectedCatCameraEventCallback(Void _item)
    {
        catSelectedCamera.gameObject.SetActive(false);
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
