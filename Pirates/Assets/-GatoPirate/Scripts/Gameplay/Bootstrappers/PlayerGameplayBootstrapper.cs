using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerGameplayBootstrapper : MonoBehaviour
{
    [Header("Ships")]
    [SerializeField]
    private ShipData playerShipData; // TODO: Someone needs to give this from another place
    [SerializeField]
    private PlayerShipAttackController playerShipAttackController;
    [SerializeField]
    private ShipHealthController playerShipHealthController;
    [SerializeField]
    private PlayerBuildShipController playerBuildShipController;
    [SerializeField]
    private PlayerDefeatedController playerDefeatedController;

    [Header("UI")]
    [SerializeField]
    private PlayerShipAttackUIController playerShipAttackUIController;
    [SerializeField]
    private ShipHealthUIController playerShipHealthUIController;

    [Header("Cannon Events")]
    [SerializeField]
    private CannonSideEvent ShootCannonEvent;
    [SerializeField]
    private CannonSideFloatEvent StartCoolDownTimerAnimationEvent;

    [Header("Special attack events")]
    [SerializeField]
    private FloatEvent InitializeSpecialAttackEvent;
    [SerializeField]
    private VoidEvent ShootSpecialAttackEvent;

    [Header("Health events")]
    [SerializeField]
    private FloatEvent CurrentPlayerHealthUIEvent;

    // Events
    public FloatEvent TriggerShakingCameraEvent { get; set; }
    public VoidEvent StartCombatEvent { get; set; }
    public VoidEvent StopCombatEvent { get; set; }
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public VoidEvent TriggerPlayerStartingAnimationEvent { get; set; }
    public VoidEvent TriggerEnemyStartingAnimationEvent { get; set; }
    public VoidEvent TriggerPlayerLostAnimationEvent { get; set; }

    // Properties
    public CatData[] CatCrewDataList { get; set; }
    public ShipData PlayerShipData { get => playerShipData; set => playerShipData = value; }
    public int NumberOfActiveCannons { get; set; }

    public void InitializeBootstrapper()
    {
        InitializePoolingSystem();
        InitializePlayer();
    }

    private void InitializePoolingSystem()
    {
        ObjectPooling.Instance.StopCombatEvent = StopCombatEvent;
        ObjectPooling.Instance.Initialize();
    }

    private void InitializePlayer()
    {
        // UI Init goes first
        playerShipAttackUIController.NumberOfActiveCannons = NumberOfActiveCannons;
        playerShipAttackUIController.ShootCannonEvent = ShootCannonEvent;
        playerShipAttackUIController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipAttackUIController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        playerShipAttackUIController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
        playerShipAttackUIController.StopCombatEvent = StopCombatEvent;
        playerShipAttackUIController.Initialize();

        playerShipHealthUIController.CurrentHealthUIEvent = CurrentPlayerHealthUIEvent;
        playerShipHealthUIController.Initialize();

        // Player build ship controller
        playerBuildShipController.CatCrewDataList = CatCrewDataList;
        playerBuildShipController.TriggerPlayerStartingAnimationEvent = TriggerPlayerStartingAnimationEvent;
        playerBuildShipController.TriggerEnemyStartingAnimationEvent = TriggerEnemyStartingAnimationEvent;
        playerBuildShipController.Initialize();

        // Player ship attack controller
        // Controller Properties
        playerShipAttackController.ShipLevelAttackMultiplier = PlayerShipData.ShipLevelAttackMultiplier;
        playerShipAttackController.ShipLevelBallSpeedMultiplier = PlayerShipData.ShipLevelBallSpeedMultiplier;
        playerShipAttackController.ShipLevelCoolDownMultiplier = PlayerShipData.ShipLevelCoolDownMultiplier;
        playerShipAttackController.ShipLevelSpecialAttackMultiplier = PlayerShipData.ShipLevelSpecialAttackMultiplier;

        playerShipAttackController.CannonBallSpeed = PlayerShipData.CannonBallSpeed;
        playerShipAttackController.BasicAttackDamage = PlayerShipData.BasicAttackDamage;
        playerShipAttackController.NormalAttackDamage = PlayerShipData.NormalAttackDamage;
        playerShipAttackController.NormalAttackCoolDownTime = PlayerShipData.NormalAttackCoolDownTime;
        playerShipAttackController.AutomaticAttackDamage = PlayerShipData.AutomaticAttackDamage;
        playerShipAttackController.AutomaticAttackCoolDownTime = PlayerShipData.AutomaticAttackCoolDownTime;
        playerShipAttackController.AutomaticAttackFireRate = PlayerShipData.AutomaticAttackFireRate;
        playerShipAttackController.SpecialAttackDamage = PlayerShipData.SpecialAttackDamage;
        playerShipAttackController.SpecialAttackChargeTime = PlayerShipData.SpecialAttackChargeTime;

        // Events
        playerShipAttackController.ShootCannonEvent = ShootCannonEvent;
        playerShipAttackController.StartCoolDownTimerAnimationEvent = StartCoolDownTimerAnimationEvent;
        playerShipAttackController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        playerShipAttackController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
        playerShipAttackController.StartCombatEvent = StartCombatEvent;
        playerShipAttackController.StopCombatEvent = StopCombatEvent;
        playerShipAttackController.Initialize();

        // Player ship health controller
        // Properties
        playerShipHealthController.ShipHealth = PlayerShipData.ShipHealth;
        playerShipHealthController.ShipLevelHealthMultiplier = PlayerShipData.ShipLevelHealthMultiplier;

        // Events
        playerShipHealthController.CurrentHealthUIEvent = CurrentPlayerHealthUIEvent;
        playerShipHealthController.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        playerShipHealthController.StopCombatEvent = StopCombatEvent;
        playerShipHealthController.ShowResultScreenEvent = ShowResultScreenEvent;
        playerShipHealthController.TriggerPlayerLostAnimationEvent = TriggerPlayerLostAnimationEvent;
        playerShipHealthController.Initialize();

        // Player defeated
        playerDefeatedController.ShowResultScreenEvent = ShowResultScreenEvent;
        playerDefeatedController.TriggerPlayerLostAnimationEvent = TriggerPlayerLostAnimationEvent;
        playerDefeatedController.TriggerShakingCameraEvent = TriggerShakingCameraEvent;
        playerDefeatedController.Initialize();

        // Trigger game init
        //TriggerPlayerStartingAnimationEvent.Raise();
        Invoke("StartingAnimation", 0.1f);
    }

    private void StartingAnimation()
    {
        TriggerPlayerStartingAnimationEvent.Raise();
    }
}
