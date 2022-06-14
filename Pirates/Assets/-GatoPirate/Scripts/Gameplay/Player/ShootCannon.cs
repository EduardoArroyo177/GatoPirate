using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    [SerializeField]
    private CannonSideEvent ShootCannonEvent;

    public void Shoot(int _cannon)
    {
        switch (_cannon)
        {
            case 0:
                ShootCannonEvent.Raise(CannonSide.LEFT);
                break;
            case 1:
                ShootCannonEvent.Raise(CannonSide.MIDDLE);
                break;
            case 2:
                ShootCannonEvent.Raise(CannonSide.RIGHT);
                break;
        }
    }

}
