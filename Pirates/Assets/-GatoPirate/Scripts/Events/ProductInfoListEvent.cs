using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UDP;

namespace UnityAtoms
{
    [CreateAssetMenu(menuName = "Gato Pirate/Events/Product Info List Event", fileName = "ProductInfoListEvent", order = 1)]

    public class ProductInfoListEvent : AtomEvent<IList<ProductInfo>> { }
}