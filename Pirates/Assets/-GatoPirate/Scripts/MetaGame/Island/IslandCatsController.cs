using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class IslandCatsController : MonoBehaviour
{
    [SerializeField]
    private List<IslandSlot> slotList;
    [SerializeField]
    private IslandShipButton islandShipButton;

    [Header("Cat faces")]
    [SerializeField]
    private CatFaceData catFaceDataGeneric;
    [SerializeField]
    private CatFaceData catFaceDataHappy;

    public CatTypeIDEvent NewCatPurchasedEvent { get; set; }
    public GameObjectEvent TriggerSelectedCatCameraEvent { get; set; }
    public StringEvent OpenSelectedCatOptionsEvent { get; set; }
    public VoidEvent CloseSelectedCatCameraEvent { get; set; }
    public VoidEvent CatSelectedEvent { get; set; }
    public StringEvent CatUpdatedEvent { get; set; }
    public BoolEvent OpenScreenEvent { get; set; }
    public VoidEvent CloseShipCameraEvent { get; set; }

    // Sound events
    public CatSoundEvent TriggerCatSoundEvent { get; set; }
    public ShipSoundEvent TriggerShipSoundEvent { get; set; }


    // For island ship
    public VoidEvent TriggerShipCameraEvent { get; set; }
    public VoidEvent OpenShipOptionsEvent { get; set; }

    public VoidEvent UnloadEventsEvent { get; set; }



    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CatType, string>.BuildEventHandler(NewCatPurchasedEvent, NewCatPurchasedEventCallback));
        _eventHandlers.Add(EventHandlerFactory<string>.BuildEventHandler(CatUpdatedEvent, CatUpdatedEventCallback));
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UnloadEventsEvent, UnloadEventsEventCallback));

        islandShipButton.TriggerShipCameraEvent = TriggerShipCameraEvent;
        islandShipButton.OpenShipOptionsEvent = OpenShipOptionsEvent;
        islandShipButton.CatSelectedEvent = CatSelectedEvent;
        islandShipButton.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
        islandShipButton.CloseShipCameraEvent = CloseShipCameraEvent;
        islandShipButton.OpenScreenEvent = OpenScreenEvent;
        islandShipButton.TriggerShipSoundEvent = TriggerShipSoundEvent;
        islandShipButton.Initialize();

        SetIslandCats();
    }

    private void SetIslandCats()
    {
        CatData catData;
        CatSkinData skinData;
        IslandSlot islandSlot;

        // Get island slots
        for (int index = 0; index < CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList.Count; index++)
        {
            if (CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot == -1)
            {
                islandSlot = GetEmptySlot();
                if (islandSlot) // If not, means there is no empty space, so we do nothing
                {
                    catData = CatsModel.Instance.GetCatData(CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].CatType);
                    skinData = CatsModel.Instance.GetSkinData(CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].SkinType);
                    if (catData)
                    {
                        // Cat data
                        islandSlot.CatData = catData;
                        // Get random face
                        islandSlot.CatFaceSprite =
                            catFaceDataGeneric.CatFaceSpriteList[Random.Range(0, catFaceDataGeneric.CatFaceSpriteList.Count)];
                        islandSlot.SkinData = skinData;
                        islandSlot.IsOccupied = true;
                        islandSlot.CatID = CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].CatID;
                        // Events
                        islandSlot.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
                        islandSlot.OpenSelectedCatOptionsEvent = OpenSelectedCatOptionsEvent;
                        islandSlot.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
                        islandSlot.CatSelectedEvent = CatSelectedEvent;
                        islandSlot.OpenScreenEvent = OpenScreenEvent;
                        islandSlot.CloseShipCameraEvent = CloseShipCameraEvent;
                        islandSlot.TriggerCatSoundEvent = TriggerCatSoundEvent;

                        islandSlot.InitializeCat();
                    }
                    else
                    {
                        Debug.LogError("Cat type not found in SkinType Model");
                    }
                }
                    
            }
            // Current crew cats
            else if (CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot != -1)
            {
                catData = CatsModel.Instance.GetCatData(CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].CatType);
                skinData = CatsModel.Instance.GetSkinData(CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].SkinType);
                if (catData)
                {
                    // Cat data
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].CatData = catData;
                    int randomFaceIndex = Random.Range(0, catFaceDataGeneric.CatFaceSpriteList.Count);
                    // Get random face
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].CatFaceSprite =
                            catFaceDataGeneric.CatFaceSpriteList[randomFaceIndex];

                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].SkinData = skinData;
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].IsOccupied = true;
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].CatID =
                        CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].CatID;
                    // Events
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].OpenSelectedCatOptionsEvent = OpenSelectedCatOptionsEvent;
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].CatSelectedEvent = CatSelectedEvent;
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].OpenScreenEvent = OpenScreenEvent;
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].CloseShipCameraEvent = CloseShipCameraEvent;
                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].TriggerCatSoundEvent = TriggerCatSoundEvent;

                    slotList[CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot].InitializeCat();
                }
                else
                {
                    Debug.LogError("Cat type not found in SkinType Model");

                }
            }
        }
    }

    #region Event callbacks
    private void NewCatPurchasedEventCallback(CatType _catType, string _catID)
    {

        IslandSlot islandSlot = GetEmptySlot();
        CatData catData;

        if (islandSlot)
        {
            catData = CatsModel.Instance.GetCatData(_catType.ToString());
            if (catData)
            {
                islandSlot.CatData = catData;
                // Get random face
                islandSlot.CatFaceSprite =
                    catFaceDataGeneric.CatFaceSpriteList[Random.Range(0, catFaceDataGeneric.CatFaceSpriteList.Count)];

                islandSlot.IsOccupied = true;
                islandSlot.CatID = _catID;
                islandSlot.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
                islandSlot.OpenSelectedCatOptionsEvent = OpenSelectedCatOptionsEvent;
                islandSlot.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
                islandSlot.CatSelectedEvent = CatSelectedEvent;
                islandSlot.OpenScreenEvent = OpenScreenEvent;
                islandSlot.CloseShipCameraEvent = CloseShipCameraEvent;
                islandSlot.TriggerCatSoundEvent = TriggerCatSoundEvent;
                islandSlot.InitializeCat();
            }
        }
    }

    private void CatUpdatedEventCallback(string _catID)
    {
        int index = slotList.FindIndex(x => x.CatID.Equals(_catID));
        if (index < 0)
            return;

        DataSaveCatStructure cat = CatsDataSaveManager.Instance.GetCatStructureData(_catID);
        if (cat != null)
        {
            slotList[index].SkinData = CatsModel.Instance.GetSkinData(cat.SkinType);
            slotList[index].InitializeCat();
        }
    }
    #endregion
    public IslandSlot GetEmptySlot()
    {
        List<IslandSlot> emptySlotList = slotList.FindAll(x => x.IsOccupied == false);

        if (emptySlotList.Count > 0)
        {
            int index = Random.Range(0, emptySlotList.Count);
            return emptySlotList[index];
        }
        else
            return null;
    }

    public void CleanSlots()
    {
        // TODO: Update for special slots (if it is only the first, or the first 4)
        for (int index = 0; index < slotList.Count; index++)
        {
            slotList[index].CleanSlot();
        }
    }

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
