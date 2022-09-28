using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UDP;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/String Purchase Info Event", fileName = "StringPurchaseInfoEvent", order = 1)]

    public class StringPurchaseInfoEvent : AtomEvent<string,PurchaseInfo> { }
}
