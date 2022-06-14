using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms {
    public abstract class AtomEvent<T1, T2> : ScriptableObject {
        private readonly List<IAtomListener<T1, T2>> eventListeners = new List<IAtomListener<T1, T2>>();

        public void Raise(T1 item1, T2 item2) {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(item1, item2);
        }

        public void RegisterListener(IAtomListener<T1, T2> listener) {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IAtomListener<T1, T2> listener) {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
    public abstract class AtomEvent<T1, T2, T3> : ScriptableObject
    {
        private readonly List<IAtomListener<T1, T2,T3>> eventListeners = new List<IAtomListener<T1, T2,T3>>();

        public void Raise(T1 item1, T2 item2, T3 item3)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(item1, item2,item3);
        }

        public void RegisterListener(IAtomListener<T1, T2, T3> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IAtomListener<T1, T2, T3> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }

    public abstract class AtomEvent<T1, T2, T3, T4> : ScriptableObject {
        private readonly List<IAtomListener<T1, T2, T3, T4>> eventListeners = new List<IAtomListener<T1, T2, T3, T4>>();

        public void Raise(T1 item1, T2 item2, T3 item3, T4 item4) {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(item1, item2, item3,item4);
        }

        public void RegisterListener(IAtomListener<T1, T2, T3, T4> listener) {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IAtomListener<T1, T2, T3, T4> listener) {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}