using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackTimerUIController : MonoBehaviour
{
    [SerializeField]
    private Button btn_specialAttack;
    [SerializeField]
    private float animationDuration;

    public SpecialAttackButtonController SpecialAttackBtnController { get; set; }

    public void RestartChargeAttackTimerAnimation()
    {
        btn_specialAttack.image.fillAmount = 0;
        btn_specialAttack.interactable = false;
    }

    public void StartChargeAttackTimerAnimation(float _duration)
    {
        animationDuration = _duration;
        btn_specialAttack.image.fillAmount = 0;
        btn_specialAttack.interactable = false;
        StartCoroutine(CircleFillAnimation());
    }

    private IEnumerator CircleFillAnimation()
    {
        float percentage;
        float activeTimer = 0;

        while (btn_specialAttack.image.fillAmount < 1)
        {
            activeTimer += Time.deltaTime;
            percentage = activeTimer / animationDuration;
            btn_specialAttack.image.fillAmount = Mathf.Lerp(0, 1, percentage);
            yield return null;
        }
        btn_specialAttack.interactable = true;
    }
}
