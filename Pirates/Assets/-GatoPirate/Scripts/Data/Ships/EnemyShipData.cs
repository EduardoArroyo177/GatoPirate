using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShip", menuName = "Gato Pirate/Ship data/Create enemy's ship data", order = 1)]
public class EnemyShipData : ShipData
{
    [Header("Number of active attacks")]
    [SerializeField]
    private int numberOfActiveCannons;

    [Header("Enemy attack")]
    [SerializeField]
    private float cannonAttackRateMin;
    [SerializeField]
    private float cannonAttackRateMax;

    [Header("Weak spot")]
    [SerializeField]
    private float weakSpotAppearanceRateMin;
    [SerializeField]
    private float weakSpotAppearanceRateMax;
    [SerializeField]
    private float weakSpotCoolDownTime;
    [SerializeField]
    private float weakSpotPlayerDamageMultiplier;

    [Header("Resources drop")]
    [SerializeField]
    private float chanceToDropResources;
    [SerializeField]
    private int basicResourcesDroppedAmntMin;
    [SerializeField]
    private int basicResourcesDroppedAmntMax;
    [SerializeField]
    private int normalResourcesDroppedAmntMin;
    [SerializeField]
    private int normalResourcesDroppedAmntMax;

    [Header("Resources box")]
    [SerializeField]
    private float chanceToDropResourcesBox;
    [SerializeField]
    private int resourcesBoxesPerCombat;
    [SerializeField]
    private int resourcesBoxAmntMin;
    [SerializeField]
    private int resourcesBoxAmntMax;
    [SerializeField]
    private float resourcesBoxTimeToDestroy;

    [Header("Chest")]
    [SerializeField]
    private float chanceToGiveChest;

    public float CannonAttackRateMin { get => cannonAttackRateMin; set => cannonAttackRateMin = value; }
    public float CannonAttackRateMax { get => cannonAttackRateMax; set => cannonAttackRateMax = value; }
    public float WeakSpotAppearanceRateMin { get => weakSpotAppearanceRateMin; set => weakSpotAppearanceRateMin = value; }
    public float WeakSpotAppearanceRateMax { get => weakSpotAppearanceRateMax; set => weakSpotAppearanceRateMax = value; }
    public float WeakSpotCoolDownTime { get => weakSpotCoolDownTime; set => weakSpotCoolDownTime = value; }
    public float WeakSpotPlayerDamageMultiplier { get => weakSpotPlayerDamageMultiplier; set => weakSpotPlayerDamageMultiplier = value; }
    public float ChanceToDropResources { get => chanceToDropResources; set => chanceToDropResources = value; }
    public int BasicResourcesDroppedAmntMin { get => basicResourcesDroppedAmntMin; set => basicResourcesDroppedAmntMin = value; }
    public int BasicResourcesDroppedAmntMax { get => basicResourcesDroppedAmntMax; set => basicResourcesDroppedAmntMax = value; }
    public int NormalResourcesDroppedAmntMin { get => normalResourcesDroppedAmntMin; set => normalResourcesDroppedAmntMin = value; }
    public int NormalResourcesDroppedAmntMax { get => normalResourcesDroppedAmntMax; set => normalResourcesDroppedAmntMax = value; }
    public float ChanceToDropResourcesBox { get => chanceToDropResourcesBox; set => chanceToDropResourcesBox = value; }
    public int ResourcesBoxesPerCombat { get => resourcesBoxesPerCombat; set => resourcesBoxesPerCombat = value; }
    public int ResourcesBoxAmntMin { get => resourcesBoxAmntMin; set => resourcesBoxAmntMin = value; }
    public int ResourcesBoxAmntMax { get => resourcesBoxAmntMax; set => resourcesBoxAmntMax = value; }
    public float ResourcesBoxTimeToDestroy { get => resourcesBoxTimeToDestroy; set => resourcesBoxTimeToDestroy = value; }
    public float ChanceToGiveChest { get => chanceToGiveChest; set => chanceToGiveChest = value; }
    public int NumberOfActiveCannons { get => numberOfActiveCannons; set => numberOfActiveCannons = value; }
}
