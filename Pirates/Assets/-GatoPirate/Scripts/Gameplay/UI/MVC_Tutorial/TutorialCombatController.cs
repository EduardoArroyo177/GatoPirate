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
    public VoidEvent UnloadEventsEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerCombatTutorialEvent, TriggerCombatTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerCombatWeakSpotTutorialEvent, TriggerCombatWeakSpotTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerCombatResourcesBoxTutorialEvent, TriggerCombatResourcesBoxTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));

        tutorialCombatView.TutorialCombatController = this;
        tutorialWeakSpotView.TutorialCombatController = this;
        tutorialResourcesBoxView.TutorialCombatController = this;
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
            // Trigger tutorial with delay
            StartCoroutine("ShowWeakSpotTutorialDelayed");
        }
    }

    private void TriggerCombatResourcesBoxTutorialEventCallback(Void _item)
    {
        if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.COMBAT_RESOURCES_BOX))
        {
            StartCoroutine("ShowResourcesBoxTutorialDelayed");
        }
    }
    #endregion

    #region Tutorial helper methods
    private IEnumerator ShowWeakSpotTutorialDelayed()
    {
        yield return new WaitForSeconds(tutorialWeakSpotView.TutorialShownDelay);
        // Pause the game
        PauseWihtoutScreenEvent.Raise();
        // Show weak spot tutorial
        tutorialWeakSpotView.gameObject.SetActive(true);
    }

    private IEnumerator ShowResourcesBoxTutorialDelayed()
    {
        yield return new WaitForSeconds(tutorialResourcesBoxView.TutorialShownDelay);
        // Pause the game
        PauseWihtoutScreenEvent.Raise();
        // Show resources box tutorial
        tutorialResourcesBoxView.gameObject.SetActive(true);
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
        TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.COMBAT_WEAK_SPOT);
        ResumeGameEvent.Raise();
        tutorialWeakSpotView.gameObject.SetActive(false);
    }

    public void CompleteResourcesBoxTutorial()
    {
        TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.COMBAT_RESOURCES_BOX);
        ResumeGameEvent.Raise();
        tutorialResourcesBoxView.gameObject.SetActive(false);
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
