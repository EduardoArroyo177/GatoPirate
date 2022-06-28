using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShip", menuName = "Gato Pirate/Ship data/Create enemy's ship data", order = 1)]
public class EnemyShipData : ShipData
{
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
    private int resourcesDroppedAmntMin;
    [SerializeField]
    private int resourcesDroppedAmntMax;

    public float CannonAttackRateMin { get => cannonAttackRateMin; set => cannonAttackRateMin = value; }
    public float CannonAttackRateMax { get => cannonAttackRateMax; set => cannonAttackRateMax = value; }
    public float WeakSpotAppearanceRateMin { get => weakSpotAppearanceRateMin; set => weakSpotAppearanceRateMin = value; }
    public float WeakSpotAppearanceRateMax { get => weakSpotAppearanceRateMax; set => weakSpotAppearanceRateMax = value; }
    public float WeakSpotCoolDownTime { get => weakSpotCoolDownTime; set => weakSpotCoolDownTime = value; }
    public float WeakSpotPlayerDamageMultiplier { get => weakSpotPlayerDamageMultiplier; set => weakSpotPlayerDamageMultiplier = value; }
    public float ChanceToDropResources { get => chanceToDropResources; set => chanceToDropResources = value; }
    public int ResourcesDroppedAmntMin { get => resourcesDroppedAmntMin; set => resourcesDroppedAmntMin = value; }
    public int ResourcesDroppedAmntMax { get => resourcesDroppedAmntMax; set => resourcesDroppedAmntMax = value; }
}
