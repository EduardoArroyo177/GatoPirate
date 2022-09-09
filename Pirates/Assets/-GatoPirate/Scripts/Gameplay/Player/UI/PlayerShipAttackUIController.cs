using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipAttackUIController : MonoBehaviour
{
    [Header("Cannons")]
    [SerializeField]
    private PlayerCannonShootButtonController btn_leftCannon;
    [SerializeField]
    private PlayerCannonShootButtonController btn_middleCannon;
    [SerializeField]
    private PlayerCannonShootButtonController btn_rightCannon;
    [Header("Special attack")]
    [SerializeField]
    private PlayerSpecialAttackButtonController specialAttackButtonController;

    // Cannons
    public CannonSideEvent ShootCannonEvent { get; set; }
    public CannonSideFloatEvent StartCoolDownTimerAnimationEvent { get; set; }

    // Special attack
    public FloatEvent InitializeSpecialAttackEvent { get; set; }
    public VoidEvent ShootSpecialAttackEvent { get; set; }
    public VoidEvent StopCombatEvent { get; set; }

    // Sounds
    public CombatSoundEvent TriggerCombatSoundEvent { get; set; }

    // Properties
    public int NumberOfActiveCannons { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide, float>.BuildEventHandler(StartCoolDownTimerAnimationEvent, StartCoolDownTimerAnimationEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StopCombatEvent, StopCombatEventCallback));

        btn_leftCannon.ShootCannonEvent = ShootCannonEvent;
        btn_middleCannon.ShootCannonEvent = ShootCannonEvent;
        btn_rightCannon.ShootCannonEvent = ShootCannonEvent;

        SetCannons();
    }

    private void StopCombatEventCallback(Void _item)
    {
        btn_leftCannon.GetComponent<Button>().interactable = false;
        btn_rightCannon.GetComponent<Button>().interactable = false;
        btn_middleCannon.GetComponent<Button>().interactable = false;
        specialAttackButtonController.StopAnimation();
        specialAttackButtonController.gameObject.SetActive(false);
    }

    private void SetCannons()
    {
        switch (NumberOfActiveCannons)
        {
            case 1:
                btn_leftCannon.GetComponent<Button>().interactable = false;
                btn_rightCannon.GetComponent<Button>().interactable = false;
                specialAttackButtonController.gameObject.SetActive(false);
                break;
            case 2:
                btn_leftCannon.GetComponent<Button>().interactable = false;
                specialAttackButtonController.gameObject.SetActive(false);
                break;
            case 3:
                specialAttackButtonController.gameObject.SetActive(false);
                break;
            case 4:
                specialAttackButtonController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
                specialAttackButtonController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
                specialAttackButtonController.TriggerCombatSoundEvent = TriggerCombatSoundEvent;
                specialAttackButtonController.Initialize();
                break;
        }
    }

    private void StartCoolDownTimerAnimationEventCallback(CannonSide _cannonSide, float _duration)
    {
        switch (_cannonSide)
        {
            case CannonSide.LEFT:
                btn_leftCannon.ShowCoolDownAnimation(_duration);
                break;
            case CannonSide.MIDDLE:
                btn_middleCannon.ShowCoolDownAnimation(_duration);
                break;
            case CannonSide.RIGHT:
                btn_rightCannon.ShowCoolDownAnimation(_duration);
                break;
        }
    }

    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }

        _eventHandlers.Clear();
    }
}
