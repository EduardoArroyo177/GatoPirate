using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/String Int Event", fileName = "StringIntEvent", order = 50)]
    public class StringIntEvent : AtomEvent<string, int> { }
}