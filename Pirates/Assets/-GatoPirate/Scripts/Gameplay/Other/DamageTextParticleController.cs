using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageTextParticleController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro damageTextShadow;
    [SerializeField]
    private TextMeshPro damageText;
    [SerializeField]
    private Color lightHitColor;
    [SerializeField]
    private Color mediumHitColor;
    [SerializeField]
    private Color heavyHitColor;

    [Header("Moving Animation")]
    [SerializeField]
    private bool isAnimated;
    [SerializeField]
    private float animationSpeed;
    [SerializeField]
    private Vector3 position;

    private Vector3 moveTo;

    private void OnEnable()
    {
        moveTo = position + transform.position;
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }

    public void ShowTextParticle(ProjectileType _projectileType, int _damage, bool _hitEnemy)
    {
        switch (_projectileType)
        {
            case ProjectileType.BASIC:
            case ProjectileType.AUTOMATIC:
                damageText.color = lightHitColor;
                break;
            case ProjectileType.NORMAL:
                damageText.color = mediumHitColor;
                break;
            case ProjectileType.SPECIAL:
                damageText.color = heavyHitColor;
                break;
        }

        damageTextShadow.text = $"-{_damage}";
        damageText.text = $"-{_damage}";
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isAnimated)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTo, animationSpeed * Time.deltaTime);
        }
    }
}
