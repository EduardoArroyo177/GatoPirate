using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class CannonShootButtonController : MonoBehaviour
{
    [SerializeField]
    private CannonSide cannonSide;
    [SerializeField]
    private CoolDownTimerUIController coolDownTimerUIController;
    public CannonSideEvent ShootCannonEvent { get; set; }

    private Button btn_shootCannon;

    private void Awake()
    {
        btn_shootCannon = GetComponent<Button>();
        coolDownTimerUIController.CannonShootBtnController = this;
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

    public void ShowCoolDownAnimation(float _duration)
    {
        coolDownTimerUIController.StartCoolDownTimerAnimation(_duration);
    }

}
