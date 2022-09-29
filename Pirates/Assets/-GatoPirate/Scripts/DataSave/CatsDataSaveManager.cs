using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsDataSaveManager : SceneSingleton<CatsDataSaveManager>
{
    [Header("Cats")]
    [SerializeField]
    private DataSaveCatCrewStructure dataSaveCatCrewStructure;
    [SerializeField]
    private DataSaveCatCrewAmountStructure dataSaveCatCrewAmountStructure;

    [Header("Skins")]
    [SerializeField]
    private DataSaveSkinPurchasedStructure dataSaveSkinPurchasedStructure;


    public const string CAT_SAVING_DATA_KEY = "CATS_OWNED";
    public const string CAT_SAVING_AMOUNT_DATA_KEY = "CATS_OWNED_AMOUNT";
    public const string SKIN_SAVING_DATA_KEY = "SKINS_OWNED";

    // Properties
    // Cats
    public DataSaveCatCrewStructure DataSaveCatCrewStructure { get => dataSaveCatCrewStructure; set => dataSaveCatCrewStructure = value; }
    public DataSaveCatCrewAmountStructure DataSaveCatCrewAmountStructure { get => dataSaveCatCrewAmountStructure; set => dataSaveCatCrewAmountStructure = value; }
    // Skins
    public DataSaveSkinPurchasedStructure DataSaveSkinPurchasedStructure { get => dataSaveSkinPurchasedStructure; set => dataSaveSkinPurchasedStructure = value; }
    
    
    #region Initialization
    public void LoadCatsSavedData()
    {
        string dataSave = PlayerPrefs.GetString(CAT_SAVING_DATA_KEY);
        if (string.IsNullOrEmpty(dataSave))
        {
            // Captain Cat
            DataSaveCatCrewStructure = new DataSaveCatCrewStructure();
            DataSaveCatStructure initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatType = CatType.ORANGE.ToString();
            initialCatStructure.CatID = "captain01";
            initialCatStructure.CatName = "Tabby cat"; // TODO: Update with final name
            initialCatStructure.IslandSlot = 0;
            initialCatStructure.CurrentIsland = Island.ISLAND1.ToString();
            initialCatStructure.SkinType = SkinType.NONE.ToString(); // TODO: Check if first cat will have a special skin by default
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Captain Amount
            DataSaveCatCrewAmountStructure = new DataSaveCatCrewAmountStructure();
            DataSaveCatAmountStructure initialCatAmountStructure = new DataSaveCatAmountStructure();
            initialCatAmountStructure.CatType = CatType.CAPTAIN.ToString();
            initialCatAmountStructure.CatsOwnedAmount = 1;
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.Add(initialCatAmountStructure);

            // Sailor cat 1
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatType = CatType.ORANGE.ToString();
            initialCatStructure.CatID = "catsailor01";
            initialCatStructure.CatName = "Tabby cat";
            initialCatStructure.IslandSlot = 1;
            initialCatStructure.CurrentIsland = Island.ISLAND1.ToString();
            initialCatStructure.SkinType = SkinType.NONE.ToString();
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Sailor cat 2
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatType = CatType.ORANGE.ToString();
            initialCatStructure.CatID = "catsailor02";
            initialCatStructure.CatName = "Tabby cat";
            initialCatStructure.IslandSlot = 2;
            initialCatStructure.CurrentIsland = Island.ISLAND1.ToString();
            initialCatStructure.SkinType = SkinType.NONE.ToString();
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Sailor cat 3
            initialCatStructure = new DataSaveCatStructure();
            initialCatStructure.CatType = CatType.ORANGE.ToString();
            initialCatStructure.CatID = "catsailor03";
            initialCatStructure.CatName = "Tabby cat";
            initialCatStructure.IslandSlot = 3;
            initialCatStructure.CurrentIsland = Island.ISLAND1.ToString();
            initialCatStructure.SkinType = SkinType.NONE.ToString();
            DataSaveCatCrewStructure.DataSaveCatCrewList.Add(initialCatStructure);
            // Sailors Amount
            initialCatAmountStructure = new DataSaveCatAmountStructure();
            initialCatAmountStructure.CatType = CatType.ORANGE.ToString();
            initialCatAmountStructure.CatsOwnedAmount = 3;
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.Add(initialCatAmountStructure);

            DataSaveSkinPurchasedStructure = new DataSaveSkinPurchasedStructure();

            SaveCatData();
            SaveSkinData();
        }
        else
        {
            // Load saved data
            DataSaveCatCrewStructure = JsonUtility.FromJson<DataSaveCatCrewStructure>(dataSave);
            string dataAmountSave = PlayerPrefs.GetString(CAT_SAVING_AMOUNT_DATA_KEY);
            DataSaveCatCrewAmountStructure = JsonUtility.FromJson<DataSaveCatCrewAmountStructure>(dataAmountSave);
            string skinDataSave = PlayerPrefs.GetString(SKIN_SAVING_DATA_KEY);
            DataSaveSkinPurchasedStructure = JsonUtility.FromJson<DataSaveSkinPurchasedStructure>(skinDataSave);
        }
    }
    #endregion

    #region Update data
    // Cats
    public void SaveNewCat(CatType _catType, string _catID, string _catName, int _islandSlot = -1, Island _island = Island.ISLAND1)
    {
        DataSaveCatStructure newCatStructure = new DataSaveCatStructure();
        newCatStructure.CatType = _catType.ToString();
        newCatStructure.CatID = _catID;
        newCatStructure.CatName = _catName;
        newCatStructure.IslandSlot = _islandSlot;
        newCatStructure.CurrentIsland = _island.ToString();
        newCatStructure.SkinType = "none"; 
        DataSaveCatCrewStructure.DataSaveCatCrewList.Add(newCatStructure);

        // Set Amount
        int catIndex = DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList.FindIndex(x => x.CatType.Equals(_catType));
        // We update existing cat Amount
        if (catIndex >= 0)
        {
            DataSaveCatCrewAmountStructure.DataSaveCatCrewAmountList[catIndex].CatsOwnedAmount++;
        }
        // We create a new cat Amount entry
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

    public void UpdateCatIslandSlot(string _catID, int _newSlotIndex)
    {
        int catIndex = DataSaveCatCrewStructure.DataSaveCatCrewList.FindIndex(x => x.CatID.Equals(_catID));
        if (catIndex < 0)
        {
            Debug.LogError("Cat ID Not found");
            return;
        }

        DataSaveCatCrewStructure.DataSaveCatCrewList[catIndex].IslandSlot = _newSlotIndex;
        SaveCatData();
    }

    // Skins
    public void UnlockSkin(SkinType _skinType)
    {
        DataSaveSkinStructure dataSaveSkinStructure = new DataSaveSkinStructure();
        dataSaveSkinStructure.SkinType = _skinType.ToString();
        DataSaveSkinPurchasedStructure.PurchasedSkinList.Add(dataSaveSkinStructure);
        SaveSkinData();
    }

    public void UpdateCatSkin(string _catID, SkinType _skinType)
    {
        int catIndex = DataSaveCatCrewStructure.DataSaveCatCrewList.FindIndex(x => x.CatID.Equals(_catID));
        if (catIndex < 0)
        {
            Debug.LogError("Cat ID Not found");
            return;
        }

        DataSaveCatCrewStructure.DataSaveCatCrewList[catIndex].SkinType = _skinType.ToString();
        SaveCatData();
    }
    #endregion

    #region Get data
    // Cats
    public DataSaveCatStructure GetCatStructureData(string _catID)
    {
        int catIndex = DataSaveCatCrewStructure.DataSaveCatCrewList.FindIndex(x => x.CatID.Equals(_catID));
        if (catIndex < 0)
            return null;

        return DataSaveCatCrewStructure.DataSaveCatCrewList[catIndex];
    }

    public List<DataSaveCatStructure> GetCatShipCrewStructureData()
    {
        return DataSaveCatCrewStructure.DataSaveCatCrewList.FindAll(x => x.IslandSlot != -1);
    }

    // Skins
    public List<DataSaveSkinStructure> GetPurchasedSkins()
    {
        return DataSaveSkinPurchasedStructure.PurchasedSkinList;
    }

    public bool IsSkinPurchased(string _skinType)
    {
        if (DataSaveSkinPurchasedStructure.PurchasedSkinList.Count <= 0)
            return false;

        int skinIndex = DataSaveSkinPurchasedStructure.PurchasedSkinList.FindIndex(x => x.SkinType.Equals(_skinType));
        if (skinIndex < 0)
            return false;
        else
            return true;
    }
    #endregion

    private void SaveCatData()
    {
        PlayerPrefs.SetString(CAT_SAVING_DATA_KEY, JsonUtility.ToJson(DataSaveCatCrewStructure));
        PlayerPrefs.SetString(CAT_SAVING_AMOUNT_DATA_KEY, JsonUtility.ToJson(DataSaveCatCrewAmountStructure));
    }

    private void SaveSkinData()
    {
        PlayerPrefs.SetString(SKIN_SAVING_DATA_KEY, JsonUtility.ToJson(DataSaveSkinPurchasedStructure));
    }
}
