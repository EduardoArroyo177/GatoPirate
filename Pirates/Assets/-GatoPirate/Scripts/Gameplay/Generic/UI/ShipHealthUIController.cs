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
    [SerializeField]
    private Color highHealthColor;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float highHealthValue;

    [SerializeField]
    private Color meddiumHealthColor;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float meddiumHealthValue;

    [SerializeField]
    private Color lowHealthColor;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float lowHealthValue;

    public FloatEvent CurrentHealthUIEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    private void Awake()
    {
        Img_currentHealth.color = highHealthColor;
        Img_currentHealth.fillAmount = 1;
    }

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(CurrentHealthUIEvent, CurrentHealthUIEventCallback));
    }

    private void CurrentHealthUIEventCallback(float _healthValue)
    {
        Img_currentHealth.fillAmount = _healthValue;

        //if (Img_currentHealth.fillAmount > highHealthValue)
        //    Img_currentHealth.color = highHealthColor;

        if (Img_currentHealth.fillAmount < highHealthValue
            && Img_currentHealth.fillAmount > meddiumHealthValue)
            Img_currentHealth.color = meddiumHealthColor;

        else if(Img_currentHealth.fillAmount < meddiumHealthValue)
            Img_currentHealth.color = lowHealthColor;

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
