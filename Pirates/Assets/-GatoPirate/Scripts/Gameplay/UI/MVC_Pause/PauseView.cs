using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseView : MonoBehaviour
{
    public PauseController PauseController { get; set; }

    public void Continue()
    {
        PauseController.ResumeGame();
    }

    public void Restart()
    {
        PauseController.RestartGame();
    }

    public void GoToMainMenu()
    {
        PauseController.QuitCombat();
    }

    public void ClosePause()
    {
        PauseController.ResumeGame();
    }
}
