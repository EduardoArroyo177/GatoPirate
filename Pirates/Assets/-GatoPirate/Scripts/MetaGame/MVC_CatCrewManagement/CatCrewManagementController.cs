using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class CatCrewManagementController : MonoBehaviour
{
    [Header("View references")]
    [SerializeField]
    private CatCrewManagementView catCrewManagementView;

    public CatTypeIDEvent NewCatPurchasedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    private int lastIndex = 0;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CatType, string>.BuildEventHandler(NewCatPurchasedEvent, NewCatPurchasedEventCallback));

        FillCatData();
    }

    #region Initialization
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
            lastIndex = index;
        }
    }
    #endregion

    #region Event callbacks
    private void NewCatPurchasedEventCallback(CatType _catType, string _catID)
    {
        GameObject catViewHelper = Instantiate(catCrewManagementView.CatView);
        OwnedCatView ownedCatViewHelper = catViewHelper.GetComponent<OwnedCatView>();
        CatData catDataHelper = CatsModel.Instance.GetCatData(_catType.ToString());

        ownedCatViewHelper = catViewHelper.GetComponent<OwnedCatView>();
        // Events
        // Setting data
        // TODO: Get cat id
        ownedCatViewHelper.SetIndexAndID(lastIndex + 1, _catID);
        ownedCatViewHelper.SetName(catDataHelper.CatName);
        ownedCatViewHelper.SetCatAndSkinData(catDataHelper);

        // TODO: Get correct content page
        ownedCatViewHelper.transform.SetParent(catCrewManagementView.OwnedCatsContent1);
    }
    #endregion

    #region OnDestroy
    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
