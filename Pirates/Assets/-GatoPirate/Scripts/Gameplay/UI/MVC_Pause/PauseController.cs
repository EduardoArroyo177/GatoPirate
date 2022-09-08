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

    public VoidEvent PauseGameEvent { get; set; }
    public VoidEvent LoadCombatSceneEvent { get; set; }
    public VoidEvent LoadMainMenuSceneEvent { get; set; }

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
    }
    #endregion

    #region Public methods
    public void ResumeGame()
    {
        pauseView.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        LoadCombatSceneEvent.Raise();
    }

    public void ShowQuitPopUp()
    {
        // TODO: show quit confirmation pop up
        popUpView.gameObject.SetActive(true);
    }

    public void QuitCombat()
    {
        // TODO: Call scene loader
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
