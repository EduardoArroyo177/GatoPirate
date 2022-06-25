using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipHealthUIController : MonoBehaviour
{
    [SerializeField]
    private Image Img_currentHealth;

    public FloatEvent CurrentHealthUIEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(CurrentHealthUIEvent, CurrentHealthUIEventCallback));
    }

    private void CurrentHealthUIEventCallback(float _healthValue)
    {
        Img_currentHealth.fillAmount = _healthValue;
    }


}
