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
    public FloatEvent SetMusicVolumeEvent { get; set; }
    public VoidEvent SetPreviousMusicVolumeEvent { get; set; }

    // Result screen events
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }
    // Ad events
    public VoidEvent LoadReviveAdEvent { get; set; }
    public VoidEvent LoadDoubleRewardAdEvent { get; set; }
    public VoidEvent LoadCombatFinishedAdEvent { get; set; }
    public VoidEvent ReviveSuccessEvent { get; set; }
    public VoidEvent DoubleRewardSuccessEvent { get; set; }


    public void Initialize()
    {
        // Pause
        pauseController.PauseGameEvent = PauseGameEvent;
        pauseController.LoadCombatSceneEvent = LoadCombatSceneEvent;
        pauseController.LoadMainMenuSceneEvent = LoadMainMenuSceneEvent;
        pauseController.SetMusicVolumeEvent = SetMusicVolumeEvent;
        pauseController.SetPreviousMusicVolumeEvent = SetPreviousMusicVolumeEvent;
        pauseController.Initialize();

        // Player resources
        playerResourcesController.PauseGameEvent = PauseGameEvent;
        playerResourcesController.Initialize();

        // Result screen
        resultScreenController.ShowResultScreenEvent = ShowResultScreenEvent;
        resultScreenController.WinChestEvent = WinChestEvent;
        resultScreenController.LoadMainMenuSceneEvent = LoadMainMenuSceneEvent;
        resultScreenController.LoadReviveAdEvent = LoadReviveAdEvent;
        resultScreenController.LoadDoubleRewardAdEvent = LoadDoubleRewardAdEvent;
        resultScreenController.LoadCombatFinishedAdEvent = LoadCombatFinishedAdEvent;
        resultScreenController.ReviveSuccessEvent = ReviveSuccessEvent;
        resultScreenController.DoubleRewardSuccessEvent = DoubleRewardSuccessEvent;
        resultScreenController.Initialize();
    }
}
