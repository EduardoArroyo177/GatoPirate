using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private PauseView pauseView;
    [SerializeField]
    private PausePopUpView popUpView;
    [SerializeField]
    private float pauseMusicVolume;

    public VoidEvent PauseGameEvent { get; set; }
    public VoidEvent LoadCombatSceneEvent { get; set; }
    public VoidEvent LoadMainMenuSceneEvent { get; set; }
    public FloatEvent SetMusicVolumeEvent { get; set; }
    public VoidEvent SetPreviousMusicVolumeEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(PauseGameEvent, PauseGameEventCallback));
        pauseView.PauseController = this;
        popUpView.PauseController = this;
    }

    #region Event callbacks
    private void PauseGameEventCallback(Void _item)
    {
        Time.timeScale = 0;
        pauseView.gameObject.SetActive(true);
        // TODO: Lower music volume
        SetMusicVolumeEvent.Raise(pauseMusicVolume);
    }
    #endregion

    #region Public methods
    public void ResumeGame()
    {
        pauseView.gameObject.SetActive(false);
        Time.timeScale = 1;
        SetPreviousMusicVolumeEvent.Raise();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SetPreviousMusicVolumeEvent.Raise();
        LoadCombatSceneEvent.Raise();
    }

    public void ShowQuitPopUp()
    {
        // TODO: show quit confirmation pop up
        popUpView.gameObject.SetActive(true);
    }

    public void QuitCombat()
    {
        SetPreviousMusicVolumeEvent.Raise();
        // Call scene loader
        Time.timeScale = 1;
        LoadMainMenuSceneEvent.Raise();
    }
    #endregion

    #region OnDestroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
