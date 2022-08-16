using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsModel : SceneSingleton<CatsModel>
{
    [SerializeField]
    private List<CatData> catsDataList;
    [SerializeField]
    private List<CatSkinData> catsSkinDataList;

    public List<CatData> CatsDataList { get => catsDataList; set => catsDataList = value; }
    public List<CatSkinData> CatsSkinDataList { get => catsSkinDataList; set => catsSkinDataList = value; }

    public CatData GetCatData(string _catType)
    {
        int index = CatsDataList.FindIndex(x => x.CatType.ToString().Equals(_catType));
        if (index < 0)
        {
            Debug.LogError($"Error. Cat type {_catType} not found");
            return null;
        }
        else
            return CatsDataList[index];
    }

    public CatSkinData GetSkinData(string _skinType)
    {
        int index = CatsSkinDataList.FindIndex(x => x.SkinType.ToString().Equals(_skinType));
        if (index < 0)
        {
            return null;
        }
        else
            return CatsSkinDataList[index];
    }

}
