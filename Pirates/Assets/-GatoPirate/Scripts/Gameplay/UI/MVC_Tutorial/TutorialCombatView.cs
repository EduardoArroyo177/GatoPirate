using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCombatView : MonoBehaviour
{

    public TutorialCombatController TutorialCombatController { get; set; }

    #region Button methods
    public void FinishTutorial()
    {
        TutorialCombatController.CompleteCombatTutorial();
    }

    // Analytics
    public void TutorialFinishedAnalytics()
    {
        GameAnalyticsController.Instance.GameFlowEvent($"Tutorial:Combat:Finished");
    }

    public void SkipTutorial()
    {
        GameAnalyticsController.Instance.GameFlowEvent("Tutorial:Combat:Skip");
    }

    public void TutorialStepFinished(int _step)
    {
        GameAnalyticsController.Instance.GameFlowEvent($"Tutorial:Combat:StepCompleted:{_step}");
    }
    #endregion
}
