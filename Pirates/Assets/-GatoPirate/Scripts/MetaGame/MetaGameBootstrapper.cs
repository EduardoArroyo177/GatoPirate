using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaGameBootstrapper : MonoBehaviour
{
    [SerializeField]
    private CatRecruitmentController catRecruitmentController;

    private void Awake()
    {
        catRecruitmentController.Initialize();
    }
}
