using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class SelectedCatSlotView : MonoBehaviour
{
    [SerializeField]
    private CatBodyBuilderUI catBodyBuilderUI;

    public CatData CatData { get; set; }
    public CatSkinData SkinData { get; set; }

    public void InitializeCat()
    {
        catBodyBuilderUI.CatData = CatData;
        catBodyBuilderUI.CatSkinData = SkinData;
        catBodyBuilderUI.InitializeData();
    }

    public void RemoveSkin()
    {
        catBodyBuilderUI.RestartSkin();
    }
}
