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
    [SerializeField]
    private bool isEnabled;

    public bool IsEnabled { get => isEnabled; set => isEnabled = value; }

    public void ShootCannonBall()
    {
        // Change this to use a pooling system
        GameObject newCannonBall = Instantiate(cannonBall);
        newCannonBall.transform.rotation = transform.rotation;
        newCannonBall.transform.position = transform.position;
        
    }

    //private void CleanEventHandlers()
    //{
    //    foreach (var item in _eventHandlers)
    //    {
    //        item.UnregisterListener();
    //    }

    //    _eventHandlers.Clear();
    //}

    //private void OnDestroy()
    //{
    //    CleanEventHandlers();
    //}
}
