using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{
    [SerializeField]
    private CannonSide cannonSide;
    [SerializeField]
    private GameObject cannonBall;

    public CannonSideEvent ShootCannonEvent { get; set; }
    public Transform PlayerShipTransform { get; set; }

    public Transform enemyShipTargetTransform;

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void SetCannonTarget(CannonSide _cannonSide, Transform _target)
    {
        if (_cannonSide.Equals(cannonSide))
        {
            enemyShipTargetTransform = _target;
        }
    }

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CannonSide>.BuildEventHandler(ShootCannonEvent, ShootCannonEventCallback));
    }

    public void ShootCannonEventCallback(CannonSide _cannonToShoot)
    {
        if (cannonSide.Equals(_cannonToShoot))
        {
            Debug.Log($"Shooting {_cannonToShoot}");
            // Change this to use a pooling system
            GameObject newCannonBall = Instantiate(cannonBall, transform);
            newCannonBall.transform.position = transform.position;
            // Give direction here
            Vector3 direction = enemyShipTargetTransform.position - PlayerShipTransform.position;
            newCannonBall.GetComponent<CannonBall>().Direction = Vector3.up;
        }
    }

    private void CleanEventHandlers()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }

        _eventHandlers.Clear();
    }

    private void OnDestroy()
    {
        CleanEventHandlers();
    }
}
