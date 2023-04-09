using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Save_System_
{
    [CreateAssetMenu(menuName = "Save System/Create SaveableData", fileName = "SaveableData", order = 0)]
    public class SaveableData<T> : SerializedScriptableObject where T : ScriptableObject
    {
        [ShowInInspector]
        public Dictionary<string, object> Data = new Dictionary<string, object>();

        public TObject GetObject<TObject>(string key)
        {
            return (TObject)Data[key];
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>(typeof(T).Name);
                }

                return _instance;
            }
            set => _instance = value;
        }

        private static T _instance;
    }
}