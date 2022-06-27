using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StartCombatController : MonoBehaviour
{
    public VoidEvent StartCombatEvent { get; set; }

    public void StartCombat()
    {
        StartCombatEvent.Raise();
    }
}
