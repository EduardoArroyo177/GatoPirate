using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class MetaGameBootstrapper : MonoBehaviour
{
    [Header("Cat recruitment")]
    [SerializeField]
    private CatRecruitmentController catRecruitmentController;
    [SerializeField]
    private StringIntEvent PurchaseCatEvent;
    [SerializeField]
    private VoidEvent CloseRecruitmentViewEvent;
    [SerializeField]
    private VoidEvent OpenGoToStorePopUpEvent;
    [SerializeField]
    private VoidEvent OpenCrewManagementPopUpEvent;

    private void Awake()
    {
        // First loading data
        CatsDataSaveManager.Instance.LoadCatsSavedData();

        // Everything else
        catRecruitmentController.PurchaseCatEvent = PurchaseCatEvent;
        catRecruitmentController.CloseRecruitmentViewEvent = CloseRecruitmentViewEvent;
        catRecruitmentController.OpenGoToStorePopUpEvent = OpenGoToStorePopUpEvent;
        catRecruitmentController.OpenCrewManagementPopUpEvent = OpenCrewManagementPopUpEvent;
        catRecruitmentController.Initialize();
    }
}
