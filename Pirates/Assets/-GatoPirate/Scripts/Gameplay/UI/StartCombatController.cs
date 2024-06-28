using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StartCombatController : MonoBehaviour
{
    [SerializeField]
    private GameObject startCombatView;
    [SerializeField]
    private float startCombatDelay;
    [SerializeField]
    private GameObject btn_skipIntro;

    public VoidEvent TriggerCombatTutorialEvent { get; set; }
    public VoidEvent StartingAnimationsFinishedEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartingAnimationsFinishedEvent, StartingAnimationsFinishedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));
    }

    private void StartingAnimationsFinishedEventCallback(Void _item)
    {
        startCombatView.SetActive(true);
    }

    public void StartCombat()
    {
        Invoke("StartCombatDelayed", startCombatDelay);
    }

    private void StartCombatDelayed()
    {
        TriggerCombatTutorialEvent.Raise();
        btn_skipIntro.SetActive(false);
        startCombatView.SetActive(false);
    }

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
