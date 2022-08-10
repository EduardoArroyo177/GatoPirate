using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityAtoms.BaseAtoms;

public class OwnedCatView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_catName;
    [SerializeField]
    private CatBodyBuilderUI catBodyBuilderUI;
    [SerializeField]
    private GameObject img_selectedCat;
    [SerializeField]
    private GameObject img_unavailableCat;

    public IntEvent SelectCatEvent { get; set; }

    public int CatIndex;// { get; set; }
    public CatType CatType { get; set; }
    public string CatID { get; set; }
    public string CatName { get; set; }

    #region Data set
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
    #endregion

    public void SelectCat()
    {
        SelectCatEvent.Raise(CatIndex);
    }

    public void SetAsSelected()
    {
        img_selectedCat.SetActive(true);
    }

    public void Deselect()
    {
        img_selectedCat.SetActive(false);
    }

    public void SetAsAvailable()
    {
        img_unavailableCat.SetActive(false);
    }

    public void SetAsUnavailable()
    {
        img_unavailableCat.SetActive(true);
    }
}
