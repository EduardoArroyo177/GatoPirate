using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class CannonShootButtonController : MonoBehaviour
{
    [SerializeField]
    private CannonSide cannonSide;
    public CannonSideEvent ShootCannonEvent { get; set; }

    private Button btn_shootCannon;

    private void Awake()
    {
        btn_shootCannon = GetComponent<Button>();
    }

    public void ShootCannon()
    {
        ShootCannonEvent.Raise(cannonSide);
        btn_shootCannon.interactable = false;
    }

    public void EnableShootButton()
    {
        btn_shootCannon.interactable = true;
    }

}
