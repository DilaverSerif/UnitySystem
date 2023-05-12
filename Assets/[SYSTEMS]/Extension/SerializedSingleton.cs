using Sirenix.OdinInspector;

namespace _SYSTEMS_.Extension
{
    public abstract class SerializedSingleton<T> : SerializedMonoBehaviour where T: SerializedMonoBehaviour
    {
        private static T _instance;
    
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();
            
                return _instance;
            }
        }
    }
}