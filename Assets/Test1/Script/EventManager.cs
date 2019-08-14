using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Test1 {
    public class EventManager {

        private Dictionary<string, System.Action<object>> eventDict = new Dictionary<string, System.Action<object>>();

        private static EventManager instance;

        public static EventManager Instance {
            get {
                if (instance == null) {
                    instance = new EventManager();
                }
                return instance;
            }
        }

        public void StartListening(string eventName, System.Action<object> listener) {
            if (!eventDict.ContainsKey(eventName)) {
                eventDict[eventName] = (obj) => { };
            }
            eventDict[eventName] += listener;
        }

        public void StopListening(string eventName, System.Action<object> listener) {
            eventDict[eventName] -= listener;
        }

        public void StopListeningAll(string eventName) {
            eventDict.Remove(eventName);
        }

        public void TriggerEvent(string eventName, object param) {
            if (eventDict.ContainsKey(eventName)) {
                eventDict[eventName](param);
            }
        }

        
    }
}


