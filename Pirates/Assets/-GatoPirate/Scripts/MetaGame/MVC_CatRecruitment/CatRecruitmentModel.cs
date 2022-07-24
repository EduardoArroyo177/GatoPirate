using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRecruitmentModel : MonoBehaviour
{
    // TODO: Add a list per each catalogue type
    [SerializeField]
    private CatCatalogueVisualizationData[] catBasicCatalogueList;
    [SerializeField]
    private CatCatalogueVisualizationData[] catBasicSkinCatalogueList;

    public CatCatalogueVisualizationData[] CatBasicCatalogueList { get => catBasicCatalogueList; set => catBasicCatalogueList = value; }
}
