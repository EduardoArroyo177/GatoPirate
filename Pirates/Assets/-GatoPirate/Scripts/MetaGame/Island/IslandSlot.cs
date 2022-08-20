using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class IslandSlot : MonoBehaviour
{
    [SerializeField]
    private CatBodyBuilder catBodyBuilder;
    [SerializeField]
    private bool isOccupied;
    [SerializeField]
    private string catID;

    public CatData CatData { get; set; }
    public CatSkinData SkinData { get; set; }
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }
    public string CatID { get => catID; set => catID = value; }

    // Events
    public GameObjectEvent TriggerSelectedCatCameraEvent { get; set; }
    public StringEvent OpenSelectedCatOptionsEvent { get; set; }
    public VoidEvent CloseSelectedCatCameraEvent { get; set; }
    public VoidEvent CatSelectedEvent { get; set; }
    public BoolEvent OpenScreenEvent { get; set; }
    public VoidEvent CloseShipCameraEvent { get; set; }


    private IslandSlotButton islandSlotButton;

    public void InitializeCat()
    {
        catBodyBuilder.CatData = CatData;
        catBodyBuilder.CatSkinData = SkinData;
        catBodyBuilder.InitializeData();

        islandSlotButton = GetComponent<IslandSlotButton>();
        if (islandSlotButton)
        {
            islandSlotButton.TriggerSelectedCatCameraEvent = TriggerSelectedCatCameraEvent;
            islandSlotButton.OpenSelectedCatOptionsEvent = OpenSelectedCatOptionsEvent;
            islandSlotButton.CloseSelectedCatCameraEvent = CloseSelectedCatCameraEvent;
            islandSlotButton.CatSelectedEvent = CatSelectedEvent;
            islandSlotButton.OpenScreenEvent = OpenScreenEvent;
            islandSlotButton.CloseShipCameraEvent = CloseShipCameraEvent;
            islandSlotButton.Initialize(CatID);
        }

        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void CleanSlot()
    {
        isOccupied = false;
        catBodyBuilder.RestartCatData();
        catBodyBuilder.RestartSkinData();

        GetComponent<BoxCollider2D>().enabled = false;
    }
}
