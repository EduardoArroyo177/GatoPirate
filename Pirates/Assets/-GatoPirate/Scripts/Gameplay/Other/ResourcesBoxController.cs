using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ResourcesBoxController : MonoBehaviour
{
    // Properties
    public int ResourcesBoxAmntMin { get; set; }
    public int ResourcesBoxAmntMax { get; set; }
    public float ResourcesBoxTimeToDestroy { get; set; }

    // Events
    public IntEvent GoldResourcesDroppedEvent { get; set; }
    public IntEvent WoodResourcesDroppedEvent { get; set; }

    private void OnEnable()
    {
        Invoke(nameof(DestroyResourcesBox), ResourcesBoxTimeToDestroy);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DestroyResourcesBox));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CannonBall"))
        {
            if (!other.GetComponent<CannonBall>().IsShotByEnemy)
            {
                CalculateEarnedResources();
                DestroyResourcesBox();
            }
        }
    }

    private void CalculateEarnedResources()
    {
        GoldResourcesDroppedEvent.Raise(Random.Range(ResourcesBoxAmntMin, ResourcesBoxAmntMax));
        WoodResourcesDroppedEvent.Raise(Random.Range(ResourcesBoxAmntMin, ResourcesBoxAmntMax));
    }

    private void DestroyResourcesBox()
    {
        // TODO: Show box particles
        gameObject.SetActive(false);
    }
}
