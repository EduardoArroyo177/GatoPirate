using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CatCrewManagementController : MonoBehaviour
{
    [Header("View references")]
    [SerializeField]
    private CatCrewManagementView catCrewManagementView;
    [SerializeField]
    private ShipSlotView[] shipSlotViewList;

    public IntEvent SelectCatEvent { get; set; }
    public ShipSlotViewEvent SelectShipSlotEvent { get; set; }
    public CatTypeIDEvent NewCatPurchasedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();
    private List<OwnedCatView> ownedCatsList = new();

    private int lastIndex = 0;

    private bool isShipSlotCatSelected;
    private bool isInventoryCatSelected;

    private CatData selectedCatData;
    private CatSkinData selectedSkinData;
    private int selectedCatIndex = -1;
    private ShipSlotView selectedSlot;
    private string selectedCatID;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<int>.BuildEventHandler(SelectCatEvent, SelectCatEventCallback));
        _eventHandlers.Add(EventHandlerFactory<ShipSlotView>.BuildEventHandler(SelectShipSlotEvent, SelectShipSlotEventCallback));
        _eventHandlers.Add(EventHandlerFactory<CatType, string>.BuildEventHandler(NewCatPurchasedEvent, NewCatPurchasedEventCallback));

        FillCatInventoryData();
        FillCurrentShipCatData();
    }

    #region Initialization
    private void FillCatInventoryData()
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
            ownedCatViewHelper.SelectCatEvent = SelectCatEvent;
            // Setting data
            ownedCatViewHelper.SetIndexAndID(index, catDataSaveListHelper[index].CatID);
            ownedCatViewHelper.SetName(catDataSaveListHelper[index].CatName);
            ownedCatViewHelper.SetCatAndSkinData(catDataHelper);

            // TODO: Divide by 10 (variable) to show in different catalogue pages
            ownedCatViewHelper.transform.SetParent(catCrewManagementView.OwnedCatsContent1);
            lastIndex = index;

            ownedCatsList.Add(ownedCatViewHelper);
        }
    }

    private void FillCurrentShipCatData()
    {
        CatData catDataHelper;
        List<DataSaveCatStructure> catDataSaveListHelper = CatsDataSaveManager.Instance.GetCatShipCrewStructureData();
        
        if (catDataSaveListHelper != null || catDataSaveListHelper.Count > 0)
        {
            for (int index = 0; index < shipSlotViewList.Length; index++)
            {
                shipSlotViewList[index].SelectShipSlotEvent = SelectShipSlotEvent;
                shipSlotViewList[index].Initialize();

                if (index < catDataSaveListHelper.Count)
                {
                    // Set cat data
                    shipSlotViewList[index].CatID = catDataSaveListHelper[index].CatID;
                    
                    catDataHelper = CatsModel.Instance.GetCatData(catDataSaveListHelper[index].CatType);
                    shipSlotViewList[index].CatData = catDataHelper;
                    
                    // TODO: Add skin data

                    shipSlotViewList[index].InitializeCat();

                    // Find cat in owned cat list and mark it as selected
                    int catIndex = ownedCatsList.FindIndex(x => x.CatID.Equals(catDataSaveListHelper[index].CatID));
                    if (catIndex >= 0)
                    {
                        ownedCatsList[catIndex].SetAsUnavailable();
                    }
                }
            }
        }
        else
        {
            for (int index = 0; index < shipSlotViewList.Length; index++)
            {
                shipSlotViewList[index].SelectShipSlotEvent = SelectShipSlotEvent;
                shipSlotViewList[index].Initialize();
            }
        }
    }
    #endregion

    #region Event callbacks
    private void SelectCatEventCallback(int _catIndex)
    {
        if (_catIndex == selectedCatIndex)
        {
            RestartSelectedData();
            return;
        }
        else if (_catIndex != selectedCatIndex && selectedCatIndex != -1)
        {
            // Deselect previous selected cat
            ownedCatsList[selectedCatIndex].Deselect();
        }

        isInventoryCatSelected = true;

        // Get cat data structure
        DataSaveCatStructure selectedCat = CatsDataSaveManager.Instance.GetCatStructureData(selectedCatID);

        selectedCatIndex = _catIndex;
        selectedCatID = ownedCatsList[_catIndex].CatID;
         // Get cat data
        selectedCatData = CatsModel.Instance.GetCatData(selectedCat.CatType);
        // TODO: Get skin data
        //CatSkinData skinData = CatsModel.Instance.GetSkinData();

        // Check if slot is already selected
        if (isShipSlotCatSelected)
        {
            // If there was a selected cat before, free it now
            int ownedCatIndex = ownedCatsList.FindIndex(x => x.CatID.Equals(selectedSlot.CatID));
            if (ownedCatIndex >= 0)
            {
                ownedCatsList[ownedCatIndex].SetAsAvailable();
            }

            // Switch cat data
            selectedSlot.CatID = selectedCatID;
            selectedSlot.CatData = selectedCatData;
            selectedSlot.SkinData = selectedSkinData;
            selectedSlot.InitializeCat();
            selectedSlot.CurrentCatIndex = selectedCatIndex;

            // TODO: Save new cat crew here?

            // Remove selected cat from list
            ownedCatsList[selectedCatIndex].SetAsUnavailable();

            // Restart everything
            RestartSelectedData();
        }
        else
        {
            ownedCatsList[selectedCatIndex].SetAsSelected();
        }
    }

    private void SelectShipSlotEventCallback(ShipSlotView _selectedSlot)
    {
        if (_selectedSlot.Equals(selectedSlot))
        {
            RestartSelectedData();
            return;
        }

        isShipSlotCatSelected = true;
        selectedSlot = _selectedSlot;

        if (isInventoryCatSelected)
        {
            // If there was a selected cat before, free it now
            int ownedCatIndex = ownedCatsList.FindIndex(x => x.CatID.Equals(selectedSlot.CatID));
            if (ownedCatIndex >= 0)
            {
                ownedCatsList[ownedCatIndex].SetAsAvailable();
            }

            // Switch cat data
            selectedSlot.CatID = selectedCatID;
            selectedSlot.CatData = selectedCatData;
            selectedSlot.SkinData = selectedSkinData;
            selectedSlot.InitializeCat();
            selectedSlot.CurrentCatIndex = selectedCatIndex;

            // TODO: Save new cat crew here?

            // Remove selected cat from list
            ownedCatsList[selectedCatIndex].SetAsUnavailable();

            // Restart everything
            RestartSelectedData();
        }
        else
        {
            selectedSlot.SetAsSelected();
        }
    }

    private void RestartSelectedData()
    {
        if (selectedCatIndex >= 0)
            ownedCatsList[selectedCatIndex].Deselect();
        selectedCatIndex = -1;
        selectedCatData = null;
        selectedSkinData = null;
        isInventoryCatSelected = false;

        isShipSlotCatSelected = false;
        if (selectedSlot)
        {
            selectedSlot.Deselect();
            selectedSlot = null;
        }
    }

    private void NewCatPurchasedEventCallback(CatType _catType, string _catID)
    {
        GameObject catViewHelper = Instantiate(catCrewManagementView.CatView);
        OwnedCatView ownedCatViewHelper = catViewHelper.GetComponent<OwnedCatView>();
        CatData catDataHelper = CatsModel.Instance.GetCatData(_catType.ToString());

        ownedCatViewHelper = catViewHelper.GetComponent<OwnedCatView>();
        // Events
        ownedCatViewHelper.SelectCatEvent = SelectCatEvent;
        // Setting data
        // TODO: Get cat id
        ownedCatViewHelper.SetIndexAndID(lastIndex + 1, _catID);
        ownedCatViewHelper.SetName(catDataHelper.CatName);
        ownedCatViewHelper.SetCatAndSkinData(catDataHelper);

        // TODO: Get correct content page
        ownedCatViewHelper.transform.SetParent(catCrewManagementView.OwnedCatsContent1);
        
        ownedCatsList.Add(ownedCatViewHelper);
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
