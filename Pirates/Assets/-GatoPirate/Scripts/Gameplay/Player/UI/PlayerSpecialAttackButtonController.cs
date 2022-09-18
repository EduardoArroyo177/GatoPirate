using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpecialAttackButtonController : MonoBehaviour
{
    [SerializeField]
    private Button btn_specialAttack;
    [SerializeField]
    private Image img_fillBar;
    [SerializeField]
    private GameObject img_attackReady;
    // TODO: Remove or hide after testing
    [SerializeField]
    private float fillAnimationDuration;
    [SerializeField]
    private ShineSpriteEffectAnimation shineSpriteEffectAnimation;

    public FloatEvent InitializeSpecialAttackEvent { get; set; }
    public VoidEvent ShootSpecialAttackEvent { get; set; }
    public CombatSoundEvent TriggerCombatSoundEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<float>.BuildEventHandler(InitializeSpecialAttackEvent, InitializeSpecialAttackEventCallback));
    }

    private void InitializeSpecialAttackEventCallback(float _duration)
    {
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
        img_attackReady.SetActive(false);
        shineSpriteEffectAnimation.StopAnimation();
        StartCoroutine(CircleFillAnimation());
    }

    private void StartChargeAttackTimerAnimation(float _duration)
    {
        fillAnimationDuration = _duration;
        img_fillBar.fillAmount = 0;
        btn_specialAttack.interactable = false;
        img_attackReady.SetActive(false);
        if (gameObject.activeInHierarchy)
            StartCoroutine(CircleFillAnimation());
    }

    private IEnumerator CircleFillAnimation()
    {
        float percentage;
        float activeTimer = 0;

        while (img_fillBar.fillAmount < 1)
        {
            activeTimer += Time.deltaTime;
            percentage = activeTimer / fillAnimationDuration;
            img_fillBar.fillAmount = Mathf.Lerp(0, 1, percentage);
            yield return null;
        }
        btn_specialAttack.interactable = true;
        shineSpriteEffectAnimation.StartShineAnimation();
        img_attackReady.SetActive(true);
        // Trigger ready sound
        TriggerCombatSoundEvent.Raise(CombatSounds.SPECIAL_CANNON_READY);
    }

    public void StopAnimation()
    {
        StopAllCoroutines();

    }

    #region On Destroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
