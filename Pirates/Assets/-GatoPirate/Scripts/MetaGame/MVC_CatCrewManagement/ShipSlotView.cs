using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class ShipSlotView : MonoBehaviour
{
    [SerializeField]
    private CatBodyBuilderUI catBodyBuilderUI;
    [SerializeField]
    private GameObject img_emptySlot;
    [SerializeField]
    private int slotIndex;

    public ShipSlotViewEvent SelectShipSlotEvent { get; set; }

    public CatData CatData { get; set; }
    public CatSkinData SkinData { get; set; }
    public int CurrentCatIndex { get; set; }
    public string CatID { get; set; }
    public int SlotIndex { get => slotIndex; set => slotIndex = value; }

    private Material currentMaterial;

    public void Initialize()
    {
        currentMaterial = GetComponent<Image>().material;
    }

    public void InitializeCat()
    {
        catBodyBuilderUI.CatData = CatData;
        catBodyBuilderUI.CatSkinData = SkinData;
        catBodyBuilderUI.InitializeData();

        // TODO: Hide empty slot image
        img_emptySlot.SetActive(true);
    }

    public void SelectSlot()
    {
        SelectShipSlotEvent.Raise(this);
    }

    public void SetAsSelected()
    {
        currentMaterial.SetFloat("_OutlineAlpha", 1f);
    }

    public void Deselect()
    {
        currentMaterial.SetFloat("_OutlineAlpha", 0f);
    }
}
