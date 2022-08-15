using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class CatOptionsView : MonoBehaviour
{
    public CatOptionsController catOptionsController;

    public void ClosePopUp()
    {
        catOptionsController.CloseSelectedCamera();
        gameObject.SetActive(false);
    }

    public void OpenCatCrew()
    {
        catOptionsController.OpenCatCrewManagement();
    }

    public void ChangeSkin()
    { 
    
    }
}
