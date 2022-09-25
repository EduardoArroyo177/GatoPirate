using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class TutorialCombatController : MonoBehaviour
{
    [SerializeField]
    private TutorialCombatView tutorialCombatView;
    [SerializeField]
    private TutorialWeakSpotView tutorialWeakSpotView;
    [SerializeField]
    private TutorialResourcesBoxView tutorialResourcesBoxView;

    public VoidEvent StartCombatEvent { get; set; }
    public VoidEvent PauseWihtoutScreenEvent { get; set; }
    public VoidEvent ResumeGameEvent { get; set; }
    public VoidEvent TriggerCombatTutorialEvent { get; set; }
    public VoidEvent TriggerCombatWeakSpotTutorialEvent { get; set; }
    public VoidEvent TriggerCombatResourcesBoxTutorialEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerCombatTutorialEvent, TriggerCombatTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerCombatWeakSpotTutorialEvent, TriggerCombatWeakSpotTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerCombatResourcesBoxTutorialEvent, TriggerCombatResourcesBoxTutorialEventCallback));

        tutorialCombatView.TutorialCombatController = this;
    }

    #region Event callbacks
    private void TriggerCombatTutorialEventCallback(Void _item)
    {
        if (TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.COMBAT))
        {
            // Trigger start game as usual
            StartCombatEvent.Raise();
        }
        else
        {
            tutorialCombatView.gameObject.SetActive(true);
        }
    }

    private void TriggerCombatWeakSpotTutorialEventCallback(Void _item)
    {
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.COMBAT_WEAK_SPOT))
        {
            // TODO: Pause the game
            PauseWihtoutScreenEvent.Raise();
            // TODO: Show weak spot tutorial
        }
    }

    private void TriggerCombatResourcesBoxTutorialEventCallback(Void _item)
    {
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.COMBAT_RESOURCES_BOX))
        {
            // TODO: Pause the game
            PauseWihtoutScreenEvent.Raise();
            // TODO: Show resources box tutorial
        }
    }
    #endregion

    #region Public methods
    public void CompleteCombatTutorial()
    {
        TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.COMBAT);
        // Start combat
        StartCombatEvent.Raise();
        tutorialCombatView.gameObject.SetActive(false);
    }

    public void CompleteWeakSpotTutorial()
    {
        ResumeGameEvent.Raise();

    }

    public void CompleteResourcesBoxTutorial()
    {
        ResumeGameEvent.Raise();
    }
    #endregion

    #region OnDestroy
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
