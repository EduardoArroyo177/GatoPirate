using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWeakSpotView : MonoBehaviour
{
    [SerializeField]
    private float tutorialShownDelay;

    public TutorialCombatController TutorialCombatController { get; set; }
    public float TutorialShownDelay { get => tutorialShownDelay; set => tutorialShownDelay = value; }

    #region Button methods
    public void FinishTutorial()
    {
        TutorialCombatController.CompleteWeakSpotTutorial();
    }

    // Analytics
    public void TutorialFinishedAnalytics()
    {
        GameAnalyticsController.Instance.GameFlowEvent($"Tutorial:WeakSpot:Finished");
    }

    public void SkipTutorial()
    {
        GameAnalyticsController.Instance.GameFlowEvent("Tutorial:WeakSpot:Skip");
    }

    public void TutorialStepFinished(int _step)
    {
        GameAnalyticsController.Instance.GameFlowEvent($"Tutorial:WeakSpot:StepCompleted:{_step}");
    }
    #endregion
}
