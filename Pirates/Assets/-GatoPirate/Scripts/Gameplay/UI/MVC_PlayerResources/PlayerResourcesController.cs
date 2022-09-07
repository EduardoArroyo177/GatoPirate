using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerResourcesController : MonoBehaviour
{
    [SerializeField]
    private PlayerResourcesView playerResourcesView;

    public VoidEvent PauseGameEvent { get; set; }

    public void Initialize()
    {
        playerResourcesView.PlayerResourcesController = this;
    }

    public void PauseGame()
    {
        PauseGameEvent.Raise();
    }

}
