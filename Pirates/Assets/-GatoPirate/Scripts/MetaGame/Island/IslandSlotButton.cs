using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class IslandSlotButton : MonoBehaviour
{
    public GameObjectEvent TriggerSelectedCatCameraEvent { get; set; }

    private string catID;// { get; set; }
    private bool isInitialized;

    public void Initialize(string _catID)
    {
        isInitialized = true;
        catID = _catID;
    }

    private void OnMouseUpAsButton()
    {
        if (isInitialized)
        {
            Debug.Log($"Cat selected! {catID}");
            TriggerSelectedCatCameraEvent.Raise(gameObject);
            // TODO: Raise another event to open pop up (with cat id)
            // TODO: Mark cat with outline or something to show this is the selected cat
        }
        //Debug.Log()
    }
}
