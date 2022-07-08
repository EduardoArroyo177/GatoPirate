using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class UICanvasBootstrapper : MonoBehaviour
{
    [SerializeField]
    private ResultScreenController resultScreenController;

    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public BoolEvent WinChestEvent { get; set; }

    public void Initialize()
    {
        resultScreenController.ShowResultScreenEvent = ShowResultScreenEvent;
        resultScreenController.WinChestEvent = WinChestEvent;
        resultScreenController.Initialize();
    }
}
