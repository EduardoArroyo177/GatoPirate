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

    public VoidEvent UpdateIslandCatsEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new();


    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(UpdateIslandCatsEvent, UpdateIslandCatsEventCallback));
        UpdateIslandCatsEventCallback(new Void());
    }

    private void UpdateIslandCatsEventCallback(Void _item)
    {
        CatData catData;
        IslandSlot islandSlot;

        // Get island slots
        for (int index = 0; index < CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList.Count; index++)
        {
            if (CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot == -1)
            {
                // Find cat by type in cats model
                catData = CatsModel.Instance.GetCatData(CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].CatType);
                if (catData)
                {
                    // Get random slot
                    islandSlot = GetEmptySlot();
                    if (islandSlot) // If not, means there is nno empty space, so we do nothing
                    {
                        islandSlot.CatData = catData;
                        islandSlot.IsOccupied = true;
                        islandSlot.InitializeCat();
                    }
                }
                else
                {
                    Debug.LogError("Cat type not found in Cats Model");
                }
                    
            }
            else if (CatsDataSaveManager.Instance.DataSaveCatCrewStructure.DataSaveCatCrewList[index].IslandSlot == 0)
            { 
                // Captain cat, set in slot 0
            }
        }
        // Get saved data
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
}
