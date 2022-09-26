using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMetaGameRecruitmentView : MonoBehaviour
{
    [SerializeField]
    private float tutorialShownDelay;

    public TutorialMetaGameController TutorialMetaGameController { get; set; }
    public float TutorialShownDelay { get => tutorialShownDelay; set => tutorialShownDelay = value; }


    #region Button methods
    public void FinishTutorial()
    {
        TutorialMetaGameController.CompleteRecruitmentTutorial();
    }
    #endregion
}
