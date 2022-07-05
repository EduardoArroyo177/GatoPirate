using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ObjectPooling : SceneSingleton<ObjectPooling>
{
    // Basic
    [Header("Basic attack projectile")]
    [SerializeField]
    private GameObject basicProjectile;
    [SerializeField]
    private int basicProjectileInstanceAmnt;

    [Header("Basic attack particles")]
    [SerializeField]
    private GameObject basicShotParticles;
    [SerializeField]
    private GameObject basicExplosionParticles;

    // Normal
    [Header("Normal attack projectile")]
    [SerializeField]
    private GameObject normalProjectile;
    [SerializeField]
    private int normalProjectileInstanceAmnt;

    [Header("Normal attack particles")]
    [SerializeField]
    private GameObject normalShotParticles;
    [SerializeField]
    private GameObject normalExplosionParticles;

    // Automatic
    [Header("Automatic attack projectile")]
    [SerializeField]
    private GameObject automaticProjectile;
    [SerializeField]
    private int automaticProjectileInstanceAmnt;

    [Header("Automatic attack particles")]
    [SerializeField]
    private GameObject automaticShotParticles;
    [SerializeField]
    private GameObject automaticExplosionParticles;

    // Special
    [Header("Special attack projectile")]
    [SerializeField]
    private GameObject specialAttackProjectile;
    [SerializeField]
    private int specialProjectileAmnt;

    [Header("Special attack particles")]
    [SerializeField]
    private GameObject specialShotParticles;
    [SerializeField]
    private GameObject specialExplosionParticles;

    // Basic
    private List<GameObject> basicProjectileList = new();
    private List<GameObject> basicProjectileShotParticleList = new();
    private List<GameObject> basicProjectileExplosionParticleList = new();

    // Normal
    private List<GameObject> normalProjectileList = new();
    private List<GameObject> normalProjectileShotParticleList = new();
    private List<GameObject> normalProjectileExplosionParticleList = new();

    // Automatic
    private List<GameObject> automaticProjectileList = new();
    private List<GameObject> automaticProjectileShotParticleList = new();
    private List<GameObject> automaticProjectileExplosionParticleList = new();

    // Special
    private List<GameObject> specialProjectileList = new();
    private List<GameObject> specialProjectileShotParticleList = new();
    private List<GameObject> specialProjectileExplosionParticleList = new();

    public VoidEvent StopCombatEvent { get; set; }

    public void Initialize()
    {
        InstantiateBasicProjectiles();
        InstantiateNormalProjectiles();
        InstantiateAutomaticProjectiles();
        InstantiateSpecialProjectiles();
    }

    #region Basic projectile
    private void InstantiateBasicProjectiles()
    {
        GameObject basicProjectileParent = new GameObject("BasicProjectileParent");
        GameObject projectileHelper;
        GameObject basicProjectileShotParticlesParent = new GameObject("BasicShotParticlesParent");
        GameObject basicProjectileShotHelper;
        GameObject basicExplosionParticlesParent = new GameObject("BasicExplosionParticlesParent");
        GameObject basicExplosionHelper;
        for (int index = 0; index < basicProjectileInstanceAmnt; index++)
        {
            // Projectile
            projectileHelper = Instantiate(basicProjectile, basicProjectileParent.transform);
            projectileHelper.GetComponent<Projectile>().StopCombatEvent = StopCombatEvent;
            projectileHelper.SetActive(false);
            basicProjectileList.Add(projectileHelper);

            // Shot particles
            basicProjectileShotHelper = Instantiate(basicShotParticles, basicProjectileShotParticlesParent.transform);
            basicProjectileShotHelper.SetActive(false);
            basicProjectileShotParticleList.Add(basicProjectileShotHelper);

            // Explosion particles
            basicExplosionHelper = Instantiate(basicExplosionParticles, basicExplosionParticlesParent.transform);
            basicExplosionHelper.SetActive(false);
            basicProjectileExplosionParticleList.Add(basicExplosionHelper);
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

    public GameObject GetBasicProjectileShotParticle()
    {
        for (int index = 0; index < basicProjectileShotParticleList.Count; index++)
        {
            if (!basicProjectileShotParticleList[index].activeInHierarchy)
            {
                return basicProjectileShotParticleList[index];
            }
        }

        return null;
    }

    public GameObject GetBasicProjectileExplosionParticle()
    {
        for (int index = 0; index < basicProjectileExplosionParticleList.Count; index++)
        {
            if (!basicProjectileExplosionParticleList[index].activeInHierarchy)
            {
                return basicProjectileExplosionParticleList[index];
            }
        }

        return null;
    }
    #endregion

    #region Normal projectile
    private void InstantiateNormalProjectiles()
    {
        GameObject normalProjectileParent = new GameObject("NormalProjectileParent");
        GameObject projectileHelper;
        GameObject normalProjectileShotParticlesParent = new GameObject("NormalShotParticlesParent");
        GameObject normalProjectileShotHelper;
        GameObject normalExplosionParticlesParent = new GameObject("NormalExplosionParticlesParent");
        GameObject normalExplosionHelper;
        for (int index = 0; index < normalProjectileInstanceAmnt; index++)
        {
            // Projectile
            projectileHelper = Instantiate(normalProjectile, normalProjectileParent.transform);
            projectileHelper.GetComponent<Projectile>().StopCombatEvent = StopCombatEvent;
            projectileHelper.SetActive(false);
            normalProjectileList.Add(projectileHelper);

            // Shot particles
            normalProjectileShotHelper = Instantiate(normalExplosionParticles, normalProjectileShotParticlesParent.transform);
            normalProjectileShotHelper.SetActive(false);
            normalProjectileShotParticleList.Add(normalProjectileShotHelper);

            // Explosion particles
            normalExplosionHelper = Instantiate(normalExplosionParticles, normalExplosionParticlesParent.transform);
            normalExplosionHelper.SetActive(false);
            normalProjectileExplosionParticleList.Add(normalExplosionHelper);
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
        GameObject automaticProjectileParent = new GameObject("AutomaticProjectileParent");
        GameObject projectileHelper;
        GameObject automaticProjectileShotParticlesParent = new GameObject("AutomaticShotParticlesParent");
        GameObject automaticProjectileShotHelper;
        GameObject automaticExplosionParticlesParent = new GameObject("AutomaticExplosionParticlesParent");
        GameObject automaticExplosionHelper;

        for (int index = 0; index < automaticProjectileInstanceAmnt; index++)
        {
            // Projectile
            projectileHelper = Instantiate(automaticProjectile, automaticProjectileParent.transform);
            projectileHelper.GetComponent<Projectile>().StopCombatEvent = StopCombatEvent;
            projectileHelper.SetActive(false);
            automaticProjectileList.Add(projectileHelper);

            // Shot particles
            automaticProjectileShotHelper = Instantiate(automaticShotParticles, automaticProjectileShotParticlesParent.transform);
            automaticProjectileShotHelper.SetActive(false);
            automaticProjectileShotParticleList.Add(automaticProjectileShotHelper);

            // Explosion particles
            automaticExplosionHelper = Instantiate(automaticExplosionParticles, automaticExplosionParticlesParent.transform);
            automaticExplosionHelper.SetActive(false);
            automaticProjectileExplosionParticleList.Add(automaticExplosionHelper);
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

    public GameObject GetAutomaticProjectileShotParticle()
    {
        for (int index = 0; index < automaticProjectileShotParticleList.Count; index++)
        {
            if (!automaticProjectileShotParticleList[index].activeInHierarchy)
            {
                return automaticProjectileShotParticleList[index];
            }
        }

        return null;
    }

    public GameObject GetAutomaticProjectileExplosionParticle()
    {
        for (int index = 0; index < automaticProjectileExplosionParticleList.Count; index++)
        {
            if (!automaticProjectileExplosionParticleList[index].activeInHierarchy)
            {
                return automaticProjectileExplosionParticleList[index];
            }
        }

        return null;
    }
    #endregion

    #region Special projectile
    private void InstantiateSpecialProjectiles()
    {
        GameObject specialProjectileParent = new GameObject("SpecialProjectileParent");
        GameObject projectileHelper;
        GameObject specialProjectileShotParticlesParent = new GameObject("SpecialShotParticlesParent");
        GameObject specialProjectileShotHelper;
        GameObject specialExplosionParticlesParent = new GameObject("SpecialExplosionParticlesParent");
        GameObject specialExplosionHelper;
        for (int index = 0; index < specialProjectileAmnt; index++)
        {
            // Projectile
            projectileHelper = Instantiate(specialAttackProjectile, specialProjectileParent.transform);
            projectileHelper.GetComponent<Projectile>().StopCombatEvent = StopCombatEvent;
            projectileHelper.SetActive(false);
            specialProjectileList.Add(projectileHelper);

            // Shot particles
            specialProjectileShotHelper = Instantiate(specialShotParticles, specialProjectileShotParticlesParent.transform);
            specialProjectileShotHelper.SetActive(false);
            specialProjectileShotParticleList.Add(specialProjectileShotHelper);

            // Explosion particles
            specialExplosionHelper = Instantiate(specialExplosionParticles, specialExplosionParticlesParent.transform);
            specialExplosionHelper.SetActive(false);
            specialProjectileExplosionParticleList.Add(specialExplosionHelper);
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

    public GameObject GetSpecialProjectileShotParticle()
    {
        for (int index = 0; index < specialProjectileShotParticleList.Count; index++)
        {
            if (!specialProjectileShotParticleList[index].activeInHierarchy)
            {
                return specialProjectileShotParticleList[index];
            }
        }

        return null;
    }

    public GameObject GetSpecialProjectileExplosionParticle()
    {
        for (int index = 0; index < specialProjectileExplosionParticleList.Count; index++)
        {
            if (!specialProjectileExplosionParticleList[index].activeInHierarchy)
            {
                return specialProjectileExplosionParticleList[index];
            }
        }

        return null;
    }
    #endregion


}
