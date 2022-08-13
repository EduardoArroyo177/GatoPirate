using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRecruitmentView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject catalogueItemView;

    [Header("Cat UI references")]
    [SerializeField]
    private Transform catBasicCatalogueContent;
    [SerializeField]
    private Transform catSpecialCatalogueContent;

    [Header("Skin UI references")]
    [SerializeField]
    private Transform skinBasicCatalogueContent;
    [SerializeField]
    private Transform skinSpecialCatalogueContent;
    [SerializeField]
    private Transform skinPremiumCatalogueContent;

    public GameObject CatalogueItemView { get => catalogueItemView; set => catalogueItemView = value; }
    // Cat content
    public Transform CatBasicCatalogueContent { get => catBasicCatalogueContent; set => catBasicCatalogueContent = value; }
    public Transform CatSpecialCatalogueContent { get => catSpecialCatalogueContent; set => catSpecialCatalogueContent = value; }
    // Skin content
    public Transform SkinBasicCatalogueContent { get => skinBasicCatalogueContent; set => skinBasicCatalogueContent = value; }
    public Transform SkinSpecialCatalogueContent { get => skinSpecialCatalogueContent; set => skinSpecialCatalogueContent = value; }
    public Transform SkinPremiumCatalogueContent { get => skinPremiumCatalogueContent; set => skinPremiumCatalogueContent = value; }
}
