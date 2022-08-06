using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/Int Catalogue Type Event", fileName = "InntCatalogueTypeEvent", order = 50)]

    public class IntCatalogueTypeEvent : AtomEvent<int, ItemTier> { }
}