using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region Cat management
[Serializable]
public class DataSaveCatStructure
{
    public string CatType;
    public string CatID;
    public string CatName;
    public int IslandSlot;
    public string CurrentIsland;
    public string SkinType;
}

[Serializable]
public class DataSaveCatCrewStructure
{
    public List<DataSaveCatStructure> DataSaveCatCrewList;

    public DataSaveCatCrewStructure()
    {
        DataSaveCatCrewList = new List<DataSaveCatStructure>();
    }
}

[Serializable]
public class DataSaveCatAmountStructure
{
    public string CatType;
    public int CatsOwnedAmount;
}

public class DataSaveCatCrewAmountStructure
{
    public List<DataSaveCatAmountStructure> DataSaveCatCrewAmountList;

    public DataSaveCatCrewAmountStructure()
    {
        DataSaveCatCrewAmountList = new List<DataSaveCatAmountStructure>();
    }
       
}
#endregion

#region Skin management

[Serializable]
public class DataSaveSkinStructure
{
    public string SkinType;
}

[Serializable]
public class DataSaveSkinPurchasedStructure
{
    public List<DataSaveSkinStructure> PurchasedSkinList;

    public DataSaveSkinPurchasedStructure()
    {
        PurchasedSkinList = new List<DataSaveSkinStructure>();
    }
}
#endregion