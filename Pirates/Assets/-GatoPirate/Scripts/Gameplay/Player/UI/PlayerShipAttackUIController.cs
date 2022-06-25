using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

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

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide, float>.BuildEventHandler(StartCoolDownTimerAnimationEvent, StartCoolDownTimerAnimationEventCallback));

        // Cannons
        btn_leftCannon.ShootCannonEvent = ShootCannonEvent;
        btn_middleCannon.ShootCannonEvent = ShootCannonEvent;
        btn_rightCannon.ShootCannonEvent = ShootCannonEvent;

        // Special attack
        specialAttackButtonController.InitializeSpecialAttackEvent = InitializeSpecialAttackEvent;
        specialAttackButtonController.ShootSpecialAttackEvent = ShootSpecialAttackEvent;
        specialAttackButtonController.Initialize();
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
}
