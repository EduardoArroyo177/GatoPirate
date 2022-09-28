using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UDP;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/Purchase Info Event", fileName = "PurchaseInfoEvent", order = 1)]

    public class PurchaseInfoEvent : AtomEvent<PurchaseInfo> { }
}

