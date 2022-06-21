using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackButtonController : MonoBehaviour
{
    [SerializeField]
    private Image img_fillBar;
    // TODO: Remove or hide after testing
    [SerializeField]
    private float animationDuration;

    public FloatEvent InitializeSpecialAttackEvent { get; set; }
    public VoidEvent ShootSpecialAttackEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();
    private Button btn_specialAttack;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(InitializeSpecialAttackEvent, InitializeSpecialAttackEventCallback));
    }

    private void InitializeSpecialAttackEventCallback(float _duration)
    {
        btn_specialAttack = GetComponent<Button>();
        StartChargeAttackTimerAnimation(_duration);
    }

    public void ShootSpecialAttack()
    {
        ShootSpecialAttackEvent.Raise();
        RestartChargeAttackTimerAnimation();
    }

    // Private
    private void RestartChargeAttackTimerAnimation()
    {
        img_fillBar.fillAmount = 0;
        btn_specialAttack.interactable = false;
        StartCoroutine(CircleFillAnimation());
    }

    private void StartChargeAttackTimerAnimation(float _duration)
    {
        animationDuration = _duration;
        img_fillBar.fillAmount = 0;
        btn_specialAttack.interactable = false;
        StartCoroutine(CircleFillAnimation());
    }

    private IEnumerator CircleFillAnimation()
    {
        float percentage;
        float activeTimer = 0;

        while (img_fillBar.fillAmount < 1)
        {
            activeTimer += Time.deltaTime;
            percentage = activeTimer / animationDuration;
            img_fillBar.fillAmount = Mathf.Lerp(0, 1, percentage);
            yield return null;
        }
        btn_specialAttack.interactable = true;
    }
}
