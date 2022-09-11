using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCrewManagementView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject catView;

    [Header("UI references")]
    [SerializeField]
    private Transform[] ownedCatsContentList;
    [SerializeField]
    private int catalogueSizePerPage;
    [SerializeField]
    private int enabledCatalogues;

    public CatCrewManagementController CatCrewManagementController { get; set; }

    public GameObject CatView { get => catView; set => catView = value; }
    public Transform[] OwnedCatsContentList { get => ownedCatsContentList; set => ownedCatsContentList = value; }
    public int CatalogueSizePerPage { get => catalogueSizePerPage; set => catalogueSizePerPage = value; }
    public int EnabledCatalogues { get => enabledCatalogues; set => enabledCatalogues = value; }


    #region Button calls
    public void GoToCombat()
    {
        CatCrewManagementController.StartCombat();
    }
    #endregion
}
