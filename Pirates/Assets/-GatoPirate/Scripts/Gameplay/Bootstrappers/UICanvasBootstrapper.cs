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
    [SerializeField]
    private TutorialCombatController tutorialCombatController;

    // Pause events
    public VoidEvent PauseGameEvent { get; set; }
    public VoidEvent PauseWihtoutScreenEvent { get; set; }
    public VoidEvent ResumeGameEvent { get; set; }
    public VoidEvent LoadCombatSceneEvent { get; set; }
    public VoidEvent LoadMainMenuSceneEvent { get; set; }
    public FloatEvent SetMusicVolumeEvent { get; set; }
    public VoidEvent SetPreviousMusicVolumeEvent { get; set; }

    // Result screen events
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }
    public VoidEvent CurrenciesUpdatedEvent { get; set; }
    // Ad events
    public VoidEvent LoadReviveAdEvent { get; set; }
    public VoidEvent LoadDoubleRewardAdEvent { get; set; }
    public VoidEvent LoadCombatFinishedAdEvent { get; set; }
    public VoidEvent ReviveSuccessEvent { get; set; }
    public VoidEvent DoubleRewardSuccessEvent { get; set; }
    // Audio events
    public CombatSoundEvent TriggerCombatSoundEvent { get; set; }
    // IAP events
    public VoidEvent RemoveAdsPurchasedEvent { get; set; }

    // Tutorial events
    public VoidEvent StartCombatEvent { get; set; }
    public VoidEvent TriggerCombatTutorialEvent { get; set; }
    public VoidEvent TriggerCombatWeakSpotTutorialEvent { get; set; }
    public VoidEvent TriggerCombatResourcesBoxTutorialEvent { get; set; }

    // Properties
    public int ReviveCurrencyPrice { get; set; }


    public void Initialize()
    {
        // Pause
        pauseController.PauseGameEvent = PauseGameEvent;
        pauseController.PauseWihtoutScreenEvent = PauseWihtoutScreenEvent;
        pauseController.ResumeGameEvent = ResumeGameEvent;
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
        resultScreenController.RemoveAdsPurchasedEvent = RemoveAdsPurchasedEvent;
        resultScreenController.ReviveCurrencyPrice = ReviveCurrencyPrice;
        resultScreenController.CurrenciesUpdatedEvent = CurrenciesUpdatedEvent;
        resultScreenController.TriggerCombatSoundEvent = TriggerCombatSoundEvent;
        resultScreenController.Initialize();

        // Tutorial 
        tutorialCombatController.StartCombatEvent = StartCombatEvent;
        tutorialCombatController.PauseWihtoutScreenEvent = PauseWihtoutScreenEvent;
        tutorialCombatController.ResumeGameEvent = ResumeGameEvent;
        tutorialCombatController.TriggerCombatTutorialEvent = TriggerCombatTutorialEvent;
        tutorialCombatController.TriggerCombatWeakSpotTutorialEvent = TriggerCombatWeakSpotTutorialEvent;
        tutorialCombatController.TriggerCombatResourcesBoxTutorialEvent = TriggerCombatResourcesBoxTutorialEvent;
        tutorialCombatController.Initialize();
    }
}
