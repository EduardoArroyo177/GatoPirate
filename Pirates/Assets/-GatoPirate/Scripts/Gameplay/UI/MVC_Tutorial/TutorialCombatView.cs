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
    #endregion
}
