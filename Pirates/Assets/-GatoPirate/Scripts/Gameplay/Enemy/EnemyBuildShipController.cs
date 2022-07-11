using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyBuildShipController : MonoBehaviour
{
    public VoidEvent TriggerEnemyStartingAnimationEvent { get; set; }
    public VoidEvent StartingAnimationsFinishedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private DOTweenAnimation doTweenAnimation;

    public void Initialize()
    {
        doTweenAnimation = GetComponent<DOTweenAnimation>();
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerEnemyStartingAnimationEvent, TriggerEnemyStartingAnimationEventCallback));
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
