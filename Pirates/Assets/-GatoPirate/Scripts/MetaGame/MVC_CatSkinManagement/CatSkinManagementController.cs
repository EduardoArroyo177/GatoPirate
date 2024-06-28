using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CatSkinManagementController : MonoBehaviour
{
    [SerializeField]
    private CatSkinManagementView catSkinManagementView;
    [SerializeField]
    private SelectedCatSlotView selectedCatSlotView;

    public IntEvent SelectSkinEvent { get; set; }
    public StringEvent OpenSkinManagementEvent { get; set; }
    public StringEvent CatUpdatedEvent { get; set; }
    public StringEvent SkinPurchasedEvent { get; set; }
    // Sounds
    public CatSoundEvent TriggerCatSoundEvent { get; set; }

    public VoidEvent UnloadEventsEvent { get; set; }


    private List<IAtomEventHandler> _eventHandlers = new();
    private List<OwnedSkinView> ownedSkinList = new();

    private int selectedSkinIndex = -1;
    private string catID = "";
    public SkinType selectedSkinType = SkinType.NONE;

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(OpenSkinManagementEvent, OpenSkinManagementEventCallback));
        _eventHandlers.Add(EventHandlerFactory<int>.BuildEventHandler(SelectSkinEvent, SelectSkinEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(SkinPurchasedEvent, SkinPurchasedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));

        catSkinManagementView.CatSkinManagementController = this;
        // Initialize current cat with cat id from event
        FillSkinInventoryData();
    }

    #region Initialization
    private void FillSkinInventoryData()
    {
        List<DataSaveSkinStructure> skinDataSaveListHelper = CatsDataSaveManager.Instance.DataSaveSkinPurchasedStructure.PurchasedSkinList;
        GameObject skinViewHelper;
        OwnedSkinView ownedSkinViewHelper;
        CatSkinData catSkinDataHelper;

        // First fill empty skin view
        skinViewHelper = Instantiate(catSkinManagementView.SkinView);
        ownedSkinViewHelper = skinViewHelper.GetComponent<OwnedSkinView>();
        // Events
        ownedSkinViewHelper.SelectSkinEvent = SelectSkinEvent;
        // Setting data
        ownedSkinViewHelper.SetIndexAndType(0, SkinType.NONE);
        ownedSkinViewHelper.SetName(catSkinManagementView.NoneSkinName);
        ownedSkinViewHelper.SetSkinSprite(catSkinManagementView.NoneSkinSprite);

        ownedSkinViewHelper.transform.SetParent(catSkinManagementView.SkinsCatalogueContent);
        ownedSkinList.Add(ownedSkinViewHelper);

        // Then current skins
        for (int index = 0; index < skinDataSaveListHelper.Count; index++)
        {
            catSkinDataHelper = CatsModel.Instance.GetSkinData(skinDataSaveListHelper[index].SkinType);
            skinViewHelper = Instantiate(catSkinManagementView.SkinView);

            ownedSkinViewHelper = skinViewHelper.GetComponent<OwnedSkinView>();
            // Events
            ownedSkinViewHelper.SelectSkinEvent = SelectSkinEvent;
            // Setting data
            ownedSkinViewHelper.SetIndexAndType(index + 1, catSkinDataHelper.SkinType);
            ownedSkinViewHelper.SetName(catSkinDataHelper.SkinName);
            ownedSkinViewHelper.SetSkinSprite(catSkinDataHelper.SkinPreviewSprite);

            ownedSkinViewHelper.transform.SetParent(catSkinManagementView.SkinsCatalogueContent);

            ownedSkinList.Add(ownedSkinViewHelper);
        }
    }
    #endregion

    #region Event callbacks
    private void OpenSkinManagementEventCallback(string _catID)
    {
        // Load cat from cat id
        DataSaveCatStructure catStructure = CatsDataSaveManager.Instance.GetCatStructureData(_catID);
        CatData catData = CatsModel.Instance.GetCatData(catStructure.CatType);
        CatSkinData skinData = CatsModel.Instance.GetSkinData(catStructure.SkinType);
        
        selectedCatSlotView.CatData = catData;
        selectedCatSlotView.SkinData = skinData;
        selectedCatSlotView.InitializeCat();

        catSkinManagementView.gameObject.SetActive(true);

        catID = _catID;

        if (skinData)
            SetInitialSkinAsUnavailable(skinData.SkinType);
        else
            SetInitialSkinAsUnavailable();
    }

    private void SetInitialSkinAsUnavailable(SkinType _skinType = SkinType.NONE)
    {
        int index = ownedSkinList.FindIndex(x => x.SkinType.Equals(_skinType));
        selectedSkinIndex = index;
        selectedSkinType = _skinType;
        if (index < 0)
            return;
        else
            ownedSkinList[index].SetAsUnavailable();
    }

    // Skin selected callback 
    private void SelectSkinEventCallback(int _skinIndex)
    {
        if (_skinIndex != selectedSkinIndex && selectedSkinIndex != -1)
            ownedSkinList[selectedSkinIndex].SetAsAvailable();

        CatSkinData skinData = CatsModel.Instance.GetSkinData(ownedSkinList[_skinIndex].SkinType.ToString());
        selectedCatSlotView.SkinData = skinData;

        if (skinData)
            selectedCatSlotView.InitializeCat();
        else
            selectedCatSlotView.RemoveSkin();

        ownedSkinList[_skinIndex].SetAsUnavailable();
        selectedSkinIndex = _skinIndex;
        selectedSkinType = ownedSkinList[_skinIndex].SkinType;

        // TODO: Trigger basic sound 
    }

    // New skin purchased
    private void SkinPurchasedEventCallback(string _skinType)
    {
        GameObject skinViewHelper = Instantiate(catSkinManagementView.SkinView);
        OwnedSkinView ownedSkinViewHelper = skinViewHelper.GetComponent<OwnedSkinView>();
        CatSkinData catSkinDataHelper = CatsModel.Instance.GetSkinData(_skinType);

        ownedSkinViewHelper.SelectSkinEvent = SelectSkinEvent;
        ownedSkinViewHelper.SetIndexAndType(ownedSkinList.Count, catSkinDataHelper.SkinType);
        ownedSkinViewHelper.SetName(catSkinDataHelper.SkinName);
        ownedSkinViewHelper.SetSkinSprite(catSkinDataHelper.SkinPreviewSprite);

        ownedSkinViewHelper.transform.SetParent(catSkinManagementView.SkinsCatalogueContent);

        ownedSkinList.Add(ownedSkinViewHelper);
    }
    #endregion

    #region Public methods
    public void SaveAndClose()
    {
        // Trigger skin cat sound
        TriggerCatSoundEvent.Raise(CatMeowSounds.SKIN_CHANGED_CAT1);

        // TODO: Update cat in save manager
        CatsDataSaveManager.Instance.UpdateCatSkin(catID, selectedSkinType);
        CatUpdatedEvent.Raise(catID);

        // Set previous cat skin as available
        int index = ownedSkinList.FindIndex(x => x.SkinType.Equals(selectedSkinType));
        if (index >= 0)
        {
            ownedSkinList[index].SetAsAvailable();
        }

        
        catSkinManagementView.gameObject.SetActive(false);
    }

    public void Close()
    {
        // Set previous cat skin as available
        int index = ownedSkinList.FindIndex(x => x.SkinType.Equals(selectedSkinType));
        if (index >= 0)
        {
            ownedSkinList[index].SetAsAvailable();
        }

        catSkinManagementView.gameObject.SetActive(false);
    }
    #endregion

    #region OnDestroy
    private void UnloadEventsEventCallback(Void _item)
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }
        _eventHandlers.Clear();
    }
    #endregion
}
