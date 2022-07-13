using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyBuildShipController : MonoBehaviour
{
    [SerializeField]
    private DOTweenAnimation doTweenAnimation;

    public VoidEvent TriggerEnemyStartingAnimationEvent { get; set; }
    public VoidEvent StartingAnimationsFinishedEvent { get; set; }
    public VoidEvent SkipInitialAnimationsEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerEnemyStartingAnimationEvent, TriggerEnemyStartingAnimationEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(SkipInitialAnimationsEvent, SkipInitialAnimationsEventCallback));
    }

    public void TriggerStartingAnimationsFinished()
    {
        Invoke("StartingAnimationsFinished", 1f);
    }

    private void StartingAnimationsFinished()
    {
        StartingAnimationsFinishedEvent.Raise();
    }

    private void TriggerEnemyStartingAnimationEventCallback(Void _item)
    {
        doTweenAnimation.DOPlay();
    }

    private void SkipInitialAnimationsEventCallback(Void _item)
    {
        doTweenAnimation.DOComplete();
    }

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
