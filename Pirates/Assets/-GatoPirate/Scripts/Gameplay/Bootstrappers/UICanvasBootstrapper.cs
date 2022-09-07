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

    // Pause events
    public VoidEvent PauseGameEvent { get; set; }
    // Result screen events
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }

    public void Initialize()
    {
        // Pause
        pauseController.PauseGameEvent = PauseGameEvent;
        pauseController.Initialize();

        // Result screen
        resultScreenController.ShowResultScreenEvent = ShowResultScreenEvent;
        resultScreenController.WinChestEvent = WinChestEvent;
        resultScreenController.Initialize();
    }
}
