using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonShooting : MonoBehaviour
{
    private float coolDownTime;
    public bool IsShooting { get; set; }

    public void StartCoolDownTimer(float _coolDownTime)
    {
        coolDownTime = _coolDownTime;
        IsShooting = true;
        StartCoroutine("CoolDownTimer");
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDownTime);
        IsShooting = false;
    }
}
