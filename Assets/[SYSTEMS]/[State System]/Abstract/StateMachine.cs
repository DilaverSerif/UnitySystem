using System.Collections.Generic;
using _SYSTEMS_._Character_System_.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._State_System_.Abstract
{
    [RequireComponent(typeof(StateManager))]

    public class StateMachine : SerializedMonoBehaviour, IReferenceContainer
    {
        [ShowInInspector]
        public Dictionary<string, object> references = new Dictionary<string, object>();
        private void Awake()
        {
            references.Add("Target", transform);
        }

        public void SetReference(string key, object value)
        {
            if (references.ContainsKey(key))
                references[key] = value;
            else
                references.Add(key, value);
        }

        public T GetReference<T>(string key)
        {
            if (references.ContainsKey(key))
            {
                return (T)references[key];
            }

            return default(T);
        }
    }
}
