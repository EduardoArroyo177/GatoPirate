using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildShipController : MonoBehaviour
{
    [Header("Cat Crew")]
    [SerializeField]
    private CatBodyBuilder[] catBodyBuilderList;

    public CatData[] CatCrewDataList { get; set; }

    public void Initialize()
    {
        for (int index = 0; index < CatCrewDataList.Length; index++)
        {
            catBodyBuilderList[index].CatData = CatCrewDataList[index];
            catBodyBuilderList[index].InitializeCat();
        }
    }
}
