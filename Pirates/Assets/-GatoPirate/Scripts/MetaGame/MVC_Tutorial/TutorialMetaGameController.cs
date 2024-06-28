using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class TutorialMetaGameController : MonoBehaviour
{
    [SerializeField]
    private TutorialMetaGameView tutorialMetaGameView;
    [SerializeField]
    private TutorialMetaGameRecruitmentView tutorialMetaGameRecruitmentView;
    [SerializeField]
    private TutorialMetaGameIslandView tutorialMetaGameIslandView;
    [SerializeField]
    private TutorialMetaGameCrewView tutorialMetaGameCrewView;

    public VoidEvent TriggerMetaGameTutorialEvent { get; set; }
    public VoidEvent TriggerMetaGameRecruitmentTutorialEvent { get; set; }
    public VoidEvent TriggerMetaGameIslandTutorialEvent { get; set; }
    public VoidEvent TriggerMetaGameCrewTutorialEvent { get; set; }
    public VoidEvent FreeRecruitmentTutorialEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerMetaGameTutorialEvent, TriggerMetaGameTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerMetaGameRecruitmentTutorialEvent, TriggerMetaGameRecruitmentTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerMetaGameIslandTutorialEvent, TriggerMetaGameIslandTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerMetaGameCrewTutorialEvent, TriggerMetaGameCrewTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(FreeRecruitmentTutorialEvent, FreeRecruitmentTutorialEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));

        tutorialMetaGameView.TutorialMetaGameController = this;
        tutorialMetaGameRecruitmentView.TutorialMetaGameController = this;
        tutorialMetaGameIslandView.TutorialMetaGameController = this;
        tutorialMetaGameCrewView.TutorialMetaGameController= this;
    }

    #region Event callbacks
    private void TriggerMetaGameTutorialEventCallback(Void _item)
    {
        StartCoroutine("ShowMetaGameTutorialDelayed");
    }

    private void TriggerMetaGameRecruitmentTutorialEventCallback(Void _item)
    {
        StartCoroutine("ShowRecruitmentTutorialDelayed");
    }

    private void TriggerMetaGameIslandTutorialEventCallback(Void _item)
    {
        StartCoroutine("ShowIslandTutorialDelayed");
    }

    private void TriggerMetaGameCrewTutorialEventCallback(Void _item)
    {
        StartCoroutine("ShowCrewTutorialDelayed");
    }

    private void FreeRecruitmentTutorialEventCallback(Void _item)
    {
        TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.FREE_FIRST_RECRUITMENT);
    }
    #endregion

    #region Tutorial helper methods
    private IEnumerator ShowMetaGameTutorialDelayed()
    {
        yield return new WaitForSeconds(tutorialMetaGameView.TutorialShownDelay);
        // Show meta game tutorial
        tutorialMetaGameView.gameObject.SetActive(true);
    }

    private IEnumerator ShowRecruitmentTutorialDelayed()
    {
        yield return new WaitForSeconds(tutorialMetaGameRecruitmentView.TutorialShownDelay);
        // Show meta game tutorial
        tutorialMetaGameRecruitmentView.gameObject.SetActive(true);
    }

    private IEnumerator ShowIslandTutorialDelayed()
    {
        yield return new WaitForSeconds(tutorialMetaGameIslandView.TutorialShownDelay);
        // Show meta game tutorial
        tutorialMetaGameIslandView.gameObject.SetActive(true);
    }

    private IEnumerator ShowCrewTutorialDelayed()
    {
        yield return new WaitForSeconds(tutorialMetaGameCrewView.TutorialShownDelay);
        // Show meta game tutorial
        tutorialMetaGameCrewView.gameObject.SetActive(true);
    }
    #endregion

    #region Public methods
    public void CompleteMetaGameTutorial()
    {
        TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.META_GAME);
        tutorialMetaGameView.gameObject.SetActive(false);
    }

    public void CompleteRecruitmentTutorial()
    {
        TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.META_GAME_RECRUITMENT);
        tutorialMetaGameRecruitmentView.gameObject.SetActive(false);
    }

    public void CompleteIslandTutorial()
    {
        TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.META_GAME_ISLAND);
        tutorialMetaGameIslandView.gameObject.SetActive(false);
    }

    public void CompleteCrewTutorial()
    {
        TutorialDataSaveManager.Instance.UpdateTutorialCompleted(TutorialType.META_GAME_CREW);
        tutorialMetaGameCrewView.gameObject.SetActive(false);
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
