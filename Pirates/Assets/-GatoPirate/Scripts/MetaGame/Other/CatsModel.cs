using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsModel : SceneSingleton<CatsModel>
{
    [SerializeField]
    private List<CatData> catsDataList;

    public List<CatData> CatsDataList { get => catsDataList; set => catsDataList = value; }

    public CatData GetCatData(string _catType)
    {
        int index = CatsDataList.FindIndex(x => x.CatType.Equals(_catType));
        if (index < 0)
            return null;
        else
            return CatsDataList[index];
    }

}
