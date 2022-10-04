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

    [Header("High health")]
    [SerializeField]
    private Color highHealthColor;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float highHealthValue;
    [SerializeField]
    private GameObject firstDamageGroup;

    [Header("Medium health")]
    [SerializeField]
    private Color mediumHealthColor;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float mediumHealthValue;
    [SerializeField]
    private GameObject secondDamageGroup;

    [Header("Low health")]
    [SerializeField]
    private Color lowHealthColor;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float lowHealthValue;
    [SerializeField]
    private GameObject thirdDamageGroup;


    public FloatEvent CurrentHealthUIEvent { get; set; }
    // Cat faces
    public VoidEvent UpdateToWorriedFaceEvent { get; set; }
    public VoidEvent UpdateToSadFaceEvent { get; set; }
    
    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    private bool mediumHealthUpdated;
    private bool lowHealthUpdated;

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

            // TODO: Ask for a 20%

            if (Img_currentHealth.fillAmount < highHealthValue
                && Img_currentHealth.fillAmount > mediumHealthValue)
                Img_currentHealth.color = mediumHealthColor;

            else if (Img_currentHealth.fillAmount < mediumHealthValue)
                Img_currentHealth.color = lowHealthColor;
        } // Not used anymore?
        else
        {
            lifeBar.localScale = new Vector3(_healthValue, 1, 1);

            // TODO: Ask for a 20%

            if (lifeBar.localScale.x == 1)
            {
                lifeBarRenderer.color = highHealthColor;
                mediumHealthUpdated = false;
                lowHealthUpdated = false;
            }

            if (lifeBar.localScale.x < 1
                && lifeBar.localScale.x > highHealthValue)
            {
                if (firstDamageGroup)
                    firstDamageGroup.SetActive(true);
            }

            
            else if (lifeBar.localScale.x < highHealthValue
                && lifeBar.localScale.x > mediumHealthValue)
            {
                if (!mediumHealthUpdated)
                {
                    lifeBarRenderer.color = mediumHealthColor;
                    if (secondDamageGroup)
                        secondDamageGroup.SetActive(true);
                    UpdateToWorriedFaceEvent.Raise();
                    mediumHealthUpdated = true;
                }
            }

            else if (lifeBar.localScale.x < mediumHealthValue)
            {
                if (!lowHealthUpdated)
                {
                    lifeBarRenderer.color = lowHealthColor;
                    if (thirdDamageGroup)
                        thirdDamageGroup.SetActive(true);
                    UpdateToSadFaceEvent.Raise();
                    lowHealthUpdated = true;
                }
            }

        }

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
