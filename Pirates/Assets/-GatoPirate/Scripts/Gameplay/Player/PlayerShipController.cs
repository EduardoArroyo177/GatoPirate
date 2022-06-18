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
    [SerializeField]
    private float cannonCoolDownTime; // TODO: Make this a property


    public CannonSideEvent ShootCannonEvent { get; set; }
    public CannonSideEvent EnableCannonEvent { get; set; }

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
                leftCannon.ShootCannonBall();
                StartCoroutine(EnableCannon(_side));
                break;
            case CannonSide.MIDDLE:
                middleCannon.ShootCannonBall();
                StartCoroutine(EnableCannon(_side));
                break;
            case CannonSide.RIGHT:
                rightCannon.ShootCannonBall();
                StartCoroutine(EnableCannon(_side));
                break;
        }
    }

    private IEnumerator EnableCannon(CannonSide _side)
    {
        yield return new WaitForSeconds(cannonCoolDownTime);
        EnableCannonEvent.Raise(_side);
    }
}
