using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : SceneSingleton<ObjectPooling>
{
    [Header("Cannon balls")]
    [SerializeField]
    private GameObject cannonBall;
    [SerializeField]
    private int cannonBallInstanceAmnt;

    [Header("Special attack projectile")]
    [SerializeField]
    private GameObject specialAttackProjectile;
    [SerializeField]
    private int specialProjectileAmnt;

    [Header("Particles")]
    [SerializeField]
    private GameObject cannonBallShotParticles;
    [SerializeField]
    private int cannonBallShotParticlesInstanceAmnt;
    [SerializeField]
    private GameObject cannonBallExplosionParticles;
    [SerializeField]
    private int cannonBallExplosionInstanceAmnt;

    private List<GameObject> cannonBallList = new();
    private List<GameObject> specialProjectileList = new();
    private List<GameObject> cannonBallShotParticleList = new();
    private List<GameObject> cannonBallExplosionParticleList = new();

    public void Initialize()
    {
        InstantiateCannonBalls();
        InstantiateSpecialProjectiles();
        InstantiateCannonBallShotParticles();
        InstantiateCannonBallExplosionParticles();
    }
    #region Cannon ball
    private void InstantiateCannonBalls()
    {
        GameObject cannonBallParent = new GameObject("CannonBallParent");
        GameObject objectHelper;
        for (int index = 0; index < cannonBallInstanceAmnt; index++)
        {
            objectHelper = Instantiate(cannonBall, cannonBallParent.transform);
            objectHelper.SetActive(false);
            cannonBallList.Add(objectHelper);
        }
    }

    public GameObject GetCannonBall()
    {
        for (int index = 0; index < cannonBallList.Count; index++)
        {
            if (!cannonBallList[index].activeInHierarchy)
            {
                return cannonBallList[index];
            }
        }

        return null;
    }
    #endregion

    #region Special projectile
    private void InstantiateSpecialProjectiles()
    {
        GameObject specialProjectileParent = new GameObject("SpecialProjectileParent");
        GameObject objectHelper;
        for (int index = 0; index < specialProjectileAmnt; index++)
        {
            objectHelper = Instantiate(specialAttackProjectile, specialProjectileParent.transform);
            objectHelper.SetActive(false);
            specialProjectileList.Add(objectHelper);
        }
    }

    public GameObject GetSpecialProjectile()
    {
        for (int index = 0; index < specialProjectileList.Count; index++)
        {
            if (!specialProjectileList[index].activeInHierarchy)
            {
                return specialProjectileList[index];
            }
        }

        return null;
    }
    #endregion

    #region Cannon ball particles
    private void InstantiateCannonBallShotParticles()
    {
        GameObject cannonBallShotParticlesParent = new GameObject("CannonBallShotParticlesParent");
        GameObject objectHelper;
        for (int index = 0; index < cannonBallShotParticlesInstanceAmnt; index++)
        {
            objectHelper = Instantiate(cannonBallShotParticles, cannonBallShotParticlesParent.transform);
            objectHelper.SetActive(false);
            cannonBallShotParticleList.Add(objectHelper);
        }
    }

    public GameObject GetCannonBallShotParticle()
    {
        for (int index = 0; index < cannonBallShotParticleList.Count; index++)
        {
            if (!cannonBallShotParticleList[index].activeInHierarchy)
            {
                return cannonBallShotParticleList[index];
            }
        }

        return null;
    }

    private void InstantiateCannonBallExplosionParticles()
    {
        GameObject cannonBallExplosionParticlesParent = new GameObject("CannonBallExplosionParticlesParent");
        GameObject objectHelper;
        for (int index = 0; index < cannonBallExplosionInstanceAmnt; index++)
        {
            objectHelper = Instantiate(cannonBallExplosionParticles, cannonBallExplosionParticlesParent.transform);
            objectHelper.SetActive(false);
            cannonBallExplosionParticleList.Add(objectHelper);
        }
    }

    public GameObject GetCannonBallExplosionParticle()
    {
        for (int index = 0; index < cannonBallExplosionParticleList.Count; index++)
        {
            if (!cannonBallExplosionParticleList[index].activeInHierarchy)
            {
                return cannonBallExplosionParticleList[index];
            }
        }

        return null;
    }
    #endregion
}
