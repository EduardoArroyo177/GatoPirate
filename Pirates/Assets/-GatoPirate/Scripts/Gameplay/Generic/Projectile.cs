using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private ProjectileType projectileType;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float projectileDamage;
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private Transform cannonBallSprite;

    public Vector3 Direction { get; set; }
    public float ProjectileDamage { get => projectileDamage; set => projectileDamage = value; }
    public ProjectileType ProjectileType { get => projectileType; set => projectileType = value; }
    public bool IsShotByEnemy { get; set; }

    public CombatShipSoundEvent TriggerShipSoundEvent { get; set; }
    public VoidEvent StopCombatEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private Vector3 currentRotationEuler;

    private void OnEnable()
    {
        if (IsShotByEnemy)
            cannonBallSprite.localEulerAngles = new Vector3(-30, 0, currentRotationEuler.z);
        else
            cannonBallSprite.localEulerAngles = new Vector3(30, currentRotationEuler.y, currentRotationEuler.z);
       
        Invoke("DestroyProjectileByTime", destroyTime);
    }

    private void OnDisable()
    {
        CancelInvoke("DestroyProjectileByTime");
    }

    private void Awake()
    {
        currentRotationEuler = cannonBallSprite.eulerAngles;
    }

    private void Start()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(StopCombatEvent, StopCombatEventCallback));
    }

    private void StopCombatEventCallback(Void _item)
    {
        DestroyProjectile(false);
    }

    void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Hit enemy
            DestroyProjectile(true);
        }
        else if (other.CompareTag("Player"))
        {
            // Hit player
            DestroyProjectile(false);
        }
        else if (!IsShotByEnemy && other.CompareTag("ResourcesBox"))
        {
            // Player hit resources box
            DestroyProjectileWithResourcesBox();
        }
    }

    private void DestroyProjectile(bool _hitEnemy)
    {
        ShowExplosionParticle(_hitEnemy);
        TriggerSound();
        gameObject.SetActive(false);
    }

    private void ShowExplosionParticle(bool _hitEnemy)
    {
        GameObject explosionParticle = null;
        GameObject damageTextHelper = null;

        switch (projectileType)
        {
            case ProjectileType.BASIC:
                explosionParticle = ObjectPooling.Instance.GetBasicProjectileExplosionParticle();
                break;
            case ProjectileType.NORMAL:
                explosionParticle = ObjectPooling.Instance.GetNormalProjectileExplosionParticle();
                break;
            case ProjectileType.AUTOMATIC:
                explosionParticle = ObjectPooling.Instance.GetAutomaticProjectileExplosionParticle();
                break;
            case ProjectileType.SPECIAL:
                explosionParticle = ObjectPooling.Instance.GetSpecialProjectileExplosionParticle();
                break;
        }

        if (explosionParticle)
        {
            explosionParticle.transform.position = transform.position;

            if (_hitEnemy)
            {
                explosionParticle.transform.localScale = new Vector3(2, 2, 2);
                damageTextHelper = ObjectPooling.Instance.GetEnemyDamageTextParticle();
            }
            else
            {
                explosionParticle.transform.localScale = Vector3.one;
                damageTextHelper = ObjectPooling.Instance.GetPlayerDamageTextParticle();
            }
            explosionParticle.SetActive(true);
            damageTextHelper.transform.position = transform.position;
            damageTextHelper.GetComponent<DamageTextParticleController>().ShowTextParticle(projectileType, (int)projectileDamage, _hitEnemy);
        }
    }

    private void TriggerSound()
    {
        switch (projectileType)
        {
            case ProjectileType.BASIC:
                TriggerShipSoundEvent.Raise(CombatShipSounds.BASIC_CANNON_HIT);                
                break;
            case ProjectileType.NORMAL:
                TriggerShipSoundEvent.Raise(CombatShipSounds.NORMAL_CANNON_HIT); 
                break;
            case ProjectileType.AUTOMATIC:
                TriggerShipSoundEvent.Raise(CombatShipSounds.AUTOMATIC_CANNON_HIT); 
                break;
            case ProjectileType.SPECIAL:
                TriggerShipSoundEvent.Raise(CombatShipSounds.SPECIAL_CANNON_HIT); 
                break;
        }
    }

    private void DestroyProjectileWithResourcesBox()
    {
        GameObject resourcesBoxParticles = ObjectPooling.Instance.GetResourcesBoxParticle();
        if (!resourcesBoxParticles)
            return;
        resourcesBoxParticles.transform.position = transform.position;
        resourcesBoxParticles.SetActive(true);
        gameObject.SetActive(false);
    }

    private void DestroyProjectileByTime()
    {
        gameObject.SetActive(false);
    }

    public void SetDamageAndSpeed(float _damage, float _speed)
    {
        ProjectileDamage = _damage;
        movementSpeed = _speed;
    }

    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
}
