using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyResourcesDrop : MonoBehaviour
{
    public float ChanceToDropResources { get; set; }
    public int ResourcesDroppedAmntMin { get; set; }
    public int ResourcesDroppedAmntMax { get; set; }

    public IntEvent GoldResourcesDroppedEvent { get; set; }
    public IntEvent WoodResourcesDroppedEvent { get; set; }

    public void DropResources()
    {
        float decimalPercentage = ChanceToDropResources / 100;

        if (Random.value > (1.0 - decimalPercentage))
        {
            // Give resources
            if (Random.Range(0, 2) == 0)
            {
                GoldResourcesDroppedEvent.Raise(Random.Range(ResourcesDroppedAmntMin, ResourcesDroppedAmntMax));
            }
            else
            {
                WoodResourcesDroppedEvent.Raise(Random.Range(ResourcesDroppedAmntMin, ResourcesDroppedAmntMax));
            }
        }
    }
}
