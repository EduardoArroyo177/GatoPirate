using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/Cat Type & ID Event", fileName = "CatTypeIDEvent", order = 50)]
    public class CatTypeIDEvent : AtomEvent<CatType, string> { }
}