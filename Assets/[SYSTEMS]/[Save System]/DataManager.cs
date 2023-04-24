using System.IO;
using UnityEngine;

namespace _SYSTEMS_._Save_System_
{
    public static class DataManager
    {
        public static SaveData SaveData;
        public static void Save()
        {
            string json = JsonUtility.ToJson(SaveData);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        public static void Load()
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
            SaveData = JsonUtility.FromJson<SaveData>(json);
        }        
    }
}