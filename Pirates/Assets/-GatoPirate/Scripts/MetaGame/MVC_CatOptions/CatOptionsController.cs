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

    private List<IAtomEventHandler> _eventHandlers = new();

    private string catID = "";

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(OpenSelectedCatOptionsEvent, OpenSelectedCatOptionsEventCallback));
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
