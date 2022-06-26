using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthUIController : MonoBehaviour
{
    [SerializeField]
    private Image Img_currentHealth;

    public FloatEvent CurrentHealthUIEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    private void Awake()
    {
        Img_currentHealth.fillAmount = 1;
    }

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(CurrentHealthUIEvent, CurrentHealthUIEventCallback));
    }

    private void CurrentHealthUIEventCallback(float _healthValue)
    {
        Img_currentHealth.fillAmount = _healthValue;
    }

    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }

        _eventHandlers.Clear();
    }
}
