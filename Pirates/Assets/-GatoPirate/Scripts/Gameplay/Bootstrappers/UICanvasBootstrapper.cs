using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class UICanvasBootstrapper : MonoBehaviour
{
    [SerializeField]
    private ResultScreenController resultScreenController;

    public CharacterTypeEvent ShowResultScreenEvent { get; set; }

    public void Initialize()
    {
        resultScreenController.ShowResultScreenEvent = ShowResultScreenEvent;
        resultScreenController.Initialize();
    }
}
