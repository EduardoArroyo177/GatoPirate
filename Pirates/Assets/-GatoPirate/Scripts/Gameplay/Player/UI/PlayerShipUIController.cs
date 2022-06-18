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
    public CannonSideEvent EnableCannonEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide>.BuildEventHandler(EnableCannonEvent, EnableCannonEventCallback));

        leftCannon.ShootCannonEvent = ShootCannonEvent;
        middleCannon.ShootCannonEvent = ShootCannonEvent;
        rightCannon.ShootCannonEvent = ShootCannonEvent;
    }

    private void EnableCannonEventCallback(CannonSide _cannonSide)
    {
        switch (_cannonSide)
        {
            case CannonSide.LEFT:
                leftCannon.EnableShootButton();
                break;
            case CannonSide.MIDDLE:
                middleCannon.EnableShootButton();
                break;
            case CannonSide.RIGHT:
                rightCannon.EnableShootButton();
                break;
        }
    }
}
