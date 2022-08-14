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

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(OpenSelectedCatOptionsEvent, OpenSelectedCatOptionsEventCallback));
        catOptionsView.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
    }

    #region Event callbacks
    private void OpenSelectedCatOptionsEventCallback(string _catID)
    { 
        
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
