using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using System;

namespace UnityAtoms {
    public class EventHandlerFactory : IAtomListener<UnityAtoms.Void>, IAtomEventHandler {
        public delegate void OnEventRaisedDelegate(UnityAtoms.Void item);
        public OnEventRaisedDelegate EventRaised { get; set; }

        public delegate void OnEventRaisedDelegateVoid();
        public OnEventRaisedDelegateVoid EventRaisedVoid { get; set; }
        private AtomEvent<UnityAtoms.Void> handlerEvent { get; set; }
        public static EventHandlerFactory BuildEventHandler(AtomEvent<UnityAtoms.Void> gameEvent, OnEventRaisedDelegate action) {
            EventHandlerFactory handler = new EventHandlerFactory();
            handler.EventRaised += action;
            handler.handlerEvent = gameEvent;
            gameEvent.RegisterListener(handler);
            return handler;
        }

        public void UnregisterListener() {
            handlerEvent.UnregisterListener(this);
        }

        public void OnEventRaised(UnityAtoms.Void item) {
            EventRaised(item);
        }
    }

    public class EventHandlerFactory<T> : IAtomListener<T>, IAtomEventHandler {
        public delegate void OnEventRaisedDelegate(T item);
        public OnEventRaisedDelegate EventRaised { get; set; }

        public delegate void OnEventRaisedDelegateVoid();
        public OnEventRaisedDelegateVoid EventRaisedVoid { get; set; }
        private AtomEvent<T> handlerEvent { get; set; }
        public static EventHandlerFactory<T> BuildEventHandler(AtomEvent<T> gameEvent, OnEventRaisedDelegate action) {
            EventHandlerFactory<T> handler = new EventHandlerFactory<T>();
            handler.EventRaised += action;
            handler.handlerEvent = gameEvent;
            gameEvent.RegisterListener(handler, false);
            return handler;
        }

        public void OnEventRaised(T item) {
            EventRaised(item);
        }

        public void UnregisterListener() {
            handlerEvent.UnregisterListener(this);
        }
    }

    public class EventHandlerFactory<T,U> : IAtomListener<T,U>, IAtomEventHandler {
        public delegate void OnEventRaisedDelegate(T first, U second);
        public OnEventRaisedDelegate EventRaised { get; set; }

        public delegate void OnEventRaisedDelegateVoid();
        public OnEventRaisedDelegateVoid EventRaisedVoid { get; set; }
        private AtomEvent<T,U> handlerEvent { get; set; }
        public static EventHandlerFactory<T,U> BuildEventHandler(AtomEvent<T,U> gameEvent, OnEventRaisedDelegate action) {
            EventHandlerFactory<T,U> handler = new EventHandlerFactory<T,U>();
            handler.EventRaised += action;
            handler.handlerEvent = gameEvent;
            gameEvent.RegisterListener(handler);
            return handler;
        }

        public void UnregisterListener() {
            handlerEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T first, U second) {
            EventRaised(first,second);
        }
    }
    public class EventHandlerFactory<T, U, V> : IAtomListener<T, U, V>, IAtomEventHandler {
        public delegate void OnEventRaisedDelegate(T first, U second, V third);
        public OnEventRaisedDelegate EventRaised { get; set; }

        public delegate void OnEventRaisedDelegateVoid();
        public OnEventRaisedDelegateVoid EventRaisedVoid { get; set; }
        private AtomEvent<T, U, V> handlerEvent { get; set; }
        public static EventHandlerFactory<T, U, V> BuildEventHandler(AtomEvent<T, U, V> gameEvent, OnEventRaisedDelegate action)
        {
            EventHandlerFactory<T, U, V> handler = new EventHandlerFactory<T, U, V>();
            handler.EventRaised += action;
            handler.handlerEvent = gameEvent;
            gameEvent.RegisterListener(handler);
            return handler;
        }

        public void UnregisterListener()
        {
            handlerEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T first, U second, V third)
        {
            EventRaised(first, second, third);
        }
    }

    public class EventHandlerFactory<T, U, V, W> : IAtomListener<T, U, V, W>, IAtomEventHandler {
        public delegate void OnEventRaisedDelegate(T first, U second, V third, W fourth);
        public OnEventRaisedDelegate EventRaised { get; set; }

        public delegate void OnEventRaisedDelegateVoid();
        public OnEventRaisedDelegateVoid EventRaisedVoid { get; set; }
        private AtomEvent<T, U, V, W> handlerEvent { get; set; }
        public static EventHandlerFactory<T, U, V, W> BuildEventHandler(AtomEvent<T, U, V, W> gameEvent, OnEventRaisedDelegate action) {
            EventHandlerFactory<T, U, V, W> handler = new EventHandlerFactory<T, U, V, W>();
            handler.EventRaised += action;
            handler.handlerEvent = gameEvent;
            gameEvent.RegisterListener(handler);
            return handler;
        }

        public void UnregisterListener() {
            handlerEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T first, U second, V third, W fourth) {
            EventRaised(first, second, third, fourth);
        }
    }
}
