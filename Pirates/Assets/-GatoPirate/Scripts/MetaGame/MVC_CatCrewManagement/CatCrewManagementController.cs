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
    private bool isSlotSelected;
    private bool isCatDataSelected;
    private CatData selectedCatData;
    private CatSkinData selectedSkinData;
    private int selectedCatIndex = -1;
    private ShipSlotView selectedSlot;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<int>.BuildEventHandler(SelectCatEvent, SelectCatEventCallback));
        _eventHandlers.Add(EventHandlerFactory<ShipSlotView>.BuildEventHandler(SelectShipSlotEvent, SelectShipSlotEventCallback));
        _eventHandlers.Add(EventHandlerFactory<CatType, string>.BuildEventHandler(NewCatPurchasedEvent, NewCatPurchasedEventCallback));

        FillCurrentShipCatData();
        FillCatInventoryData();
    }

    #region Initialization
    private void FillCurrentShipCatData()
    {
        // Need to initialize current saved cats inside ship
        // Then initialize slot data
        for (int index = 0; index < shipSlotViewList.Length; index++)
        {
            shipSlotViewList[index].SelectShipSlotEvent = SelectShipSlotEvent;
            shipSlotViewList[index].Initialize();
        }
    }

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
            // De select previous selected cat
            ownedCatsList[selectedCatIndex].Deselect();
        }

        selectedCatIndex = _catIndex;
        string catID = ownedCatsList[_catIndex].CatID;
        
        // Get cat data structure
        DataSaveCatStructure selectedCat = CatsDataSaveManager.Instance.GetCatStructureData(catID);
        // Get cat data
        selectedCatData = CatsModel.Instance.GetCatData(selectedCat.CatType);
        // Get skin data
        //CatSkinData skinData = CatsModel.Instance.GetSkinData();
        isCatDataSelected = true;

        // Check if slot is already selected
        if (isSlotSelected)
        {
            // Set cat data and skin data to slot
            selectedSlot.CatData = selectedCatData;
            selectedSlot.SkinData = selectedSkinData;
            selectedSlot.InitializeCat();
            selectedSlot.CurrentCatIndex = selectedCatIndex;

            // TODO: Save new cat crew here?
            // Remove selected cat from list
            ownedCatsList[selectedCatIndex].SetAsUnavailable();
            // TODO: If there was a selected cat before, free it now
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

        isSlotSelected = true;
        selectedSlot = _selectedSlot;

        if (isCatDataSelected)
        {
            // Switch 
            _selectedSlot.CatData = selectedCatData;
            _selectedSlot.SkinData = selectedSkinData;
            _selectedSlot.InitializeCat();
            _selectedSlot.CurrentCatIndex = selectedCatIndex;
            // TODO: Save new cat crew here?
            // TODO: Remove selected cat from list -> Deactivate cat from list (not remove), add selected cat index to selected slot (to recover selected cat when changed)
            ownedCatsList[selectedCatIndex].SetAsUnavailable();
            // TODO: If there was a selected cat before, free it now
            RestartSelectedData();
        }
        else
        {
            _selectedSlot.SetAsSelected();
        }
    }

    private void RestartSelectedData()
    {
        if (selectedCatIndex >= 0)
            ownedCatsList[selectedCatIndex].Deselect();
        selectedCatIndex = -1;
        selectedCatData = null;
        selectedSkinData = null;
        isCatDataSelected = false;

        isSlotSelected = false;
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
