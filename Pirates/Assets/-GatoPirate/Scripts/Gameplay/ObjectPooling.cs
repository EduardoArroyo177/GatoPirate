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

    private List<GameObject> cannonBallList = new List<GameObject>();

    public void Initialize()
    {
        InstantiateCannonBalls();
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
}
