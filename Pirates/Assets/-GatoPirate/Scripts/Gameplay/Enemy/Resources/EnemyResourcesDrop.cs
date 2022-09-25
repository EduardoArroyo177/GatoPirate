using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyResourcesDrop : MonoBehaviour
{
    [Header("Resources box")]
    [SerializeField]
    private ResourcesBoxController leftResourcesBox;
    [SerializeField]
    private ResourcesBoxController middleResourcesBox;
    [SerializeField]
    private ResourcesBoxController rightResourcesBox;

    [Header("Chest")]
    [SerializeField]
    private GameObject chest;

    // Resources
    public float ChanceToDropResources { get; set; }
    public int BasicResourcesDroppedAmntMin { get; set; }
    public int BasicResourcesDroppedAmntMax { get; set; }
    public int NormalResourcesDroppedAmntMin { get; set; }
    public int NormalResourcesDroppedAmntMax { get; set; }

    // Resources box
    public float ChanceToDropResourcesBox { get; set; }
    public int ResourcesBoxesPerCombat { get; set; }

    public int ResourcesBoxAmntMin { get; set; }
    public int ResourcesBoxAmntMax { get; set; }
    public float ResourcesBoxTimeToDestroy { get; set; }

    public IntEvent GoldResourcesDroppedEvent { get; set; }
    public IntEvent WoodResourcesDroppedEvent { get; set; }

    // Chest
    public float ChanceToGiveChest { get; set; }
    public BoolEvent WinChestEvent { get; set; }

    // Tutorial
    public VoidEvent TriggerCombatResourcesBoxTutorialEvent { get; set; }

    private int currentAvailableResourcesBoxes;

    public void Initialize()
    {
        currentAvailableResourcesBoxes = ResourcesBoxesPerCombat;

        leftResourcesBox.ResourcesBoxAmntMax = ResourcesBoxAmntMax;
        leftResourcesBox.ResourcesBoxAmntMin = ResourcesBoxAmntMin;
        leftResourcesBox.ResourcesBoxTimeToDestroy = ResourcesBoxTimeToDestroy;

        leftResourcesBox.GoldResourcesDroppedEvent = GoldResourcesDroppedEvent;
        leftResourcesBox.WoodResourcesDroppedEvent = WoodResourcesDroppedEvent;

        middleResourcesBox.ResourcesBoxAmntMax = ResourcesBoxAmntMax;
        middleResourcesBox.ResourcesBoxAmntMin = ResourcesBoxAmntMin;
        middleResourcesBox.ResourcesBoxTimeToDestroy = ResourcesBoxTimeToDestroy;

        middleResourcesBox.GoldResourcesDroppedEvent = GoldResourcesDroppedEvent;
        middleResourcesBox.WoodResourcesDroppedEvent = WoodResourcesDroppedEvent;

        rightResourcesBox.ResourcesBoxAmntMax = ResourcesBoxAmntMax;
        rightResourcesBox.ResourcesBoxAmntMin = ResourcesBoxAmntMin;
        rightResourcesBox.ResourcesBoxTimeToDestroy = ResourcesBoxTimeToDestroy;

        rightResourcesBox.GoldResourcesDroppedEvent = GoldResourcesDroppedEvent;
        rightResourcesBox.WoodResourcesDroppedEvent = WoodResourcesDroppedEvent;

        CalculateChest();
    }

    private void CalculateChest()
    {
        float decimalPercentage = ChanceToGiveChest / 100;

        if (Random.value > (1.0 - decimalPercentage))
        {
            chest.SetActive(true);
            WinChestEvent.Raise(true);
        }
        else
            WinChestEvent.Raise(false);
    }

    public void DropBasicResources()
    {
        float decimalPercentage = ChanceToDropResources / 100;

        if (Random.value > (1.0 - decimalPercentage))
        {
            // Give resources
            if (Random.Range(0, 2) == 0)
            {
                GoldResourcesDroppedEvent.Raise(Random.Range(BasicResourcesDroppedAmntMin, BasicResourcesDroppedAmntMax));
            }
            else
            {
                WoodResourcesDroppedEvent.Raise(Random.Range(BasicResourcesDroppedAmntMin, BasicResourcesDroppedAmntMax));
            }
        }
    }

    public void DropNormalResources()
    {
        // Resources
        float decimalPercentage = ChanceToDropResources / 100;

        if (Random.value > (1.0 - decimalPercentage))
        {
            // Give resources
            if (Random.Range(0, 2) == 0)
            {
                GoldResourcesDroppedEvent.Raise(Random.Range(NormalResourcesDroppedAmntMin, NormalResourcesDroppedAmntMax));
            }
            else
            {
                WoodResourcesDroppedEvent.Raise(Random.Range(NormalResourcesDroppedAmntMin, NormalResourcesDroppedAmntMax));
            }
        }

        // Resources box
        if (currentAvailableResourcesBoxes > 0)
        {
            currentAvailableResourcesBoxes--;

            float boxDecimalPercentage = ChanceToDropResourcesBox / 100;

            if (Random.value > (1.0 - boxDecimalPercentage))
            {
                if (!TutorialDataSaveManager.Instance.GetTutorialCompletedStatus(TutorialType.COMBAT_RESOURCES_BOX))
                    TriggerCombatResourcesBoxTutorialEvent.Raise();

                switch (Random.Range(0, 3))
                {
                    case 0:
                        leftResourcesBox.gameObject.SetActive(true);
                        break;
                    case 1:
                        middleResourcesBox.gameObject.SetActive(true);
                        break;
                    case 2:
                        rightResourcesBox.gameObject.SetActive(true);
                        break;
                }
            }
        }
    }
}
