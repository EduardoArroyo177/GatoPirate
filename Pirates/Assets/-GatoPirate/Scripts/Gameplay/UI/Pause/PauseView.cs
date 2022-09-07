using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseView : MonoBehaviour
{
    public PauseController pauseController;

    public void Continue()
    {
        pauseController.ResumeGame();
    }

    public void Restart()
    {
        pauseController.ResumeGame();
    }

    public void GoToMainMenu()
    {
        pauseController.QuitCombat();
    }
}
