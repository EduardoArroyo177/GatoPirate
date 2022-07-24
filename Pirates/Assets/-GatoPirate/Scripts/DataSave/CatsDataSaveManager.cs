using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsDataSaveManager : SceneSingleton<CatsDataSaveManager>
{
    [SerializeField]
    private DataSaveCatCrewStructure dataSaveCatCrewStructure;
    [SerializeField]
    private DataSaveCatCrewAmountStructure dataSaveCatCrewAmountStructure;

    public const string CAT_SAVING_DATA_KEY = "CATS_OWNED";
    public const string CAT_SAVING_AMOUNT_DATA_KEY = "CATS_OWNED_AMOUNT";

    // Properties
    public DataSaveCatCrewStructure DataSaveCatCrewStructure { get => dataSaveCatCrewStructure; set => dataSaveCatCrewStructure = value; }
    public DataSaveCatCrewAmountStructure DataSaveCatCrewAmountStructure { get => dataSaveCatCrewAmountStructure; set => dataSaveCatCrewAmountStructure = value; }

    public void LoadCatsSavedData()
    {
        string dataSave = PlayerPrefs.GetString(CAT_SAVING_DATA_KEY);
        if (string.IsNullOrEmpty(dataSave))
        {
            // Captain Cat
            DataSaveCatCrewStructure = new DataSaveCatCrewStructure();
            DataSaveCatStructure initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatID = "captain01";
            initialCatStructure.CatName = "Captain"; // TODO: Update with final name
            initialCatStructure.IslandSlot = 0;
            initialCatStructure.CurrentIsland = Island.ISLAND1;
            initialCatStructure.SkinID = "none"; // TODO: Check if first cat will have a special skin by default
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);

            DataSaveCatCrewAmountStructure = new DataSaveCatCrewAmountStructure();
            DataSaveCatAmountStructure initialCatAmountStructure = new DataSaveCatAmountStructure();
            initialCatAmountStructure.CatName = "Captain";
            initialCatAmountStructure.CatsOwnedAmount = 1;
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.Add(initialCatAmountStructure);

            // Sailor cat 1
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatID = "catsailor01";
            initialCatStructure.CatName = "Sailor";
            initialCatStructure.IslandSlot = -1;
            initialCatStructure.CurrentIsland = Island.ISLAND1;
            initialCatStructure.SkinID = "none";
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Sailor cat 2
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatID = "catsailor02";
            initialCatStructure.CatName = "Sailor";
            initialCatStructure.IslandSlot = -1;
            initialCatStructure.CurrentIsland = Island.ISLAND1;
            initialCatStructure.SkinID = "none";
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Sailor cat 3
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatID = "catsailor03";
            initialCatStructure.CatName = "Sailor";
            initialCatStructure.IslandSlot = -1;
            initialCatStructure.CurrentIsland = Island.ISLAND1;
            initialCatStructure.SkinID = "none";
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);

            initialCatAmountStructure = new DataSaveCatAmountStructure();
            initialCatAmountStructure.CatName = "Sailor";
            initialCatAmountStructure.CatsOwnedAmount = 3;
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.Add(initialCatAmountStructure);

            SaveCatData();
        }
        else
        {
            // Load saved data
            DataSaveCatCrewStructure = JsonUtility.FromJson<DataSaveCatCrewStructure>(dataSave);
        }
    }

    public void SaveNewCat(string _catID, string _catName, int _islandSlot = -1, Island _island = Island.ISLAND1)
    {
        DataSaveCatStructure newCatStructure = new DataSaveCatStructure();
        newCatStructure.CatID = _catID;
        newCatStructure.CatName = _catName;
        newCatStructure.IslandSlot = _islandSlot;
        newCatStructure.CurrentIsland = _island;
        newCatStructure.SkinID = "none"; 
        DataSaveCatCrewStructure.DataSaveCatCrewList.Add(newCatStructure);

        // Set amount
        int catIndex = DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.FindIndex(x => x.CatName.Equals(_catName));
        // We update existing cat amount
        if (catIndex >= 0)
        {
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList[catIndex].CatsOwnedAmount++;
        }
        // We create a new cat amount entry
        else
        {
            DataSaveCatAmountStructure initialCatAmountStructure = new DataSaveCatAmountStructure();
            initialCatAmountStructure = new DataSaveCatAmountStructure();
            initialCatAmountStructure.CatName = _catName;
            initialCatAmountStructure.CatsOwnedAmount = 1;
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.Add(initialCatAmountStructure);
        }

        SaveCatData();
    }

    private void SaveCatData()
    {
        PlayerPrefs.SetString(CAT_SAVING_DATA_KEY, JsonUtility.ToJson(DataSaveCatCrewStructure));
        PlayerPrefs.SetString(CAT_SAVING_AMOUNT_DATA_KEY, JsonUtility.ToJson(DataSaveCatCrewAmountStructure));
    }
}
