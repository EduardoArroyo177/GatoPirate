using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject virtualCamera1;
    [SerializeField]
    private GameObject shakingCamera;
    [SerializeField]
    private GameObject startingCameraPlayer;
    [SerializeField]
    private GameObject startingCameraEnemy;

    public FloatEvent TriggerShakingCameraEvent { get; set; }
    public VoidEvent TriggerPlayerStartingAnimationEvent { get; set; }
    public VoidEvent TriggerEnemyStartingAnimationEvent { get; set; }
    public VoidEvent StartingAnimationsFinishedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(TriggerShakingCameraEvent, TriggerShakingCameraEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerPlayerStartingAnimationEvent, TriggerPlayerStartingAnimationEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerEnemyStartingAnimationEvent, TriggerEnemyStartingAnimationEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartingAnimationsFinishedEvent, StartingAnimationsFinishedEventCallback));

    }

    #region Starting camera animation
    private void TriggerPlayerStartingAnimationEventCallback(Void _item)
    {
        //virtualCamera1.SetActive(false);
        startingCameraPlayer.SetActive(true);
    }

    private void TriggerEnemyStartingAnimationEventCallback(Void _item)
    {
        startingCameraEnemy.SetActive(true);
        startingCameraPlayer.SetActive(false);
    }

    private void StartingAnimationsFinishedEventCallback(Void _item)
    {
        //virtualCamera1.SetActive(true);
        startingCameraEnemy.SetActive(false);
    }
    #endregion

    #region Shaking camera
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
    #endregion

    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }

}
