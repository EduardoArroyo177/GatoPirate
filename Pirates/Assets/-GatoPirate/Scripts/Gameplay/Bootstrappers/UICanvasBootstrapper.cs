using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class UICanvasBootstrapper : MonoBehaviour
{
    [SerializeField]
    private ResultScreenController resultScreenController;
    [SerializeField]
    private PauseController pauseController;
    [SerializeField]
    private PlayerResourcesController playerResourcesController;

    // Pause events
    public VoidEvent PauseGameEvent { get; set; }
    public VoidEvent LoadCombatSceneEvent { get; set; }
    public VoidEvent LoadMainMenuSceneEvent { get; set; }

    // Result screen events
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }
    
    public void Initialize()
    {
        // Pause
        pauseController.PauseGameEvent = PauseGameEvent;
        pauseController.LoadCombatSceneEvent = LoadCombatSceneEvent;
        pauseController.LoadMainMenuSceneEvent = LoadMainMenuSceneEvent;    
        pauseController.Initialize();

        // Player resources
        playerResourcesController.PauseGameEvent = PauseGameEvent;
        playerResourcesController.Initialize();

        // Result screen
        resultScreenController.ShowResultScreenEvent = ShowResultScreenEvent;
        resultScreenController.WinChestEvent = WinChestEvent;
        resultScreenController.Initialize();
    }
}
