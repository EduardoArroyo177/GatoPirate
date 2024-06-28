using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private MainMenuView mainMenuView;

    public VoidEvent CatSelectedEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CatSelectedEvent, CatSelectedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));
    }

    private void CatSelectedEventCallback(Void _item)
    {
        // There's a zoom in, so we disable main menu
        mainMenuView.gameObject.SetActive(false);
    }

    #region OnDestroy
    private void UnloadEventsEventCallback(Void _item)
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
