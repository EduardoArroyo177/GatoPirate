using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StartCombatBootstrapper : MonoBehaviour
{
    [SerializeField]
    private StartCombatController startCombatController;
    [SerializeField]
    private PlayerGameplayBootstrapper playerGameplayBootstrapper;
    [SerializeField]
    private EnemyGameplayBootstrapper enemyGameplayBootstrapper;

    [Header("Events")]
    [SerializeField]
    private VoidEvent StartCombatEvent;

    private void Awake()
    {
        startCombatController.StartCombatEvent = StartCombatEvent;

        playerGameplayBootstrapper.InitializeBootstrapper();

        enemyGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        enemyGameplayBootstrapper.InitializeBootstrapper();
    }
}
