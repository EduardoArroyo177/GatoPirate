using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShip", menuName = "Gato Pirate/Ship data/Create enemy's ship data", order = 1)]
public class EnemyShipData : ShipData
{
    [Header("Enemy specific")]
    [SerializeField]
    private float cannonAttackRateMin;
    [SerializeField]
    private float cannonAttackRateMax;

    public float CannonAttackRateMin { get => cannonAttackRateMin; set => cannonAttackRateMin = value; }
    public float CannonAttackRateMax { get => cannonAttackRateMax; set => cannonAttackRateMax = value; }
}
