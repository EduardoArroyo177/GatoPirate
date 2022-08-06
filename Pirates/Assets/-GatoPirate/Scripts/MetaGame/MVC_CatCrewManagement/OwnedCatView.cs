using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OwnedCatView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_catName;
    [SerializeField]
    private CatBodyBuilderUI catBodyBuilderUI;

    public int CatIndex { get; set; }
    public CatType CatType { get; set; }
    public string CatID { get; set; }
    public string CatName { get; set; }

    public void SetIndexAndID(int _catIndex, string _catID)
    {
        CatIndex = _catIndex;
        CatID = _catID;
    }

    public void SetName(string _catName)
    {
        CatName = _catName;

        lbl_catName.text = _catName;
    }

    public void SetCatAndSkinData(CatData _catData, CatSkinData _skinData = null)
    {
        catBodyBuilderUI.CatData = _catData;
        catBodyBuilderUI.CatSkinData = _skinData;
        catBodyBuilderUI.InitializeData();
    }
}
