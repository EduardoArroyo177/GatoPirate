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

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public int PlayerNumberOfCannons { get; set; }

    private bool isWeakSpotActive;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StartCombatEvent, StartCombatEventCallback));

        weakSpotIndicator.EnemyShipHealthController = EnemyShipHealthController;
        weakSpotIndicator.WeakSpotPlayerDamageMultiplier = WeakSpotPlayerDamageMultiplier;
        //StartCoroutine(HandleWeakSpot());
    }

    public void StartCombatEventCallback(Void _item)
    {
        StartCoroutine(HandleWeakSpot());
    }

    private IEnumerator HandleWeakSpot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(WeakSpotAppearanceRateMin, WeakSpotAppearanceRateMax));
            
            if (!isWeakSpotActive)
            {
                switch (PlayerNumberOfCannons)
                {
                    case 1:
                        // Only middle
                        break;
                    case 2:
                        // left and right
                        break;
                    case 3:
                        // left, middle, right
                        switch (Random.Range(0, PlayerNumberOfCannons))
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
