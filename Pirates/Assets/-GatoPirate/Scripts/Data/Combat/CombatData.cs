using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatData", menuName = "Gato Pirate/Combat data/Create new combat data", order = 1)]
public class CombatData : ScriptableObject
{
    [Header("Player")]
    [SerializeField]
    private PlayerShipData playerShipData;
    [SerializeField]
    private CatCombatData[] catCrewDataList;

    [Header("Enemy")]
    [SerializeField]
    private EnemyShipData enemyShipData;

    public PlayerShipData PlayerShipData { get => playerShipData; set => playerShipData = value; }
    public EnemyShipData EnemyShipData { get => enemyShipData; set => enemyShipData = value; }
    public CatCombatData[] CatCrewDataList { get => catCrewDataList; set => catCrewDataList = value; }
}
