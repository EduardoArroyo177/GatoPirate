using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesView : MonoBehaviour
{
    public PlayerResourcesController PlayerResourcesController { get; set; }

    public void Pause()
    {
        PlayerResourcesController.PauseGame();
    }
}
