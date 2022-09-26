using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShipList", menuName = "Gato Pirate/Ship data/Create enemy's ship data list", order = 1)]

public class IslandEnemyShipDataList : ScriptableObject
{
    [SerializeField]
    private List<EnemyShipData> enemyShipDataList;

    public List<EnemyShipData> EnemyShipDataList { get => enemyShipDataList; set => enemyShipDataList = value; }
}
