using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownTimerUIController : MonoBehaviour
{
    [SerializeField]
    private Image img_fillBar;
    [SerializeField]
    private float animationDuration;

    public CannonShootButtonController CannonShootBtnController { get; set; }

    public void StartCoolDownTimerAnimation(float _duration)
    {
        animationDuration = _duration;
        img_fillBar.fillAmount = 0;
        gameObject.SetActive(true);
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
        CannonShootBtnController.EnableShootButton();
        gameObject.SetActive(false);
    }
}
