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
            initialCatStructure.CatType = Cats.CAPTAIN.ToString();
            initialCatStructure.CatID = "captain01";
            initialCatStructure.CatName = "Captain"; // TODO: Update with final name
            initialCatStructure.IslandSlot = 0;
            initialCatStructure.CurrentIsland = Island.ISLAND1.ToString();
            initialCatStructure.SkinType = Skins.NONE.ToString(); // TODO: Check if first cat will have a special skin by default
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Captain amount
            DataSaveCatCrewAmountStructure = new DataSaveCatCrewAmountStructure();
            DataSaveCatAmountStructure initialCatAmountStructure = new DataSaveCatAmountStructure();
            initialCatAmountStructure.CatType = Cats.CAPTAIN.ToString();
            initialCatAmountStructure.CatsOwnedAmount = 1;
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.Add(initialCatAmountStructure);

            // Sailor cat 1
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatType = Cats.ORANGE.ToString();
            initialCatStructure.CatID = "catsailor01";
            initialCatStructure.CatName = "Sailor";
            initialCatStructure.IslandSlot = -1;
            initialCatStructure.CurrentIsland = Island.ISLAND1.ToString();
            initialCatStructure.SkinType = Skins.NONE.ToString();
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Sailor cat 2
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatType = Cats.ORANGE.ToString();
            initialCatStructure.CatID = "catsailor02";
            initialCatStructure.CatName = "Sailor";
            initialCatStructure.IslandSlot = -1;
            initialCatStructure.CurrentIsland = Island.ISLAND1.ToString();
            initialCatStructure.SkinType = Skins.NONE.ToString();
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Sailor cat 3
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatType = Cats.ORANGE.ToString();
            initialCatStructure.CatID = "catsailor03";
            initialCatStructure.CatName = "Sailor";
            initialCatStructure.IslandSlot = -1;
            initialCatStructure.CurrentIsland = Island.ISLAND1.ToString();
            initialCatStructure.SkinType = Skins.NONE.ToString();
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Sailors amount
            initialCatAmountStructure = new DataSaveCatAmountStructure();
            initialCatAmountStructure.CatType = Cats.ORANGE.ToString();
            initialCatAmountStructure.CatsOwnedAmount = 3;
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.Add(initialCatAmountStructure);
            
            SaveCatData();
        }
        else
        {
            // Load saved data
            DataSaveCatCrewStructure = JsonUtility.FromJson<DataSaveCatCrewStructure>(dataSave);
            string dataAmountSave = PlayerPrefs.GetString(CAT_SAVING_AMOUNT_DATA_KEY);
            DataSaveCatCrewAmountStructure = JsonUtility.FromJson<DataSaveCatCrewAmountStructure>(dataAmountSave);

        }
    }

    public void SaveNewCat(Cats _catType, string _catID, string _catName, int _islandSlot = -1, Island _island = Island.ISLAND1)
    {
        DataSaveCatStructure newCatStructure = new DataSaveCatStructure();
        newCatStructure.CatType = _catType.ToString();
        newCatStructure.CatID = _catID;
        newCatStructure.CatName = _catName;
        newCatStructure.IslandSlot = _islandSlot;
        newCatStructure.CurrentIsland = _island.ToString();
        newCatStructure.SkinType = "none"; 
        DataSaveCatCrewStructure.DataSaveCatCrewList.Add(newCatStructure);

        // Set amount
        int catIndex = DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.FindIndex(x => x.CatType.Equals(_catType));
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
            initialCatAmountStructure.CatType = _catType.ToString();
            initialCatAmountStructure.CatsOwnedAmount = 1;
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.Add(initialCatAmountStructure);
        }

        SaveCatData();
    }

    public void GetCaptainCat()
    {
        int index = DataSaveCatCrewStructure.DataSaveCatCrewList.FindIndex(x => x.IslandSlot == 0);
        if (index < 0)
        {
            Debug.Log("CAT NOT FOUND; SEND RANDOM CAT");
        }
        else
        { 
            // return cat
        }

    }

    private void SaveCatData()
    {
        PlayerPrefs.SetString(CAT_SAVING_DATA_KEY, JsonUtility.ToJson(DataSaveCatCrewStructure));
        PlayerPrefs.SetString(CAT_SAVING_AMOUNT_DATA_KEY, JsonUtility.ToJson(DataSaveCatCrewAmountStructure));
    }
}
