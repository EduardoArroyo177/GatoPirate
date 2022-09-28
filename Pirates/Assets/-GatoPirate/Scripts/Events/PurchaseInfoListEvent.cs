using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UDP;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/Purchase Info List Event", fileName = "PurchaseInfoListEvent", order = 1)]

    public class PurchaseInfoListEvent : AtomEvent<IList<PurchaseInfo>> { }
}