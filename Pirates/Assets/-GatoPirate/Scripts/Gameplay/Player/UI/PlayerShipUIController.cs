using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class PlayerShipUIController : MonoBehaviour
{
    [SerializeField]
    private CannonShootButtonController leftCannon;
    [SerializeField]
    private CannonShootButtonController middleCannon;
    [SerializeField]
    private CannonShootButtonController rightCannon;

    public CannonSideEvent ShootCannonEvent { get; set; }
    public CannonSideFloatEvent StartCoolDownTimerAnimationEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide, float>.BuildEventHandler(StartCoolDownTimerAnimationEvent, StartCoolDownTimerAnimationEventCallback));

        leftCannon.ShootCannonEvent = ShootCannonEvent;
        middleCannon.ShootCannonEvent = ShootCannonEvent;
        rightCannon.ShootCannonEvent = ShootCannonEvent;
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
