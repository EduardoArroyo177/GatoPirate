using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCrewManagementController : MonoBehaviour
{
    [Header("View references")]
    [SerializeField]
    private CatCrewManagementView catCrewManagementView;

    public void Initialize()
    {
        FillCatData();
    }

    private void FillCatData()
    {
        GameObject catViewHelper;
        OwnedCatView ownedCatViewHelper;
        CatData catDataHelper;
        List<DataSaveCatStructure> catDataSaveListHelper = CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList;
        for (int index = 0; index < catDataSaveListHelper.Count; index++)
        {
            catDataHelper = CatsModel.Instance.GetCatData(catDataSaveListHelper[index].CatType);
            catViewHelper = Instantiate(catCrewManagementView.CatView);
            // TODO: Get skin data

            ownedCatViewHelper = catViewHelper.GetComponent<OwnedCatView>();
            // Events
            // Setting data
            ownedCatViewHelper.SetIndexAndID(index, catDataSaveListHelper[index].CatID);
            ownedCatViewHelper.SetName(catDataSaveListHelper[index].CatName);
            ownedCatViewHelper.SetCatAndSkinData(catDataHelper);

            // TODO: Divide by 10 (variable) to show in different catalogue pages
            ownedCatViewHelper.transform.SetParent(catCrewManagementView.OwnedCatsContent1);
        }
    }
}
