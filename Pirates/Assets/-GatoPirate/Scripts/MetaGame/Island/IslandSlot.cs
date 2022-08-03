using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSlot : MonoBehaviour
{
    [SerializeField]
    private CatData catData;
    [SerializeField]
    private bool isOccupied;

    public CatData CatData { get => catData; set => catData = value; }
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }

    public void InitializeCat()
    { 
        
    }
}
