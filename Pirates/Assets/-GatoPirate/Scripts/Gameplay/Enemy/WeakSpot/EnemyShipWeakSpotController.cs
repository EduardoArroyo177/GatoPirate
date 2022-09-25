using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyShipWeakSpotController : MonoBehaviour
{
    [SerializeField]
    private EnemyWeakSpot weakSpotIndicator;
    [SerializeField]
    private GameObject leftWeakSpotPoint;
    [SerializeField]
    private GameObject middleWeakSpotPoint;
    [SerializeField]
    private GameObject rightWeakSpotPoint;

    [Header("Sprite references")]
    [SerializeField]
    private GameObject leftWeakSpotWindow;
    [SerializeField]
    private GameObject middleWeakSpotWindow;
    [SerializeField]
    private GameObject rightWeakSpotWindow;


    public float WeakSpotAppearanceRateMin { get; set; }
    public float WeakSpotAppearanceRateMax { get; set; }
    public float WeakSpotCoolDownTime { get; set; }
    public float WeakSpotPlayerDamageMultiplier { get; set; }
    public ShipHealthController EnemyShipHealthController { get; set; }

    // Events
    public VoidEvent StartCombatEvent { get; set; }
    public VoidEvent StopCombatEvent { get; set; }
    public VoidEvent ResumeCombatEvent { get; set; }
    public CombatSoundEvent TriggerCombatSoundEvent { get; set; }
    public VoidEvent TriggerCombatWeakSpotTutorialEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public int NumberOfActiveCannons { get; set; }


    private bool isWeakSpotActive;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartCombatEvent, StartCombatEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StopCombatEvent, StopCombatEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(ResumeCombatEvent, ResumeCombatEventCallback));

        weakSpotIndicator.EnemyShipHealthController = EnemyShipHealthController;
        weakSpotIndicator.WeakSpotPlayerDamageMultiplier = WeakSpotPlayerDamageMultiplier;
        weakSpotIndicator.TriggerCombatSoundEvent = TriggerCombatSoundEvent;
    }

    #region Event callbacks
    private void StartCombatEventCallback(Void _item)
    {
        StartCoroutine(HandleWeakSpot());
    }

    private void StopCombatEventCallback(Void _item)
    {
        StopAllCoroutines();
    }

    private void ResumeCombatEventCallback(Void _item)
    {
        StartCoroutine(HandleWeakSpot());
    }
    #endregion

    private IEnumerator HandleWeakSpot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(WeakSpotAppearanceRateMin, WeakSpotAppearanceRateMax));
            
            if (!isWeakSpotActive)
            {
                switch (NumberOfActiveCannons)
                {
                    case 1:
                        // Only middle
                        weakSpotIndicator.transform.position = middleWeakSpotPoint.transform.position;
                        isWeakSpotActive = true;
                        weakSpotIndicator.gameObject.SetActive(true);
                        middleWeakSpotWindow.SetActive(false);
                        StartCoroutine(WeakSpotActive());
                        
                        break;
                    case 2:
                        // left and right
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                weakSpotIndicator.transform.position = leftWeakSpotPoint.transform.position;
                                leftWeakSpotWindow.SetActive(true);
                                break;
                            case 1:
                                weakSpotIndicator.transform.position = rightWeakSpotPoint.transform.position;
                                rightWeakSpotWindow.SetActive(true);
                                break;
                        }
                        isWeakSpotActive = true;
                        weakSpotIndicator.gameObject.SetActive(true);
                        StartCoroutine(WeakSpotActive());
                        break;
                    case 3:
                        // left, middle, right
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                // Left side
                                weakSpotIndicator.transform.position = leftWeakSpotPoint.transform.position;
                                leftWeakSpotWindow.SetActive(true);
                                break;
                            case 1:
                                // Middle side
                                weakSpotIndicator.transform.position = middleWeakSpotPoint.transform.position;
                                middleWeakSpotWindow.SetActive(true);
                                break;
                            case 2:
                                // Right side
                                weakSpotIndicator.transform.position = rightWeakSpotPoint.transform.position;
                                rightWeakSpotWindow.SetActive(true);
                                break;
                        }
                        isWeakSpotActive = true;
                        weakSpotIndicator.gameObject.SetActive(true);
                        StartCoroutine(WeakSpotActive());
                        break;
                    case 4:
                        // left, middle, right
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                // Left side
                                weakSpotIndicator.transform.position = leftWeakSpotPoint.transform.position;
                                leftWeakSpotWindow.SetActive(true);
                                break;
                            case 1:
                                // Middle side
                                weakSpotIndicator.transform.position = middleWeakSpotPoint.transform.position;
                                middleWeakSpotWindow.SetActive(true);
                                break;
                            case 2:
                                // Right side
                                weakSpotIndicator.transform.position = rightWeakSpotPoint.transform.position;
                                rightWeakSpotWindow.SetActive(true);
                                break;
                        }
                        isWeakSpotActive = true;
                        weakSpotIndicator.gameObject.SetActive(true);
                        StartCoroutine(WeakSpotActive());
                        break;
                }
            }
        }
    }

    private IEnumerator WeakSpotActive()
    {
        // Trigger sound for weak spot active
        TriggerCombatSoundEvent.Raise(CombatSounds.WEAK_SPOT_ACTIVE);
        // Show particles for weak spot active
        GameObject particles = ObjectPooling.Instance.GetWeakSpotActiveParticles();
        if (particles)
        {
            particles.transform.position = weakSpotIndicator.transform.position;
            particles.SetActive(true);
        }



        yield return new WaitForSeconds(WeakSpotCoolDownTime);
        // Disable weak spot
        weakSpotIndicator.gameObject.SetActive(false);
        isWeakSpotActive = false;
        leftWeakSpotWindow.SetActive(false);
        middleWeakSpotWindow.SetActive(false);
        rightWeakSpotWindow.SetActive(false);
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
