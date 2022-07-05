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

    public VoidEvent StopCombatEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private Vector3 currentRotationEuler;

    private void OnEnable()
    {
        if (IsShotByEnemy)
            cannonBallSprite.localEulerAngles = new Vector3(-30, 0, currentRotationEuler.z);
        else
            cannonBallSprite.localEulerAngles = new Vector3(30, currentRotationEuler.y, currentRotationEuler.z);
       
        Invoke(nameof(DestroyCannonBall), destroyTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DestroyCannonBall));
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
        DestroyCannonBall(false);
    }

    void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            DestroyCannonBall(true);
        else
        {
            if(!IsShotByEnemy && other.CompareTag("ResourcesBox"))
                DestroyCannonBall(false);
        }
    }

    private void DestroyCannonBall(bool _hitEnemy)
    {
        ShowExplosionParticle(_hitEnemy);
        gameObject.SetActive(false);
    }

    private void ShowExplosionParticle(bool _isEnemy)
    {
        GameObject explosionParticle = null;
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

            if (_isEnemy)
                explosionParticle.transform.localScale = new Vector3(2, 2, 2);
            else
                explosionParticle.transform.localScale = Vector3.one;
            explosionParticle.SetActive(true);
        }
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
