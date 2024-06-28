using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CatOptionsController : MonoBehaviour
{
    [SerializeField]
    private CatOptionsView catOptionsView;
    
    public StringEvent OpenSelectedCatOptionsEvent { get; set; }
    public VoidEvent CloseSelectedCatCameraEvent { get; set; }
    public StringEvent OpenCatCrewManagementEvent { get; set; }
    public StringEvent OpenSkinManagementEvent { get; set; }
    public VoidEvent UnloadEventsEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();

    private string catID = "";

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(OpenSelectedCatOptionsEvent, OpenSelectedCatOptionsEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));
        catOptionsView.catOptionsController = this;
    }

    #region Event callbacks
    private void OpenSelectedCatOptionsEventCallback(string _catID)
    {
        catID = _catID;
        catOptionsView.gameObject.SetActive(true);
    }
    #endregion

    #region Public methods
    public void CloseSelectedCamera()
    {
        CloseSelectedCatCameraEvent.Raise();
    }

    public void OpenCatCrewManagement()
    {
        OpenCatCrewManagementEvent.Raise(catID);
    }

    public void OpenSkinManagementScreen()
    {
        OpenSkinManagementEvent.Raise(catID);
    }
    #endregion

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
