using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class MetaGameBootstrapper : MonoBehaviour
{
    [Header("Cat recruitment")]
    [SerializeField]
    private CatRecruitmentController catRecruitmentController;
    [SerializeField]
    private StringIntEvent PurchaseCatEvent;

    private void Awake()
    {
        // First loading data
        CatsDataSaveManager.Instance.LoadCatsSavedData();

        // Everything else
        catRecruitmentController.PurchaseCatEvent = PurchaseCatEvent;
        catRecruitmentController.Initialize();
    }
}
