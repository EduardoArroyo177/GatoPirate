using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialResourcesBoxView : MonoBehaviour
{
    [SerializeField]
    private float tutorialShownDelay;

    public TutorialCombatController TutorialCombatController { get; set; }
    public float TutorialShownDelay { get => tutorialShownDelay; set => tutorialShownDelay = value; }

    #region Button methods
    public void FinishTutorial()
    {
        TutorialCombatController.CompleteResourcesBoxTutorial();
    }

    // Analytics
    public void TutorialFinishedAnalytics()
    {
        GameAnalyticsController.Instance.GameFlowEvent($"Tutorial:ResourcesBox:Finished");
    }

    public void SkipTutorial()
    {
        GameAnalyticsController.Instance.GameFlowEvent("Tutorial:ResourcesBox:Skip");
    }

    public void TutorialStepFinished(int _step)
    {
        GameAnalyticsController.Instance.GameFlowEvent($"Tutorial:ResourcesBox:StepCompleted:{_step}");
    }
    #endregion
}
