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
    [SerializeField]
    private VirtualCameraController virtualCameraController;

    [Header("Events")]
    [SerializeField]
    private VoidEvent StartCombatEvent;
    [SerializeField]
    private FloatEvent TriggerShakingCameraEvent;

    private void Awake()
    {
        startCombatController.StartCombatEvent = StartCombatEvent;

        virtualCameraController.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        virtualCameraController.Initialize();

        playerGameplayBootstrapper.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        playerGameplayBootstrapper.InitializeBootstrapper();

        enemyGameplayBootstrapper.StartCombatEvent = StartCombatEvent;
        enemyGameplayBootstrapper.InitializeBootstrapper();
    }
}
