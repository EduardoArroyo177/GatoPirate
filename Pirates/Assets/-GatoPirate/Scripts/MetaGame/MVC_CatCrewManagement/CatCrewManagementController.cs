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
            // Deselect
            ownedCatsList[_catIndex].Deselect();
            selectedCatIndex = -1;
            selectedCatData = null;
            selectedSkinData = null;
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
            
            RestartSelectedData();
            // TODO: Save new cat crew here?
            // TODO: Remove selected cat from list
        }
        else
        {
            ownedCatsList[_catIndex].SetAsSelected();
        }
    }

    private void SelectShipSlotEventCallback(ShipSlotView _selectedSlot)
    {
        isSlotSelected = true;
        selectedSlot = _selectedSlot;

        if (isCatDataSelected)
        {
            // Switch 
            _selectedSlot.CatData = selectedCatData;
            _selectedSlot.SkinData = selectedSkinData;
            _selectedSlot.InitializeCat();

            RestartSelectedData();
            // TODO: Save new cat crew here?
            // TODO: Remove selected cat from list
        }
        else
        {
            _selectedSlot.SetAsSelected();
        }
    }

    private void RestartSelectedData()
    {
        ownedCatsList[selectedCatIndex].Deselect();
        selectedCatIndex = -1;
        selectedCatData = null;
        selectedSkinData = null;
        isCatDataSelected = false;

        isSlotSelected = false;
        selectedSlot.Deselect();
        selectedSlot = null;
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
