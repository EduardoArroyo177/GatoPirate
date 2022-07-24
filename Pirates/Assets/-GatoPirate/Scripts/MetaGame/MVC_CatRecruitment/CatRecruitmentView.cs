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

    [Header("UI references")]
    [SerializeField]
    private Transform basicCatCatalogueContent;

    public GameObject CatCatalogueItemView { get => catCatalogueItemView; set => catCatalogueItemView = value; }
    public Transform BasicCatCatalogueContent { get => basicCatCatalogueContent; set => basicCatCatalogueContent = value; }

}
