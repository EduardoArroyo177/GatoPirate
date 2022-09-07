using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private PauseView pauseView;

    public VoidEvent PauseGameEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(PauseGameEvent, PauseGameEventCallback));
        pauseView.pauseController = this;
    }

    #region Event callbacks
    private void PauseGameEventCallback(Void _item)
    {
        Time.timeScale = 0;
        pauseView.gameObject.SetActive(true);
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
        // TODO: Call scene loader
    }

    public void QuitCombat()
    { 
        // TODO: show quit confirmation pop up
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
