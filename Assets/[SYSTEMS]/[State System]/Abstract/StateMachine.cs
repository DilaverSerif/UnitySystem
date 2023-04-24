using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._State_System_.Abstract
{
    [RequireComponent(typeof(StateManager))]

    public class StateMachine : SerializedMonoBehaviour, IReferenceContainer
    {
        [ShowInInspector]
        public Dictionary<string, object> References = new();
        private void Awake()
        {
            References.Add("Target", transform);
        }

        public void SetReference(string key, object value)
        {
            References[key] = value;
        }

        public T GetReference<T>(string key)
        {
            if (References.TryGetValue(key, out var reference))
            {
                return (T)reference;
            }

            return default;
        }
    }
}
