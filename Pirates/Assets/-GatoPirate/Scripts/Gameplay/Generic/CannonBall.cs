using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float ballDamage;
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private Transform cannonBallSprite;

    public Vector3 Direction { get; set; }
    public float BallDamage { get => ballDamage; set => ballDamage = value; }

    public bool IsShotByEnemy;// { get; set; }

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
        GameObject explosionParticle = ObjectPooling.Instance.GetCannonBallExplosionParticle();
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
        BallDamage = _damage;
        movementSpeed = _speed;
    }

    public void SetDamage(float _damage)
    {
        BallDamage = _damage;
    }
}
