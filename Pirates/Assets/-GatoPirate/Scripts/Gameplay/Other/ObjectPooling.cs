using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ObjectPooling : SceneSingleton<ObjectPooling>
{
    [Header("Basic attack projectile")]
    [SerializeField]
    private GameObject basicProjectile;
    [SerializeField]
    private int basicProjectileInstanceAmnt;

    [Header("Normal attack projectile")]
    [SerializeField]
    private GameObject normalProjectile;
    [SerializeField]
    private int normalProjectileInstanceAmnt;

    [Header("Normal attack particles")]
    [SerializeField]
    private GameObject normalShotParticles;
    [SerializeField]
    private int normalShotParticlesInstanceAmnt;
    [SerializeField]
    private GameObject normalExplosionParticles;
    [SerializeField]
    private int normalExplosionInstanceAmnt;

    [Header("Automatic attack projectile")]
    [SerializeField]
    private GameObject automaticProjectile;
    [SerializeField]
    private int automaticProjectileInstanceAmnt;

    [Header("Special attack projectile")]
    [SerializeField]
    private GameObject specialAttackProjectile;
    [SerializeField]
    private int specialProjectileAmnt;

    private List<GameObject> basicProjectileList = new();

    private List<GameObject> normalProjectileList = new();
    private List<GameObject> normalProjectileShotParticleList = new();
    private List<GameObject> normalProjectileExplosionParticleList = new();

    private List<GameObject> automaticProjectileList = new();

    private List<GameObject> specialProjectileList = new();

    public VoidEvent StopCombatEvent { get; set; }

    public void Initialize()
    {
        InstantiateBasicProjectiles();

        InstantiateNormalProjectiles();
        InstantiateNormalProjectileParticles();
        InstantiateNormalProjectileExplosionParticles();

        InstantiateAutomaticProjectiles();

        InstantiateSpecialProjectiles();
    }

    #region Basic projectile
    private void InstantiateBasicProjectiles()
    {
        GameObject cannonBallParent = new GameObject("BasicProjectileParent");
        GameObject objectHelper;
        for (int index = 0; index < basicProjectileInstanceAmnt; index++)
        {
            objectHelper = Instantiate(basicProjectile, cannonBallParent.transform);
            objectHelper.GetComponent<Projectile>().StopCombatEvent = StopCombatEvent;
            objectHelper.SetActive(false);
            basicProjectileList.Add(objectHelper);
        }
    }

    public GameObject GetBasicProjectile()
    {
        for (int index = 0; index < basicProjectileList.Count; index++)
        {
            if (!basicProjectileList[index].activeInHierarchy)
            {
                return basicProjectileList[index];
            }
        }

        return null;
    }
    #endregion

    #region Normal projectile
    private void InstantiateNormalProjectiles()
    {
        GameObject cannonBallParent = new GameObject("NormalProjectileParent");
        GameObject objectHelper;
        for (int index = 0; index < normalProjectileInstanceAmnt; index++)
        {
            objectHelper = Instantiate(normalProjectile, cannonBallParent.transform);
            objectHelper.GetComponent<Projectile>().StopCombatEvent = StopCombatEvent;
            objectHelper.SetActive(false);
            normalProjectileList.Add(objectHelper);
        }
    }

    public GameObject GetNormalProjectile()
    {
        for (int index = 0; index < normalProjectileList.Count; index++)
        {
            if (!normalProjectileList[index].activeInHierarchy)
            {
                return normalProjectileList[index];
            }
        }

        return null;
    }
    #endregion

    #region Normal projectile particles
    private void InstantiateNormalProjectileParticles()
    {
        GameObject cannonBallShotParticlesParent = new GameObject("CannonBallShotParticlesParent");
        GameObject objectHelper;
        for (int index = 0; index < normalShotParticlesInstanceAmnt; index++)
        {
            objectHelper = Instantiate(normalShotParticles, cannonBallShotParticlesParent.transform);
            objectHelper.SetActive(false);
            normalProjectileShotParticleList.Add(objectHelper);
        }
    }

    public GameObject GetNormalProjectileShotParticle()
    {
        for (int index = 0; index < normalProjectileShotParticleList.Count; index++)
        {
            if (!normalProjectileShotParticleList[index].activeInHierarchy)
            {
                return normalProjectileShotParticleList[index];
            }
        }

        return null;
    }

    private void InstantiateNormalProjectileExplosionParticles()
    {
        GameObject cannonBallExplosionParticlesParent = new GameObject("CannonBallExplosionParticlesParent");
        GameObject objectHelper;
        for (int index = 0; index < normalExplosionInstanceAmnt; index++)
        {
            objectHelper = Instantiate(normalExplosionParticles, cannonBallExplosionParticlesParent.transform);
            objectHelper.SetActive(false);
            normalProjectileExplosionParticleList.Add(objectHelper);
        }
    }

    public GameObject GetNormalProjectileExplosionParticle()
    {
        for (int index = 0; index < normalProjectileExplosionParticleList.Count; index++)
        {
            if (!normalProjectileExplosionParticleList[index].activeInHierarchy)
            {
                return normalProjectileExplosionParticleList[index];
            }
        }

        return null;
    }
    #endregion

    #region Automatic projectile
    private void InstantiateAutomaticProjectiles()
    {
        GameObject cannonBallParent = new GameObject("AutomaticProjectileParent");
        GameObject objectHelper;
        for (int index = 0; index < automaticProjectileInstanceAmnt; index++)
        {
            objectHelper = Instantiate(automaticProjectile, cannonBallParent.transform);
            objectHelper.GetComponent<Projectile>().StopCombatEvent = StopCombatEvent;
            objectHelper.SetActive(false);
            automaticProjectileList.Add(objectHelper);
        }
    }

    public GameObject GetAutomaticProjectile()
    {
        for (int index = 0; index < automaticProjectileList.Count; index++)
        {
            if (!automaticProjectileList[index].activeInHierarchy)
            {
                return automaticProjectileList[index];
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
            objectHelper.GetComponent<Projectile>().StopCombatEvent = StopCombatEvent;
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


}
