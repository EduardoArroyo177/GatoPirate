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

    private List<GameObject> cannonBallList = new List<GameObject>();
    private List<GameObject> specialProjectileList = new List<GameObject>();

    public void Initialize()
    {
        InstantiateCannonBalls();
        InstantiateSpecialProjectiles();
    }

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
}
