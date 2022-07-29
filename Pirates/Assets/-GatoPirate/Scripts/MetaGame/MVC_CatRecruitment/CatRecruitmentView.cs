using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRecruitmentView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject catCatalogueItemView;
    [SerializeField]
    private GameObject catSkinCatalogueItemView;

    [Header("Cats UI references")]
    [SerializeField]
    private Transform catBasicCatalogueContent;
    [SerializeField]
    private Transform catSpecialCatalogueContent;

    public GameObject CatCatalogueItemView { get => catCatalogueItemView; set => catCatalogueItemView = value; }
    public Transform CatBasicCatalogueContent { get => catBasicCatalogueContent; set => catBasicCatalogueContent = value; }
    public Transform CatSpecialCatalogueContent { get => catSpecialCatalogueContent; set => catSpecialCatalogueContent = value; }
}
