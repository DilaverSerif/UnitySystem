using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Save_System_
{
    [CreateAssetMenu(menuName = "Save System/Create SaveData", fileName = "SaveData", order = 0)]
    public partial class SaveData : SaveableData<SaveData>
    {
        public int Money;
        public int Level;

#if UNITY_EDITOR

        [Button]
        public void SaveManually()
        {
            DataManager<SaveData>.SaveData();
        }
        
        [Button]
        public void LoadManually()
        {
            DataManager<SaveData>.LoadData("SaveData");
        }
        
#endif
    }
}