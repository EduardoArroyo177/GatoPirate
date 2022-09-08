using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopUpView : MonoBehaviour
{
    public PauseController PauseController { get; set; }

    public void Quit()
    {
        PauseController.QuitCombat();
    }
}
