using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialAttackShooting : MonoBehaviour
{
    public EnemyShipAttackController EnemyShipAtkController { get; set; }

    private float coolDownTime;

    public void StartCoolDownTimer(float _coolDownTime)
    {
        coolDownTime = _coolDownTime;
        StartCoroutine("CoolDownTimer");
    }

    public void StopCoolDownTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator CoolDownTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(coolDownTime);
            // Attack
            EnemyShipAtkController.ShootSpecialAttack();
        }
    }
}
