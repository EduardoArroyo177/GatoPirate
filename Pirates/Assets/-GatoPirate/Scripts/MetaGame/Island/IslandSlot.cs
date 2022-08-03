using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSlot : MonoBehaviour
{
    [SerializeField]
    private CatBodyBuilder catBodyBuilder;
    [SerializeField]
    private bool isOccupied;

    public CatData CatData { get; set; }
    public CatSkinData SkinData { get; set; }
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }

    public void InitializeCat()
    {
        catBodyBuilder.CatData = CatData;
        catBodyBuilder.InitializeCat();
        catBodyBuilder.CatSkinData = SkinData;
        catBodyBuilder.InitializeSkin();
    }
}
