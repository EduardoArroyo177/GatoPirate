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
    private GameObject leftWeakSpot;
    [SerializeField]
    private GameObject middleWeakSpot;
    [SerializeField]
    private GameObject rightWeakSpot;

    public float WeakSpotAppearanceRateMin { get; set; }
    public float WeakSpotAppearanceRateMax { get; set; }
    public float WeakSpotCoolDownTime { get; set; }
    public float WeakSpotPlayerDamageMultiplier { get; set; }
    public ShipHealthController EnemyShipHealthController { get; set; }

    // Events
    public VoidEvent StartCombatEvent { get; set; }
    public VoidEvent StopCombatEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public int NumberOfActiveCannons { get; set; }


    private bool isWeakSpotActive;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartCombatEvent, StartCombatEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StopCombatEvent, StopCombatEventCallback));

        weakSpotIndicator.EnemyShipHealthController = EnemyShipHealthController;
        weakSpotIndicator.WeakSpotPlayerDamageMultiplier = WeakSpotPlayerDamageMultiplier;
    }

    private void StartCombatEventCallback(Void _item)
    {
        StartCoroutine(HandleWeakSpot());
    }

    private void StopCombatEventCallback(Void _item)
    {
        StopAllCoroutines();
    }

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
                        weakSpotIndicator.transform.position = middleWeakSpot.transform.position;
                        isWeakSpotActive = true;
                        weakSpotIndicator.gameObject.SetActive(true);
                        StartCoroutine(WeakSpotActive());
                        
                        break;
                    case 2:
                        // left and right
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                weakSpotIndicator.transform.position = leftWeakSpot.transform.position;
                                break;
                            case 1:
                                weakSpotIndicator.transform.position = rightWeakSpot.transform.position;
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
                                weakSpotIndicator.transform.position = leftWeakSpot.transform.position;
                                break;
                            case 1:
                                // Middle side
                                weakSpotIndicator.transform.position = middleWeakSpot.transform.position;
                                break;
                            case 2:
                                // Right side
                                weakSpotIndicator.transform.position = rightWeakSpot.transform.position;
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
                                weakSpotIndicator.transform.position = leftWeakSpot.transform.position;
                                break;
                            case 1:
                                // Middle side
                                weakSpotIndicator.transform.position = middleWeakSpot.transform.position;
                                break;
                            case 2:
                                // Right side
                                weakSpotIndicator.transform.position = rightWeakSpot.transform.position;
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
        yield return new WaitForSeconds(WeakSpotCoolDownTime);
        weakSpotIndicator.gameObject.SetActive(false);
        isWeakSpotActive = false;
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
