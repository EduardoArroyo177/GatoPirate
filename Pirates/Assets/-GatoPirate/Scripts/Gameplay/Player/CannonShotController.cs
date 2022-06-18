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

    public void ShootCannonBall()
    {
        // Change this to use a pooling system
        GameObject newCannonBall = ObjectPooling.Instance.GetCannonBall();
        newCannonBall.transform.rotation = transform.rotation;
        newCannonBall.transform.position = transform.position;
        newCannonBall.SetActive(true);
    }
}
