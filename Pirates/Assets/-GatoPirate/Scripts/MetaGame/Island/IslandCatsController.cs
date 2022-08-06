using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class IslandCatsController : MonoBehaviour
{
    [SerializeField]
    private IslandSlot captainSlot;
    [SerializeField]
    private List<IslandSlot> slotList;

    public CatTypeEvent NewCatPurchasedEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory<CatType>.BuildEventHandler(NewCatPurchasedEvent, NewCatPurchasedEventCallback));
        SetIslandCats();
    }

    private void SetIslandCats()
    {
        CatData catData;
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
                    if (catData)
                    {
                        islandSlot.CatData = catData;
                        islandSlot.IsOccupied = true;
                        // TODO: Get Cat Skin Data
                        islandSlot.InitializeCat();
                    }
                    else
                    {
                        Debug.LogError("Cat type not found in CatType Model");
                    }
                }
                    
            }
            else if (CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot == 0)
            { 
                // Captain cat, set in slot 0
            }
        }
        // Get saved data
    }

    private void NewCatPurchasedEventCallback(CatType _catType)
    {

        IslandSlot islandSlot = GetEmptySlot();
        CatData catData;

        if (islandSlot)
        {
            catData = CatsModel.Instance.GetCatData(_catType.ToString());
            if (catData)
            {
                islandSlot.CatData = catData;
                islandSlot.IsOccupied = true;
                islandSlot.InitializeCat();
            }
        }
    }

    public IslandSlot GetEmptySlot()
    {
        List<IslandSlot> emptySlotList = slotList.FindAll(x => x.IsOccupied == false);

        if (emptySlotList.Count >= 0)
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
        for (int index = 1; index < slotList.Count; index++)
        {
            slotList[index].CleanSlot();
        }
    }
}
