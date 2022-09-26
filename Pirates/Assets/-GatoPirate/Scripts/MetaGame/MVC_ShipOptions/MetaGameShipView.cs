using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaGameShipView : MonoBehaviour
{
    public MetaGameShipController ShipOptionsController { get; set; }

    public void ClosePopUp()
    {
        ShipOptionsController.CloseCamera();
        gameObject.SetActive(false);
    }

    public void OpenCatCrew()
    {
        ShipOptionsController.OpenCatCrewManagement();
    }

    public void Combat()
    {
        ShipOptionsController.StartCombat();
    }
}
