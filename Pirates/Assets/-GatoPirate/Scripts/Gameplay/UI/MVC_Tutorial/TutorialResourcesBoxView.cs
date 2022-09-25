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
    #endregion
}
