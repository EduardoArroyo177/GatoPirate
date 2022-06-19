using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField]
    private CannonShotController leftCannon;
    [SerializeField]
    private CannonShotController middleCannon;
    [SerializeField]
    private CannonShotController rightCannon;
    [SerializeField]
    private float cannonCoolDownTime; // TODO: Make this a property


    public CannonSideEvent ShootCannonEvent { get; set; }
    public CannonSideFloatEvent StartCoolDownTimerAnimationEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide>.BuildEventHandler(ShootCannonEvent, ShootCannonEventCallback));
    }

    private void ShootCannonEventCallback(CannonSide _side)
    {
        switch (_side)
        {
            case CannonSide.LEFT:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.LEFT, cannonCoolDownTime);
                leftCannon.ShootCannonBall();
                break;
            case CannonSide.MIDDLE:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.MIDDLE, cannonCoolDownTime);
                middleCannon.ShootCannonBall();
                break;
            case CannonSide.RIGHT:
                StartCoolDownTimerAnimationEvent.Raise(CannonSide.RIGHT, cannonCoolDownTime);
                rightCannon.ShootCannonBall();
                break;
        }
    }
}
