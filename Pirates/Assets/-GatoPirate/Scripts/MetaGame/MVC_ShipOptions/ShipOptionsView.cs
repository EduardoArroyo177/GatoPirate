using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipOptionsView : MonoBehaviour
{
    public ShipOptionsController shipOptionsController;

    public void ClosePopUp()
    {
        shipOptionsController.CloseCamera();
        gameObject.SetActive(false);
    }

    public void OpenCatCrew()
    {
        shipOptionsController.OpenCatCrewManagement();
    }

    public void Combat()
    {
        shipOptionsController.StartCombat();
    }
}
