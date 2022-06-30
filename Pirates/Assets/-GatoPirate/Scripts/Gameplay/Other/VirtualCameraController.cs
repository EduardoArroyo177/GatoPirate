using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject shakingCamera;

    public FloatEvent TriggerShakingCameraEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(TriggerShakingCameraEvent, TriggerShakingCameraEventCallback));
    }

    private void TriggerShakingCameraEventCallback(float _duration)
    {
        shakingCamera.SetActive(true);
        StartCoroutine(DisableCameraAfterTime(shakingCamera, _duration));
    }

    private IEnumerator DisableCameraAfterTime(GameObject _cameraToDisable, float _duration)
    {
        yield return new WaitForSeconds(_duration);
        _cameraToDisable.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }

}
