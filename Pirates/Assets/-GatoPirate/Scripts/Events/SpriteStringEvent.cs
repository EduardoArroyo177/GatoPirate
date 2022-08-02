using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/Sprite String Event", fileName = "SpriteStringEvent", order = 50)]

    public class SpriteStringEvent : AtomEvent<Sprite, string> { }
}