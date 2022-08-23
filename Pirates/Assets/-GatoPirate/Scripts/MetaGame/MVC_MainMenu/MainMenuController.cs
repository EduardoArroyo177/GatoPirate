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

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(CatSelectedEvent, CatSelectedEventCallback));

    }

    private void CatSelectedEventCallback(Void _item)
    {
        // There's a zoom in, so we disable main menu
        mainMenuView.gameObject.SetActive(false);
    }

    #region On Destroy
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
