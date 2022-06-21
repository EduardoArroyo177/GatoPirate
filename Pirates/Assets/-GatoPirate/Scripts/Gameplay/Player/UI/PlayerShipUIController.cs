using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerShipUIController : MonoBehaviour
{
    [Header("Cannons")]
    [SerializeField]
    private CannonShootButtonController leftCannon;
    [SerializeField]
    private CannonShootButtonController middleCannon;
    [SerializeField]
    private CannonShootButtonController rightCannon;
    [Header("Special attack")]
    [SerializeField]
    private SpecialAttackButtonController specialAttackButtonController;

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
        leftCannon.ShootCannonEvent = ShootCannonEvent;
        middleCannon.ShootCannonEvent = ShootCannonEvent;
        rightCannon.ShootCannonEvent = ShootCannonEvent;

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
                leftCannon.ShowCoolDownAnimation(_duration);
                break;
            case CannonSide.MIDDLE:
                middleCannon.ShowCoolDownAnimation(_duration);
                break;
            case CannonSide.RIGHT:
                rightCannon.ShowCoolDownAnimation(_duration);
                break;
        }
    }
}
