using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region Cat management
[Serializable]
public class DataSaveCatStructure
{
    public string CatID;
    public string CatName;
    public int IslandSlot;
    public Island CurrentIsland;
    public string SkinID;
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
    public string CatName;
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