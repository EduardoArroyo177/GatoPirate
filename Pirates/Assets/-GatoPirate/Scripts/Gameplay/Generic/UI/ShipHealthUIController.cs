using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthUIController : MonoBehaviour
{
    [SerializeField]
    private bool isUI;
    [SerializeField]
    private Image Img_currentHealth;
    [SerializeField]
    private Transform lifeBar;
    [SerializeField]
    private SpriteRenderer lifeBarRenderer;
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
    public FloatEvent TriggerShakingCameraEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(CurrentHealthUIEvent, CurrentHealthUIEventCallback));
        if (isUI)
            Img_currentHealth.color = highHealthColor;
        else
            lifeBarRenderer.color = highHealthColor;
    }

    private void CurrentHealthUIEventCallback(float _healthValue)
    {
        if (isUI)
        {
            Img_currentHealth.fillAmount = _healthValue;

            if (Img_currentHealth.fillAmount < highHealthValue
                && Img_currentHealth.fillAmount > meddiumHealthValue)
                Img_currentHealth.color = meddiumHealthColor;

            else if (Img_currentHealth.fillAmount < meddiumHealthValue)
                Img_currentHealth.color = lowHealthColor;
        }
        else
        {
            lifeBar.localScale = new Vector3(_healthValue, 1, 1);

            if (lifeBar.localScale.x < highHealthValue
                && lifeBar.localScale.x > meddiumHealthValue)
                lifeBarRenderer.color = meddiumHealthColor;

            else if(lifeBar.localScale.x < meddiumHealthValue)
                lifeBarRenderer.color = lowHealthColor; 

        }

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
