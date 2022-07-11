using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerBuildShipController : MonoBehaviour
{
    [Header("Cat Crew")]
    [SerializeField]
    private CatBodyBuilder[] catBodyBuilderList;

    public VoidEvent TriggerPlayerStartingAnimationEvent { get; set; }
    public VoidEvent TriggerEnemyStartingAnimationEvent { get; set; }

    public CatData[] CatCrewDataList { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private DOTweenAnimation doTweenAnimation; 

    // Crew members
    public void Initialize()
    {
        doTweenAnimation = GetComponent<DOTweenAnimation>();

        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerPlayerStartingAnimationEvent, TriggerPlayerStartingAnimationEventCallback));

        for (int index = 0; index < CatCrewDataList.Length; index++)
        {
            catBodyBuilderList[index].CatData = CatCrewDataList[index];
            catBodyBuilderList[index].InitializeCat();
        }
    }

    public void TriggerEnemyEntranceAnimation()
    {
        Invoke("EnemyEntranceAnimation", 1f);
    }

    private void EnemyEntranceAnimation()
    {
        TriggerEnemyStartingAnimationEvent.Raise();
    }

    // Ship entrance animation

    private void TriggerPlayerStartingAnimationEventCallback(Void _item)
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
