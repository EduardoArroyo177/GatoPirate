using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    [SerializeField]
    private CannonShotController leftCannon;
    [SerializeField]
    private CannonShotController middleCannon;
    [SerializeField]
    private CannonShotController rightCannon;

    public CannonSideEvent ShootCannonEvent { get; set; }

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
                if(leftCannon.IsEnabled)
                    leftCannon.ShootCannonBall();
                break;
            case CannonSide.MIDDLE:
                if(middleCannon.IsEnabled)
                    middleCannon.ShootCannonBall();
                break;
            case CannonSide.RIGHT:
                if(rightCannon.IsEnabled)
                    rightCannon.ShootCannonBall();
                break;
        }
    }
}
