using System;
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
    private CatalogueNavigationView catalogueNavigationView;
    [SerializeField]
    private ShipSlotView[] shipSlotViewList;

    public IntEvent SelectCatEvent { get; set; }
    public ShipSlotViewEvent SelectShipSlotEvent { get; set; }
    public CatTypeIDEvent NewCatPurchasedEvent { get; set; }
    public StringEvent OpenCatCrewManagementEvent { get; set; }
    public StringEvent CatUpdatedEvent { get; set; }
    public VoidEvent OpenCatCrewManagementNoIDEvent { get; set; }
    public VoidEvent StartCombatEvent { get; set; }
    // Sounds
    public CatSoundEvent TriggerCatSoundEvent { get; set; }



    private List<IAtomEventHandler> _eventHandlers = new();
    private List<OwnedCatView> ownedCatsList = new();

    private int currentContentIndex = 0;

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
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(OpenCatCrewManagementEvent, OpenCatCrewManagementEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(CatUpdatedEvent, CatUpdatedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(OpenCatCrewManagementNoIDEvent, OpenCatCrewManagementNoIDEventCallback));

        catCrewManagementView.CatCrewManagementController = this;

        FillCatInventoryData();
        FillCurrentShipCatData();
    }

    #region Initialization
    private void FillCatInventoryData()
    {
        GameObject catViewHelper;
        OwnedCatView ownedCatViewHelper;
        CatData catDataHelper;
        CatSkinData skinDataHelper;
        List<DataSaveCatStructure> catDataSaveListHelper = CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList;
        for (int index = 0; index < catDataSaveListHelper.Count; index++)
        {
            catDataHelper = CatsModel.Instance.GetCatData(catDataSaveListHelper[index].CatType);
            skinDataHelper = CatsModel.Instance.GetSkinData(catDataSaveListHelper[index].SkinType);
            catViewHelper = Instantiate(catCrewManagementView.CatView);

            ownedCatViewHelper = catViewHelper.GetComponent<OwnedCatView>();
            // Events
            ownedCatViewHelper.SelectCatEvent = SelectCatEvent;
            // Setting data
            ownedCatViewHelper.SetIndexAndID(index, catDataSaveListHelper[index].CatID);
            ownedCatViewHelper.SetName(catDataSaveListHelper[index].CatName);
            ownedCatViewHelper.SetCatAndSkinData(catDataHelper, skinDataHelper);

            // Set catalogues
            catCrewManagementView.EnabledCatalogues = SetEnabledCatalogues(index);
            ownedCatViewHelper.transform.SetParent(catCrewManagementView.OwnedCatsContentList[currentContentIndex]);
            ownedCatsList.Add(ownedCatViewHelper);
        }

        catalogueNavigationView.Initialize(catCrewManagementView.EnabledCatalogues);
    }

    private int SetEnabledCatalogues(int _catsAmount)
    {
        if (_catsAmount < catCrewManagementView.CatalogueSizePerPage)
        {
            currentContentIndex = 0;
            return 1;
        }
        else if (_catsAmount < catCrewManagementView.CatalogueSizePerPage * 2)
        {
            currentContentIndex = 1;
            return 2;
        }
        else if (_catsAmount < catCrewManagementView.CatalogueSizePerPage * 3)
        {
            currentContentIndex = 2;
            return 3;
        }
        else if (_catsAmount < catCrewManagementView.CatalogueSizePerPage * 4)
        {
            currentContentIndex = 3;
            return 4;
        }
        else if (_catsAmount < catCrewManagementView.CatalogueSizePerPage * 5)
        {
            currentContentIndex = 4;
            return 5;
        }
        else if (_catsAmount < catCrewManagementView.CatalogueSizePerPage * 6)
        {
            currentContentIndex = 5;
            return 6;
        }
        else if (_catsAmount < catCrewManagementView.CatalogueSizePerPage * 7)
        {
            currentContentIndex = 6;
            return 7;
        }
        else if (_catsAmount < catCrewManagementView.CatalogueSizePerPage * 8)
        {
            currentContentIndex = 7;
            return 8;
        }
        else if (_catsAmount < catCrewManagementView.CatalogueSizePerPage * 9)
        {
            currentContentIndex = 8;
            return 9;
        }
        else
        {
            currentContentIndex = 9;
            return 10;
        }
    }

    private void FillCurrentShipCatData()
    {
        for (int index = 0; index < shipSlotViewList.Length; index++)
        {
            shipSlotViewList[index].SelectShipSlotEvent = SelectShipSlotEvent;
            shipSlotViewList[index].Initialize();
        }

        CatData catDataHelper;
        CatSkinData skinDataHelper;
        List<DataSaveCatStructure> catDataSaveListHelper = CatsDataSaveManager.Instance.GetCatShipCrewStructureData();
        int slotIndexHelper = 0;
        if (catDataSaveListHelper != null || catDataSaveListHelper.Count > 0)
        {
            for (int index = 0; index < catDataSaveListHelper.Count; index++)
            {
                slotIndexHelper = catDataSaveListHelper[index].IslandSlot;
                shipSlotViewList[slotIndexHelper].CatID = catDataSaveListHelper[index].CatID;
                // Cat data
                catDataHelper = CatsModel.Instance.GetCatData(catDataSaveListHelper[index].CatType);
                shipSlotViewList[slotIndexHelper].CatData = catDataHelper;
                skinDataHelper = CatsModel.Instance.GetSkinData(catDataSaveListHelper[index].SkinType);
                shipSlotViewList[slotIndexHelper].SkinData = skinDataHelper;

                shipSlotViewList[slotIndexHelper].InitializeCat();

                // Find cat in owned cat list and mark it as selected
                int catIndex = ownedCatsList.FindIndex(x => x.CatID.Equals(catDataSaveListHelper[index].CatID));
                if (catIndex >= 0)
                {
                    ownedCatsList[catIndex].SetAsUnavailable();
                }
            }
        }
    }
    #endregion

    #region Event callbacks
    private void OpenCatCrewManagementEventCallback(string _catID)
    {
        // TODO: Get cat from cat id and select it by default through this
        catCrewManagementView.gameObject.SetActive(true);
    }

    private void OpenCatCrewManagementNoIDEventCallback(UnityAtoms.Void _item)
    {
        catCrewManagementView.gameObject.SetActive(true);
    }

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

        selectedCatIndex = _catIndex;
        selectedCatID = ownedCatsList[_catIndex].CatID;
        // Get cat data structure
        DataSaveCatStructure selectedCat = CatsDataSaveManager.Instance.GetCatStructureData(selectedCatID);
        
         // Get cat data
        selectedCatData = CatsModel.Instance.GetCatData(selectedCat.CatType);
        selectedSkinData = CatsModel.Instance.GetSkinData(selectedCat.SkinType);

        // Check if slot is already selected
        if (isShipSlotCatSelected)
        {
            // If there was a selected cat before, free it now
            int ownedCatIndex = ownedCatsList.FindIndex(x => x.CatID.Equals(selectedSlot.CatID));
            if (ownedCatIndex >= 0)
            {
                ownedCatsList[ownedCatIndex].SetAsAvailable();
                CatsDataSaveManager.Instance.UpdateCatIslandSlot(ownedCatsList[ownedCatIndex].CatID, -1);
            }

            // Switch cat data
            selectedSlot.CatID = selectedCatID;
            selectedSlot.CatData = selectedCatData;
            selectedSlot.SkinData = selectedSkinData;
            selectedSlot.InitializeCat();
            selectedSlot.CurrentCatIndex = selectedCatIndex;

            // TODO: Trigger cat switched sound
            TriggerCatSoundEvent.Raise(CatMeowSounds.CREW_SWITCHED_CAT1);

            // TODO: Move save data to its own button ((Accept/Combat button)
            CatsDataSaveManager.Instance.UpdateCatIslandSlot(selectedSlot.CatID, selectedSlot.SlotIndex);

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
                CatsDataSaveManager.Instance.UpdateCatIslandSlot(ownedCatsList[ownedCatIndex].CatID, -1);

            }

            // Switch cat data
            selectedSlot.CatID = selectedCatID;
            selectedSlot.CatData = selectedCatData;
            selectedSlot.SkinData = selectedSkinData;
            selectedSlot.InitializeCat();
            selectedSlot.CurrentCatIndex = selectedCatIndex;

            // Trigger cat switched sound
            TriggerCatSoundEvent.Raise(CatMeowSounds.CREW_SWITCHED_CAT1);

            // Save new cat crew here
            CatsDataSaveManager.Instance.UpdateCatIslandSlot(selectedSlot.CatID, selectedSlot.SlotIndex);

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

    private void NewCatPurchasedEventCallback(CatType _catType, string _catID)
    {
        GameObject catViewHelper = Instantiate(catCrewManagementView.CatView);

        OwnedCatView ownedCatViewHelper = catViewHelper.GetComponent<OwnedCatView>();
        CatData catDataHelper = CatsModel.Instance.GetCatData(_catType.ToString());

        ownedCatViewHelper = catViewHelper.GetComponent<OwnedCatView>();
        // Events
        ownedCatViewHelper.SelectCatEvent = SelectCatEvent;
        // Setting data
        ownedCatViewHelper.SetIndexAndID(ownedCatsList.Count, _catID);
        ownedCatViewHelper.SetName(catDataHelper.CatName);
        ownedCatViewHelper.SetCatAndSkinData(catDataHelper);
        ownedCatsList.Add(ownedCatViewHelper);

        // Update catalogues
        catCrewManagementView.EnabledCatalogues = SetEnabledCatalogues(ownedCatsList.Count - 1);
        ownedCatViewHelper.transform.SetParent(catCrewManagementView.OwnedCatsContentList[currentContentIndex]);
        catalogueNavigationView.Initialize(catCrewManagementView.EnabledCatalogues);
    }

    private void CatUpdatedEventCallback(string _catID)
    {
        DataSaveCatStructure cat = CatsDataSaveManager.Instance.GetCatStructureData(_catID);
        CatData catData = CatsModel.Instance.GetCatData(cat.CatType); ;
        CatSkinData skinData = CatsModel.Instance.GetSkinData(cat.SkinType); 

        // Find cat in catalogues
        int catIndex = ownedCatsList.FindIndex(x => x.CatID.Equals(_catID));
        if (catIndex >= 0)
        {
            ownedCatsList[catIndex].SetCatAndSkinData(catData, skinData);
        }

        // Find cat in ship slots
        for (int index = 0; index < shipSlotViewList.Length; index++)
        {
            if (shipSlotViewList[index].CatID.Equals(_catID))
            {
                shipSlotViewList[index].CatData = catData;
                shipSlotViewList[index].SkinData = skinData;
                shipSlotViewList[index].InitializeCat();
            }
        }
    }
    #endregion

    #region Helper methods
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

    public void StartCombat()
    {
        catCrewManagementView.gameObject.SetActive(false);
        StartCombatEvent.Raise();
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
